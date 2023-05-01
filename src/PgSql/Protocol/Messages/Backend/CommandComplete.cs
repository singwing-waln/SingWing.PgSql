using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the message received from PostgreSQL when a command has completed, 
/// providing the number of rows affected or queried.
/// </summary>
internal sealed class CommandComplete : IBackendMessage
{
    /// <summary>
    /// The cache for <see cref="CommandComplete"/> when the number of rows affected or queried is less than 128.
    /// </summary>
    private static readonly CommandComplete[] Cache;

    /// <summary>
    /// The size of the <see cref="CommandComplete"/> cache.
    /// </summary>
    private const int CacheSize = 128;

    /// <summary>
    /// Initializes the <see cref="CommandComplete"/> cache.
    /// </summary>
    static CommandComplete()
    {
        Cache = new CommandComplete[CacheSize];

        for (int i = 0; i < CacheSize; i++)
        {
            Cache[i] = new CommandComplete(i);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandComplete"/> class 
    /// with the number of rows affected or queried.
    /// </summary>
    /// <param name="rowCount">The number of rows affected or queried.</param>
    internal CommandComplete(long rowCount) => RowCount = rowCount;

    /// <summary>
    /// Gets the number of rows affected or queried.
    /// </summary>
    internal long RowCount { get; private set; }

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.CommandComplete;

    /// <summary>
    /// Reads the <see cref="CommandComplete"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="predefined">A predefined object that is populated and returned when the number of rows is greater than or equal to 128.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The read <see cref="CommandComplete"/> instance.</returns>
    internal static async ValueTask<CommandComplete> ReadAsync(
        Reader reader,
        int length,
        CommandComplete predefined,
        CancellationToken cancellationToken)
    {
        var chars = await reader.ReadStringAsync(length, cancellationToken);
        if (chars is null || chars.Value.IsEmpty)
        {
            return Cache[0];
        }

        var tag = chars.Value;
        // The last space is followed by the number of rows.
        var index = tag.Span.LastIndexOf(' ');

        if (index == -1 || index == tag.Length - 1)
        {
            return Cache[0];
        }

        _ = long.TryParse(tag.Span[(index + 1)..], out var rowCount);

        if (rowCount < 0)
        {
            rowCount = 0;
        }

        if (rowCount < CacheSize)
        {
            return Cache[(int)rowCount];
        }

        predefined.RowCount = rowCount;

        return predefined;
    }
}
