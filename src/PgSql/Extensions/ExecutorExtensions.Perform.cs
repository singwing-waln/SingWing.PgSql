namespace SingWing.PgSql;

/// <summary>
/// Provides extension methods for executing multiple-statements commands.
/// </summary>
public static partial class ExecutorExtensions
{
    /// <summary>
    /// Executes the specified command consisting of multiple statements separated by ';'.
    /// </summary>
    /// <param name="executor">The <see cref="IExecutor" /> for executing the command.</param>
    /// <param name="statements">The command text.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous execution.</param>
    /// <exception cref="ArgumentException">The command text is empty.</exception>
    public static ValueTask PerformAsync(
        this IExecutor executor,
        string statements,
        CancellationToken cancellationToken = default) =>
        executor.PerformAsync(
            statements.AsMemory(),
            cancellationToken);
}
