using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

/// <summary>
/// Represents a parameter whose value is JSON data in a binary stream.
/// </summary>
internal sealed class StreamJsonParameter : Parameter
{
    /// <summary>
    /// The parameter whose value is <see langword="null"/>.
    /// </summary>
    internal static readonly StreamJsonParameter Null = new(null, -1, false);

    /// <summary>
    /// The number of bytes of JSON data.
    /// </summary>
    private int _binaryLength;

    /// <summary>
    /// The parameter value.
    /// </summary>
    private Stream? _value;

    /// <summary>
    /// Indicates whether the current parameter owns the target stream. 
    /// If true, closes the data stream after sending to the server.
    /// </summary>
    private bool _ownsStream;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharMemoryJsonParameter"/> class 
    /// with the specified value.
    /// </summary>
    /// <param name="value">The parameter value.</param>
    /// <param name="binaryLength">The number of bytes of JSON data in the stream.</param>
    /// <param name="ownsStream">Indicates whether the parameter owns the stream.</param>
    private StreamJsonParameter(Stream? value, int binaryLength, bool ownsStream)
    {
        if (value is null || ReferenceEquals(value, Stream.Null))
        {
            _binaryLength = -1;
            _value = null;
            _ownsStream = false;
        }
        else
        {
            _binaryLength = binaryLength < 0 ? (int)value.Length : binaryLength;
            _value = value;
            _ownsStream = ownsStream;
        }
    }

    /// <inheritdoc />
    public override object? Value
    {
        get => _value;
        set
        {
            if (ReferenceEquals(_value, value))
            {
                return;
            }

            if (_ownsStream)
            {
                _value?.Dispose();
            }

            _value = value as Stream;
        }
    }

    /// <inheritdoc />
    public override DataType DataType => DataType.Jsonb;

    /// <inheritdoc />
    internal override int BinaryLength => _value is null ? -1 : sizeof(byte) + _binaryLength;

    /// <inheritdoc />
    public override Parameter Clone() =>
        ReferenceEquals(this, Null)
        ? this
        : (Parameter)new StreamJsonParameter(_value, _binaryLength, _ownsStream);

    /// <inheritdoc />
    public override void Reset()
    {
        if (ReferenceEquals(this, Null))
        {
            return;
        }

        if (_ownsStream)
        {
            _value?.Dispose();
        }

        _binaryLength = -1;
        _value = null;
        _ownsStream = false;
    }

    /// <inheritdoc />
    internal override async ValueTask WriteAsync(
        Writer writer,
        CancellationToken cancellationToken)
    {
        if (_value is null || _binaryLength <= 0)
        {
            return;
        }

        try
        {
            await writer.WriteByteAsync(Versions.JsonbVersion, cancellationToken);
            await writer.WriteStreamAsync(_value, _binaryLength, cancellationToken);
        }
        finally
        {
            if (_ownsStream)
            {
                _value.Dispose();
            }
        }
    }

    /// <summary>
    /// Creates or returns an instance of the <see cref="StreamJsonParameter"/> class 
    /// with the specified value.
    /// </summary>
    /// <param name="value">The parameter value.</param>
    /// <param name="binaryLength">The number of bytes of JSON data in the stream.</param>
    /// <param name="ownsStream">Indicates whether the parameter owns the stream.</param>
    /// <returns>
    /// Returns <see cref="Null"/> if <paramref name="value"/> is <see langword="null"/> or Stream.Null, 
    /// a new instance of the <see cref="StreamJsonParameter"/> class.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static StreamJsonParameter From(Stream? value, int binaryLength, bool ownsStream) =>
        value is null || ReferenceEquals(value, Stream.Null)
        ? Null
        : new StreamJsonParameter(value, binaryLength, ownsStream);
}
