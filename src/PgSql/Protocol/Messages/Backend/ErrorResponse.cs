namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents an error response message received from PostgreSQL.
/// </summary>
internal sealed class ErrorResponse : ProblemResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
    /// </summary>
    internal ErrorResponse() { }

    /// <inheritdoc />
    public override BackendMessageType Type => BackendMessageType.ErrorResponse;
}
