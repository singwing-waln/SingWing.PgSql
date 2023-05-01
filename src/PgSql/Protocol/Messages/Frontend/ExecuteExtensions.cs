using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Sends Execute messages to database servers.
/// </summary>
internal static class ExecuteExtensions
{
    /// <summary>
    /// Writes a Execute message to the output stream.
    /// </summary>
    /// <param name="writer">The output stream to the database service.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask WriteExecuteMessageAsync(
        this Writer writer,
        CancellationToken cancellationToken)
    {
        // https://www.postgresql.org/docs/current/protocol-message-formats.html
        const char MessageType = 'E';
        const int MaxRowsNoLimit = 0;
        
        await writer.WriteByteAsync(MessageType, cancellationToken);
        await writer.WriteInt32Async(
            sizeof(int) + sizeof(byte) + sizeof(int),
            cancellationToken);
        // The name of the portal to execute (an empty string selects the unnamed portal).
        await writer.WriteStringTerminatorAsync(cancellationToken);
        // Maximum number of rows to return, if portal contains
        // a query that returns rows (ignored otherwise).
        // Zero denotes “no limit”.
        await writer.WriteInt32Async(MaxRowsNoLimit, cancellationToken);
    }

    /// <summary>
    /// Sends a Execute message to the output stream.
    /// </summary>
    /// <param name="writer">The output stream to the database service.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask SendExecuteMessageAsync(
        this Writer writer,
        CancellationToken cancellationToken)
    {
        await writer.WriteExecuteMessageAsync(cancellationToken);
        await writer.FlushAsync(cancellationToken);
    }
}
