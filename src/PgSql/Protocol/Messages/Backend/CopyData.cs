using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the CopyData message received from PostgreSQL.
/// The COPY command is not currently supported, so CopyData is ignored.
/// </summary>
internal sealed class CopyData : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="CopyData"/> class.
    /// </summary>
    private static readonly CopyData Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CopyData"/> class.
    /// </summary>
    private CopyData() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.CopyData;

    /// <summary>
    /// Reads the <see cref="CopyData"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<CopyData> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
