using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the BackendKeyData message received from PostgreSQL.
/// The command cancellation is not currently supported, so BackendKeyData is ignored.
/// </summary>
internal sealed class BackendKeyData : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="BackendKeyData"/> class.
    /// </summary>
    private static readonly BackendKeyData Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BackendKeyData"/> class.
    /// </summary>
    private BackendKeyData() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.BackendKeyData;

    /// <summary>
    /// Reads the <see cref="BackendKeyData"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<BackendKeyData> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
