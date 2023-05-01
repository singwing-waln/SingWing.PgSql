using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the CopyOutResponse message recevied from PostgreSQL.
/// The COPY command is not currently supported, so CopyOutResponse is ignored.
/// </summary>
internal sealed class CopyOutResponse : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="CopyOutResponse"/> class.
    /// </summary>
    private static readonly CopyOutResponse Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CopyOutResponse"/> class.
    /// </summary>
    private CopyOutResponse() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.CopyOutResponse;

    /// <summary>
    /// Reads the <see cref="CopyOutResponse"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<CopyOutResponse> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
