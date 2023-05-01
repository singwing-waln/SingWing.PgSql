namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// <see cref="ErrorResponse"/> 和 <see cref="NoticeResponse"/> 的 Severity。
/// </summary>
/// <remarks>
/// https://www.postgresql.org/docs/current/runtime-config-logging.html#RUNTIME-CONFIG-SEVERITY-LEVELS
/// </remarks>
internal enum ProblemSeverity
{
    Other = 0,

    /// <summary>
    /// Reports information of interest to administrators, e.g., checkpoint activity.
    /// </summary>
    Log = 1,

    /// <summary>
    /// Provides successively-more-detailed information for use by developers.
    /// </summary>
    Debug = 2,

    /// <summary>
    /// Provides information implicitly requested by the user, e.g., output from VACUUM VERBOSE.
    /// </summary>
    Info = 3,

    /// <summary>
    /// Provides information that might be helpful to users, e.g., notice of truncation of long identifiers.
    /// </summary>
    Notice = 4,

    /// <summary>
    /// Provides warnings of likely problems, e.g., COMMIT outside a transaction block.
    /// </summary>
    Warning = 5,

    /// <summary>
    /// Reports an error that caused the current command to abort.
    /// </summary>
    Error = 6,

    /// <summary>
    /// Reports an error that caused the current session to abort.
    /// </summary>
    Fatal = 7,

    /// <summary>
    /// Reports an error that caused all database sessions to abort.
    /// </summary>
    Panic = 8
}