using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.DataTypes;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents a row of data received from PostgreSQL.
/// </summary>
internal sealed class DataRow : IBackendMessage, IRow, IDisposable, IAsyncDisposable
{
    /// <summary>
    /// The underlying database connection.
    /// </summary>
    private readonly Connection _connection;

    /// <summary>
    /// The description information of this data row.
    /// </summary>
    private RowDescription _description;

    /// <summary>
    /// The length of the data in the current row that has not yet been read.
    /// </summary>
    private int _remainingLength;

    /// <summary>
    /// The enumerator for the columns in this row.
    /// </summary>
    private readonly ColumnAsyncEnumerator _columnEnumerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRow"/> class
    /// with the underlying database connection.
    /// </summary>
    /// <param name="connection">The underlying database connection.</param>
    internal DataRow(Connection connection)
    {
        _connection = connection;
        _description = RowDescription.Empty;
        _remainingLength = 0;
        _columnEnumerator = new ColumnAsyncEnumerator(this);
    }

    /// <summary>
    /// Gets the input stream that receives data from the database service.
    /// </summary>
    internal Reader Reader => _connection.Reader;

    /// <summary>
    /// Gets the description information of this row.
    /// </summary>
    internal RowDescription Description => _description;

    /// <inheritdoc />
    public BackendMessageType Type => BackendMessageType.DataRow;

    /// <summary>
    /// Advances the read position after the row data has been read.
    /// </summary>
    /// <param name="byteCount">The number of bytes processed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Advance(int byteCount)
    {
        if (byteCount > 0)
        {
            Debug.Assert(byteCount <= _remainingLength);
            _remainingLength -= byteCount;
        }
    }

    #region IDisposable

    /// <inheritdoc/>
    public ValueTask DisposeAsync() => _columnEnumerator.DisposeAsync();

    /// <inheritdoc/>
    void IDisposable.Dispose() =>
        DisposeAsync().AsTask().ConfigureAwait(false).GetAwaiter().GetResult();

    #endregion

    #region IRow

    /// <inheritdoc />
    int IRow.ColumnCount => _description.ColumnCount;

    /// <inheritdoc />
    ReadOnlySpan<char> IRow.NameAt(int index) => _description[index].Name;

    /// <inheritdoc />
    uint IRow.DataTypeAt(int index) => _description[index].DataType;

    /// <inheritdoc />
    public async ValueTask DiscardAsync(CancellationToken cancellationToken)
    {
        if (_remainingLength > 0)
        {
            await Reader.DiscardAsync(_remainingLength, cancellationToken);
            _remainingLength = 0;
        }
    }

    #endregion

    #region IAsyncEnumerable

    /// <inheritdoc />
    IAsyncEnumerator<IColumn> IAsyncEnumerable<IColumn>.GetAsyncEnumerator(
        CancellationToken cancellationToken) => _columnEnumerator;

    #endregion

    #region Cursors

    /// <summary>
    /// Gets a value indicating whether the current row contains a refcursor.
    /// </summary>
    internal bool HasCursors
    {
        get
        {
            var count = _description.ColumnCount;

            for (var i = 0; i < count; i++)
            {
                if (_description[i].DataType == VarcharProtocol.RefCursorTypeCode)
                {
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Returns the first cursor name when the row contains cursors.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The cursor name represented by read-only memory. 
    /// Returns an empty string if the current row does not contain a cursor.
    /// </returns>
    internal async ValueTask<string> ReadCursorNameAsync(
        CancellationToken cancellationToken)
    {
        if (_description.ColumnCount == 0)
        {
            return string.Empty;
        }

        var name = string.Empty;

        await foreach (var col in this)
        {
            if (col.DataType == VarcharProtocol.RefCursorTypeCode)
            {
                name = await col.ToStringAsync(cancellationToken);
            }
            else
            {
                await col.DiscardAsync(cancellationToken);
            }
        }

        return name;
    }

    #endregion

    #region Read

    /// <summary>
    /// Reads the <see cref="DataRow"/> message from the specified connection.
    /// </summary>
    /// <param name="reader">The input stream for reading data from a database service.</param>
    /// <param name="description">The description of the row.</param>
    /// <param name="length">The length of the message body.</param>
    /// <param name="row">A predefined <see cref="DataRow"/> to populate and return.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>An object instance of the <see cref="DataRow"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask<DataRow> ReadAsync(
        Reader reader,
        RowDescription description,
        int length,
        DataRow row,
        CancellationToken cancellationToken)
    {
        Debug.Assert(length >= sizeof(short));
        var columnCount = await reader.ReadInt16Async(cancellationToken);
        length -= sizeof(short);

        Debug.Assert(columnCount == description.ColumnCount);
        row._description = description;
        row._remainingLength = length;
        row._columnEnumerator.Reset();
        return row;
    }

    #endregion

    #region ColumnAsyncEnumerator

    /// <summary>
    /// Supports asynchronous reading of columns from a <see cref="DataRow"/>.
    /// </summary>
    private sealed class ColumnAsyncEnumerator : IAsyncEnumerator<IColumn>
    {
        /// <summary>
        /// Indicates whether DisposeAsync has been called.
        /// </summary>
        private int _disposed;

        /// <summary>
        /// The column currently being read.
        /// </summary>
        private readonly DataColumn _column;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnAsyncEnumerator"/> class
        /// with the row to read data from.
        /// </summary>
        /// <param name="row">The row to read data from.</param>
        internal ColumnAsyncEnumerator(DataRow row)
        {
            _disposed = 0;
            _column = new DataColumn(row);
        }

        /// <summary>
        /// Resets this enumerator after reading a new data row.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Reset()
        {
            _column.Reset();
            _disposed = 0;
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 1)
            {
                return;
            }

            // All remaining unread data in current row is discarded.
            try
            {
                await _column.Row.DiscardAsync(default);
            }
            catch (Exception exc)
            {
                Db.Logger.LogError(exc.Message, exc);
            }

            _column.Reset();
        }

        /// <inheritdoc />
        IColumn IAsyncEnumerator<IColumn>.Current => _column;

        /// <inheritdoc />
        async ValueTask<bool> IAsyncEnumerator<IColumn>.MoveNextAsync() =>
            _disposed == 0 && await _column.MoveNextAsync();
    }

    #endregion
}
