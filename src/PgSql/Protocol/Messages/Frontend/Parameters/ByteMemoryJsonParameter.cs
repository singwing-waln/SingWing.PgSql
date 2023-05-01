using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.DataTypes;

namespace SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

/// <summary>
/// Represents a parameter whose value is JSON data in binary format.
/// </summary>
internal sealed class ByteMemoryJsonParameter : Parameter
{
    /// <summary>
    /// The parameter whose value is <see langword="null"/>.
    /// </summary>
    private static readonly ByteMemoryJsonParameter Null = new(null);

    /// <summary>
    /// The parameter value.
    /// </summary>
    private ReadOnlyMemory<byte>? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="ByteMemoryJsonParameter"/> class 
    /// with the specified value.
    /// </summary>
    /// <param name="value">The parameter value.</param>
    internal ByteMemoryJsonParameter(ReadOnlyMemory<byte>? value) => _value = value;

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

            if (value is string @string)
            {
                _value = Encoding.UTF8.GetBytes(@string);
                return;
            }

            _value = Conversion.ToByteMemory(value);
        }
    }

    /// <inheritdoc />
    public override DataType DataType => DataType.Jsonb;

    /// <inheritdoc />
    internal override int BinaryLength => _value is null ? -1 : sizeof(byte) + _value.Value.Length;

    /// <inheritdoc />
    public override Parameter Clone() =>
        ReferenceEquals(this, Null) ? this : (Parameter)new ByteMemoryJsonParameter(_value);

    /// <inheritdoc />
    public override void Reset()
    {
        if (ReferenceEquals(this, Null))
        {
            return;
        }

        _value = null;
    }

    /// <inheritdoc />
    internal override async ValueTask WriteAsync(
        Writer writer,
        CancellationToken cancellationToken)
    {
        if (_value is null || _value.Value.IsEmpty)
        {
            return;
        }

        await writer.WriteByteAsync(Versions.JsonbVersion, cancellationToken);
        await writer.WriteBinaryAsync(_value.Value, cancellationToken);
    }

    /// <summary>
    /// Creates or returns an instance of the <see cref="ByteMemoryJsonParameter"/> class 
    /// with the specified value.
    /// </summary>
    /// <param name="value">The parameter value.</param>
    /// <returns>
    /// Returns <see cref="Null"/> if <paramref name="value"/> is <see langword="null"/> or an empty collection, 
    /// a new instance of the <see cref="ByteMemoryJsonParameter"/> class.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ByteMemoryJsonParameter From(ReadOnlyMemory<byte>? value) =>
        value is null || value.Value.IsEmpty ? Null : new ByteMemoryJsonParameter(value);
}
