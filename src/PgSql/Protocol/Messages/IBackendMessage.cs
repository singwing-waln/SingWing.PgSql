namespace SingWing.PgSql.Protocol.Messages;

/// <summary>
/// Represents a backend message received from database servers.
/// </summary>
internal interface IBackendMessage
{
    /// <summary>
    /// Gets the type code of the message.
    /// </summary>
    BackendMessageType Type { get; }
}
