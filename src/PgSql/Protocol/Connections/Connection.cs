using SingWing.PgSql.Balancing;

namespace SingWing.PgSql.Protocol.Connections;

/// <summary>
/// Represents a network connection to a PostgreSQL server.
/// </summary>
internal sealed partial class Connection : IConnection
{
    #region Fields

    /// <summary>
    /// The ID of the next connection.
    /// </summary>
    private static ulong _nextId = 0L;

    /// <summary>
    /// Indicates whether the current connection has been closed.
    /// </summary>
    private int _closed;

    /// <summary>
    /// Indicates whether the connection is ready to execute commands.
    /// </summary>
    private int _ready;

    /// <summary>
    /// Indicates whether the connection has been broken.
    /// </summary>
    private int _broken;

    /// <summary>
    /// Indicates whether this connection has been returned to the connection pool.
    /// </summary>
    private int _released;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="Connection"/> class
    /// with the node and the underlying network stream.
    /// </summary>
    /// <param name="node">The node to connect to.</param>
    /// <param name="socket">The underlying network stream.</param>
    private Connection(Node node, Socket socket)
    {
        _closed = 0;
        _ready = 0;
        _broken = 0;
        _released = 0;

        Id = Interlocked.Increment(ref _nextId);
        Node = node;
        Transaction = new(this);
        Rows = new(this);
        Socket = socket;
        Reader = new Reader(this);
        Writer = new Writer(this);
    }

    #region Properties

    /// <summary>
    /// Gets or sets the ID of the current connection.
    /// </summary>
    internal ulong Id { get; }

    /// <summary>
    /// Gets the node to connect to.
    /// </summary>
    internal Node Node { get; }

    /// <summary>
    /// Gets a value that indicates whether the connection has been broken.
    /// </summary>
    internal bool IsBroken => _broken == 1 || !Socket.Connected;

    /// <summary>
    /// Gets or sets the error that occurred on this connection.
    /// </summary>
    internal Exception? Error { get; private set; }

    /// <summary>
    /// Gets a value that indicates whether this connection is ready to execute commands.
    /// </summary>
    internal bool IsReady => _ready == 1;

    /// <summary>
    /// Gets a value that indicates whether this connection is released 
    /// (returning or already returning to the connection pool).
    /// </summary>
    internal bool IsReleased => _released == 1;

    /// <summary>
    /// Gets the transaction on this connection.
    /// </summary>
    internal Transaction Transaction { get; }

    /// <summary>
    /// Gets the asynchronous iterator over rows.
    /// </summary>
    internal RowAsyncEnumerable Rows { get; }

    /// <summary>
    /// Gets the underlying network stream.
    /// </summary>
    internal Socket Socket { get; }

    /// <summary>
    /// Gets the reader for receiving messages from the database.
    /// </summary>
    internal Reader Reader { get; }

    /// <summary>
    /// Gets the writer for sending messages to the database.
    /// </summary>
    internal Writer Writer { get; }

    #endregion

    /// <summary>
    /// Marks the connection as broken and provide an error that indicates why.
    /// </summary>
    /// <param name="exception">The error that caused this connection to be broken.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Break(Exception? exception = null)
    {
        Error ??= exception;
        _ = Interlocked.CompareExchange(ref _broken, 1, 0);
    }

    /// <summary>
    /// Marks this connection as already in use.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Activate() => Interlocked.Exchange(ref _released, 0);

    /// <summary>
    /// Returns this connection to the connection pool.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Release()
    {
        if (Interlocked.CompareExchange(ref _released, 1, 0) != 0)
        {
            return;
        }

        Node.Release(this);
    }

    #region IAsyncDisposable

    /// <inheritdoc/>
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await Rows.DisposeAsync();
        await Transaction.DisposeAsync();
        Release();
    }

    #endregion

    #region IConnection

    /// <inheritdoc/>
    IDatabase IConnection.Database => Node.Database;

    /// <inheritdoc/>
    INode IConnection.Node => Node;

    #endregion

    #region ITransactional

    /// <inheritdoc/>
    public ValueTask<ITransaction> BeginAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken)
    {
        if (IsBroken)
        {
            throw new InvalidOperationException(Strings.ConnectionDisconnected);
        }

        if (IsReleased)
        {
            throw new InvalidOperationException(Strings.ConnectionNotReady);
        }

        return Transaction.BeginAsync(isolationLevel, cancellationToken);
    }

    #endregion
}
