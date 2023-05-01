using SingWing.PgSql.Pooling.Commands;
using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Balancing;

/// <summary>
/// Maintains and manages all connections from a single user to a single database.
/// </summary>
internal sealed partial class Database : Dispatcher, IDatabase, INodeCollection, IConnectionPoolOptions
{
    /// <summary>
    /// All nodes that host this database.
    /// </summary>
    private readonly List<Node> _nodes;

    /// <summary>
    /// The index of the <see cref="Node"/> from which the next <see cref="Connection"/> is acquired.
    /// </summary>
    private volatile int _nextNodeIndex;

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
    /// Initializes a new instance of the <see cref="Database"/> class
    /// with the specified server list and id.
    /// </summary>
    /// <param name="servers">The servers that host the database.</param>
    /// <param name="name">The database name.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="password">The user password.</param>
    /// <param name="id">The unique ID provided by the caller.</param>
    internal Database(Server[] servers, string name, string userName, string password, Guid id)
    {
        Id = id;
        Name = name;
        UserName = userName;
        Password = password;
        ExtendedQueryCache = new();

        _nodes = new();
        _nextNodeIndex = 0;
        _connectTimeoutSeconds = DefaultTimeoutSeconds;
        _receiveTimeoutSeconds = DefaultTimeoutSeconds;
        _sendTimeoutSeconds = DefaultTimeoutSeconds;
        _waitTimeoutSeconds = DefaultTimeoutSeconds;

        foreach (var server in servers)
        {
            _nodes.Add(new Node(server, this));
        }
    }

    /// <summary>
    /// Gets the password for the user.
    /// </summary>
    /// <value>Comparisons of this value are case-sensitive.</value>
    internal string Password { get; }

    /// <summary>
    /// The cache for extended queries.
    /// </summary>
    internal ExtendedQuery.Cache ExtendedQueryCache { get; }

    /// <summary>
    /// Updates the node list as the server hosting the database grows.
    /// </summary>
    /// <param name="servers">The new list of servers that host the database.</param>
    internal void UpdateNodes(Server[] servers)
    {
        lock (_nodes)
        {
            foreach (var server in servers)
            {
                if (_nodes.Any(node => node.Server.Host == server.Host))
                {
                    continue;
                }

                _nodes.Add(new Node(server, this));
            }
        }
    }

    /// <summary>
    /// Finds one available idle connection from all nodes of this database. 
    /// If no connection is available, there is no waiting.
    /// </summary>
    /// <param name="connection">The acquired connection.</param>
    /// <returns><see langword="true"/> if a connection was acquired, <see langword="false"/> otherwise.</returns>
    private bool TryAcquire(out Connection? connection)
    {
        lock (_nodes)
        {
            connection = null;
            if (_nodes.Count == 0)
            {
                return false;
            }

            int index;
            _ = _nodes[_nextNodeIndex].TryAcquire(out connection);

            if (connection is not null)
            {
                index = _nextNodeIndex;
                goto Acquired;
            }

            if (_nodes.Count == 1)
            {
                return false;
            }

            var i = _nextNodeIndex + 1;
            for (; i < _nodes.Count; i++)
            {
                var node = _nodes[i];
                _ = node.TryAcquire(out connection);

                if (connection is not null)
                {
                    index = i;
                    goto Acquired;
                }
            }

            for (i = 0; i < _nextNodeIndex; i++)
            {
                var node = _nodes[i];
                _ = node.TryAcquire(out connection);

                if (connection is not null)
                {
                    index = i;
                    goto Acquired;
                }
            }

            return false;

        Acquired:
            _nextNodeIndex = index == _nodes.Count - 1 ? 0 : index + 1;
            return true;
        }
    }

    /// <summary>
    /// Gets one available idle connection to query the maximum number of connections for the specified server.
    /// </summary>
    /// <param name="server">The server whose maximum number of connections to update.</param>
    /// <param name="connection">The acquired idle connection or <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if a connection was acquired, <see langword="false"/> otherwise.</returns>
    internal bool TryAcquireForHeartbeat(Server server, out Connection? connection)
    {
        connection = null;

        var node = Find(server.Host);
        if (node is null)
        {
            return false;
        }

        return node.TryAcquire(out connection);

        Node? Find(in Host host)
        {
            lock (_nodes)
            {
                foreach (var node in _nodes)
                {
                    if (node.Server.Host == host)
                    {
                        return node;
                    }
                }

                return null;
            }
        }
    }

