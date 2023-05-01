namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents a notice response message received from PostgreSQL.
/// </summary>
internal sealed class NoticeResponse : ProblemResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoticeResponse"/> class.
    /// </summary>
    internal NoticeResponse() { }
}
