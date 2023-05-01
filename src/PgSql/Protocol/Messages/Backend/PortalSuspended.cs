namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the message that PostgreSQL notifies the client that another Execute should be sent.
/// </summary>
internal sealed class PortalSuspended : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="PortalSuspended"/> class。
    /// </summary>
    internal static readonly PortalSuspended Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="PortalSuspended"/> class.
    /// </summary>
    private PortalSuspended() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.PortalSuspended;
}
