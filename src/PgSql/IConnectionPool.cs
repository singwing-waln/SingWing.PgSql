namespace SingWing.PgSql;

/// <summary>
/// Represents a pool that can provide connections.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="IDatabase"/> and <see cref="INode"/> implement this interface.
/// </para>
/// <para>
/// This interface does not define release-related methods, connections can be returned to the pool 
/// by calling the IAsyncDisposable.DisposeAsync defined on <see cref="IConnection"/>.
/// </para>
/// </remarks>
public interface IConnectionPool
{
    /// <summary>
    /// Gets the configuration options for this connection pool.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="IConnectionPoolOptions"/> allows callers to dynamically adjust connection pool options 
    /// of a <see cref="INode"/> or a <see cref="IDatabase"/>
    /// </para>
    /// <para>
    /// Before setting an option, a <see cref="INode"/> inherits the option value from its <see cref="IDatabase"/>.
    /// </para>
    /// </remarks>
    IConnectionPoolOptions Options { get; }

    /// <summary>
    /// Acquires a connection from the pool.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The acquired <see cref="IConnection"/> that can be used to execute commands or start transactions.</returns>
    /// <remarks>
    /// <para>
    /// Typically, a connection is acquired when the caller wants all subsequent operations, 
    /// such as executing commands and starting transactions, to be performed on the same connection.
    /// </para>
    /// <para>
    /// The caller should return the connection to the connection pool through the IAsyncDisposable interface 
    /// on the <see cref="IConnection"/> when the connection is no longer in use.
    /// </para>
    /// </remarks>
    ValueTask<IConnection> AcquireAsync(CancellationToken cancellationToken = default);
}
