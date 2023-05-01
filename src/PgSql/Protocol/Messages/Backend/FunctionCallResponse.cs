using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the result of the response to a function call. 
/// The function call protocol is not supported, so FunctionCallResponse is ignored.
/// </summary>
internal sealed class FunctionCallResponse : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="FunctionCallResponse"/> class.
    /// </summary>
    private static readonly FunctionCallResponse Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionCallResponse"/> class.
    /// </summary>
    private FunctionCallResponse() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.FunctionCallResponse;

    /// <summary>
    /// Reads the <see cref="FunctionCallResponse"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<FunctionCallResponse> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
