using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Backend;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Provides methods to send the first message of SCRAM-SHA-256 to database servers.
/// </summary>
internal static class SaslInitialResponseExtensions
{
    /// <summary>
    /// The binary data for the "SCRAM-SHA-256" string.
    /// </summary>
    private static readonly byte[] SaslMechanismBytes =
        Encoding.UTF8.GetBytes(AuthenticationSasl.SaslMechanism);

    /// <summary>
    /// Sents the first message for SCRAM-SHA-256 to the specified server.
    /// </summary>
    /// <param name="writer">The output writer connected to the server.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The client's first message thats contains only the user and nonce, and the client nonce string.</returns>
    internal static async ValueTask<(ReadOnlyMemory<char>, ReadOnlyMemory<char>)> SendClientFirstMessageAsync(
        this Writer writer,
        CancellationToken cancellationToken)
    {
        const char MessageType = 'p';

        // When SCRAM-SHA-256 is used in PostgreSQL, the server will ignore
        // the user name that the client sends in the client-first-message.
        // The user name that was already sent in the startup message is used instead. 
        // https://www.postgresql.org/docs/current/sasl-authentication.html#SASL-SCRAM-SHA-256
        // https://www.rfc-editor.org/rfc/rfc7677.html#section-3
        var nonce = GenerateNonce();
        var text = $"n,,n=*,r={nonce}";
        var byteLength = Encoding.UTF8.GetByteCount(text);
        var length =
            // Length of message contents in bytes, including self.
            sizeof(int) +
            // Name of the SASL authentication mechanism that the client selected.
            SaslMechanismBytes.Length + sizeof(byte) +
            // Length of SASL mechanism specific "Initial Client Response" that follows,
            // or -1 if there is no Initial Response.
            sizeof(int) +
            // SASL mechanism specific "Initial Response".
            byteLength;

        writer.DebugAssertEnoughSpace(sizeof(byte) + length);

        writer.WriteByte(MessageType);
        writer.WriteInt32(length);
        writer.WriteBinary(SaslMechanismBytes);
        writer.WriteStringTerminator();
        writer.WriteInt32(byteLength);
        writer.WriteString(text);
        await writer.FlushAsync(cancellationToken);

        return (
            text.AsMemory(3, text.Length - 3),
            text.AsMemory(9, text.Length - 9));

        static string GenerateNonce()
        {
            Span<byte> nonce = stackalloc byte[AuthenticationSasl.ClientNonceBinaryLength];
            RandomNumberGenerator.Fill(nonce);
            return Convert.ToBase64String(nonce);
        }
    }
}
