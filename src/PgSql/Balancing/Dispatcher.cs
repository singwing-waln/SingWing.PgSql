using SingWing.PgSql.Balancing.Commands;
using SingWing.PgSql.Pooling.Commands;
using SingWing.PgSql.Protocol.Connections;
using System.Globalization;
using System.Threading.Channels;

namespace SingWing.PgSql.Balancing;

/// <summary>
/// Dispatches commands to the underlying connection pool(s).
/// </summary>
internal abstract class Dispatcher : IDisposable
{
    /// <summary>
    /// The queue of commands to execute.
    /// </summary>
    private readonly CommandQueue _commands;

    /// <summary>
    /// Indicates whether this dispatcher is running a loop for queued commands.
    /// </summary>
    private int _running;

    /// <summary>
    /// Initializes a new instance of the <see cref="Dispatcher"/> class.
    /// </summary>
    protected Dispatcher()
    {
        _commands = new();
        _running = 0;
    }

    /// <summary>
    /// Gets the current database name.
    /// </summary>
    internal abstract string DatabaseName { get; }

    /// <summary>
    /// Gets the number of commands queued for execution.
    /// </summary>
    internal int CommandCount => _commands.Count;

    /// <summary>
    /// Gets the number of idle connections.
    /// </summary>
    internal abstract int IdleConnectionCount { get; }

    /// <summary>
    /// Acquires an idle connection for executing the specified command from the underlying connection pool.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The acquired connection.</returns>
    internal abstract ValueTask<Connection> AcquireAsync(
        ICommand command,
        CancellationToken cancellationToken);

    /// <summary>
    /// Clears the command queue.
    /// </summary>
    public virtual void Dispose() => _commands.Dispose();

    /// <summary>
    /// Prunes the specified number of connections.
    /// </summary>
    /// <param name="countPruning">The number of connections to prune.</param>
    /// <returns>The number of connections that were actually pruned.</returns>
    internal abstract int PruneConnections(int countPruning);

    /// <summary>
    /// Prunes the specified number of connections from the specified nodes or databases.
    /// </summary>
    /// <typeparam name="T">The type of nodes or databases.</typeparam>
    /// <param name="dispatchers">The collection of nodes or databases.</param>
    /// <param name="countPruning">The number of connections to prune.</param>
    /// <returns>The number of connections that were actually pruned.</returns>
    internal static int PruneConnections<T>(ICollection<T> dispatchers, int countPruning) where T : Dispatcher
    {
        if (countPruning < 1)
        {
            return 0;
        }

        var items = new (T, int)[dispatchers.Count];

        // The total number of connections of all dispatchers.
        var total = 0;
        var i = 0;
        foreach (var dispatcher in dispatchers)
        {
            var count = dispatcher.IdleConnectionCount;
            items[i++] = (dispatcher, count);
            total += count;
        }

        if (total == 0)
        {
            return 0;
        }

        // Sort descending so that dispatchers with more connections are closed first.
        Array.Sort(items, (a, b) => b.Item2 - a.Item2);

        var countClosed = 0;
        for (i = 0; i < items.Length; i++)
        {
            var item = items[i];
            var count = (int)Math.Round((double)item.Item2 * countPruning / total);
            countClosed += item.Item1.PruneConnections(count);

            if (countClosed >= countPruning)
            {
                break;
            }
        }

        return countClosed;
    }

    /// <summary>
    /// Adds the specified command to the end of the queue.
    /// </summary>
    /// <typeparam name="TResult">The type of the return value for the command.</typeparam>
    /// <param name="command">The command to queue for execution.</param>
    protected ValueTask<TResult> AppendCommand<TResult>(Command<TResult> command)
    {
        var valueTask = command.ValueTaskOfResult;

        if (!_commands.TryEnqueue(command))
        {
            command.SetException(new InvalidOperationException(
                string.Format(CultureInfo.CurrentCulture, Strings.DatabaseTooBusy, DatabaseName)));
        }
        else
        {
            EnsureExecutionLoop();
        }

        return valueTask;
    }

    /// <summary>
    /// Adds the specified command to the end of the queue.
    /// </summary>
    /// <param name="command">The command to queue for execution.</param>
    protected ValueTask AppendCommand(Command<VoidResult> command)
    {
        var valueTask = command.ValueTask;

        if (!_commands.TryEnqueue(command))
        {
            command.SetException(new InvalidOperationException(
                string.Format(CultureInfo.CurrentCulture, Strings.DatabaseTooBusy, DatabaseName)));
        }
        else
        {
            EnsureExecutionLoop();
        }

        return valueTask;
    }

