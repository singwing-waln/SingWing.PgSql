using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Sends CopyDone messages to database servers.
/// </summary>
internal static class CopyDoneExtensions
{
    /// <summary>
    /// The complete binary data of CopyDone message.
    /// </summary>
    private static readonly byte[] Binary;

    static CopyDoneExtensions()
    {
        const char MessageType = 'c';
        Binary = Helper.GenerateTinyMessageBinary(MessageType);
    }

    /// <summary>
    /// Sends a CopyDone message to the output stream.
    /// </summary>
    /// <param name="writer">The output stream to the database service.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask SendCopyDoneMessageAsync(
        this Writer writer,
        CancellationToken cancellationToken)
    {
        await writer.WriteBinaryAsync(Binary, cancellationToken);
        await writer.FlushAsync(cancellationToken);
    }
}
