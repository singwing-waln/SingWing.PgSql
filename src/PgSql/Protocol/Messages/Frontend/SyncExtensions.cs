using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Provides methods to send Sync messages to database servers.
/// </summary>
internal static class SyncExtensions
{
    /// <summary>
    /// The complete binary data of the Sync message.
    /// </summary>
    private static readonly byte[] Binary;

    static SyncExtensions()
    {
        const char MessageType = 'S';

        Binary = Helper.GenerateTinyMessageBinary(MessageType);
    }

    /// <summary>
    /// Writes the Sync message to the output writer.
    /// </summary>
    /// <param name="writer">The output writer connected to the server.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ValueTask WriteSyncMessageAsync(
        this Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteBinaryAsync(Binary, cancellationToken);
}
