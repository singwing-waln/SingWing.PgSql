namespace SingWing.PgSql;

/// <summary>
/// Represents a collection of options for connections in a connection pool.
/// </summary>
/// <remarks>
/// Through <see cref="IConnectionPoolOptions"/>, callers can dynamically adjust connection pool options 
/// of a <see cref="INode"/> or a <see cref="IDatabase"/>.
/// </remarks>
public interface IConnectionPoolOptions
{
    /// <summary>
    /// Gets or sets the time span to wait before a connection establishing times out, in seconds.
    /// </summary>
    /// <value>This value ranges from 1 to 900 seconds (15 minutes), with a default value of 15 seconds.</value>
    /// <remarks>
    /// <para>
    /// If set to less than 1 second, 1 is used. When more than 900 seconds, 900 is used.
    /// But for a <see cref="INode"/>, setting a value less than or equal to 0 means that 
    /// the option value for this node is inherited from its <see cref="INode.Database"/>.
    /// </para>
    /// </remarks>
    int ConnectTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets or sets the time span to wait before a data receiving times out, in seconds.
    /// </summary>
    /// <value>This value ranges from 1 to 900 seconds (15 minutes), with a default value of 15 seconds.</value>
    /// <remarks>
    /// <para>
    /// This value defines the timeout for performing a network read operation, not for reading a full result set.
    /// So, it also affects data reads in user authentications.
    /// </para>
    /// <para>
    /// If set to less than 1 second, 1 is used. When more than 900 seconds, 900 is used.
    /// But for a <see cref="INode"/>, setting a value less than or equal to 0 means that 
    /// the option value for this node is inherited from its <see cref="INode.Database"/>.
    /// </para>
    /// </remarks>
    int ReceiveTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets or sets the time span to wait before a data sending times out, in seconds.
    /// </summary>
    /// <value>This value ranges from 1 to 900 seconds (15 minutes), with a default value of 15 seconds.</value>
    /// <remarks>
    /// <para>
    /// This value defines the timeout for performing a network write operation, not for sending a full command.
    /// So, it also affects data writes in user authentications.
    /// </para>
    /// <para>
    /// If set to less than 1 second, 1 is used. When more than 900 seconds, 900 is used.
    /// But for a <see cref="INode"/>, setting a value less than or equal to 0 means that 
    /// the option value for this node is inherited from its <see cref="INode.Database"/>.
    /// </para>
    /// </remarks>
    int SendTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets or sets the time span to wait before a connection is returned, in seconds.
    /// </summary>
    /// <value>This value ranges from 1 to 900 seconds (15 minutes), with a default value of 15 seconds.</value>
    /// <remarks>
    /// <para>
    /// If set to less than 1 second, 1 is used. When more than 900 seconds, 900 is used.
    /// But for a <see cref="INode"/>, setting a value less than or equal to 0 means that 
    /// the option value for this node is inherited from its <see cref="INode.Database"/>.
    /// </para>
    /// </remarks>
    int WaitTimeoutSeconds { get; set; }
}
