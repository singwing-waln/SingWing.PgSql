using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the first authentication message (server-first-message) received from PostgreSQL.
/// </summary>
internal sealed class AuthenticationSaslContinue : AuthenticationRequest
{
    /// <summary>
    /// The original text of the message.
    /// </summary>
    private readonly ReadOnlyMemory<char> _text;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationSaslContinue"/> class 
    /// with the original text of the message and the security options.
    /// </summary>
    /// <param name="text">The original text of the message.</param>
    /// <param name="nonce">The parsed combination of client nonce and server nonce.</param>
    /// <param name="salt">The salt of the user's password.</param>
    /// <param name="iteration">The iteration count.</param>
    private AuthenticationSaslContinue(
        ReadOnlyMemory<char> text,
        ReadOnlyMemory<char> nonce,
        ReadOnlyMemory<char> salt,
        int iteration)
    {
        _text = text;
        Nonce = nonce;
        ClientNonce = nonce[..AuthenticationSasl.ClientNonceStringLength];
        ServerNonce = nonce[AuthenticationSasl.ClientNonceStringLength..];
        Salt = salt;
        IterationCount = iteration;
    }

    /// <summary>
    /// Gets the combination of client nonce and server nonce.
    /// </summary>
    internal ReadOnlyMemory<char> Nonce { get; }

    /// <summary>
    /// Gets the client nonce.
    /// </summary>
    internal ReadOnlyMemory<char> ClientNonce { get; }

    /// <summary>
    /// Gets the server nonce.
    /// </summary>
    internal ReadOnlyMemory<char> ServerNonce { get; }

    /// <summary>
    /// Gets the salt of the user's password.
    /// </summary>
    internal ReadOnlyMemory<char> Salt { get; }

    /// <summary>
    /// Gets the iteration count.
    /// </summary>
    internal int IterationCount { get; }

    /// <summary>
    /// Verifies that the nonce contained in this message begins with the client nonce.
    /// </summary>
    /// <param name="clientNonce">The client nonce previously sent to the server.</param>
    /// <returns>
    /// <see langword="true"/> if the nonce begins with <paramref name="clientNonce"/>, 
    /// <see langword="false"/> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool Verify(in ReadOnlyMemory<char> clientNonce) =>
        ClientNonce.Span.Equals(clientNonce.Span, StringComparison.Ordinal);

    /// <summary>
    /// Returns the original text of the message.
    /// </summary>
    /// <returns>The text of server-first-message.</returns>
    public override string ToString() => _text.ToString();

    /// <summary>
    /// Reads the <see cref="AuthenticationSaslContinue"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// An <see cref="AuthenticationSaslContinue"/> instance if read is successful,
    /// <see cref="Unknown.Shared"/> otherwise.</returns>
    internal static new async ValueTask<IBackendMessage> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        // r=rOprNGfwEbeRWgbNEkqO%hvYDpWUa2RaTCAfuxFIlj)hNlF$k0,s=W22ZaJ0SNY7soEsUEjb6gQ==,i=4096
        // https://www.rfc-editor.org/rfc/rfc7677.html#section-3
        var chars = await reader.ReadStringAsync(length, cancellationToken);

        if (chars is null || chars.Value.IsEmpty)
        {
            return Unknown.Shared;
        }

        var text = chars.Value;
        var i1 = text.Span.IndexOf(',');
        if (i1 <= 0)
        {
            return Unknown.Shared;
        }

        var i2 = text.Span[(i1 + 1)..].IndexOf(',') + i1 + 1;

        if (i2 <= i1)
        {
            return Unknown.Shared;
        }

        if (i2 + 3 >= text.Length)
        {
            return Unknown.Shared;
        }

        var rSegment = text[..i1];
        if (rSegment.Length <= AuthenticationSasl.ClientNonceStringLength + 2 ||
            !rSegment.Span.StartsWith("r=", StringComparison.Ordinal))
        {
            return Unknown.Shared;
        }

        var sSegment = text.Slice(i1 + 1, i2 - i1 - 1);
        if (sSegment.Length < 3 ||
            !sSegment.Span.StartsWith("s=", StringComparison.Ordinal))
        {
            return Unknown.Shared;
        }

        var iSegment = text.Slice(i2 + 1, text.Length - i2 - 1);
        if (!iSegment.Span.StartsWith("i=", StringComparison.Ordinal))
        {
            return Unknown.Shared;
        }

        if (!int.TryParse(iSegment.Span[2..], out var iteration) || iteration <= 0)
        {
            return Unknown.Shared;
        }

        return new AuthenticationSaslContinue(
            text,
            rSegment[2..],
            sSegment[2..],
            iteration);
    }
}
