namespace SingWing.PgSql;

/// <summary>
/// Represents a transactional object that can start transactions.
/// </summary>
/// <remarks>
/// <see cref="IDatabase"/>, <see cref="INode"/> and <see cref="IConnection"/> implement this interface.
/// </remarks>
public interface ITransactional : IExecutor
{
    /// <summary>
    /// Starts a transaction with the specified isolation level.
    /// </summary>
    /// <param name="isolationLevel">The isolation level of the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// An <see cref="ITransaction"/> instance used to execute commands in a single transaction.
    /// </returns>
    /// <remarks>
    /// <para>
    /// PostgreSQL does not support nested or parallel transactions. 
    /// If this method is called on a connection pool, the transaction may be started 
    /// with a different connection each time it is called. 
    /// If there is only one connection in the connection pool and no new connection can be opened, 
    /// an InvalidOperationException is generated instead of waiting for the connection to be returned.
    /// </para>
    /// <para>
    /// If an attempt is made to start a transaction on a connection that has already started another transaction, 
    /// an InvalidOperationException is generated immediately.
    /// </para>
    /// <para>
    /// Once a transaction is started, it monopolizes a connection and must be ended through the IAsyncDisposable 
    /// interface on <see cref="ITransaction"/> or <see cref="IConnection"/> to return the connection to its connection pool.
    /// </para>
    /// </remarks>
    ValueTask<ITransaction> BeginAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default);
}
