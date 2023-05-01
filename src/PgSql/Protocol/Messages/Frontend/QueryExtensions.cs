using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Sends Simple Query messages to database server.
/// </summary>
internal static class QueryExtensions
{
    /// <summary>
    /// The message type code for the simple query command.
    /// </summary>
    private const char MessageType = 'Q';

    /// <summary>
    /// The complete binary datas for predefined Simple Queries.
    /// </summary>
    private static readonly byte[][] PredefinedQueries;

    static QueryExtensions()
    {
        PredefinedQueries = new byte[8][];

        PredefinedQueries[0] = Array.Empty<byte>();

        PredefinedQueries[(int)PredefinedSimpleQuery.BeginReadUncommitted] =
            Generate("BEGIN ISOLATION LEVEL READ UNCOMMITTED");

        PredefinedQueries[(int)PredefinedSimpleQuery.BeginReadCommitted] =
            Generate("BEGIN ISOLATION LEVEL READ COMMITTED");

        PredefinedQueries[(int)PredefinedSimpleQuery.BeginRepeatableRead] =
            Generate("BEGIN ISOLATION LEVEL REPEATABLE READ");

        PredefinedQueries[(int)PredefinedSimpleQuery.BeginSerializable] =
            Generate("BEGIN ISOLATION LEVEL SERIALIZABLE");

        PredefinedQueries[(int)PredefinedSimpleQuery.Commit] =
            Generate("COMMIT");

        PredefinedQueries[(int)PredefinedSimpleQuery.Rollback] =
            Generate("ROLLBACK");

        PredefinedQueries[(int)PredefinedSimpleQuery.DiscardAll] =
            Generate("DISCARD ALL");

        static byte[] Generate(string text)
        {
            // The length of the complete message, including the message type.
            var length =
                // Identifies the message as a simple query.
                sizeof(byte) +
                // Length of message contents in bytes, including self.
                sizeof(int) +
                // The query string itself.
                Encoding.UTF8.GetByteCount(text) +
                // A zero byte.
                sizeof(byte);

            var binary = GC.AllocateUninitializedArray<byte>(length);
            var writer = new Writer(binary);

            writer.WriteByte(MessageType);
            writer.WriteInt32(length - sizeof(byte));
            writer.WriteStringWithTerminator(text);

            return binary;
        }
    }

    /// <summary>
    /// Sends the Query message for a predefined simple query command.
    /// </summary>
    /// <param name="writer">The output stream that sends data to the database service.</param>
    /// <param name="query">A <see cref="PredefinedSimpleQuery"/> representing a predefined command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask SendQueryMessageAsync(
        this Writer writer,
        PredefinedSimpleQuery query,
        CancellationToken cancellationToken)
    {
        await writer.WriteBinaryAsync(
            PredefinedQueries[(int)query],
            cancellationToken);
        await writer.FlushAsync(cancellationToken);
    }

    /// <summary>
    /// Sends a Query message for command text consisting of multiple statements.
    /// </summary>
    /// <param name="writer">The output stream that sends data to the database service.</param>
    /// <param name="statements">The command text consisting of multiple statements.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask SendQueryMessageAsync(
        this Writer writer,
        ReadOnlyMemory<char> statements,
        CancellationToken cancellationToken)
    {
        // The length of the complete message, excluding the message type.
        var length =
            // Length of message contents in bytes, including self.
            sizeof(int) +
            // The query string itself.
            Encoding.UTF8.GetByteCount(statements.Span) +
            // A zero byte (empty string).
            sizeof(byte);

        await writer.WriteByteAsync(MessageType, cancellationToken);
        await writer.WriteInt32Async(length, cancellationToken);
        await writer.WriteStringWithTerminatorAsync(statements, cancellationToken);

        await writer.FlushAsync(cancellationToken);
    }
}
