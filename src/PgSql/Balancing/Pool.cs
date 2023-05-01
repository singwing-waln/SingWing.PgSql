using SingWing.PgSql.Pooling.Commands;
using SingWing.PgSql.Protocol.Connections;
using System.Collections.Concurrent;
using System.Globalization;
using System.Threading.Channels;

namespace SingWing.PgSql.Balancing;

/// <summary>
/// Represents a pool of connections to a single node.
/// </summary>
internal sealed class Pool : IDisposable
{
    /// <summary>
    /// The node to which this connection pool belongs.
    /// </summary>
    private readonly Node _node;

    /// <summary>
    /// The list of all connections, including idle and active connections.
    /// </summary>
    private readonly ConcurrentDictionary<ulong, Connection> _allConnections;

    /// <summary>
    /// The queue of idle connections.
    /// </summary>
    private readonly IdleConnectionQueue _idleConnections;

    /// <summary>
    /// Initializes a new instance of the <see cref="Pool"/> class
    /// with the specified node.
    /// </summary>
    /// <param name="node">The node to which the new connection pool belongs.</param>
    internal Pool(Node node)
    {
        _node = node;
        _allConnections = new();
        _idleConnections = new(node);
    }

    /// <summary>
    /// Gets the number of idle connections in the pool.
    /// </summary>
    internal int IdleConnectionCount => _idleConnections.Count;

    /// <summary>
    /// Attempts to get an idle connection and returns immediately if no connection is available.
    /// </summary>
    /// <param name="connection">The acquired connection.</param>
    /// <returns><see langword="true"/> if a connection was acquired, <see langword="false"/> otherwise.</returns>
    internal bool TryAcquire(out Connection? connection) => _idleConnections.TryDequeue(out connection);

    /// <summary>
    /// Gets an idle connection for executing the specified command from this connection pool,
    /// opens a new connection if necessary.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The acquired connection.</returns>
    internal async ValueTask<Connection> AcquireAsync(ICommand? command, CancellationToken cancellationToken)
    {
        // Get an idle connection and do not wait when no idle connection is available.
        _ = _idleConnections.TryDequeue(out var connection);
        if (connection is not null)
        {
            return connection;
        }

        // Try to open a new connection.
        connection = await OpenConnectionAsync(cancellationToken);
        if (connection is not null)
        {
            if (_allConnections.TryAdd(connection.Id, connection))
            {
                return connection;
            }

            connection.Close();
        }

        lock (_allConnections)
        {
            if (_allConnections.IsEmpty)
            {
                // No connections will be returned.
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Strings.DatabaseTooBusy, _node.DatabaseName));
            }

