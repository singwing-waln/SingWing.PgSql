using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the final authentication message (server-final-message) received from PostgreSQL.
/// </summary>
internal sealed class AuthenticationSaslFinal : AuthenticationRequest
{
    /// <summary>
    /// The original text of the message.
    /// </summary>
    private readonly ReadOnlyMemory<char> _text;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationSaslFinal"/> class 
    /// with the original text of the message.
    /// </summary>
    /// <param name="text">The original text of the message.</param>
    private AuthenticationSaslFinal(ReadOnlyMemory<char> text)
    {
        _text = text;
        Signature = text[2..];
    }

    /// <summary>
    /// Gets the server signature string.
    /// </summary>
    internal ReadOnlyMemory<char> Signature { get; }

    /// <summary>
    /// Verifies that the server signature in this message matches the signature previously calculated by the client itself.
    /// </summary>
    /// <param name="serverSignature">The signature that the client computed itself.</param>
    /// <returns>
    /// <see langword="true"/> if <see cref="Signature"/> equals to <paramref name="serverSignature"/>, 
    /// <see langword="false"/> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool Verify(string serverSignature) =>
        Signature.Span.Equals(serverSignature, StringComparison.Ordinal);

    /// <summary>
    /// Returns the original text of the message.
    /// </summary>
    /// <returns>The text of server-first-message.</returns>
    public override string ToString() => _text.ToString();

    /// <summary>
    /// Reads the <see cref="AuthenticationSaslFinal"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// An <see cref="AuthenticationSaslFinal"/> instance if read is successful,
    /// <see cref="Unknown.Shared"/> otherwise.</returns>
    internal static new async ValueTask<IBackendMessage> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        var chars = await reader.ReadStringAsync(length, cancellationToken);

        if (chars is null)
        {
            return Unknown.Shared;
        }

        var text = chars.Value;
        return text.Length > 2 && text.Span.StartsWith("v=")
            ? new AuthenticationSaslFinal(text)
            : Unknown.Shared;
    }
}