    /// <summary>
    /// Tests all connections in the queue and cleans up disconnected connections.
    /// </summary>
    internal async ValueTask TestAndCleanupAsync()
    {
        foreach (var node in this)
        {
            await ((Node)node).TestAndCleanupAsync();
        }
    }

    #region INodeCollection

    /// <summary>
    /// Returns the zero-based index of the specified host.
    /// </summary>
    /// <param name="host">The host of the node.</param>
    /// <returns>The zero-based index of the node, -1 if not found.</returns>
    private int IndexOf(in Host host)
    {
        for (var i = 0; i < _nodes.Count; i++)
        {
            if (_nodes[i].Server.Host == host)
            {
                return i;
            }
        }

        return -1;
    }

    /// <inheritdoc/>
    int INodeCollection.Count => _nodes.Count;

    /// <inheritdoc/>
    INode INodeCollection.this[int index] => _nodes[index];

    /// <inheritdoc/>
    INode? INodeCollection.this[in Host host]
    {
        get
        {
            lock (_nodes)
            {
                var index = IndexOf(host);
                return index == -1 ? null : _nodes[index];
            }
        }
    }

    /// <inheritdoc/>
    bool INodeCollection.Contains(in Host host) => IndexOf(host) != -1;

    /// <inheritdoc/>
    INode INodeCollection.Add(in Host host)
    {
        lock (_nodes)
        {
            var index = IndexOf(host);
            if (index != -1)
            {
                return _nodes[index];
            }

            var server = Manager.Shared.EnsureServer(host);
            var node = new Node(server, this);
            _nodes.Add(node);
            server.EnsureDatabase(this);
            return node;
        }
    }

    /// <inheritdoc/>
    bool INodeCollection.Remove(in Host host)
    {
        Node node;

        lock (_nodes)
        {
            var index = IndexOf(host);
            if (index == -1)
            {
                return false;
            }

            node = _nodes[index];
            _nodes.RemoveAt(index);

            if (_nextNodeIndex >= _nodes.Count)
            {
                _nextNodeIndex = _nodes.Count - 1;
            }
        }

        node.Server.RemoveDatabase(this);
        node.Dispose();

        return true;
    }

    /// <inheritdoc/>
    void INodeCollection.Clear()
    {
        Server[] servers;

        lock (_nodes)
        {
            if (_nodes.Count == 0)
            {
                return;
            }

            servers = new Server[_nodes.Count];

            _nextNodeIndex = 0;
            for (var i = 0; i < _nodes.Count; i++)
            {
                servers[i] = _nodes[i].Server;
                _nodes[i].Dispose();
            }

            _nodes.Clear();
        }

        foreach (var server in servers)
        {
            server.RemoveDatabase(this);
        }
    }

    /// <inheritdoc/>
    IEnumerator<INode> IEnumerable<INode>.GetEnumerator() => _nodes.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

    #endregion

    #region Dispatcher

    /// <inheritdoc/>
    internal override int IdleConnectionCount
    {
        get
        {
            lock (_nodes)
            {
                if (_nodes.Count == 1)
                {
                    return _nodes[0].IdleConnectionCount;
                }

                var count = 0;
                foreach (var node in _nodes)
                {
                    count += node.IdleConnectionCount;
                }

                return count;
            }
        }
    }

    /// <summary>
    /// Gets the name of the database.
    /// </summary>
    /// <value>Comparisons of this value are case-sensitive.</value>
    internal override string DatabaseName => Name;

    /// <inheritdoc/>
    internal override ValueTask<Connection> AcquireAsync(
        ICommand command,
        CancellationToken cancellationToken = default)
    {
        return TryAcquire(out var connection)
            ? new(connection!)
            : LongAcquireAsync(command, cancellationToken);

        async ValueTask<Connection> LongAcquireAsync(
            ICommand command,
            CancellationToken cancellationToken)
        {
            Node node;

            lock (_nodes)
            {
                node = _nodes[_nextNodeIndex];
            }

            var connection = await node.AcquireAsync(command, cancellationToken);

            lock (_nodes)
            {
                _nextNodeIndex = _nextNodeIndex >= _nodes.Count - 1 ? 0 : _nextNodeIndex + 1;
            }

            return connection;
        }
    }

