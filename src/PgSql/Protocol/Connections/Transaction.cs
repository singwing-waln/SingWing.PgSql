namespace SingWing.PgSql.Protocol.Connections;

/// <summary>
/// Represents a transaction initiated on a connection.
/// </summary>
internal sealed class Transaction : ITransaction, IDisposable
{
    /// <summary>
    /// Indicates whether the current transaction has been ended (committed or rolled back).
    /// </summary>
    private int _ended;

    /// <summary>
    /// Indicates whether the transaction has been disposed.
    /// </summary>
    private int _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class
    /// with the specified connection and isolation level.
    /// </summary>
    /// <param name="connection">The connection on which the transaction is started.</param>
    internal Transaction(Connection connection)
    {
        _ended = 1;
        _disposed = 0;
        Connection = connection;
        IsolationLevel = IsolationLevel.ReadCommitted;
        OwnsConnection = false;
    }

    /// <summary>
    /// Gets the connection of the transaction.
    /// </summary>
    internal Connection Connection { get; }

    /// <summary>
    /// Gets or sets a value that indicates whether this transaction should also return the underlying connection when dispose.
    /// </summary>
    internal bool OwnsConnection { get; set; }

    /// <summary>
    /// Gets a value that indicates whether this transaction has started.
    /// </summary>
    private bool Started => _ended == 0;

    /// <summary>
    /// Starts this transaction.
    /// </summary>
    /// <param name="isolationLevel">The isolation level of the transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="InvalidOperationException">A transaction on the connection has started.</exception>
    /// <returns>
    /// An <see cref="ITransaction"/> instance used to execute commands in a single transaction.
    /// </returns>
    internal async ValueTask<ITransaction> BeginAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        if (Interlocked.CompareExchange(ref _ended, 0, 1) == 0)
        {
            throw new InvalidOperationException(Strings.AnotherTransactionIsRunning);
        }

        try
        {
            await Connection.SimpleQueryAsync((PredefinedSimpleQuery)isolationLevel, cancellationToken);
        }
        catch
        {
            _ = Interlocked.Exchange(ref _ended, 1);
            throw;
        }

        _ = Interlocked.Exchange(ref _disposed, 0);
        IsolationLevel = isolationLevel;
        OwnsConnection = false;
        return this;
    }

    #region IExecutor

    /// <inheritdoc/>
    public ValueTask<long> ExecuteAsync(
        ReadOnlyMemory<char> commandText,
        ReadOnlyMemory<Parameter> parameters,
        CancellationToken cancellationToken = default)
    {
        if (!Started || _disposed == 1)
        {
            throw new InvalidOperationException(Strings.TransactionNotRunning);
        }

        return Connection.ExecuteAsync(commandText, parameters, cancellationToken);
    }

    /// <inheritdoc/>
    public ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        ReadOnlyMemory<char> commandText,
        ReadOnlyMemory<Parameter> parameters,
        CancellationToken cancellationToken = default)
    {
        if (!Started || _disposed == 1)
        {
            throw new InvalidOperationException(Strings.TransactionNotRunning);
        }

        return Connection.QueryAsync(commandText, parameters, cancellationToken);
    }

    /// <inheritdoc/>
    public ValueTask PerformAsync(
        ReadOnlyMemory<char> commandText,
        CancellationToken cancellationToken = default)
    {
        if (!Started || _disposed == 1)
        {
            throw new InvalidOperationException(Strings.TransactionNotRunning);
        }

        return Connection.PerformAsync(commandText, cancellationToken);
    }

    #endregion

    #region ITransaction

    /// <summary>
    /// Gets the isolation level of the transaction.
    /// </summary>
    public IsolationLevel IsolationLevel { get; private set; }

    /// <inheritdoc/>
    IDatabase ITransaction.Database => Connection.Node.Database;

    /// <inheritdoc/>
    INode ITransaction.Node => Connection.Node;

    /// <inheritdoc/>
    IConnection ITransaction.Connection => Connection;

    /// <inheritdoc/>
    public ValueTask SaveAsync(
        string savepointName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(savepointName))
        {
            throw new ArgumentException(Strings.SavepointNameIsEmpty, nameof(savepointName));
        }

        if (!Started || _disposed == 1)
        {
            throw new InvalidOperationException(Strings.TransactionNotRunning);
        }

        return Connection.PerformAsync(
            $"SAVEPOINT {savepointName}".AsMemory(),
            cancellationToken);
    }

    /// <inheritdoc/>
    public ValueTask ReleaseAsync(
        string savepointName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(savepointName))
        {
            throw new ArgumentException(Strings.SavepointNameIsEmpty, nameof(savepointName));
        }

        if (!Started || _disposed == 1)
        {
            throw new InvalidOperationException(Strings.TransactionNotRunning);
        }

        return Connection.PerformAsync(
            $"RELEASE SAVEPOINT {savepointName}".AsMemory(),
            cancellationToken);
    }

    /// <inheritdoc/>
    public ValueTask CommitAsync(CancellationToken cancellationToken = default)
    {
        if (Interlocked.CompareExchange(ref _ended, 1, 0) == 1 || _disposed == 1)
        {
            return ValueTask.CompletedTask;
        }

        return Connection.SimpleQueryAsync(PredefinedSimpleQuery.Commit, cancellationToken);
    }

    /// <inheritdoc/>
    public ValueTask RollbackAsync(string savepointName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(savepointName))
        {
            if (Interlocked.CompareExchange(ref _ended, 1, 0) == 1 || _disposed == 1)
            {
                return ValueTask.CompletedTask;
            }

            return Connection.SimpleQueryAsync(PredefinedSimpleQuery.Rollback, cancellationToken);
        }
        else
        {
            if (!Started || _disposed == 1)
            {
                throw new InvalidOperationException(Strings.TransactionNotRunning);
            }

            return Connection.PerformAsync(
                $"ROLLBACK TO SAVEPOINT {savepointName}".AsMemory(),
                cancellationToken);
        }
    }

    #endregion

    #region IAsyncDisposable

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 1)
        {
            return;
        }

        try
        {
            await EndAsync();
        }
        finally
        {
            if (OwnsConnection)
            {
                Connection.Release();
            }
        }

        async ValueTask EndAsync()
        {
            if (Interlocked.CompareExchange(ref _ended, 1, 0) == 1)
            {
                return;
            }

            if (!Connection.IsBroken)
            {
                try
                {
                    await Connection.SimpleQueryAsync(PredefinedSimpleQuery.Rollback, default);
                }
                catch (Exception exc)
                {
                    Db.Logger.LogError(Strings.RollbackTransactionFailed, exc);
                }
            }
        }
    }

    /// <summary>
    /// Ends the transaction.
    /// </summary>
    public void Dispose()
    {
        DisposeAsync().AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    #endregion
}