    /// <summary>
    /// Ensures that the dispatcher is dispatching the commands.
    /// </summary>
    private void EnsureExecutionLoop()
    {
        if (Interlocked.CompareExchange(ref _running, 1, 0) == 1)
        {
            return;
        }

        _ = Task
            .Run(ExecutionLoop)
            .ContinueWith(OnExecutionLoopCompleted);

        async Task ExecutionLoop()
        {
            while (await _commands.WaitForDequeueAsync())
            {
                while (_commands.TryDequeue(out var command))
                {
                    if (command is not null)
                    {
                        Dispatch(command);
                    }
                }
            }

            // Dispatch a command in the thread pool.
            void Dispatch(ICommand command)
            {
                _ = Task
                    .Run(async () =>
                    {
                        var released = true;
                        Connection connection;

                        try
                        {
                            connection = await AcquireAsync(command, command.CancellationToken);
                        }
                        catch (Exception exc)
                        {
                            command.SetException(exc);
                            return;
                        }

                        try
                        {
                            released = await command.ExecuteAsync(connection) == ConnectionReleaseState.Released;
                        }
                        catch (Exception exc)
                        {
                            connection.Release();
                            command.SetException(exc);
                            return;
                        }

                        if (released)
                        {
                            connection.Release();
                        }
                    })
                    .ContinueWith(task =>
                    {
                        if (task.Exception is not null)
                        {
                            command.SetException(task.Exception);
                        }
                    });
            }
        }

        void OnExecutionLoopCompleted(Task task)
        {
            _ = Interlocked.Exchange(ref _running, 0);

            if (task.Exception is null)
            {
                return;
            }

            foreach (var exc in task.Exception.InnerExceptions)
            {
                Db.Logger.LogError(exc.Message, exc);
            }
        }
    }

    #region CommandQueue

    /// <summary>
    /// Represents a queue of commands to be executed on a database.
    /// </summary>
    private sealed class CommandQueue : IDisposable
    {
        /// <summary>
        /// The maximum number of commands queued: 1M.
        /// </summary>
        private const int MaxCommands = 1024 * 1024;

        /// <summary>
        /// The reader that read commands from the queue.
        /// </summary>
        private readonly ChannelReader<ICommand> _reader;

        /// <summary>
        /// The writer that write commands to the queue.
        /// </summary>
        private readonly ChannelWriter<ICommand> _writer;

        /// <summary>
        /// On Dispose, cancel reads and writes.
        /// </summary>
        private readonly CancellationTokenSource _dispositionTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandQueue"/> class.
        /// </summary>
        internal CommandQueue()
        {
            var channel = Channel.CreateBounded<ICommand>(new BoundedChannelOptions(MaxCommands)
            {
                FullMode = BoundedChannelFullMode.DropWrite,
                SingleReader = false,
                SingleWriter = false,
                AllowSynchronousContinuations = false
            });
            _reader = channel.Reader;
            _writer = channel.Writer;
            _dispositionTokenSource = new();
        }

        /// <summary>
        /// Gets the number of commands queued for execution.
        /// </summary>
        internal int Count => _reader.Count;

        /// <summary>
        /// Adds a command to the queue. If the queue is full, return immediately.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns><see langword="true"/> if the command was enqueued, <see langword="false"/> otherwise.</returns>
        internal bool TryEnqueue(ICommand command) => _writer.TryWrite(command);

        /// <summary>
        /// Waits until the queue is not empty.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if at least one command can be dequeued; otherwise, 
        /// <see langword="false"/> if the underlying channel completes.
        /// </returns>
        internal ValueTask<bool> WaitForDequeueAsync() =>
            _reader.WaitToReadAsync(_dispositionTokenSource.Token);

        /// <summary>
        /// Dequeues a command to execute, does not wait.
        /// </summary>
        /// <param name="command">The dequeued command.</param>
        /// <returns><see langword="true"/> if the command was dequeued, <see langword="false"/> otherwise.</returns>
        internal bool TryDequeue(out ICommand? command) => _reader.TryRead(out command);

        /// <summary>
        /// Marks as completed and clears the queue.
        /// </summary>
        public void Dispose()
        {
            lock (_dispositionTokenSource)
            {
                if (_dispositionTokenSource.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    _dispositionTokenSource.Cancel();
                }
                catch
                {
                }

                _dispositionTokenSource.Dispose();
            }

            try
            {
                // Mark the channel as complete.
                _ = _writer.TryComplete();

                // Clear the queue.
                while (_reader.TryRead(out var command))
                {
                    command.SetException(new OperationCanceledException());
                }
            }
            catch
            {
            }
        }
    }

    #endregion
}
