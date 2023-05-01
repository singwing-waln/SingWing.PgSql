namespace SingWing.PgSql;

/// <summary>
/// Represents a connection to a database running on a PostgreSQL node.
/// </summary>
public interface IConnection : ITransactional, IAsyncDisposable
{
    /// <summary>
    /// Gets the database to which this connection is connected.
    /// </summary>
    IDatabase Database { get; }

    /// <summary>
    /// Gets the node to which this connection is connected.
    /// </summary>
    INode Node { get; }
}
