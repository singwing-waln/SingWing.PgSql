namespace SingWing.PgSql;

/// <summary>
/// Represents a server running one or more PostgreSQL database services.
/// </summary>
public interface IServer
{
    /// <summary>
    /// Gets the host information for this server.
    /// </summary>
    Host Host { get; }

    /// <summary>
    /// Gets or sets the proportion of the maximum number of connections for the current client.
    /// </summary>
    /// <value>A value greater than 0 and less than or equal to 1.</value>
    /// <remarks>
    /// <para>
    /// This value defines the proportion of the maximum number of connections that can be established by the current client to this server.
    /// For example, if the "max_connecions" of the server is 100, and this value is 0.6, 
    /// then the maximum number of connections that the current client can open is: 0.6 * (100 - 5) = 57.
    /// </para>
    /// <para>
    /// Setting this value does not immediately recalculate the maximum number of connections available. 
    /// The heartbeat timer uses the new value the next time it is scheduled, within <see cref="HeartbeatIntervalSeconds"/> seconds.
    /// </para>
    /// </remarks>
    double ConnectionsProportion { get; set; }

    /// <summary>
    /// Gets the maximum number of connections that the current client can establish to this server.
    /// </summary>
    /// <value>This value is -1 until the first connection is established.</value>
    int MaxConnectionCount { get; }

    /// <summary>
    /// Gets or sets the interval of the heartbeat timer, in seconds.
    /// </summary>
    /// <value>This value is between 1 and 86400 (24 hours), with a default value of 60.</value>
    /// <remarks>
    /// <para>
    /// If the timer has already started, it is updated immediately.
    /// </para>
    /// </remarks>
    int HeartbeatIntervalSeconds { get; set; }
}
