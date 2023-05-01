using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the base class for authentication-related messages received from PostgreSQL.
/// </summary>
internal abstract class AuthenticationRequest : IBackendMessage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationRequest"/> class.
    /// </summary>
    protected AuthenticationRequest() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.AuthenticationRequest;

    /// <summary>
    /// Reads an <see cref="AuthenticationRequest"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// An <see cref="AuthenticationRequest"/> instance if read is successful, 
    /// <see cref="Unknown.Shared"/> otherwise.</returns>
    internal static async ValueTask<IBackendMessage> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        var type = await reader.ReadInt32Async(cancellationToken);
        length -= sizeof(int);

        return type switch
        {
            0 => length == 0
                ? AuthenticationOk.Shared
                : await Unknown.ReadAsync(reader, length, cancellationToken),
            10 => await AuthenticationSasl.ReadAsync(
                reader, length, cancellationToken),
            11 => await AuthenticationSaslContinue.ReadAsync(
                reader, length, cancellationToken),
            12 => await AuthenticationSaslFinal.ReadAsync(
                reader, length, cancellationToken),
            _ => await Unknown.ReadAsync(
                reader, length, cancellationToken)
        };
    }
}
