using SingWing.PgSql.Protocol.Connections;
using System.Runtime.InteropServices;

namespace SingWing.PgSql.Balancing;

/// <summary>
/// Represents a cacheable extended query command.
/// </summary>
internal readonly struct ExtendedQuery
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExtendedQuery"/> class
    /// with the specified command text and parameters.
    /// </summary>
    /// <param name="commandText">The command text.</param>
    /// <param name="parameters">The parameter list used by the command.</param>
    private ExtendedQuery(
        in ReadOnlyMemory<char> commandText,
        in ReadOnlyMemory<Parameter> parameters)
    {
        ParameterDataTypes = TypesOf(parameters);
        ParseMessageBinary = GenerateParseMessageBinary(commandText, ParameterDataTypes);

        static uint[] TypesOf(in ReadOnlyMemory<Parameter> parameters)
        {
            if (parameters.Length == 0)
            {
                return Array.Empty<uint>();
            }

            var spanParameters = parameters.Span;
            var types = new uint[spanParameters.Length];

            for (var i = 0; i < spanParameters.Length; i++)
            {
                types[i] = (uint)spanParameters[i].DataType;
            }

            return types;
        }

        // Generates binary data of the Parse message for the specified command.
        static byte[] GenerateParseMessageBinary(
            in ReadOnlyMemory<char> commandText,
            uint[] parameterTypes)
        {
            // https://www.postgresql.org/docs/current/protocol-message-formats.html
            const char MessageType = 'P';

            var spanText = commandText.Span;
            var binaryLength = Encoding.UTF8.GetByteCount(spanText);

            // The total binary length of the Parse message.
            var length =
                // Identifies the message as a Parse command.
                sizeof(byte) +
                // Length of message contents in bytes, including self.
                sizeof(int) +
                // The name of the destination prepared statement
                // (an empty string selects the unnamed prepared statement).
                sizeof(byte) +
                // The query string to be parsed.
                binaryLength + sizeof(byte) +
                // The number of parameter data types specified (can be zero).
                // Note that this is not an indication of the number of parameters
                // that might appear in the query string, only the number that
                // the frontend wants to prespecify types for.
                sizeof(short) +
                // for each parameter...
                // Specifies the object ID of the parameter data type.
                (sizeof(uint) * parameterTypes.Length);

            var binary = GC.AllocateUninitializedArray<byte>(length);
            var writer = new Writer(binary);

            writer.WriteByte(MessageType);
            writer.WriteInt32(length - sizeof(byte));
            writer.WriteStringTerminator();
            writer.WriteStringWithTerminator(spanText, binaryLength);
            writer.WriteInt16((short)parameterTypes.Length);

            for (var i = 0; i < parameterTypes.Length; i++)
            {
                writer.WriteUInt32(parameterTypes[i]);
            }

            return binary;
        }
    }

    /// <summary>
    /// Gets the list of parameter type OIDs.
    /// </summary>
    internal uint[] ParameterDataTypes { get; }

    /// <summary>
    /// Gets the cached binary data of the Parse message for this command.
    /// </summary>
    internal byte[] ParseMessageBinary { get; }

    /// <summary>
    /// Represents a cache of extended query commands for a database.
    /// </summary>
    /// <remarks>
    /// By caching extended query commands, we can reduce the time it takes to generate Parse message binary data for commands.
    /// </remarks>
    internal sealed class Cache
    {
        /// <summary>
        /// The list of cached commands for the current database.
        /// </summary>
        private readonly Dictionary<ReadOnlyMemory<char>, ExtendedQuery> _commands;

        /// <summary>
        /// The maximum text length of a command that can be cached.
        /// </summary>
        private int _maxCommandTextLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache"/> class.
        /// </summary>
        internal Cache()
        {
            const int DefaultMaxCommandTextLength = 256;

            _commands = new(TextComparer.Shared);
            _maxCommandTextLength = DefaultMaxCommandTextLength;
        }

        /// <inheritdoc cref="IDatabase.MaxTextLengthOfCachedExtendedQuery"/>
        internal int MaxCommandTextLength
        {
            get => _maxCommandTextLength;
            set
            {
                var newValue = value < 0 ? 0 : value;

                lock (_commands)
                {
                    if (_maxCommandTextLength == newValue)
                    {
                        return;
                    }

                    _maxCommandTextLength = newValue;
                    if (newValue > _maxCommandTextLength || _commands.Count == 0)
                    {
                        return;
                    }

                    if (newValue == 0)
                    {
                        _commands.Clear();
                        return;
                    }

                    var commandTexts = _commands.Keys.ToArray();

                    foreach (var commandText in commandTexts)
                    {
                        if (commandText.Length > newValue)
                        {
                            _commands.Remove(commandText);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a cached command. If the command text is not too long, it is added to the cache.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameter list used by the command.</param>
        /// <returns>A <see cref="ExtendedQuery"/> instance that contains the binary data for sending.</returns>
        internal ExtendedQuery Make(
            in ReadOnlyMemory<char> commandText,
            in ReadOnlyMemory<Parameter> parameters)
        {
            if (commandText.Length > MaxCommandTextLength)
            {
                return new(commandText, parameters);
            }

            ExtendedQuery command;

            lock (_commands)
            {
                if (!_commands.TryGetValue(commandText, out command))
                {
                    command = new(commandText, parameters);
                    _commands.Add(commandText, command);
                }
            }

            return command;
        }

        #region TextComparer

        /// <summary>
        /// Defines methods to support the case-sensitive comparison of command texts for equality.
        /// </summary>
        private sealed class TextComparer : IEqualityComparer<ReadOnlyMemory<char>>
        {
            /// <summary>
            /// A shared singleton instance of the <see cref="TextComparer"/> class.
            /// </summary>
            internal static readonly TextComparer Shared = new();

            /// <summary>
            /// Initializes a new instance of the <see cref="TextComparer"/> class.
            /// </summary>
            private TextComparer() { }

            /// <summary>
            /// Determines whether the specified commands are equal.
            /// </summary>
            /// <param name="x">The first command text to compare.</param>
            /// <param name="y">The second command text to compare.</param>
            /// <returns><see langword="true"/> if the specified command texts are equal, otherwise <see langword="false"/>.</returns>
            public bool Equals(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y) =>
                x.Span.Equals(y.Span, StringComparison.Ordinal);

            /// <summary>
            /// Returns the hash code for the specified command text.
            /// </summary>
            /// <param name="text">The command text for which the hash code is to be returned.</param>
            /// <returns>The hash code for the command <paramref name="text"/>.</returns>
            public int GetHashCode(ReadOnlyMemory<char> text)
            {
                var hash = new HashCode();
                hash.AddBytes(MemoryMarshal.AsBytes(text.Span));
                return hash.ToHashCode();
            }
        }

        #endregion
    }
}
