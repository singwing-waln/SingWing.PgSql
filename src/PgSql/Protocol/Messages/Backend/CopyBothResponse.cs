using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the CopyBothResponse message.
/// The COPY command is not currently supported, so CopyBothResponse is ignored.
/// </summary>
internal sealed class CopyBothResponse : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="CopyBothResponse"/> class.
    /// </summary>
    private static readonly CopyBothResponse Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CopyBothResponse"/> class.
    /// </summary>
    private CopyBothResponse() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.CopyBothResponse;

    /// <summary>
    /// Reads the <see cref="CopyBothResponse"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<CopyBothResponse> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
