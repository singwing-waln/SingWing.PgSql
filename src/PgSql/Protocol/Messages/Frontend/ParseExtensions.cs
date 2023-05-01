using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Provides methods to send Parse messages to database servers.
/// </summary>
internal static class ParseExtensions
{
    /// <summary>
    /// Writes a Parse message to the output stream.
    /// </summary>
    /// <param name="writer">The output stream to the database service.</param>
    /// <param name="parseBinary">The binary data of the Parse message.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ValueTask WriteParseMessageAsync(
        this Writer writer,
        byte[] parseBinary,
        CancellationToken cancellationToken) => 
        writer.WriteBinaryAsync(
            parseBinary,
            cancellationToken);
}
