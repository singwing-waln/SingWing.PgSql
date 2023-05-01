using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the message notified by PostgreSQL when the server parameters change. 
/// Currently we don't care about these parameters and discard the message content.
/// </summary>
internal sealed class ParameterStatus : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="ParameterStatus"/> class.
    /// </summary>
    private static readonly ParameterStatus Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterStatus"/> class.
    /// </summary>
    private ParameterStatus() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.ParameterStatus;

    /// <summary>
    /// Reads the <see cref="ParameterStatus"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<ParameterStatus> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
