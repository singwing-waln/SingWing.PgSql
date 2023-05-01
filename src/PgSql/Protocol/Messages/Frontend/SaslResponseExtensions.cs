using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Backend;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Provides methods to send the final message of SCRAM-SHA-256 to database servers.
/// </summary>
internal static class SaslResponseExtensions
{
    /// <summary>
    /// The binary data for "Client Key".
    /// </summary>
    private static readonly byte[] ClientKeyBinary = Encoding.UTF8.GetBytes("Client Key");

    /// <summary>
    /// The binary data for "Server Key".
    /// </summary>
    private static readonly byte[] ServerKeyBinary = Encoding.UTF8.GetBytes("Server Key");

    /// <summary>
    /// Sends the final message of SCRAM-SHA-256 to the specified server.
    /// </summary>
    /// <param name="writer">The output writer connected to the server.</param>
    /// <param name="password">The password of database user.</param>
    /// <param name="clientFirstMessageBare">The client's first message text contains only the user and nonce.</param>
    /// <param name="serverFirstMessage">The first message received by the server before.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The server signature string so that the server can be validated.</returns>
    internal static async ValueTask<string> SendClientFinalMessageAsync(
        this Writer writer,
        string password,
        ReadOnlyMemory<char> clientFirstMessageBare,
        AuthenticationSaslContinue serverFirstMessage,
        CancellationToken cancellationToken)
    {
        const char MessageType = 'p';

        // https://www.postgresql.org/docs/current/sasl-authentication.html#SASL-SCRAM-SHA-256
        // https://www.rfc-editor.org/rfc/rfc7677.html#section-3
        // https://www.rfc-editor.org/rfc/rfc5802
        var salt = Convert.FromBase64String(serverFirstMessage.Salt.ToString());
        var saltedPassword = Hi(
            Encoding.UTF8.GetBytes(password.Normalize(NormalizationForm.FormKC)),
            salt,
            serverFirstMessage.IterationCount);
        var clientKey = HMAC(saltedPassword, ClientKeyBinary);
        var storedKey = H(clientKey);
        var clientFinalMessageWithoutProof = $"c=biws,r={serverFirstMessage.Nonce}";
        var authMessage = $"{clientFirstMessageBare},{serverFirstMessage},{clientFinalMessageWithoutProof}";
        var authMessageBinary = Encoding.UTF8.GetBytes(authMessage);

        var clientSignature = HMAC(storedKey, authMessageBinary);
        var clientProof = Convert.ToBase64String(Xor(clientKey, clientSignature));

        // c=biws,r=rOprNGfwEbeRWgbNEkqO%hvYDpWUa2RaTCAfuxFIlj)hNlF$k0,p=dHzbZapWIk4jUhN+Ute9ytag9zjfMHgsqmmiz7AndVQ=
        var text = $"{clientFinalMessageWithoutProof},p={clientProof}";

        var byteLength = Encoding.UTF8.GetByteCount(text);
        var length =
            // Length of message contents in bytes, including self.
            sizeof(int) +
            // SASL mechanism specific message data.
            byteLength;

        writer.DebugAssertEnoughSpace(sizeof(byte) + length);

        writer.WriteByte(MessageType);
        writer.WriteInt32(length);
        writer.WriteString(text);
        await writer.FlushAsync(cancellationToken);

        var serverKey = HMAC(saltedPassword, ServerKeyBinary);
        var serverSignature = HMAC(serverKey, authMessageBinary);
        return Convert.ToBase64String(serverSignature);

        static byte[] Hi(byte[] passwordBinary, byte[] salt, int count)
        {
            using var hmac = new HMACSHA256(passwordBinary);
            Span<byte> buffer = stackalloc byte[HMACSHA256.HashSizeInBytes << 1];
            var hi = buffer[..HMACSHA256.HashSizeInBytes];
            var u2 = buffer[HMACSHA256.HashSizeInBytes..];

            // U1 = HMAC(passwordBinary, salt + INT(1))
            _ = hmac.TryComputeHash(SaltPlus1(salt, buffer: u2), hi, out _);
            var u1 = hi;

            for (var i = 1; i < count; i++)
            {
                _ = hmac.TryComputeHash(u1, u2, out _);
                _ = Xor(hi, u2);
                u1 = u2;
            }

            return hi.ToArray();

            // salt + INT(1)
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static ReadOnlySpan<byte> SaltPlus1(byte[] salt, Span<byte> buffer)
            {
                salt.CopyTo(buffer[..salt.Length]);
                buffer[salt.Length + 0] = 0;
                buffer[salt.Length + 1] = 0;
                buffer[salt.Length + 2] = 0;
                buffer[salt.Length + 3] = 1;
                return buffer[..(salt.Length + 4)];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Span<byte> Xor(Span<byte> left, in ReadOnlySpan<byte> right)
        {
            for (var i = 0; i < left.Length; i++)
            {
                left[i] ^= right[i];
            }

            return left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static byte[] H(byte[] clientKey) => SHA256.HashData(clientKey);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static byte[] HMAC(byte[] data, byte[] keyBinary) => HMACSHA256.HashData(data, keyBinary);
    }
}
