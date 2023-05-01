using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// When PostgreSQL completes authentication, or completes a command, notifies the client that it can send the next command.
/// </summary>
internal sealed class ReadyForQuery : IBackendMessage
{
    /// <summary>
    /// The command completed and the session is not currently in a transaction block.
    /// </summary>
    private static readonly ReadyForQuery NotInTransaction =
        new(ReadyStatus.NotInTransaction);

    /// <summary>
    /// The command completed and the session is in a succeeded transaction block.
    /// </summary>
    private static readonly ReadyForQuery SucceededTransaction =
        new(ReadyStatus.SucceededTransaction);

    /// <summary>
    /// The command completed and the session is in a failed transaction block.
    /// </summary>
    private static readonly ReadyForQuery FailedTransaction =
        new(ReadyStatus.FailedTransaction);

    /// <summary>
    /// The command completed and the session is in an unknown status.
    /// </summary>
    private static readonly ReadyForQuery Unknown =
        new(ReadyStatus.Unknown);

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadyForQuery"/> class
    /// with the specified status code.
    /// </summary>
    /// <param name="status">The status code when the command completes.</param>
    private ReadyForQuery(ReadyStatus status) => Status = status;

    /// <summary>
    /// Gets the status code when the command completes.
    /// </summary>
    internal ReadyStatus Status { get; }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.ReadyForQuery;

    /// <summary>
    /// Reads the <see cref="ReadyForQuery"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The read <see cref="ReadyForQuery"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<ReadyForQuery> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        Debug.Assert(length == sizeof(byte));

        var status = await reader.ReadByteAsync(cancellationToken);

        return status switch
        {
            (byte)ReadyStatus.NotInTransaction => NotInTransaction,
            (byte)ReadyStatus.SucceededTransaction => SucceededTransaction,
            (byte)ReadyStatus.FailedTransaction => FailedTransaction,
            _ => Unknown,
        };
    }
}
