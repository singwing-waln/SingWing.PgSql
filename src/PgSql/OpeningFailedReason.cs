namespace SingWing.PgSql;

/// <summary>
/// Defines the reason for a database connection or authentication failure.
/// </summary>
public enum OpeningFailedReason
{
    /// <summary>
    /// Unable to connect to the host(s) of a PostgreSQL service.
    /// </summary>
    ConnectionFailed,

    /// <summary>
    /// Database user authentication failed, such as incorrect user name or password.
    /// </summary>
    AuthenticationFailed
}
