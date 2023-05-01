using System.Runtime.Loader;

namespace SingWing.PgSql.Balancing;

/// <summary>
/// Represents the manager of <see cref="Server"/>s and <see cref="Database"/>s.
/// </summary>
internal sealed class Manager : IDisposable
{
    /// <summary>
    /// The shared singleton manager.
    /// </summary>
    internal static readonly Manager Shared;

    static Manager()
    {
        Shared = new();

        // When the main application assembly is unloaded,
        // closes all connections and disposes all servers.
        AssemblyLoadContext.Default.Unloading += Application_Unloading;

        static void Application_Unloading(AssemblyLoadContext context)
        {
            (Shared as IDisposable).Dispose();
        }
    }

    /// <summary>
    /// All managed servers.
    /// </summary>
    private readonly Dictionary<Host, Server> _servers;

    /// <summary>
    /// All managed databases.
    /// </summary>
    private readonly Dictionary<Guid, Database> _databases;

    /// <summary>
    /// Initializes a new instance of the <see cref="Manager"/> class.
    /// </summary>
    private Manager()
    {
        _servers = new();
        _databases = new();
    }

    /// <summary>
    /// Finds the database for the specified ID.
    /// </summary>
    /// <param name="id">The unique database ID.</param>
    /// <returns>The found <see cref="Database"/> instance, <see langword="null"/> if not found.</returns>
    internal Database? FindDatabase(Guid id)
    {
        lock (_databases)
        {
            _ = _databases.TryGetValue(id, out var database);
            return database;
        }
    }

    /// <summary>
    /// Finds the specified database and, if necessary, creates a new instance of <see cref="Database"/>.
    /// </summary>
    /// <param name="hosts">A list of hosts that host the database.</param>
    /// <param name="databaseName">The database name.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="password">The user password.</param>
    /// <param name="id">The unique database ID provided by the caller.</param>
    /// <returns>The found or created <see cref="Database"/> instance.</returns>
    /// <remarks>
    /// If the database already exists and there are more new hosts than the old host list, 
    /// add the new hosts to that database.
    /// </remarks>
    internal Database EnsureDatabase(
        IList<(Host, double)> hosts,
        string databaseName,
        string userName,
        string password,
        Guid? id)
    {
        var servers = new Server[hosts.Count];

        lock (_servers)
        {
            for (var i = 0; i < hosts.Count; i++)
            {
                servers[i] = TryAddServer(hosts[i].Item1, hosts[i].Item2);
            }
        }

        return EnsureDatabase(servers, databaseName, userName, password, id);
    }

    /// <summary>
    /// Finds the specified database and, if necessary, creates a new instance of <see cref="Database"/>.
    /// </summary>
    /// <param name="servers">A list of servers that host the database.</param>
    /// <param name="databaseName">The database name.</param>
    /// <param name="userName">The user name.</param>
    /// <param name="password">The user password.</param>
    /// <param name="id">The unique database ID provided by the caller.</param>
    /// <returns>The found or created <see cref="Database"/> instance.</returns>
    /// <remarks>
    /// If the database already exists and there are more new servers than the old server list, 
    /// add the new hosts to that database.
    /// </remarks>
    internal Database EnsureDatabase(
        Server[] servers,
        string databaseName,
        string userName,
        string password,
        Guid? id)
    {
        Debug.Assert(servers.Length > 0);

        id ??= GenerateId(databaseName, userName);

        lock (_databases)
        {
            _ = _databases.TryGetValue(id.Value, out var database);

            if (database is null)
            {
                database = new Database(servers, databaseName, userName, password, id.Value);
                _databases.Add(id.Value, database);
            }
            else
            {
                database.UpdateNodes(servers);
            }

            foreach (var server in servers)
            {
                server.EnsureDatabase(database);
            }

            return database;
        }

        static Guid GenerateId(string databaseName, string userName)
        {
            Span<byte> bytes = stackalloc byte[16];
            bytes.Clear();
            Unsafe.WriteUnaligned(ref bytes[8], databaseName.GetHashCode());
            Unsafe.WriteUnaligned(ref bytes[12], userName.GetHashCode());
            return new Guid(bytes);
        }
    }

    /// <summary>
    /// Attempts to add a server of the specified host.
    /// </summary>
    /// <param name="host">The host of the server.</param>
    /// <returns>The server of the host.</returns>
    internal Server EnsureServer(in Host host)
    {
        lock (_servers)
        {
            return TryAddServer(host, proportion: 1d);
        }
    }

    /// <summary>
    /// Removes the specified server.
    /// </summary>
    /// <param name="server">The server to remove.</param>
    internal void RemoveServer(Server server)
    {
        lock (_servers)
        {
            if (_servers.Remove(server.Host))
            {
                server.Dispose();
            }
        }
    }

    /// <summary>
    /// Attempts to add a server of the specified host.
    /// </summary>
    /// <param name="host">The host of the server.</param>
    /// <param name="proportion">The proportion of the maximum number of connections that can be established by the current client to the host.</param>
    /// <returns>The server of the host.</returns>
    private Server TryAddServer(in Host host, double proportion)
    {
        if (_servers.TryGetValue(host, out var server))
        {
            return server;
        }

        var newServer = new Server(host, proportion);
        _servers.Add(host, newServer);
        return newServer;
    }

    /// <summary>
    /// Closes all connections.
    /// </summary>
    void IDisposable.Dispose()
    {
        lock (_servers)
        {
            foreach (var server in _servers.Values)
            {
                server.Dispose();
            }
        }

        lock (_databases)
        {
            foreach (var database in _databases.Values)
            {
                database.Dispose();
            }
        }
    }
}
