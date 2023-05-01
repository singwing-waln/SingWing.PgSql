using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents AuthenticationSASL messages received from PostgreSQL. Only SCRAM-SHA-256 is supported.
/// </summary>
internal sealed class AuthenticationSasl : AuthenticationRequest
{
    /// <summary>
    /// The name of the SASL authentication mechanism: SCRAM-SHA-256。
    /// </summary>
    internal const string SaslMechanism = "SCRAM-SHA-256";

    /// <summary>
    /// The length in bytes of client nonces.
    /// </summary>
    internal const int ClientNonceBinaryLength = 18;

    /// <summary>
    /// Base64 string length of client nonces.
    /// </summary>
    internal const int ClientNonceStringLength = ClientNonceBinaryLength * 4 / 3;

    /// <summary>
    /// A shared singleton instance of the <see cref="AuthenticationSasl"/> class.
    /// </summary>
    private static readonly AuthenticationSasl Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationSasl"/> class.
    /// </summary>
    private AuthenticationSasl() { }

    /// <summary>
    /// Reads the <see cref="AuthenticationSasl"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// An <see cref="AuthenticationSasl"/> instance if read is successful.
    /// Or <see cref="Unknown.Shared"/> if read fails or the authentication mechanism is not supported.</returns>
    internal static new async ValueTask<IBackendMessage> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        var supported = false;

        // The message body is a list of SASL authentication mechanisms,
        // in the server's order of preference.
        while (length > 0)
        {
            var (name, bytes) = await reader.ReadNullTerminatedStringAsync(
                length, cancellationToken);
            length -= bytes;

            if (name.Equals(SaslMechanism, StringComparison.OrdinalIgnoreCase))
            {
                supported = true;
                break;
            }
        }

        // A zero byte is required as terminator
        // after the last authentication mechanism name.
        if (length > 0)
        {
            await reader.DiscardAsync(length, cancellationToken);
        }

        return supported ? AuthenticationSasl.Shared : Unknown.Shared;
    }
}