            if (command is TransactionCommand or QueryCommand)
            {
                // Try again to get an existing idle connection.
                _ = _idleConnections.TryDequeue(out connection);
                if (connection is not null)
                {
                    return connection;
                }

                if (AllAreActive())
                {
                    // If all connections in the pool are active,
                    // attempting to start a command that occupies a connection can lead to a deadlock-like state
                    // because no connections are returned until the started commands have ended,
                    // and the new command may prevent the end of those commands, for example, starting a new transaction..
                    throw new InvalidOperationException(
                        string.Format(CultureInfo.CurrentCulture, Strings.DatabaseTooBusy, _node.DatabaseName));
                }
            }
        }

        // Get an idle connection from the connection queue again,
        // and wait for a connection to be released.
        return await _idleConnections.DequeueAsync(cancellationToken);

        async ValueTask<Connection?> OpenConnectionAsync(CancellationToken cancellationToken)
        {
            if (_node.Server.IncreaseConnectionCount())
            {
                // We were allowed to open more connections.
                return await OpenNewAsync(cancellationToken);
            }

            if (!_allConnections.IsEmpty)
            {
                // Wait for a connection to be returned.
                return null;
            }

            // Urge the server to provide at least one connection.
            if (_node.Server.UrgeConnections() && _node.Server.IncreaseConnectionCount())
            {
                return await OpenNewAsync(cancellationToken);
            }

            return null;

            async ValueTask<Connection?> OpenNewAsync(CancellationToken cancellationToken)
            {
                Connection? connection;

                try
                {
                    connection = await Connection.OpenAsync(_node, cancellationToken);
                }
                catch (Exception exc)
                {
                    _node.Server.DecreaseConnectionCount();

                    if (IsTooManyConnectionsError(exc))
                    {
                        _node.Server.LogTooManyConnections();
                        // Return null to wait for a connection to be released.
                        return null;
                    }

                    throw;
                }

                // If the new connection is the first connection to the destination server,
                // use that connection to initialize the maximum number of connections for that server.
                try
                {
                    await _node.Server.InitializeMaxConnectionsAsync(connection, cancellationToken);
                }
                catch
                {
                    connection.Close();
                    throw;
                }

                if (connection.IsBroken)
                {
                    connection.Close();
                    return null;
                }

                return connection;

                static bool IsTooManyConnectionsError(Exception exception)
                {
                    // too_many_connections:
                    // https://www.postgresql.org/docs/current/errcodes-appendix.html
                    const string TooManyConnectionsSqlState = "53300";

                    if (exception is ServerException serverException1 &&
                        serverException1.SqlState == TooManyConnectionsSqlState)
                    {
                        return true;
                    }

                    if (exception is OpeningFailedException openingFailedException &&
                        openingFailedException.InnerException is ServerException serverException2 &&
                        serverException2.SqlState == TooManyConnectionsSqlState)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        bool AllAreActive()
        {
            foreach (var connection in _allConnections.Values)
            {
                // The connection may be returning to the pool or has been returned to the pool.
                if (connection.IsReleased)
                {
                    return false;
                }
            }

            return true;
        }
    }

    /// <summary>
    /// Returns the specified connection to this connection pool.
    /// </summary>
    /// <param name="connection">The connection to release.</param>
    /// <remarks>
    /// If the connection has been broken, or if the return failed, the connection is closed.
    /// </remarks>
    internal void Release(Connection connection)
    {
        if (connection.IsBroken)
        {
            connection.Close();
            return;
        }

        if (!_allConnections.ContainsKey(connection.Id) || 
            !_idleConnections.TryEnqueue(connection))
        {
            connection.Close();
        }
    }

    /// <summary>
    /// Removes a connection from the pool. This method is only called in Node.Remove.
    /// </summary>
    /// <param name="connectionId">The Id of the connection to remove.</param>
    internal void Remove(ulong connectionId)
    {
        _node.Server.DecreaseConnectionCount();
        _ = _allConnections.Remove(connectionId, out _);
    }

    /// <summary>
    /// Tests all connections in the queue and cleans up disconnected connections.
    /// </summary>
    internal ValueTask TestAndCleanupAsync() => _idleConnections.TestAndCleanupAsync();

    /// <summary>
    /// Prunes the specified number of connections.
    /// </summary>
    /// <param name="countPruning">The number of connections to prune.</param>
    /// <returns>The number of connections that were actually pruned.</returns>
    internal int Prune(int countPruning) => _idleConnections.Prune(countPruning);

    /// <summary>
    /// Closes all connections.
    /// </summary>
    public void Dispose()
    {
        lock (_allConnections)
        {
            _idleConnections.Dispose();

            foreach (var connection in _allConnections.Values)
            {
                connection.Close();
            }
        }
    }

    #region IdleConnectionQueue

    /// <summary>
    /// Represents the queue of idle connections connected to a node.
    /// </summary>
    private sealed class IdleConnectionQueue : IDisposable
    {
        /// <summary>
        /// The options for the current connection pool.
        /// </summary>
        private readonly IConnectionPoolOptions _options;

        /// <summary>
        /// The reader that read commands from the queue.
        /// </summary>
        private readonly ChannelReader<Connection> _reader;

        /// <summary>
        /// The writer that write commands to the queue.
        /// </summary>
        private readonly ChannelWriter<Connection> _writer;

        /// <summary>
        /// When <see cref="Dispose"/>, cancel reads and writes.
        /// </summary>
        private readonly CancellationTokenSource _dispositionTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdleConnectionQueue"/> class
        /// with the specified connection pool options.
        /// </summary>
        internal IdleConnectionQueue(IConnectionPoolOptions options)
        {
            _options = options;
            var channel = Channel.CreateUnbounded<Connection>();
            _reader = channel.Reader;
            _writer = channel.Writer;
            _dispositionTokenSource = new();
        }

        /// <summary>
        /// Gets the number of idle connections.
        /// </summary>
        internal int Count => _reader.Count;

        /// <summary>
        /// Attempts to return an idle connection to the queue.
        /// </summary>
        /// <param name="connection">The connection to return.</param>
        /// <returns><see langword="true"/> if the connection was enqueued, <see langword="false"/> otherwise.</returns>
        internal bool TryEnqueue(Connection connection) => _writer.TryWrite(connection);

        /// <summary>
        /// Attempts to get an idle connection, and immediately returns <see langword="false"/> if no connection is available.
        /// </summary>
        /// <param name="connection">The dequeued connection.</param>
        /// <returns><see langword="true"/> if a connection was dequeued, <see langword="false"/> otherwise.</returns>
        internal bool TryDequeue(out Connection? connection)
        {
            if (_reader.TryRead(out connection))
            {
                connection.Activate();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets an idle connection from the queue. If no connection is available, wait indefinitely.
        /// </summary>
        /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The dequeued connection.</returns>
        internal async ValueTask<Connection> DequeueAsync(CancellationToken cancellationToken)
        {
            using var source = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken, _dispositionTokenSource.Token);

            source.CancelAfter(_options.WaitTimeoutSeconds * 1000);

            Connection connection;

            try
            {
                connection = await _reader.ReadAsync(source.Token);
            }
            catch (OperationCanceledException exc)
            {
                if (exc.CancellationToken == cancellationToken)
                {
                    // Cancellation was initiated externally or by disposition.
                    throw;
                }
                else
                {
                    // Time out.
                    throw new TimeoutException(Strings.OperationTimeout, exc);
                }
            }

            connection.Activate();
            return connection;
        }

        /// <summary>
        /// Tests all connections in the queue and cleans up disconnected connections.
        /// </summary>
        internal async ValueTask TestAndCleanupAsync()
        {
            while (_reader.TryRead(out var connection))
            {
                if (!await connection.TestAsync() || !_writer.TryWrite(connection))
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Prunes the specified number of connections.
        /// </summary>
        /// <param name="countPruning">The number of connections to prune.</param>
        /// <returns>The number of connections that were actually pruned.</returns>
        internal int Prune(int countPruning)
        {
            if (countPruning < 1)
            {
                return 0;
            }

            var countPruned = 0;
            while (countPruning > 0)
            {
                if (_reader.TryRead(out var connection))
                {
                    connection.Close();
                    if (++countPruned == countPruning)
                    {
                        break;
                    }
                }
            }

            return countPruned;
        }

        /// <summary>
        /// Clears the queue without closing the connections.
        /// </summary>
        public void Dispose()
        {
            lock (_dispositionTokenSource)
            {
                if (_dispositionTokenSource.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    _dispositionTokenSource.Cancel();
                }
                catch
                {
                }

                _dispositionTokenSource.Dispose();
            }

            try
            {
                // Mark the channel as complete.
                _ = _writer.TryComplete();

                // Clear the queue, connections are closed from Pool._allConnections.
                while (_reader.TryRead(out _)) { }
            }
            catch
            {
            }
        }
    }

    #endregion
}
