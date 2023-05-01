using SingWing.PgSql.Balancing.Commands;
using SingWing.PgSql.Protocol.Connections;
using System.Threading.Tasks.Sources;

namespace SingWing.PgSql.Pooling.Commands;

/// <summary>
/// Represents a command that returns a value of the specified type.
/// </summary>
/// <typeparam name="TResult">The type of the return value.</typeparam>
/// <remarks>
/// <para>
/// <see href="https://github.com/dotnet/runtime/blob/main/src/libraries/System.Threading.Channels/src/System/Threading/Channels/AsyncOperation.cs"/>.
/// </para>
/// <para>
/// <see href="https://github.com/dotnet/corefx/blob/master/src/Common/tests/System/Threading/Tasks/Sources/ManualResetValueTaskSource.cs"/>.
/// </para>
/// </remarks>
internal abstract class Command<TResult> : ICommand, IValueTaskSource, IValueTaskSource<TResult>
{
    /// <summary>
    /// Registration with a provided cancellation token.
    /// </summary>
    private CancellationTokenRegistration _registration;

    /// <summary>
    /// The underlying task source.
    /// </summary>
    private ManualResetValueTaskSourceCore<TResult> _taskSource;

    /// <summary>
    /// Initialize a new instance of the <see cref="Command{TResult}"/> class
    /// with the specified cancellation token.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the asynchronous command.</param>
    protected Command(CancellationToken cancellationToken)
    {
        if (cancellationToken.CanBeCanceled)
        {
            CancellationToken = cancellationToken;
            _registration = cancellationToken.UnsafeRegister(static s =>
            {
                var thisRef = (Command<TResult>)s!;
                thisRef._taskSource.SetException(new OperationCanceledException(thisRef.CancellationToken));
            }, this);
        }
    }

    /// <summary>
    /// Gets a <see cref="ValueTask{TResult}"/> backed by this instance and its current token.
    /// </summary>
    internal ValueTask<TResult> ValueTaskOfResult => new(this, _taskSource.Version);

    /// <summary>
    /// Gets a <see cref="ValueTask"/> backed by this instance and its current token.
    /// </summary>
    internal ValueTask ValueTask => new(this, _taskSource.Version);

    /// <summary>
    /// Completes with a successful result.
    /// </summary>
    /// <param name="result">The result.</param>
    internal void SetResult(TResult result)
    {
        if (CancellationToken.CanBeCanceled)
        {
            _registration.Dispose();
            _registration = default;
        }

        _taskSource.SetResult(result);
    }

    #region ICommand

    /// <inheritdoc/>
    public CancellationToken CancellationToken { get; }

    /// <inheritdoc/>
    public abstract ValueTask<ConnectionReleaseState> ExecuteAsync(Connection connection);

    /// <inheritdoc/>
    public void SetException(Exception exception)
    {
        if (CancellationToken.CanBeCanceled)
        {
            _registration.Dispose();
            _registration = default;
        }

        _taskSource.SetException(exception);
    }

    #endregion

    #region IValueTaskSource

    /// <inheritdoc/>
    void IValueTaskSource.GetResult(short token) => _taskSource.GetResult(token);

    /// <inheritdoc/>
    TResult IValueTaskSource<TResult>.GetResult(short token) => _taskSource.GetResult(token);

    /// <inheritdoc/>
    public ValueTaskSourceStatus GetStatus(short token) => _taskSource.GetStatus(token);

    /// <inheritdoc/>
    public void OnCompleted(
        Action<object?> continuation,
        object? state,
        short token,
        ValueTaskSourceOnCompletedFlags flags) =>
        _taskSource.OnCompleted(continuation, state, token, flags);

    #endregion
}
