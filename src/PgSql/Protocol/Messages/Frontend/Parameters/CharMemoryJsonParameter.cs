using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.DataTypes;

namespace SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

/// <summary>
/// Represents a parameter whose value is JSON data in text format.
/// </summary>
internal sealed class CharMemoryJsonParameter : Parameter
{
    /// <summary>
    /// The parameter whose value is <see langword="null"/>.
    /// </summary>
    private static readonly CharMemoryJsonParameter Null = new(null);

    /// <summary>
    /// The number of bytes of JSON data.
    /// </summary>
    private int _binaryLength;

    /// <summary>
    /// The parameter value.
    /// </summary>
    private ReadOnlyMemory<char>? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharMemoryJsonParameter"/> class 
    /// with the specified value.
    /// </summary>
    /// <param name="value">The parameter value.</param>
    private CharMemoryJsonParameter(ReadOnlyMemory<char>? value)
    {
        _binaryLength = value is null ? -1 : Encoding.UTF8.GetByteCount(value.Value.Span);
        _value = value;
    }

    /// <inheritdoc />
    public override object? Value
    {
        get => _value;
        set
        {
            if (ReferenceEquals(this, Null))
            {
                return;
            }

            _value = Conversion.ToCharMemory(value);
            _binaryLength = _value is null ? -1 : Encoding.UTF8.GetByteCount(_value.Value.Span);
        }
    }

    /// <inheritdoc />
    public override DataType DataType => DataType.Jsonb;

    /// <inheritdoc />
    internal override int BinaryLength => _value is null ? -1 : sizeof(byte) + _binaryLength;

    /// <inheritdoc />
    public override Parameter Clone() =>
        ReferenceEquals(this, Null) ? this : (Parameter)new CharMemoryJsonParameter(_value);

    /// <inheritdoc />
    public override void Reset()
    {
        if (ReferenceEquals(this, Null))
        {
            return;
        }

        _binaryLength = -1;
        _value = null;
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

        await writer.WriteByteAsync(Versions.JsonbVersion, cancellationToken);
        await writer.WriteStringAsync(_value.Value, cancellationToken);
    }

    /// <summary>
    /// Creates or returns an instance of the <see cref="CharMemoryJsonParameter"/> class 
    /// with the specified value.
    /// </summary>
    /// <param name="value">The parameter value.</param>
    /// <returns>
    /// Returns <see cref="Null"/> if <paramref name="value"/> is <see langword="null"/> or an empty collection, 
    /// a new instance of the <see cref="CharMemoryJsonParameter"/> class.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CharMemoryJsonParameter From(ReadOnlyMemory<char>? value) =>
        value is null || value.Value.IsEmpty ? Null : new CharMemoryJsonParameter(value);
}
