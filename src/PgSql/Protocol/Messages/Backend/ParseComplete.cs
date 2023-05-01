namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the ParseComplete message received from PostgreSQL after sending a Parse message.
/// </summary>
internal sealed class ParseComplete : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="ParseComplete"/> class。
    /// </summary>
    internal static readonly ParseComplete Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseComplete"/> class.
    /// </summary>
    private ParseComplete() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.ParseComplete;
}
