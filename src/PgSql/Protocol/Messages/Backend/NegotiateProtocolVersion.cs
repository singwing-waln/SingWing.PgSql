using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents a message that is responded to by PostgreSQL 
/// when the requested protocol version or certain options are not recognized or supported by the server.
/// This message is followed by an ErrorResponse or a message indicating the success or failure of authentication, 
/// so NegotiateProtocolVersion is ignored.
/// </summary>
internal sealed class NegotiateProtocolVersion : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="NegotiateProtocolVersion"/> class.
    /// </summary>
    private static readonly NegotiateProtocolVersion Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="NegotiateProtocolVersion"/> class.
    /// </summary>
    private NegotiateProtocolVersion() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.NegotiateProtocolVersion;

    /// <summary>
    /// Reads the <see cref="NegotiateProtocolVersion"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<NegotiateProtocolVersion> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