    /// <inheritdoc/>
    internal override int PruneConnections(int countPruning)
    {
        lock (_nodes)
        {
            return PruneConnections(_nodes, countPruning);
        }
    }

    /// <summary>
    /// Closes all connections to this database, clears the command queue.
    /// </summary>
    public override void Dispose()
    {
        ((INodeCollection)this).Clear();
        base.Dispose();
    }

    #endregion

    #region IDatabase

    /// <inheritdoc/>
    public Guid Id { get; }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public string UserName { get; }

    /// <inheritdoc/>
    public INodeCollection Nodes => this;

    /// <inheritdoc/>
    public int MaxTextLengthOfCachedExtendedQuery
    {
        get => ExtendedQueryCache.MaxCommandTextLength;
        set => ExtendedQueryCache.MaxCommandTextLength = value;
    }

    /// <inheritdoc/>
    IDatabase IDatabase.Use(string databaseName, Guid? id)
    {
        if (string.IsNullOrEmpty(databaseName))
        {
            throw new ArgumentException(Strings.DatabaseNameIsEmpty, nameof(databaseName));
        }

        if (databaseName == Name)
        {
            return this;
        }

        return Manager.Shared.EnsureDatabase(
            Nodes.Select(node => (Server)node.Server).ToArray(),
            databaseName,
            UserName,
            Password,
            id);
    }

    #endregion

    #region IConnectionPoolOptions

    /// <summary>
    /// The default time span to wait before an operation times out, 15 seconds.
    /// </summary>
    internal const int DefaultTimeoutSeconds = 15;

    /// <summary>
    /// The minimum time span to wait before an operation times out, 1 seconds.
    /// </summary>
    internal const int MinTimeoutSeconds = 1;

    /// <summary>
    /// The maximum time span to wait before an operation times out, 900 seconds.
    /// </summary>
    internal const int MaxTimeoutSeconds = 900;

    /// <inheritdoc/>
    public int ConnectTimeoutSeconds
    {
        get => _connectTimeoutSeconds;
        set => _connectTimeoutSeconds = value < MinTimeoutSeconds
            ? MinTimeoutSeconds
            : (value > MaxTimeoutSeconds ? MaxTimeoutSeconds : value);
    }

    /// <inheritdoc/>
    public int ReceiveTimeoutSeconds
    {
        get => _receiveTimeoutSeconds;
        set => _receiveTimeoutSeconds = value < MinTimeoutSeconds
            ? MinTimeoutSeconds
            : (value > MaxTimeoutSeconds ? MaxTimeoutSeconds : value);
    }

    /// <inheritdoc/>
    public int SendTimeoutSeconds
    {
        get => _sendTimeoutSeconds;
        set => _sendTimeoutSeconds = value < MinTimeoutSeconds
            ? MinTimeoutSeconds
            : (value > MaxTimeoutSeconds ? MaxTimeoutSeconds : value);
    }

    /// <inheritdoc/>
    public int WaitTimeoutSeconds
    {
        get => _waitTimeoutSeconds;
        set => _waitTimeoutSeconds = value < MinTimeoutSeconds
            ? MinTimeoutSeconds
            : (value > MaxTimeoutSeconds ? MaxTimeoutSeconds : value);
    }

    #endregion

    #region IConnectionPool

    /// <inheritdoc/>
    IConnectionPoolOptions IConnectionPool.Options => this;

    /// <inheritdoc/>
    async ValueTask<IConnection> IConnectionPool.AcquireAsync(CancellationToken cancellationToken)
    {

        return TryAcquire(out var connection)
            ? connection!
            : await LongAcquireAsync(cancellationToken);

        async ValueTask<IConnection> LongAcquireAsync(CancellationToken cancellationToken)
        {
            Node node;

            lock (_nodes)
            {
                node = _nodes[_nextNodeIndex];
            }

            var connection = await ((INode)node).AcquireAsync(cancellationToken);

            lock (_nodes)
            {
                _nextNodeIndex = _nextNodeIndex >= _nodes.Count - 1 ? 0 : _nextNodeIndex + 1;
            }

            return connection;
        }
    }

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
