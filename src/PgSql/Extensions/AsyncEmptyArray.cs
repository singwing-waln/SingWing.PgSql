namespace SingWing.PgSql;

/// <summary>
/// Represents the asynchronous empty array value of an <see cref="IColumn"/>.
/// </summary>
/// <typeparam name="T">The type of elements in the array.</typeparam>
internal sealed class AsyncEmptyArray<T> : IAsyncEnumerable<T>
{
    /// <summary>
    /// A shared singleton instance of the <see cref="AsyncEmptyArray{T}"/> class.
    /// </summary>
    /// <remarks>
    /// When getting the value of a column, this instance is returned if the value of that column cannot be converted to 
    /// an array of the specified type, or the column value is a database null.
    /// </remarks>
    internal static readonly AsyncEmptyArray<T> Shared = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncEmptyArray{T}"/> class.
    /// </summary>
    private AsyncEmptyArray() { }

    /// <inheritdoc/>
    IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken) =>
        Enumerator.Shared;

    /// <summary>
    /// Supports the asynchronous iteration over an <see cref="AsyncEmptyArray{T}"/>.
    /// </summary>
    private sealed class Enumerator : IAsyncEnumerator<T>
    {
        /// <summary>
        /// A shared singleton instance of the <see cref="Enumerator"/> class.
        /// </summary>
        internal static readonly Enumerator Shared = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Enumerator"/> class.
        /// </summary>
        private Enumerator() { }

        /// <inheritdoc/>
        T IAsyncEnumerator<T>.Current => throw new InvalidOperationException(Strings.NeedsToCallMoveNext);

        /// <inheritdoc/>
        ValueTask IAsyncDisposable.DisposeAsync() => ValueTask.CompletedTask;

        /// <inheritdoc/>
        ValueTask<bool> IAsyncEnumerator<T>.MoveNextAsync() => new(false);
    }
}
