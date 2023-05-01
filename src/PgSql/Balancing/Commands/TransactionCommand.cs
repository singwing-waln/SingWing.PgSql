using SingWing.PgSql.Balancing.Commands;
using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Pooling.Commands;

/// <summary>
/// Represents a command that starts a transaction.
/// </summary>
internal sealed class TransactionCommand : Command<ITransaction>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionCommand"/> class
    /// with the specified isolation level and the cancellation token.
    /// </summary>
    /// <param name="isolationLevel">The isolation level for the new transaction.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the asynchronous command.</param>
    internal TransactionCommand(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken) : base(cancellationToken)
    {
        IsolationLevel = isolationLevel;
    }

    /// <summary>
    /// Gets the isolation level of the transaction.
    /// </summary>
    internal IsolationLevel IsolationLevel { get; }

    /// <inheritdoc/>
    public override async ValueTask<ConnectionReleaseState> ExecuteAsync(Connection connection)
    {
        var transaction = await connection.BeginAsync(IsolationLevel, CancellationToken);
        ((Transaction)transaction).OwnsConnection = true;
        SetResult(transaction);

        // The connection continues to be used until the transaction is complete.
        return ConnectionReleaseState.InUse;
    }
}
