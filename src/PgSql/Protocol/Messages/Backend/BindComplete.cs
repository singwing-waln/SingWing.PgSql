namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the BindComplete message received from PostgreSQL after sending a Bind message.
/// </summary>
internal sealed class BindComplete : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="BindComplete"/> class。
    /// </summary>
    internal static readonly BindComplete Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BindComplete"/> class.
    /// </summary>
    private BindComplete() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.BindComplete;
}
