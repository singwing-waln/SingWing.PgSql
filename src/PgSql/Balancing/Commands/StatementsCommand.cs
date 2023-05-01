using SingWing.PgSql.Balancing.Commands;
using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Pooling.Commands;

/// <summary>
/// Represents a command that contains multiple statements, separated by semicolons.
/// </summary>
internal sealed class StatementsCommand : Command<VoidResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StatementsCommand"/> class
    /// with the specified command text and the cancellation token.
    /// </summary>
    /// <param name="statements">The command text.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the asynchronous command.</param>
    internal StatementsCommand(
        in ReadOnlyMemory<char> statements,
        CancellationToken cancellationToken) : base(cancellationToken)
    {
        Statements = statements;
    }

    /// <summary>
    /// Gets the command text.
    /// </summary>
    internal ReadOnlyMemory<char> Statements { get; }

    /// <inheritdoc/>
    public override async ValueTask<ConnectionReleaseState> ExecuteAsync(Connection connection)
    {
        await connection.PerformAsync(Statements, CancellationToken);
        SetResult(default);
        return ConnectionReleaseState.Released;
    }
}
