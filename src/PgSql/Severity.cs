using SingWing.PgSql.Protocol.Messages.Backend;

namespace SingWing.PgSql;

/// <summary>
/// Defines the severity of the error in <see cref="ServerException"/>.
/// </summary>
public enum Severity
{
    /// <summary>
    /// An error that caused a command to fail.
    /// </summary>
    Error = ProblemSeverity.Error,

    /// <summary>
    /// An error that caused a session (connection) to fail.
    /// </summary>
    Fatal = ProblemSeverity.Fatal,

    /// <summary>
    /// An error that caused all sessions (connections) to a PostgreSQL service to fail.
    /// </summary>
    Panic = ProblemSeverity.Panic
}
