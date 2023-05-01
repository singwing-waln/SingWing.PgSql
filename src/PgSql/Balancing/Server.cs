using SingWing.PgSql.Protocol.Connections;
using System.Collections.Concurrent;
using System.Globalization;

namespace SingWing.PgSql.Balancing;

/// <summary>
/// Represents a database server.
/// </summary>
/// <remarks>
/// <para>
/// PostgreSQL's architecture is process-based instead of thread-based, so the maximum number of connections may be very small.
/// <see cref="Server"/> provides unified management of all connections to a single server and balances connections across different databases.
/// </para>
/// <para>
/// </para>
/// </remarks>
internal sealed class Server : IServer, IDisposable
{
    #region Fields

    /// <summary>
    /// All databases running on this server.
    /// </summary>
    private readonly ConcurrentDictionary<Guid, Database> _databases;

    /// <summary>
    /// The timer for periodically querying the maximum number of connections.
    /// It is also a heartbeat timer to test whether the server is still running.
    /// </summary>
    private readonly Timer _timer;

    /// <summary>
    /// Indicates whether the timer has started.
    /// </summary>
    private bool _timerStarted;

    /// <summary>
    /// The interval for the heartbeat timer, in seconds.
    /// </summary>
    private int _heartbeatIntervalSeconds;

    /// <summary>
    /// Indicates whether a heartbeat operation is in progress.
    /// </summary>
    private int _heartbeating;

    /// <summary>
    /// The proportion of the maximum number of connections that can be established by the current client to this server.
    /// </summary>
    private double _connectionsProportion;

    /// <summary>
    /// The max_connections of the back-end server.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see href="https://www.postgresql.org/docs/current/runtime-config-connection.html"/>.
    /// </para>
    /// </remarks>
    private int _backendMaxConnections;

    /// <summary>
    /// The maximum number of connections that can be made by the current client to this server.
    /// </summary>
    private int _maxConnectionCount;

    /// <summary>
    /// The number of connections that have been established so far.
    /// </summary>
    private int _connectionCount;

    /// <summary>
    /// Indicates whether some connections are being closed.
    /// </summary>
    private int _pruning;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="Server"/> class
    /// with the specified <see cref="PgSql.Host"/> and the connections proportion.
    /// </summary>
    /// <param name="host">The server host information.</param>
    /// <param name="proportion">The proportion of the maximum number of connections that can be established by the current client to the host.</param>
    internal Server(Host host, double proportion)
    {
        // In seconds.
        const int DefaultHeartbeatIntervalSeconds = 60;

        _databases = new();
        // We do not need to start the timer until the first connection to the server is established.
        _timer = new(Heartbeat, this, Timeout.Infinite, Timeout.Infinite);
        _timerStarted = false;
        _heartbeatIntervalSeconds = DefaultHeartbeatIntervalSeconds;
        _heartbeating = 0;
        _backendMaxConnections = -1;
        // -1 means that this server has not yet queried the maximum connections parameters from the backend.
        _maxConnectionCount = -1;
        _connectionCount = 0;
        _pruning = 0;

        Host = host;
        ConnectionsProportion = proportion;
    }

    /// <inheritdoc/>
    public Host Host { get; }

    /// <inheritdoc/>
    public double ConnectionsProportion
    {
        get => _connectionsProportion;
        set
        {
            _connectionsProportion = value <= 0d || value > 1d ? 1d : value;

            if (_backendMaxConnections == -1)
            {
                return;
            }

            _maxConnectionCount = Math.Max((int)Math.Round(_backendMaxConnections * _connectionsProportion), 1);
        }
    }

    /// <inheritdoc/>
    public int MaxConnectionCount => _maxConnectionCount;

    /// <inheritdoc/>
    public int HeartbeatIntervalSeconds
    {
        get => _heartbeatIntervalSeconds;
        set
        {
            const int MinIntervalSeconds = 1;
            const int MaxIntervalSeconds = 24 * 60 * 60;

            var seconds = value < MinIntervalSeconds
                ? MinIntervalSeconds
                : (value > MaxIntervalSeconds ? MaxIntervalSeconds : value);

            lock (_timer)
            {
                if (seconds == _heartbeatIntervalSeconds)
                {
                    return;
                }

                _heartbeatIntervalSeconds = seconds;

                if (_timerStarted)
                {
                    var ms = seconds * 1000;
                    _timer.Change(ms, ms);
                }
            }
        }
    }

    /// <summary>
    /// Stops this server from running.
    /// </summary>
    public void Dispose()
    {
        // Stop the heartbeat timer.
        _timer.Dispose();
        _timerStarted = false;
        // Databases will be disposed by Manager, so only the collection is cleared here.
        _databases.Clear();
    }

    /// <summary>
    /// Adds a database that is hosted by this server.
    /// If the data already exists, does nothing.
    /// </summary>
    /// <param name="database">The database to add.</param>
    internal void EnsureDatabase(Database database) => _ = _databases.TryAdd(database.Id, database);

    /// <summary>
    /// Removes a database.
    /// </summary>
    /// <param name="database">The database to remove.</param>
    internal void RemoveDatabase(Database database)
    {
        lock (_databases)
        {
            _ = _databases.Remove(database.Id, out _);

            if (_databases.IsEmpty)
            {
                Manager.Shared.RemoveServer(this);
            }
        }
    }

