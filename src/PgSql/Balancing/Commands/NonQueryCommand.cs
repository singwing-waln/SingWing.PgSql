using SingWing.PgSql.Balancing.Commands;
using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Pooling.Commands;

/// <summary>
/// Represents a non-query command.
/// </summary>
internal sealed class NonQueryCommand : Command<long>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NonQueryCommand"/> class
    /// with the specified command text, parameters and the cancellation token.
    /// </summary>
    /// <param name="commandText">The command text.</param>
    /// <param name="parameters">The command parameters.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the asynchronous command.</param>
    internal NonQueryCommand(
        in ReadOnlyMemory<char> commandText,
        in ReadOnlyMemory<Parameter> parameters,
        CancellationToken cancellationToken) : base(cancellationToken)
    {
        CommandText = commandText;
        Parameters = parameters;
    }

    /// <summary>
    /// Gets the command text.
    /// </summary>
    internal ReadOnlyMemory<char> CommandText { get; }

    /// <summary>
    /// Gets the command parameters.
    /// </summary>
    internal ReadOnlyMemory<Parameter> Parameters { get; }

    /// <inheritdoc/>
    public override async ValueTask<ConnectionReleaseState> ExecuteAsync(Connection connection)
    {
        SetResult(await connection.ExecuteAsync(CommandText, Parameters, CancellationToken));
        return ConnectionReleaseState.Released;
    }
}
