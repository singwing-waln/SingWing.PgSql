namespace SingWing.PgSql.Protocol.Connections;

/// <summary>
/// Represents a predefined simple query.
/// </summary>
internal enum PredefinedSimpleQuery
{
    /// <summary>
    /// Begins a transaction with isolation level READ UNCOMMITTED.
    /// </summary>
    /// <remarks>
    /// In PostgreSQL READ UNCOMMITTED is treated as READ COMMITTED.
    /// </remarks>
    BeginReadUncommitted = 1,

    /// <summary>
    /// Begins a transaction with isolation level READ COMMITTED.
    /// </summary>
    BeginReadCommitted = 2,

    /// <summary>
    /// Begins a transaction with isolation level REPEATABLE READ.
    /// </summary>
    BeginRepeatableRead = 3,

    /// <summary>
    /// Begins a transaction with isolation level SERIALIZABLE.
    /// </summary>
    BeginSerializable = 4,

    /// <summary>
    /// Commits the database transaction.
    /// </summary>
    Commit = 5,

    /// <summary>
    /// Rolls back the database transaction.
    /// </summary>
    Rollback = 6,

    /// <summary>
    /// Releases all temporary resources associated with a session and resets the session to its initial state.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/sql-discard.html"/>.
    /// </remarks>
    DiscardAll = 7
}
