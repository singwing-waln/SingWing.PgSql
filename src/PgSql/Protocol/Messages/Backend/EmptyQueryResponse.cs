namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents a response message when PostgreSQL receives an empty command.
/// </summary>
internal sealed class EmptyQueryResponse : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="EmptyQueryResponse"/> class.
    /// </summary>
    internal static readonly EmptyQueryResponse Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyQueryResponse"/> class.
    /// </summary>
    private EmptyQueryResponse() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.EmptyQueryResponse;
}
