namespace SingWing.PgSql;

/// <summary>
/// Represents a PostgreSQL database.
/// </summary>
/// <remarks>
/// <para>
/// A database may be distributed, which means that it may run in one or more distributed nodes.
/// </para>
/// </remarks>
public interface IDatabase : ITransactional, IConnectionPool
{
    /// <summary>
    /// Gets the unique ID for this database.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the name of the database.
    /// </summary>
    /// <value>Comparisons of this value are case-sensitive.</value>
    string Name { get; }

    /// <summary>
    /// Gets the name of the user connecting to the database.
    /// </summary>
    /// <value>Comparisons of this value are case-sensitive.</value>
    string UserName { get; }

    /// <summary>
    /// Gets the collection of all nodes that host the database.
    /// </summary>
    INodeCollection Nodes { get; }

    /// <summary>
    /// Gets or sets the maximum text length of a extended query command that can be cached.
    /// </summary>
    /// <value>The default value is 256. Set to 0 or less than 0 to disable caching.</value>
    int MaxTextLengthOfCachedExtendedQuery { get; set; }

    /// <summary>
    /// Returns another database with the specified name based on the user authentication information for the current database.
    /// </summary>
    /// <param name="databaseName">The name of another database.</param>
    /// <param name="id"></param>
    /// <returns>An <see cref="IDatabase"/> instance that uses the same user authentication information as the current database.</returns>
    /// <exception cref="ArgumentException"><paramref name="databaseName"/> is empty.</exception>
    /// <remarks>
    /// If the <paramref name="databaseName"/> is the same as the current <see cref="Name"/>, 
    /// the current <see cref="IDatabase"/> instance is returned. In this case, the <paramref name="id"/> is ignored.
    /// </remarks>
    IDatabase Use(string databaseName, Guid? id = null);
}
