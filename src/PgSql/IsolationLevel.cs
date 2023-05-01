using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql;

/// <summary>
/// Defines the isolation level for PostgreSQL transactions.
/// </summary>
/// <remarks>
/// <see href="https://www.postgresql.org/docs/current/transaction-iso.html"/>.
/// </remarks>
public enum IsolationLevel
{
    /// <summary>
    /// Read Uncommitted (1). PostgreSQL's Read Uncommitted mode behaves like Read Committed.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/transaction-iso.html#XACT-READ-COMMITTED"/>.
    /// </remarks>
    ReadUncommitted = PredefinedSimpleQuery.BeginReadUncommitted,

    /// <summary>
    /// Read Committed (2). This is the default value in PostgreSQL.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/transaction-iso.html#XACT-READ-COMMITTED"/>.
    /// </remarks>
    ReadCommitted = PredefinedSimpleQuery.BeginReadCommitted,

    /// <summary>
    /// Repeatable Read (3).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/transaction-iso.html#XACT-REPEATABLE-READ"/>.
    /// </remarks>
    RepeatableRead = PredefinedSimpleQuery.BeginRepeatableRead,

    /// <summary>
    /// Serializable (4).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/transaction-iso.html#XACT-SERIALIZABLE"/>.
    /// </remarks>
    Serializable = PredefinedSimpleQuery.BeginSerializable
}
