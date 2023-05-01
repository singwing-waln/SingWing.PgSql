namespace SingWing.PgSql.Protocol.Connections;

/// <summary>
/// Represents a timeout timer for asynchronous network reads and writes.
/// </summary>
internal sealed class TimeoutTimer : IDisposable
{
    /// <summary>
    /// The connection options that provide timeout values.
    /// </summary>
    private readonly IConnectionPoolOptions _options;

    /// <summary>
    /// The operation type.
    /// </summary>
    private readonly Operation _operation;

    /// <summary>
    /// Lock for the _cancellationTokenSource.
    /// </summary>
    private readonly object _lock;

    /// <summary>
    /// Signals to the current operation that it should be time out.
    /// </summary>
    private volatile CancellationTokenSource _cancellationTokenSource;

    /// <summary>
    /// A callback delegate that has been registered with the current operation.
    /// </summary>
    private CancellationTokenRegistration _registration;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeoutTimer"/> class
    /// with the specified options and operation type.
    /// </summary>
    /// <param name="options">The connection options that provide timeout values.</param>
    /// <param name="operation">The type of asynchronous operation that may time out.</param>
    internal TimeoutTimer(IConnectionPoolOptions options, Operation operation)
    {
        _options = options;
        _operation = operation;
        _lock = new();
        _cancellationTokenSource = new();
        _registration = default;
    }

    /// <summary>
    /// Gets the time span in milliseconds.
    /// </summary>
    private int Milliseconds => _operation switch
    {
        Operation.Sending => _options.SendTimeoutSeconds * 1000,
        _ => _options.ReceiveTimeoutSeconds * 1000
    };

    /// <inheritdoc />
    public void Dispose()
    {
        lock (_lock)
        {
            _registration.Dispose();
            _cancellationTokenSource.Dispose();
        }
    }

    /// <summary>
    /// Starts the timer for an asynchronous operation.
    /// </summary>
    /// <param name="cancellationToken">The caller token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A token that can be used to cancel the asynchronous operation.</returns>
    internal CancellationToken Start(CancellationToken cancellationToken)
    {
        lock (_lock)
        {
            // Try to start the timer, if the IsCancellationRequested is true,
            // the timer will not be scheduled.
            _cancellationTokenSource.CancelAfter(Milliseconds);

            if (_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new(Milliseconds);
            }

            // If the caller's token can be cancelled, stop the timer when the token is cancelled.
            if (cancellationToken.CanBeCanceled)
            {
                _registration = cancellationToken.Register(_cancellationTokenSource.Cancel);
            }

            return _cancellationTokenSource.Token;
        }
    }

    /// <summary>
    /// Stops the timer.
    /// </summary>
    internal void Stop()
    {
        lock (_lock)
        {
            _registration.Dispose();

            if (!TryReset(_cancellationTokenSource))
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool TryReset(CancellationTokenSource cancellationTokenSource)
        {
            try
            {
                // TryReset may throw an ObjectDisposedException. 
                return cancellationTokenSource.TryReset();
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Defines the type of asynchronous operation that may time out.
    /// </summary>
    internal enum Operation
    {
        /// <summary>
        /// Receive data from the backend.
        /// </summary>
        Receiving,

        /// <summary>
        /// Send data to the backend.
        /// </summary>
        Sending
    }
}
