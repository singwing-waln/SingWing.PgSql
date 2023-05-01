using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents a message received from PostgreSQL that describes the data row information in the result set.
/// </summary>
internal sealed class RowDescription : IBackendMessage
{
    /// <summary>
    /// Represents a data row of information without any columns.
    /// </summary>
    internal static readonly RowDescription Empty = new();

    /// <summary>
    /// When there is a lot of idle column information, the threshold of the column information list is reconstructed.
    /// </summary>
    private const int RebuildThreshold = 50;

    /// <summary>
    /// The collection of column information in the row.
    /// </summary>
    private ColumnDescription[] _columns;

    /// <summary>
    /// Initializes a new instance of the <see cref="RowDescription"/> class.
    /// </summary>
    internal RowDescription()
    {
        _columns = Array.Empty<ColumnDescription>();
        ColumnCount = 0;
    }

    /// <summary>
    /// Gets the actual number of columns.
    /// </summary>
    /// <value>
    /// This value may be less than the length of _columns.
    /// </value>
    internal int ColumnCount { get; private set; }

    /// <summary>
    /// Gets the information for the column at the specified position.
    /// </summary>
    /// <param name="index">0-based position.</param>
    /// <returns>
    /// Information about the column at <paramref name="index"/>, 
    /// or <see cref="ColumnDescription.Empty"/> if the index is out of range.
    /// </returns>
    internal ColumnDescription this[int index] =>
        index < 0 || index >= ColumnCount ? ColumnDescription.Empty : _columns[index];

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.RowDescription;

    /// <summary>
    /// Reads the <see cref="RowDescription"/> message from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="predefined">A predefined <see cref="RowDescription"/> object to populate and return.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The read <see cref="RowDescription"/> instance.</returns>
    internal static async ValueTask<RowDescription> ReadAsync(
        Reader reader,
        int length,
        RowDescription predefined,
        CancellationToken cancellationToken)
    {
        Debug.Assert(length >= sizeof(short));

        // Number of columns, up to 1600 columns per table.
        // https://www.postgresql.org/docs/current/limits.html
        var count = await reader.ReadInt16Async(cancellationToken);
        length -= sizeof(short);

        if (count < 1)
        {
            Debug.Assert(length == 0);
            return Empty;
        }

        var columns = EnsureColumns(predefined, count);

        for (var i = 0; i < count; i++)
        {
            var read = await columns[i].ReadAsync(
                reader,
                length,
                i,
                cancellationToken);
            length -= read;
        }

        Debug.Assert(length == 0);

        return predefined;

        static ColumnDescription[] EnsureColumns(RowDescription row, short count)
        {
            if (row._columns.Length < count ||
                row._columns.Length - count > RebuildThreshold)
            {
                row._columns = CreateColumns(row, count);
            }

            row.ColumnCount = count;
            return row._columns;

            static ColumnDescription[] CreateColumns(RowDescription row, short count)
            {
                var oldCount = (short)row._columns.Length;
                var columns = new ColumnDescription[count];

                if (oldCount > 0)
                {
                    Array.Copy(row._columns, columns, Math.Min(oldCount, count));
                }

                if (oldCount < count)
                {
                    for (var i = oldCount; i < count; i++)
                    {
                        columns[i] = new ColumnDescription();
                    }
                }

                return columns;
            }
        }
    }
}
