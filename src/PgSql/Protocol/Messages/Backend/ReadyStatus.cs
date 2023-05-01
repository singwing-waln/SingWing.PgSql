namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// The status code carried in <see cref="ReadyForQuery"/>.
/// </summary>
internal enum ReadyStatus : byte
{
    /// <summary>
    /// Idle, not in a transaction block.
    /// </summary>
    NotInTransaction = (byte)'I',

    /// <summary>
    /// In a transaction block.
    /// </summary>
    SucceededTransaction = (byte)'T',

    /// <summary>
    /// In a failed transaction block (queries will be rejected until block is ended).
    /// </summary>
    FailedTransaction = (byte)'E',

    /// <summary>
    /// Other unknown status.
    /// </summary>
    Unknown = (byte)'\0'
}
