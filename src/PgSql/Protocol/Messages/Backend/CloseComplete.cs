namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the CloseComplete message received from PostgreSQL after sending a Close message.
/// </summary>
internal sealed class CloseComplete : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="CloseComplete"/> class。
    /// </summary>
    internal static readonly CloseComplete Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CloseComplete"/> class.
    /// </summary>
    private CloseComplete() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.CloseComplete;
}
