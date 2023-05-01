namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the message sent from PostgreSQL when the authentication exchange is successfully completed.
/// </summary>
internal sealed class AuthenticationOk : AuthenticationRequest
{
    /// <summary>
    /// A shared singleton instance of the <see cref="AuthenticationOk"/> class.
    /// </summary>
    internal static readonly AuthenticationOk Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationOk"/> class.
    /// </summary>
    private AuthenticationOk() { }
}
