using SingWing.PgSql.Protocol.Messages;
using SingWing.PgSql.Protocol.Messages.Backend;
using SingWing.PgSql.Protocol.Messages.Frontend;

namespace SingWing.PgSql.Protocol.Connections;

/// <summary>
/// Supports a simple asynchronous iteration over a result set.
/// </summary>
internal sealed class RowAsyncEnumerable : IAsyncEnumerable<IRow>, IAsyncEnumerator<IRow>, IDisposable
{
    /// <summary>
    /// The cancellation token that can be used to cancel the asynchronous iteration.
    /// </summary>
    private CancellationToken _cancellationToken;

    /// <summary>
    /// Indicates whether the result set has been closed.
    /// </summary>
    private int _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="RowAsyncEnumerable"/> class
    /// with the specified underlying connection.
    /// </summary>
    /// <param name="connection">The underlying connection.</param>
    internal RowAsyncEnumerable(Connection connection)
    {
        _cancellationToken = default;
        _disposed = 1;
        Connection = connection;
        Row = new(connection);
        AffectedRows = 0L;
        OwnsConnection = false;
    }

    /// <summary>
    /// Gets the connection of the iteration.
    /// </summary>
    internal Connection Connection { get; }

    /// <summary>
    /// Gets the data row currently being read.
    /// </summary>
    internal DataRow Row { get; }

    /// <summary>
    /// Gets the number of rows inserted, deleted, updated, moved, copied or retrieved.
    /// </summary>
    internal long AffectedRows { get; private set; }

    /// <summary>
    /// Gets or sets a value that indicates whether this result set should also return the underlying connection when dispose.
    /// </summary>
    internal bool OwnsConnection { get; set; }

    /// <summary>
    /// Resets this result set with the specified cancellation token.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous iteration.</param>
    internal void Reset(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        _disposed = 0;
        AffectedRows = 0L;
        OwnsConnection = false;
    }

    #region IAsyncEnumerable

    /// <inheritdoc />
    IAsyncEnumerator<IRow> IAsyncEnumerable<IRow>.GetAsyncEnumerator(CancellationToken cancellationToken) => this;

    #endregion

    #region IAsyncEnumerator

    /// <inheritdoc />
    IRow IAsyncEnumerator<IRow>.Current => Row;

    /// <inheritdoc />
    async ValueTask<bool> IAsyncEnumerator<IRow>.MoveNextAsync()
    {
        ErrorResponse? recoverableError;

        try
        {
            await Row.DiscardAsync(_cancellationToken);
            recoverableError = await ReadRowAsync();
        }
        catch (Exception exc)
        {
            if (exc is not ServerException serverException ||
                serverException.Error.IsUnrecoverable)
            {
                Connection.Break(exc);
            }

            throw;
        }

        return recoverableError is null ? !Connection.IsReady : throw new ServerException(recoverableError);

        async ValueTask<ErrorResponse?> ReadRowAsync()
        {
            while (true)
            {
                var message = await Connection.Reader.ReadMessageAsync(_cancellationToken);

                switch (message.Type)
                {
                    case BackendMessageType.DataRow:
                        return null;
                    case BackendMessageType.CommandComplete:
                        // Retrieves the number of rows in the command tag.
                        AffectedRows = ((CommandComplete)message).RowCount;
                        break;
                    case BackendMessageType.ReadyForQuery:
                        // The commands are all over and ready to execute the next command.
                        Connection.SetReady();
                        return null;
                    case BackendMessageType.PortalSuspended:
                        // Sends an Execute message to the server.
                        await Connection.Writer.SendExecuteMessageAsync(_cancellationToken);
                        break;
                    case BackendMessageType.CopyBothResponse:
                    case BackendMessageType.CopyInResponse:
                        // Streaming copy and COPY are not supported, only sends a CopyDone message.
                        await Connection.Writer.SendCopyDoneMessageAsync(_cancellationToken);
                        break;
                    case BackendMessageType.ErrorResponse:
                        var error = (ErrorResponse)message;
                        if (error.IsUnrecoverable)
                        {
                            throw new ServerException(error);
                        }

                        await DiscardRowsAndWaitForReadyAsync(_cancellationToken);

                        // A recoverable error.
                        return error;
                    case BackendMessageType.NoticeResponse:
                        ((NoticeResponse)message).Log();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    #endregion

    #region IAsyncDisposable

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 1)
        {
            return;
        }

        var cancellationToken = _cancellationToken;
        _cancellationToken = default;

        try
        {
            await EndAsync(cancellationToken);
        }
        finally
        {
            if (OwnsConnection)
            {
                Connection.Release();
            }
        }

        async ValueTask EndAsync(CancellationToken cancellationToken)
        {
            if (Connection.IsBroken)
            {
                return;
            }

            try
            {
                // Reads and discards all remaining rows in the result set.
                await Row.DisposeAsync();
                await DiscardRowsAndWaitForReadyAsync(cancellationToken);
            }
            catch (Exception exc)
            {
                if (exc is not ServerException serverException ||
                    serverException.Error.IsUnrecoverable)
                {
                    Connection.Break(exc);
                }

                Db.Logger.LogError(exc.Message, exc);
            }
        }
    }

    /// <summary>
    /// Discards all rows and waits for ready.
    /// </summary>
    public void Dispose()
    {
        DisposeAsync().AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    #endregion

    #region DiscardRowsAndWaitForReadyAsync

    /// <summary>
    /// Discards rows and Waits for the server to respond with a ReadyForQuery.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="ServerException">The server sent an unrecoverable error.</exception>
    private async ValueTask DiscardRowsAndWaitForReadyAsync(CancellationToken cancellationToken)
    {
        while (!Connection.IsReady && !Connection.IsBroken)
        {
            var message = await Connection.Reader.ReadMessageAsync(cancellationToken);

            switch (message.Type)
            {
                case BackendMessageType.DataRow:
                    await ((DataRow)message).DiscardAsync(cancellationToken);
                    break;
                case BackendMessageType.CommandComplete:
                    // Retrieves the number of rows in the command tag.
                    AffectedRows = ((CommandComplete)message).RowCount;
                    break;
                case BackendMessageType.ReadyForQuery:
                    // The commands are all over and ready to execute the next command.
                    Connection.SetReady();
                    return;
                case BackendMessageType.PortalSuspended:
                    // Sends an Execute message to the server.
                    await Connection.Writer.SendExecuteMessageAsync(cancellationToken);
                    break;
                case BackendMessageType.CopyBothResponse:
                case BackendMessageType.CopyInResponse:
                    // Streaming copy and COPY are not supported, only sends a CopyDone message.
                    await Connection.Writer.SendCopyDoneMessageAsync(cancellationToken);
                    break;
                case BackendMessageType.ErrorResponse:
                    var error = (ErrorResponse)message;
                    if (error.IsUnrecoverable)
                    {
                        throw new ServerException(error);
                    }

                    // A recoverable error.
                    error.Log();
                    break;
                case BackendMessageType.NoticeResponse:
                    ((NoticeResponse)message).Log();
                    break;
                default:
                    break;
            }
        }
    }

    #endregion
}
