using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the message sent by PostgreSQL that describes the parameter information of a statement.
/// Describe is not currently sent, so we should not receive this message. 
/// ParameterDescription is ignored.
/// </summary>
internal sealed class ParameterDescription : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="ParameterDescription"/> class.
    /// </summary>
    private static readonly ParameterDescription Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterDescription"/> class.
    /// </summary>
    private ParameterDescription() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.ParameterDescription;

    /// <summary>
    /// Reads the <see cref="ParameterDescription"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<ParameterDescription> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
