using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Provides helper methods for sending or generating messages.
/// </summary>
internal static class Helper
{
    /// <summary>
    /// Generates the complete binary data for a simple message of the specified type.
    /// </summary>
    /// <param name="type">The message type.</param>
    /// <returns>The complete binary data of the message.</returns>
    /// <remarks>
    /// Used for messages that have no message body, only message type and length, such as Sync, Terminate, etc.
    /// </remarks>
    internal static byte[] GenerateTinyMessageBinary(char type)
    {
        var binary = new byte[sizeof(byte) + sizeof(int)];
        var writer = new Writer(binary);

        writer.WriteByte(type);
        writer.WriteInt32(sizeof(int));

        return binary;
    }
}
