using System.Globalization;
using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents an unrecognized or unsupported type of message received from PostgreSQL.
/// </summary>
internal sealed class Unknown : IBackendMessage
{
    /// <summary>
    /// A shared singleton instance of the <see cref="Unknown"/> class.
    /// </summary>
    internal static readonly Unknown Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Unknown"/> class.
    /// </summary>
    private Unknown() { }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.Unknown;

    /// <summary>
    /// Reads the <see cref="Unknown"/> message from the specified <see cref="Reader"/>. 
    /// Data is discarded, and a Warning message is logged.
    /// </summary>
    /// <param name="reader">The <see cref="Reader"/> to read data from.</param>
    /// <param name="type">The message type code.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns><see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<Unknown> ReadAsync(
        Reader reader,
        byte type,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        Db.Logger.LogWarning(string.Format(
            CultureInfo.CurrentCulture, Strings.UnknownBackendMessage, (char)type, length));
        return Shared;
    }

    /// <summary>
    /// Reads the <see cref="Unknown"/> message from the specified <see cref="Reader"/>. 
    /// Data is discarded, no Warning message is logged.
    /// </summary>
    /// <param name="reader">The <see cref="Reader"/> to read data from.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns><see cref="Shared"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<Unknown> ReadAsync(
        Reader reader,
        int length,
        CancellationToken cancellationToken)
    {
        await reader.DiscardAsync(length, cancellationToken);
        return Shared;
    }
}
