namespace SingWing.PgSql;

/// <summary>
/// Provides extension methods to <see cref="ITransaction"/>.
/// </summary>
public static partial class TransactionExtensions
{
    /// <summary>
    /// Rolls back the database transaction.
    /// </summary>
    /// <param name="transaction">The <see cref="ITransaction" /> will be rolled back.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    public static ValueTask RollbackAsync(
        this ITransaction transaction,
        CancellationToken cancellationToken = default) =>
        transaction.RollbackAsync(string.Empty, cancellationToken);
}
