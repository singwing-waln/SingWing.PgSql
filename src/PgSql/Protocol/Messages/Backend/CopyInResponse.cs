using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the CopyInResponse message recevied from PostgreSQL.
/// The COPY command is not currently supported, so CopyInResponse is ignored.
/// </summary>
internal sealed class CopyInResponse : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="CopyInResponse"/> class.
    /// </summary>
    private static readonly CopyInResponse Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CopyInResponse"/> class.
    /// </summary>
    private CopyInResponse() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.CopyInResponse;

    /// <summary>
    /// Reads the <see cref="CopyInResponse"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<CopyInResponse> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
