using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents a response to a client's LISTEN. 
/// LISTEN is not currently supported, so NotificationResponse is ignored.
/// </summary>
internal sealed class NotificationResponse : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="NotificationResponse"/> class.
    /// </summary>
    private static readonly NotificationResponse Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationResponse"/> class.
    /// </summary>
    private NotificationResponse() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.NotificationResponse;

    /// <summary>
    /// Reads the <see cref="NotificationResponse"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<NotificationResponse> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