    /// <summary>
    /// Initializes the maximum number of connections using the specified connection.
    /// </summary>
    /// <param name="connection">The connection to execute the query command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask InitializeMaxConnectionsAsync(
        Connection connection,
        CancellationToken cancellationToken)
    {
        if (Interlocked.CompareExchange(ref _maxConnectionCount, 0, -1) != -1)
        {
            // Initialization started or completed, do nothing.
            return;
        }

        try
        {
            _backendMaxConnections = await QueryMaxConnectionsAsync(connection, cancellationToken);
        }
        catch
        {
            // If initialization fails, revert to -1 so that we have a chance to initialize again.
            Interlocked.Exchange(ref _maxConnectionCount, -1);
            throw;
        }

        _maxConnectionCount = Math.Max((int)Math.Round(_backendMaxConnections * _connectionsProportion), 1);

        StartTimer();

        async ValueTask<int> QueryMaxConnectionsAsync(
            Connection connection,
            CancellationToken cancellationToken)
        {
            // For max_connections, see https://www.postgresql.org/docs/current/runtime-config-connection.html.
            const int DefaultMaxConnections = 100;
            // To reduce one database query, always assume that superuser_reserved_connections is 5.
            // The default value for superuser_reserved_connections is 3.
            const int DefaultSuperuserReservedConnections = 5;
            const string MaxConnectionsCommandText = "SHOW max_connections";
            const string ReservedConnectionsCommandText = "SHOW superuser_reserved_connections";

            var max = await QueryConnectionsAsync(
                connection,
                MaxConnectionsCommandText,
                DefaultMaxConnections,
                cancellationToken);

            var reserved = await QueryConnectionsAsync(
                connection,
                ReservedConnectionsCommandText,
                DefaultSuperuserReservedConnections,
                cancellationToken);

            return Math.Max(max - reserved, 1);

            static async ValueTask<int> QueryConnectionsAsync(
                Connection connection,
                string commandText,
                int defaultValue,
                CancellationToken cancellationToken)
            {
                var count = 0;
                var rows = await connection.QueryAsync(
                    commandText.AsMemory(),
                    cancellationToken);

                await foreach (var row in rows)
                {
                    await foreach (var col in row)
                    {
                        count = await col.ToInt32Async(cancellationToken);
                    }
                }

                return count < 1 ? defaultValue : count;
            }
        }

        void StartTimer()
        {
            lock (_timer)
            {
                var ms = _heartbeatIntervalSeconds * 1000;
                _timer.Change(ms, ms);
                _timerStarted = true;
            }
        }
    }

    /// <summary>
    /// Increases the connection count.
    /// </summary>
    /// <returns><see langword="true"/> if a new connection can be established, <see langword="false"/> otherwise.</returns>
    internal bool IncreaseConnectionCount()
    {
        if (_maxConnectionCount <= 0)
        {
            Interlocked.Increment(ref _connectionCount);
            return true;
        }

        if (Interlocked.Increment(ref _connectionCount) > _maxConnectionCount)
        {
            Interlocked.Decrement(ref _connectionCount);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Decreases the connection count.
    /// </summary>
    internal void DecreaseConnectionCount() =>
        Interlocked.Decrement(ref _connectionCount);

    /// <summary>
    /// Urges this server to provide connections.
    /// </summary>
    /// <returns><see langword="true"/> if the server closes at least one connection, <see langword="false"/> otherwise.</returns>
    internal bool UrgeConnections()
    {
        var countPruning = _connectionCount - _maxConnectionCount;
        if (countPruning < 1)
        {
            countPruning = 1;
        }

        return PruneConnections(countPruning) > 0;

        int PruneConnections(int countPruning)
        {
            if (Interlocked.CompareExchange(ref _pruning, 1, 0) == 1)
            {
                return 0;
            }

            try
            {
                return Dispatcher.PruneConnections(_databases.Values, countPruning);
            }
            finally
            {
                Interlocked.Exchange(ref _pruning, 0);
            }
        }
    }

    /// <summary>
    /// Logs a too_many_connections warning for this server.
    /// </summary>
    internal void LogTooManyConnections()
    {
        Db.Logger.LogWarning(string.Format(
            CultureInfo.CurrentCulture,
            Strings.TooManyConnectionsWarning,
            Host,
            _connectionCount,
            _backendMaxConnections));
    }

    /// <summary>
    /// Maintains connections to the server by sending a Sync message.
    /// </summary>
    /// <param name="state">The <see cref="Server"/> instance.</param>
    private static void Heartbeat(object? state)
    {
        var server = (Server)state!;

        if (Interlocked.CompareExchange(ref server._heartbeating, 1, 0) == 1)
        {
            // A heartbeat has been initiated before.
            return;
        }

        Connection? connection = null;

        try
        {
            foreach (var database in server._databases.Values)
            {
                if (database.TryAcquireForHeartbeat(server, out connection))
                {
                    break;
                }
            }
        }
        catch
        {
            Interlocked.Exchange(ref server._heartbeating, 0);
            throw;
        }

        // No connection available for querying max_connections.
        if (connection is null)
        {
            Interlocked.Exchange(ref server._heartbeating, 0);
            return;
        }

        _ = Task
            .Run(async () =>
            {
                try
                {
                    await connection.SyncAsync();
                }
                catch (Exception exc)
                {
                    Db.Logger.LogTrace(
                        string.Format(CultureInfo.CurrentCulture, Strings.AbnormalHeartbeatDetected, server.Host),
                        exc);

                    // An abnormal heartbeat may mean that the server is no longer running, explicitly taking a connection test.
                    // If the server is done, test all connections to that server.
                    if (!await connection.TestAsync())
                    {
                        connection.Break(exc);

                        foreach (var database in server._databases.Values)
                        {
                            await database.TestAndCleanupAsync();
                        }
                    }
                }
                finally
                {
                    connection.Release();
                    Interlocked.Exchange(ref server._heartbeating, 0);
                }
            });
    }
}
