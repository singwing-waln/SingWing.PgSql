namespace SingWing.PgSql;

/// <summary>
/// Represents a PostgreSQL node that hosts a database.
/// </summary>
public interface INode : ITransactional, IConnectionPool
{
    /// <summary>
    /// Gets the server of this node.
    /// </summary>
    IServer Server { get; }

    /// <summary>
    /// Gets the database hosted by this node.
    /// </summary>
    IDatabase Database { get; }
}
