using SingWing.PgSql.Balancing.Commands;
using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Pooling.Commands;

/// <summary>
/// Represents a command queued for execution.
/// </summary>
internal interface ICommand
{
    /// <summary>
    /// Gets the cancellation token that can be used to cancel the asynchronous command.
    /// </summary>
    CancellationToken CancellationToken { get; }

    /// <summary>
    /// Completes the command with an error.
    /// </summary>
    /// <param name="exception">The exception to report to the caller.</param>
    void SetException(Exception exception);

    /// <summary>
    /// Executes the current command using the specified connection.
    /// </summary>
    /// <param name="connection">The connection used to execute the command.</param>
    /// <returns>
    /// <see cref="ConnectionReleaseState.Released"/> if the connection is released after the execution is complete, 
    /// <see cref="ConnectionReleaseState.InUse"/> otherwise.
    /// </returns>
    ValueTask<ConnectionReleaseState> ExecuteAsync(Connection connection);
}
