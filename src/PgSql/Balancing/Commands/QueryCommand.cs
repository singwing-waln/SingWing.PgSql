using SingWing.PgSql.Balancing.Commands;
using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Pooling.Commands;

/// <summary>
/// Represents a query command.
/// </summary>
internal sealed class QueryCommand : Command<IAsyncEnumerable<IRow>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryCommand"/> class
    /// with the specified command text, parameters and the cancellation token.
    /// </summary>
    /// <param name="commandText">The command text.</param>
    /// <param name="parameters">The command parameters.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the asynchronous command.</param>
    internal QueryCommand(
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
        var rows = await connection.QueryAsync(CommandText, Parameters, CancellationToken);
        ((RowAsyncEnumerable)rows).OwnsConnection = true;
        SetResult(rows);

        // The connection continues to be used until the rows iteration is complete.
        return ConnectionReleaseState.InUse;
    }
}
