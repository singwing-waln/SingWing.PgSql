namespace SingWing.PgSql;

/// <summary>
/// Represents a collection of nodes that host a database.
/// </summary>
/// <remarks>
/// <see cref="INodeCollection"/> makes it possible to dynamically manage the nodes of a database.
/// </remarks>
public interface INodeCollection : IEnumerable<INode>
{
    /// <summary>
    /// Gets the number of nodes that host the current database.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets the node of the specified position.
    /// </summary>
    /// <param name="index">The 0-based position of the node.</param>
    /// <returns>The found <see cref="INode"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0, greater than or equal to <see cref="Count"/>.</exception>
    INode this[int index] { get; }

    /// <summary>
    /// Gets the node of the specified host.
    /// </summary>
    /// <param name="host">The host of the node.</param>
    /// <returns>The found <see cref="INode"/>, <see langword="null"/> if not found.</returns>
    INode? this[in Host host] { get; }

    /// <summary>
    /// Determines whether the collection contains a specific node.
    /// </summary>
    /// <param name="host">The host of the node.</param>
    /// <returns><see langword="true"/> if the node is found, <see langword="false"/> otherwise.</returns>
    bool Contains(in Host host);

    /// <summary>
    /// Adds the specified node to the collection.
    /// Returns a node that already exists, or adds a new node.
    /// </summary>
    /// <param name="host">The host of the node.</param>
    /// <returns>The node that has been added to the collection.</returns>
    INode Add(in Host host);

    /// <summary>
    /// Removes a node, all connections to this node will be closed.
    /// </summary>
    /// <param name="host">The host of the node.</param>
    /// <returns><see langword="true"/> if the node was found and removed, <see langword="false"/> otherwise.</returns>
    bool Remove(in Host host);

    /// <summary>
    /// Removes all nodes from the collection and closes all connections.
    /// </summary>
    void Clear();
}
