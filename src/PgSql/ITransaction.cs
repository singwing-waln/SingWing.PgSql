namespace SingWing.PgSql;

/// <summary>
/// Represents a PostgreSQL transaction to be made in a PostgreSQL database.
/// </summary>
/// <remarks>
/// <para>
/// If a transaction is not explicitly committed, 
/// calling DisposeAsync on an <see cref="ITransaction"/> instance will attempt to rollback the transaction.
/// </para>
/// </remarks>
public interface ITransaction : IExecutor, IAsyncDisposable
{
    /// <summary>
    /// Gets the database in which the transaction resides.
    /// </summary>
    IDatabase Database { get; }

    /// <summary>
    /// Gets the node in which the transaction resides.
    /// </summary>
    INode Node { get; }

    /// <summary>
    /// Gets the underlying connection of the transaction.
    /// </summary>
    IConnection Connection { get; }

    /// <summary>
    /// Gets the <see cref="IsolationLevel"/> of the transaction.
    /// </summary>
    /// <value>The default value is <see cref="IsolationLevel.ReadCommitted"/>.</value>
    IsolationLevel IsolationLevel { get; }

    /// <summary>
    /// Commits the database transaction.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    ValueTask CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back to the specified savepoint.
    /// </summary>
    /// <param name="savepointName">The name of the savepoint to roll back.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <remarks>
    /// <para>
    /// If <paramref name="savepointName"/> is empty, the entire transaction is rolled back, 
    /// otherwise rolled back to the specified savepoint.
    /// </para>
    /// <para>
    /// For more information about savepoints, see <see href="https://www.postgresql.org/docs/current/sql-savepoint.html"/>.
    /// </para>
    /// </remarks>
    ValueTask RollbackAsync(string savepointName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Establishes a new save point within the current transaction.
    /// </summary>
    /// <param name="savepointName">The name of the savepoint.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="ArgumentException"><paramref name="savepointName"/> is empty.</exception>
    /// <remarks>
    /// For more information about savepoints, see <see href="https://www.postgresql.org/docs/current/sql-savepoint.html"/>.
    /// </remarks>
    ValueTask SaveAsync(string savepointName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Destroys a save point previously defined in the current transaction.
    /// </summary>
    /// <param name="savepointName">The name of the savepoint to release.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="ArgumentException"><paramref name="savepointName"/> is empty.</exception>
    /// <remarks>
    /// For more information about savepoints, see <see href="https://www.postgresql.org/docs/current/sql-savepoint.html"/>.
    /// </remarks>
    ValueTask ReleaseAsync(string savepointName, CancellationToken cancellationToken = default);
}
