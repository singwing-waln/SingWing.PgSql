namespace SingWing.PgSql.Balancing.Commands;

/// <summary>
/// Defines the release state of connections after a command has finished executing.
/// </summary>
internal enum ConnectionReleaseState
{
    /// <summary>
    /// The connection is still in use.
    /// </summary>
    InUse,

    /// <summary>
    /// The connection has been released.
    /// </summary>
    Released
}
