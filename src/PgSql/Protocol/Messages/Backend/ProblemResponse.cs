using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// The abstract base class for <see cref="ErrorResponse"/> and <see cref="NoticeResponse"/>.
/// </summary>
/// <remarks>
/// <see href="https://www.postgresql.org/docs/current/protocol-error-fields.html"/>.
/// </remarks>
internal abstract class ProblemResponse : IBackendMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProblemResponse"/> class.
    /// </summary>
    protected internal ProblemResponse() { }

    /// <inheritdoc />
    public virtual BackendMessageType Type => BackendMessageType.NoticeResponse;

    /// <inheritdoc cref="ServerException.Severity"/>
    internal ProblemSeverity Severity { get; private set; } = ProblemSeverity.Other;

    /// <inheritdoc cref="ServerException.SqlState"/>
    internal string SqlState { get; private set; } = "";

    /// <summary>
    /// Gets the primary human-readable error message.
    /// </summary>
    internal string Message { get; private set; } = "";

    /// <inheritdoc cref="ServerException.Detail"/>
    internal string Detail { get; private set; } = "";

    /// <inheritdoc cref="ServerException.Hint"/>
    internal string Hint { get; private set; } = "";

    /// <inheritdoc cref="ServerException.Position"/>
    internal int Position { get; private set; }

    /// <summary>
    /// This is defined the same as <see cref="Position"/>, 
    /// but it is used when the cursor position refers to an internally generated command 
    /// rather than the one submitted by the client. 
    /// </summary>
    internal int InternalPosition { get; private set; }

    /// <summary>
    /// Gets he text of a failed internally-generated command. 
    /// This could be, for example, an SQL query issued by a PL/pgSQL function.
    /// </summary>
    internal string InternalQuery { get; private set; } = "";

    /// <inheritdoc cref="ServerException.Where"/>
    internal string Where { get; private set; } = "";

    /// <inheritdoc cref="ServerException.SchemaName"/>
    internal string SchemaName { get; private set; } = "";

    /// <inheritdoc cref="ServerException.TableName"/>
    internal string TableName { get; private set; } = "";

    /// <inheritdoc cref="ServerException.ColumnName"/>
    internal string ColumnName { get; private set; } = "";

    /// <inheritdoc cref="ServerException.DataTypeName"/>
    internal string DataTypeName { get; private set; } = "";

    /// <inheritdoc cref="ServerException.ConstraintName"/>
    internal string ConstraintName { get; private set; } = "";

    /// <summary>
    /// Gets the file name of the source-code location where the error was reported.
    /// </summary>
    internal string File { get; private set; } = "";

    /// <inheritdoc cref="ServerException.LineNumber"/>
    internal string Line { get; private set; } = "";

    /// <inheritdoc cref="ServerException.RoutineName"/>
    internal string Routine { get; private set; } = "";

    /// <summary>
    /// Gets a value that indicates whether this message represents a critical, unrecoverable error.
    /// </summary>
    internal bool IsUnrecoverable
    {
        get
        {
            if (Severity == ProblemSeverity.Fatal || Severity == ProblemSeverity.Panic)
            {
                return true;
            }

            var state = SqlState;

            if (state.Equals("57P01", StringComparison.OrdinalIgnoreCase))
            {
                // admin_shutdown
                return true;
            }

            if (state.Equals("57P02", StringComparison.OrdinalIgnoreCase))
            {
                // crash_shutdown
                return true;
            }

            if (state.Equals("57P03", StringComparison.OrdinalIgnoreCase))
            {
                // cannot_connect_now
                return true;
            }

            if (state.Equals("57P04", StringComparison.OrdinalIgnoreCase))
            {
                // database_dropped
                return true;
            }

            if (state.StartsWith("08", StringComparison.OrdinalIgnoreCase))
            {
                // Connection Exception
                return true;
            }

            if (state.StartsWith("53", StringComparison.OrdinalIgnoreCase))
            {
                // Insufficient Resources
                return true;
            }

            if (state.StartsWith("58", StringComparison.OrdinalIgnoreCase))
            {
                // System Error (errors external to PostgreSQL itself)
                return true;
            }

            if (state.StartsWith("F0", StringComparison.OrdinalIgnoreCase))
            {
                // Configuration File Error
                return true;
            }

            if (state.StartsWith("XX", StringComparison.OrdinalIgnoreCase))
            {
                // Internal Error
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Writes this message to the log using <see cref="Db.Logger"/>.
    /// </summary>
    internal void Log()
    {
        switch (Severity)
        {
            case ProblemSeverity.Debug:
                Db.Logger.LogDebug($"{SqlState}: {Message}\r\n{Detail}");
                break;
            case ProblemSeverity.Warning:
                Db.Logger.LogWarning($"{SqlState}: {Message}\r\n{Detail}");
                break;
            case ProblemSeverity.Error:
                Db.Logger.LogError($"{SqlState}: {Message}\r\n{Detail}");
                break;
            case ProblemSeverity.Fatal:
            case ProblemSeverity.Panic:
                Db.Logger.LogFatal($"{SqlState}: {Message}\r\n{Detail}");
                break;
            case ProblemSeverity.Other:
                Db.Logger.LogInformation($"{SqlState}: {Message}\r\n{Detail}");
                break;
            case ProblemSeverity.Log:
                Db.Logger.LogInformation($"{SqlState}: {Message}\r\n{Detail}");
                break;
            case ProblemSeverity.Info:
                Db.Logger.LogInformation($"{SqlState}: {Message}\r\n{Detail}");
                break;
            case ProblemSeverity.Notice:
                Db.Logger.LogInformation($"{SqlState}: {Message}\r\n{Detail}");
                break;
            default:
                Db.Logger.LogInformation($"{SqlState}: {Message}\r\n{Detail}");
                break;
        }
    }

    /// <summary>
    /// Reads the <see cref="ProblemResponse"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="predefined">A predefined <see cref="ProblemResponse"/> object to populate and return.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The read <see cref="ProblemResponse"/> instance.</returns>
    internal static async ValueTask<ProblemResponse> ReadAsync(
        Reader reader,
        int length,
        ProblemResponse predefined,
        CancellationToken cancellationToken)
    {
        var done = false;
        while (length > 0)
        {
            var field = await reader.ReadByteAsync(cancellationToken);
            length -= sizeof(byte);

            if (field == 0)
            {
                done = true;
                break;
            }

            var (text, read) = await reader.ReadNullTerminatedStringAsync(
                length, cancellationToken);
            length -= read;

            switch (field)
            {
                case (byte)'V':
                    predefined.Severity = ToSeverity(text);
                    break;
                case (byte)'C':
                    predefined.SqlState = text;
                    break;
                case (byte)'M':
                    predefined.Message = text;
                    break;
                case (byte)'D':
                    predefined.Detail = text;
                    break;
                case (byte)'H':
                    predefined.Hint = text;
                    break;
                case (byte)'P':
                    _ = int.TryParse(text, out var position1);
                    predefined.Position = position1 < 1 ? 1 : position1;
                    break;
                case (byte)'p':
                    _ = int.TryParse(text, out var position2);
                    predefined.InternalPosition = position2 < 1 ? 1 : position2;
                    break;
                case (byte)'q':
                    predefined.InternalQuery = text;
                    break;
                case (byte)'W':
                    predefined.Where = text;
                    break;
                case (byte)'s':
                    predefined.SchemaName = text;
                    break;
                case (byte)'t':
                    predefined.TableName = text;
                    break;
                case (byte)'c':
                    predefined.ColumnName = text;
                    break;
                case (byte)'d':
                    predefined.DataTypeName = text;
                    break;
                case (byte)'n':
                    predefined.ConstraintName = text;
                    break;
                case (byte)'F':
                    predefined.File = text;
                    break;
                case (byte)'L':
                    predefined.Line = text;
                    break;
                case (byte)'R':
                    predefined.Routine = text;
                    break;
                default:
                    // Fields that are not recognized are ignored.
                    break;
            }
        }

        Debug.Assert(done && length == 0);

        return predefined;

        static ProblemSeverity ToSeverity(string text)
        {
            if (text.Equals("ERROR", StringComparison.OrdinalIgnoreCase))
            {
                return ProblemSeverity.Error;
            }

            if (text.Equals("FATAL", StringComparison.OrdinalIgnoreCase))
            {
                return ProblemSeverity.Fatal;
            }

            if (text.Equals("PANIC", StringComparison.OrdinalIgnoreCase))
            {
                return ProblemSeverity.Panic;
            }

            if (text.Equals("WARNING", StringComparison.OrdinalIgnoreCase))
            {
                return ProblemSeverity.Warning;
            }

            if (text.Equals("NOTICE", StringComparison.OrdinalIgnoreCase))
            {
                return ProblemSeverity.Notice;
            }

            if (text.Equals("DEBUG", StringComparison.OrdinalIgnoreCase))
            {
                return ProblemSeverity.Debug;
            }

            if (text.Equals("INFO", StringComparison.OrdinalIgnoreCase))
            {
                return ProblemSeverity.Info;
            }

            return text.Equals("LOG", StringComparison.OrdinalIgnoreCase)
                ? ProblemSeverity.Log
                : ProblemSeverity.Other;
        }
    }
}
