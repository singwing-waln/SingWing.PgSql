namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the CopyDone message received from PostgreSQL.
/// </summary>
internal sealed class CopyDone : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="CopyDone"/> class.
    /// </summary>
    internal static readonly CopyDone Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CopyDone"/> class.
    /// </summary>
    private CopyDone() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.CopyDone;
}
