namespace SingWing.PgSql.Protocol.Messages;

/// <summary>
/// Defines the type codes for backend messages received from database servers.
/// </summary>
/// <remarks>
/// <see href="https://www.postgresql.org/docs/current/protocol-message-formats.html"/>.
/// </remarks>
internal enum BackendMessageType : byte
{
    AuthenticationRequest = (byte)'R',
    BackendKeyData = (byte)'K',
    BindComplete = (byte)'2',
    CloseComplete = (byte)'3',
    CommandComplete = (byte)'C',
    CopyData = (byte)'d',
    CopyDone = (byte)'c',
    CopyBothResponse = (byte)'W',
    CopyInResponse = (byte)'G',
    CopyOutResponse = (byte)'H',
    DataRow = (byte)'D',
    EmptyQueryResponse = (byte)'I',
    ErrorResponse = (byte)'E',
    FunctionCallResponse = (byte)'V',
    NegotiateProtocolVersion = (byte)'v',
    NoData = (byte)'n',
    NoticeResponse = (byte)'N',
    NotificationResponse = (byte)'A',
    ParameterDescription = (byte)'t',
    ParameterStatus = (byte)'S',
    ParseComplete = (byte)'1',
    PortalSuspended = (byte)'s',
    ReadyForQuery = (byte)'Z',
    RowDescription = (byte)'T',

    /// <summary>
    /// Uses this type code when a message type that is not supported by the current protocol is received.
    /// </summary>
    Unknown = 0
}
