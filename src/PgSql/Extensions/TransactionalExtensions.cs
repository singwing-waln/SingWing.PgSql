namespace SingWing.PgSql;

/// <summary>
/// Provides extension methods to <see cref="ITransactional"/>.
/// </summary>
public static class TransactionalExtensions
{
    /// <summary>
    /// Starts a transaction with the default isolation level (<see cref="IsolationLevel.ReadCommitted"/>).
    /// </summary>
    /// <param name="transactional">The <see cref="ITransactional" /> for starting the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// An <see cref="ITransaction"/> instance used to execute commands in a single transaction.
    /// </returns>
    /// <remarks>
    /// <para>
    /// PostgreSQL does not support nested or parallel transactions. 
    /// When this method is called again within one transaction, 
    /// a new transaction is started in another connection.
    /// </para>
    /// <para>
    /// Once a transaction is started, 
    /// it monopolizes a connection and must be ended by calling DisposeAsync to return the connection to the connection pool.
    /// </para>
    /// </remarks>
    public static ValueTask<ITransaction> BeginAsync(
        this ITransactional transactional,
        CancellationToken cancellationToken = default) =>
        transactional.BeginAsync(IsolationLevel.ReadCommitted, cancellationToken);

    /// <summary>
    /// Starts a transaction and performs the specified action in the transaction.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    /// <param name="transactional">The <see cref="ITransactional"/> to start the transaction.</param>
    /// <param name="action">The action to perform in the transaction.</param>
    /// <param name="isolationLevel">The isolation level of the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The returned value of the <paramref name="action"/>.</returns>
    public static async ValueTask<TResult> TransactAsync<TResult>(
        this ITransactional transactional,
        Func<ITransaction, CancellationToken, ValueTask<TResult>> action,
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await transactional.BeginAsync(isolationLevel, cancellationToken);

        try
        {
            var result = await action(transaction, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    /// <summary>
    /// Starts a transaction and performs the specified action in the transaction.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    /// <param name="transactional">The <see cref="ITransactional"/> to start the transaction.</param>
    /// <param name="action">The action to perform in the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The returned value of the <paramref name="action"/>.</returns>
    public static ValueTask<TResult> TransactAsync<TResult>(
        this ITransactional transactional,
        Func<ITransaction, CancellationToken, ValueTask<TResult>> action,
        CancellationToken cancellationToken = default) =>
        TransactAsync(transactional, action, IsolationLevel.ReadCommitted, cancellationToken);

    /// <summary>
    /// Starts a transaction and performs the specified action in the transaction.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    /// <param name="transactional">The <see cref="ITransactional"/> to start the transaction.</param>
    /// <param name="action">The action to perform in the transaction.</param>
    /// <param name="isolationLevel">The isolation level of the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The returned value of the <paramref name="action"/>.</returns>
    public static async ValueTask<TResult> TransactAsync<TResult>(
        this ITransactional transactional,
        Func<ITransaction, ValueTask<TResult>> action,
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await transactional.BeginAsync(isolationLevel, cancellationToken);

        try
        {
            var result = await action(transaction);
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    /// <summary>
    /// Starts a transaction and performs the specified action in the transaction.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    /// <param name="transactional">The <see cref="ITransactional"/> to start the transaction.</param>
    /// <param name="action">The action to perform in the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The returned value of the <paramref name="action"/>.</returns>
    public static ValueTask<TResult> TransactAsync<TResult>(
        this ITransactional transactional,
        Func<ITransaction, ValueTask<TResult>> action,
        CancellationToken cancellationToken = default) =>
        TransactAsync(transactional, action, IsolationLevel.ReadCommitted, cancellationToken);

    /// <summary>
    /// Starts a transaction and performs the specified action in the transaction.
    /// </summary>
    /// <param name="transactional">The <see cref="ITransactional"/> to start the transaction.</param>
    /// <param name="action">The action to perform in the transaction.</param>
    /// <param name="isolationLevel">The isolation level of the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    public static async ValueTask TransactAsync(
        this ITransactional transactional,
        Func<ITransaction, CancellationToken, ValueTask> action,
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await transactional.BeginAsync(isolationLevel, cancellationToken);

        try
        {
            await action(transaction, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    /// <summary>
    /// Starts a transaction and performs the specified action in the transaction.
    /// </summary>
    /// <param name="transactional">The <see cref="ITransactional"/> to start the transaction.</param>
    /// <param name="action">The action to perform in the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    public static ValueTask TransactAsync(
        this ITransactional transactional,
        Func<ITransaction, CancellationToken, ValueTask> action,
        CancellationToken cancellationToken = default) =>
        TransactAsync(transactional, action, IsolationLevel.ReadCommitted, cancellationToken);

    /// <summary>
    /// Starts a transaction and performs the specified action in the transaction.
    /// </summary>
    /// <param name="transactional">The <see cref="ITransactional"/> to start the transaction.</param>
    /// <param name="action">The action to perform in the transaction.</param>
    /// <param name="isolationLevel">The isolation level of the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    public static async ValueTask TransactAsync(
        this ITransactional transactional,
        Func<ITransaction, ValueTask> action,
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await transactional.BeginAsync(isolationLevel, cancellationToken);

        try
        {
            await action(transaction);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    /// <summary>
    /// Starts a transaction and performs the specified action in the transaction.
    /// </summary>
    /// <param name="transactional">The <see cref="ITransactional"/> to start the transaction.</param>
    /// <param name="action">The action to perform in the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    public static ValueTask TransactAsync(
        this ITransactional transactional,
        Func<ITransaction, ValueTask> action,
        CancellationToken cancellationToken = default) =>
        TransactAsync(transactional, action, IsolationLevel.ReadCommitted, cancellationToken);
}
