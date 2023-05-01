using SingWing.PgSql.Protocol.Messages;
using SingWing.PgSql.Protocol.Messages.Backend;
using SingWing.PgSql.Protocol.Messages.Frontend;
using System.Globalization;

namespace SingWing.PgSql.Protocol.Connections;

internal sealed partial class Connection
{
    /// <inheritdoc/>
    public async ValueTask<IAsyncEnumerable<IRow>> QueryAsync(
       ReadOnlyMemory<char> commandText,
       ReadOnlyMemory<Parameter> parameters,
       CancellationToken cancellationToken)
    {
        // The maximum number of parameters for a single command (65535).
        // 
        // https://doxygen.postgresql.org/libpq-fe_8h.html#afcd90c8ad3fd816d18282eb622678c25
        // #define PQ_QUERY_PARAM_MAX_LIMIT   65535
        const int MaxParameterCount = 65535;

        if (commandText.IsEmpty)
        {
            throw new ArgumentException(Strings.CommandTextIsEmpty, nameof(commandText));
        }

        if (parameters.Length > MaxParameterCount)
        {
            throw new ArgumentException(string.Format(
                CultureInfo.CurrentCulture,
                Strings.TooManyParameters,
                MaxParameterCount),
                nameof(parameters));
        }

        // Extended Query:
        // https://www.postgresql.org/docs/current/protocol-flow.html#PROTOCOL-FLOW-EXT-QUERY
        CheckReady();

        var command = Node.Database.ExtendedQueryCache.Make(commandText, parameters);
        var writer = Writer;

        try
        {
            // Send extended query messages.
            await writer.WriteParseMessageAsync(command.ParseMessageBinary, cancellationToken);
            await writer.WriteBindMessageAsync(parameters, cancellationToken);
            await writer.WriteDescribeMessageAsync(cancellationToken);
            await writer.WriteExecuteMessageAsync(cancellationToken);
            await writer.WriteSyncMessageAsync(cancellationToken);
            await writer.FlushAsync(cancellationToken);
        }
        catch (Exception exc)
        {
            Break(exc);
            throw;
        }

        Rows.Reset(cancellationToken);
        return Rows;
    }

    /// <inheritdoc/>
    public async ValueTask<long> ExecuteAsync(
       ReadOnlyMemory<char> commandText,
       ReadOnlyMemory<Parameter> parameters,
       CancellationToken cancellationToken)
    {
        var rows = await QueryAsync(commandText, parameters, cancellationToken);

        try
        {
            // Discards all data rows.
            await foreach (var row in rows)
            {
                await row.DiscardAsync(cancellationToken);
            }
        }
        catch (Exception exc)
        {
            if (exc is not ServerException serverException ||
                serverException.Error.IsUnrecoverable)
            {
                Break(exc);
            }

            throw;
        }

        return Rows.AffectedRows;
    }

    /// <inheritdoc/>
    public async ValueTask PerformAsync(
       ReadOnlyMemory<char> statements,
       CancellationToken cancellationToken)
    {
        if (statements.IsEmpty)
        {
            throw new ArgumentException(Strings.CommandTextIsEmpty, nameof(statements));
        }

        CheckReady();

        try
        {
            await Writer.SendQueryMessageAsync(statements, cancellationToken);
            await WaitReadyForSimpleQueryAsync(cancellationToken);
        }
        catch (Exception exc)
        {
            if (exc is not ServerException serverException ||
                serverException.Error.IsUnrecoverable)
            {
                Break(exc);
            }

            throw;
        }
    }

    /// <summary>
    /// Executes a predefined simple query.
    /// </summary>
    /// <param name="query">The predefined simple query.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask SimpleQueryAsync(
        PredefinedSimpleQuery query,
        CancellationToken cancellationToken = default)
    {
        CheckReady();

        try
        {
            await Writer.SendQueryMessageAsync(query, cancellationToken);
            await WaitReadyForSimpleQueryAsync(cancellationToken);
        }
        catch (Exception exc)
        {
            if (exc is not ServerException serverException ||
                serverException.Error.IsUnrecoverable)
            {
                Break(exc);
            }

            throw;
        }
    }

    /// <summary>
    /// Sends a Sync message to the server.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask SyncAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await Writer.WriteSyncMessageAsync(cancellationToken);
            await Writer.FlushAsync(cancellationToken);
            await WaitReadyForSimpleQueryAsync(cancellationToken);
        }
        catch (Exception exc)
        {
            if (exc is not ServerException serverException ||
                serverException.Error.IsUnrecoverable)
            {
                Break(exc);
            }

            throw;
        }
    }

    /// <summary>
    /// Sets this connection to be ready to execute commands.
    /// </summary>
    internal void SetReady() => _ = Interlocked.Exchange(ref _ready, 1);

    /// <summary>
    /// Sets the connection to busy. If the connection is not ready to execute commands, an error is generated.
    /// </summary>
    /// <exception cref="InvalidOperationException">The connection is not ready to execute commands.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckReady()
    {
        if (IsBroken)
        {
            throw new InvalidOperationException(Strings.ConnectionDisconnected);
        }

        if (_released != 0 || Interlocked.CompareExchange(ref _ready, 0, 1) == 0)
        {
            throw new InvalidOperationException(Strings.ConnectionNotReady);
        }
    }

    #region WaitReadyForSimpleQueryAsync

    /// <summary>
    /// After sending the Query, waits for the server to respond with a ReadyForQuery.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="ServerException">An ErrorResponse message received.</exception>
    private async ValueTask WaitReadyForSimpleQueryAsync(CancellationToken cancellationToken)
    {
        // The ErrorResponse received before ReadyForQuery.
        ErrorResponse? error = null;

        while (true)
        {
            IBackendMessage message;

            try
            {
                message = await Reader.ReadMessageAsync(cancellationToken);
            }
            catch (Exception exc)
            {
                Break(exc);
                throw;
            }

            if (message.Type == BackendMessageType.ReadyForQuery)
            {
                Interlocked.Exchange(ref _ready, 1);

                if (error is not null)
                {
                    throw new ServerException(error);
                }

                return;
            }

            if (message is ErrorResponse errorResponse)
            {
                if (errorResponse.IsUnrecoverable)
                {
                    var exc = new ServerException(errorResponse);
                    Break(exc);
                    throw exc;
                }

                error ??= errorResponse;
            }

            if (message is NoticeResponse notice)
            {
                notice.Log();
            }
        }
    }

    #endregion
}
