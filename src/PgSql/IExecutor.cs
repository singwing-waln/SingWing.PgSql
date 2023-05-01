namespace SingWing.PgSql;

/// <summary>
/// Represents an executor of PostgreSQL commands.
/// </summary>
/// <remarks>
/// <para>
/// The parameter placeholders in command texts must use the numeric syntax: $n.
/// $1 represents the first parameter, $2 represents the second, and so on.
/// </para>
/// <para>
/// If a command is executed in a connection pool, the connection will be automatically returned 
/// to the connection pool as soon as the command completes.
/// </para>
/// <para>
/// If a command is executed on a connection or in a transaction, the connection needs to be returned 
/// to the connection pool by calling the IAsyncDisposable.DisposeAsync defined on <see cref="IConnection"/>.
/// </para>
/// <para>
/// <see cref="IDatabase"/>, <see cref="INode"/>, <see cref="IConnection"/> and <see cref="ITransaction"/> implement this interface.
/// If the caller does not care which connection or node commands should be executed on, they should be executed using <see cref="IDatabase"/>.
/// If the caller needs commands to be executed on a node, but does not care whether they are executed on the same connection, 
/// then <see cref="INode"/> should be used to execute them.
/// <see cref="IConnection"/> or <see cref="ITransaction"/> is only used if commands must be executed on the same connection.
/// </para>
/// </remarks>
public interface IExecutor
{
    /// <summary>
    /// Executes the specified query command and returns a single result set.
    /// </summary>
    /// <param name="commandText">The command text, such as "SELECT", "FETCH", etc.</param>
    /// <param name="parameters">The list of parameters used by the command. The number of parameters cannot exceed 65535.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>An enumerator that reads each row of data in the result set.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
        ReadOnlyMemory<char> commandText,
        ReadOnlyMemory<Parameter> parameters,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified non-query command and returns the number of rows affected.
    /// </summary>
    /// <param name="commandText">The command text, such as "INSERT", "UPDATE", etc.</param>
    /// <param name="parameters">The list of parameters used by the command. The number of parameters cannot exceed 65535.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The total number of rows inserted, updated, or deleted.</returns>
    /// <exception cref="ArgumentException">The command text is empty, or there are too many parameters.</exception>
    /// <remarks>
    /// <para>
    /// The parameter placeholders in <paramref name="commandText"/> must use the numeric syntax: $n.
    /// $1 represents the first parameter, $2 represents the second, and so on.
    /// </para>
    /// </remarks>
    ValueTask<long> ExecuteAsync(
        ReadOnlyMemory<char> commandText,
        ReadOnlyMemory<Parameter> parameters,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified command consisting of multiple statements separated by ';'.
    /// </summary>
    /// <param name="statements">The statements are executed as a single transaction.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="ArgumentException">The command text is empty.</exception>
    ValueTask PerformAsync(
        ReadOnlyMemory<char> statements,
        CancellationToken cancellationToken = default);
}
