using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Provides methods to send Terminate messages to database servers.
/// </summary>
internal static class TerminateExtensions
{
    /// <summary>
    /// The complete binary data of the Terminate message.
    /// </summary>
    private static readonly byte[] Bytes;

    static TerminateExtensions()
    {
        const char MessageType = 'X';

        Bytes = Helper.GenerateTinyMessageBinary(MessageType);
    }

    /// <summary>
    /// Writes the Terminate message to the output writer.
    /// </summary>
    /// <param name="writer">The output writer connected to the server.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask SendTerminateMessageAsync(
        this Writer writer)
    {
        await writer.WriteBinaryAsync(Bytes, cancellationToken: default);
        await writer.FlushAsync(default);
    }
}
