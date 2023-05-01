using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Sends Describe messages to database servers.
/// </summary>
internal static class DescribeExtensions
{
    /// <summary>
    /// Writes a Describe message to the output stream.
    /// </summary>
    /// <param name="writer">The output stream to the database service.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask WriteDescribeMessageAsync(
        this Writer writer,
        CancellationToken cancellationToken)
    {
        // https://www.postgresql.org/docs/current/protocol-message-formats.html
        const char MessageType = 'D';
        const char TargetType = 'P';

        await writer.WriteByteAsync(MessageType, cancellationToken);
        await writer.WriteInt32Async(
            sizeof(int) + sizeof(byte) + sizeof(byte), 
            cancellationToken);
        // 'S' to describe a prepared statement; or 'P' to describe a portal.
        await writer.WriteByteAsync(TargetType, cancellationToken);
        // The name of the prepared statement or portal to describe
        // (an empty string selects the unnamed prepared statement or portal).
        await writer.WriteStringTerminatorAsync(cancellationToken);
    }
}
