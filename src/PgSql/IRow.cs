namespace SingWing.PgSql;

/// <summary>
/// Provides a way of reading forward-only, non-cached reading of columns in a row from PostgreSQL.
/// </summary>
public interface IRow : IAsyncEnumerable<IColumn>
{
    /// <summary>
    /// Gets the number of columns in this row.
    /// </summary>
    int ColumnCount { get; }

    /// <summary>
    /// Gets the name of the column at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index.</param>
    /// <returns>The column name at <paramref name="index"/>, or an empty string if the index is out of range.</returns>
    ReadOnlySpan<char> NameAt(int index);

    /// <summary>
    /// Gets the data type code of the column at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index.</param>
    /// <returns>The data type code at <paramref name="index"/>, or zero if the index is out of range.</returns>
    uint DataTypeAt(int index);

    /// <summary>
    /// Reads and discards all remaining data in the row.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    ValueTask DiscardAsync(CancellationToken cancellationToken = default);
}
