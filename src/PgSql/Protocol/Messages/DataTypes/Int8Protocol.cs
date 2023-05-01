using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.Int8Array"/> parameters.
/// </summary>
internal sealed class Int8Protocol : IDataTypeProtocol<long, Int8Protocol>
{
    /// <summary>
    /// The parameter whose value is 0.
    /// </summary>
    private static readonly PrimitiveParameter<long, Int8Protocol> Zero = new(0);

    /// <summary>
    /// The parameter whose value is long.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<long, Int8Protocol> Min = new(long.MinValue);

    /// <summary>
    /// The parameter whose value is long.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<long, Int8Protocol> Max = new(long.MaxValue);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="Int8Protocol"/> class from outside.
    /// </summary>
    private Int8Protocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.Int8Array;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Int8;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in long element) => sizeof(long);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        long element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteInt64Async(element, cancellationToken);

    /// <inheritdoc />
    public static long? ToElement(object? value) => Conversion.ToInt64(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<long, Int8Protocol> parameter) =>
        ReferenceEquals(parameter, Zero) ||
        ReferenceEquals(parameter, Min) ||
        ReferenceEquals(parameter, Max);

    /// <inheritdoc />
    public static PrimitiveParameter<long, Int8Protocol>? PredefinedParameterFor(in long value)
    {
        if (value == 0)
        {
            return Zero;
        }

        if (value == long.MinValue)
        {
            return Min;
        }

        if (value == long.MaxValue)
        {
            return Max;
        }

        return null;
    }

    /// <inheritdoc />
    public static async ValueTask<long?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        return elementDataTypeCode switch
        {
            (uint)DataType.Int8 => await reader.ReadInt64Async(cancellationToken),
            (uint)DataType.Int4 => await reader.ReadInt32Async(cancellationToken),
            (uint)DataType.Int2 => await reader.ReadInt16Async(cancellationToken),
            (uint)DataType.Numeric => unchecked((long?)await NumericProtocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            (uint)DataType.Float4 => unchecked((long?)await Float4Protocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            (uint)DataType.Float8 => unchecked((long?)await Float8Protocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            _ => await DiscardAsync(),
        };

        async ValueTask<long?> DiscardAsync()
        {
            await reader.DiscardAsync(binaryLength, cancellationToken);
            return null;
        }
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) => elementDataTypeCode is
        ((uint)DataType.Int8) or
        ((uint)DataType.Int4) or
        ((uint)DataType.Int2) or
        ((uint)DataType.Numeric) or
        ((uint)DataType.Float4) or
        ((uint)DataType.Float8);

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is
        ((uint)DataType.Int8Array) or
        ((uint)DataType.Int4Array) or
        ((uint)DataType.Int2Array) or
        ((uint)DataType.NumericArray) or
        ((uint)DataType.Float4Array) or
        ((uint)DataType.Float8Array);

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.Int8Array => (uint)DataType.Int8,
        (uint)DataType.Int4Array => (uint)DataType.Int4,
        (uint)DataType.Int2Array => (uint)DataType.Int2,
        (uint)DataType.NumericArray => (uint)DataType.Numeric,
        (uint)DataType.Float4Array => (uint)DataType.Float4,
        (uint)DataType.Float8Array => (uint)DataType.Float8,
        _ => 0
    };
}
