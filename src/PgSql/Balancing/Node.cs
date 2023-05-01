using SingWing.PgSql.Pooling.Commands;
using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Balancing;

/// <summary>
/// Represents a distributed database service node.
/// </summary>
internal sealed class Node : Dispatcher, INode, IConnectionPoolOptions
{
    /// <summary>
    /// The underlying connection pool.
    /// </summary>
    private readonly Pool _pool;

    /// <summary>
    /// The time span to wait before a connection establishing times out, in seconds.
    /// </summary>
    private int _connectTimeoutSeconds;

    /// <summary>
    /// The time span to wait before a data receiving times out, in seconds.
    /// </summary>
    private int _receiveTimeoutSeconds;

    /// <summary>
    /// The time span to wait before a data sending times out, in seconds.
    /// </summary>
    private int _sendTimeoutSeconds;

    /// <summary>
    /// The time span to wait before a connection is returned, in seconds.
    /// </summary>
    private int _waitTimeoutSeconds;

    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class
    /// with the specified server and database.
    /// </summary>
    /// <param name="server">The server on which this node runs.</param>
    /// <param name="database">The database hosted by the node.</param>
    internal Node(Server server, Database database)
    {
        Server = server;
        Database = database;
        _pool = new(this);

        // 0 indicates that the option value is inherited from its database.
        _connectTimeoutSeconds = 0;
        _receiveTimeoutSeconds = 0;
        _sendTimeoutSeconds = 0;
        _waitTimeoutSeconds = 0;
    }

    /// <summary>
    /// Gets the server on which this node runs.
    /// </summary>
    internal Server Server { get; }

    /// <summary>
    /// Gets the database hosted by the node.
    /// </summary>
    internal Database Database { get; }

    /// <summary>
    /// Attempts to get an idle connection and returns immediately if no connection is available.
    /// </summary>
    /// <param name="connection">The acquired connection.</param>
    /// <returns><see langword="true"/> if a connection was acquired, <see langword="false"/> otherwise.</returns>
    internal bool TryAcquire(out Connection? connection) => _pool.TryAcquire(out connection);

    /// <summary>
    /// Returns a connection to the underlying connection pool.
    /// </summary>
    /// <param name="connection">The connection to return.</param>
    internal void Release(Connection connection) => _pool.Release(connection);

    /// <summary>
    /// Tests all connections in the queue and cleans up disconnected connections.
    /// </summary>
    internal ValueTask TestAndCleanupAsync() => _pool.TestAndCleanupAsync();

    /// <summary>
    /// Removes a connection from the underlying connection pool.
    /// This method is only called in <see cref="Connection.Close"/>.
    /// </summary>
    /// <param name="connectionId">The id of the connection to remove.</param>
    internal void Remove(ulong connectionId) => _pool.Remove(connectionId);

    #region Dispatcher

    /// <summary>
    /// Gets the number of idle connections.
    /// </summary>
    internal override int IdleConnectionCount => Math.Max(0, _pool.IdleConnectionCount - CommandCount);

    /// <inheritdoc/>
    internal override string DatabaseName => Database.DatabaseName;

    /// <inheritdoc/>
    internal override ValueTask<Connection> AcquireAsync(
        ICommand command,
        CancellationToken cancellationToken) =>
        _pool.AcquireAsync(command, cancellationToken);

    /// <inheritdoc/>
    internal override int PruneConnections(int countPruning) => _pool.Prune(countPruning);

    /// <summary>
    /// Closes all connections, clears the command queue.
    /// </summary>
    public override void Dispose()
    {
        _pool.Dispose();
        base.Dispose();
    }

    #endregion

    #region INode

    /// <inheritdoc/>
    IDatabase INode.Database => Database;

    /// <inheritdoc/>
    IServer INode.Server => Server;

    #endregion

    #region IConnectionPoolOptions

    /// <inheritdoc/>
    public int ConnectTimeoutSeconds
    {
        get => _connectTimeoutSeconds == 0 ? Database.ConnectTimeoutSeconds : _connectTimeoutSeconds;
        set => _connectTimeoutSeconds = value < 0
            ? 0
            : (value > Database.MaxTimeoutSeconds ? Database.MaxTimeoutSeconds : value);
    }

    /// <inheritdoc/>
    public int ReceiveTimeoutSeconds
    {
        get => _receiveTimeoutSeconds == 0 ? Database.ReceiveTimeoutSeconds : _receiveTimeoutSeconds;
        set => _receiveTimeoutSeconds = value < 0
            ? 0
            : (value > Database.MaxTimeoutSeconds ? Database.MaxTimeoutSeconds : value);
    }

    /// <inheritdoc/>
    public int SendTimeoutSeconds
    {
        get => _sendTimeoutSeconds == 0 ? Database.SendTimeoutSeconds : _sendTimeoutSeconds;
        set => _sendTimeoutSeconds = value < 0
            ? 0
            : (value > Database.MaxTimeoutSeconds ? Database.MaxTimeoutSeconds : value);
    }

    /// <inheritdoc/>
    public int WaitTimeoutSeconds
    {
        get => _waitTimeoutSeconds == 0 ? Database.WaitTimeoutSeconds : _waitTimeoutSeconds;
        set => _waitTimeoutSeconds = value < 0
            ? 0
            : (value > Database.MaxTimeoutSeconds ? Database.MaxTimeoutSeconds : value);
    }

    #endregion

    #region IConnectionPool

    /// <inheritdoc/>
    IConnectionPoolOptions IConnectionPool.Options => this;

    /// <inheritdoc/>
    async ValueTask<IConnection> IConnectionPool.AcquireAsync(CancellationToken cancellationToken) =>
        await _pool.AcquireAsync(command: null, cancellationToken);

    #endregion

    #region ITransactional

    /// <inheritdoc/>
    ValueTask<ITransaction> ITransactional.BeginAsync(
       IsolationLevel isolationLevel,
       CancellationToken cancellationToken) =>
       AppendCommand(new TransactionCommand(isolationLevel, cancellationToken));

    #endregion

    #region IExecutor

    /// <inheritdoc/>
    ValueTask<IAsyncEnumerable<IRow>> IExecutor.QueryAsync(
       ReadOnlyMemory<char> commandText,
       ReadOnlyMemory<Parameter> parameters,
       CancellationToken cancellationToken) =>
       AppendCommand(new QueryCommand(commandText, parameters, cancellationToken));

    /// <inheritdoc/>
    ValueTask<long> IExecutor.ExecuteAsync(
       ReadOnlyMemory<char> commandText,
       ReadOnlyMemory<Parameter> parameters,
       CancellationToken cancellationToken) =>
       AppendCommand(new NonQueryCommand(commandText, parameters, cancellationToken));

    /// <inheritdoc/>
    ValueTask IExecutor.PerformAsync(
       ReadOnlyMemory<char> statements,
       CancellationToken cancellationToken) =>
       AppendCommand(new StatementsCommand(statements, cancellationToken));

    #endregion
}
