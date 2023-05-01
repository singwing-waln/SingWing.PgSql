namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the message received from PostgreSQL when a query command returns no rows.
/// </summary>
internal sealed class NoData : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="NoData"/> class.
    /// </summary>
    internal static readonly NoData Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="NoData"/> class.
    /// </summary>
    private NoData() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.NoData;
}
