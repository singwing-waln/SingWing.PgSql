using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.Int2Array"/> parameters.
/// </summary>
internal sealed class Int2Protocol : IDataTypeProtocol<short, Int2Protocol>
{
    /// <summary>
    /// The parameter whose value is 0.
    /// </summary>
    private static readonly PrimitiveParameter<short, Int2Protocol> Zero = new(0);

    /// <summary>
    /// The parameter whose value is short.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<short, Int2Protocol> Min = new(short.MinValue);

    /// <summary>
    /// The parameter whose value is short.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<short, Int2Protocol> Max = new(short.MaxValue);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="Int2Protocol"/> class from outside.
    /// </summary>
    private Int2Protocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.Int2Array;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Int2;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in short element) => sizeof(short);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        short element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteInt16Async(element, cancellationToken);

    /// <inheritdoc />
    public static short? ToElement(object? value) => Conversion.ToInt16(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<short, Int2Protocol> parameter) =>
        ReferenceEquals(parameter, Zero) ||
        ReferenceEquals(parameter, Min) ||
        ReferenceEquals(parameter, Max);

    /// <inheritdoc />
    public static PrimitiveParameter<short, Int2Protocol>? PredefinedParameterFor(in short value)
    {
        if (value == 0)
        {
            return Zero;
        }

        if (value == short.MinValue)
        {
            return Min;
        }

        if (value == short.MaxValue)
        {
            return Max;
        }

        return null;
    }

    /// <inheritdoc />
    public static async ValueTask<short?> ReadElementAsync(
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
            (uint)DataType.Int2 => await reader.ReadInt16Async(cancellationToken),
            (uint)DataType.Int4 => unchecked((short?)await reader.ReadInt32Async(cancellationToken)),
            (uint)DataType.Int8 => unchecked((short?)await reader.ReadInt64Async(cancellationToken)),
            (uint)DataType.Numeric => unchecked((short?)await NumericProtocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            (uint)DataType.Float4 => unchecked((short?)await Float4Protocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            (uint)DataType.Float8 => unchecked((short?)await Float8Protocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            _ => await DiscardAsync(),
        };

        async ValueTask<short?> DiscardAsync()
        {
            await reader.DiscardAsync(binaryLength, cancellationToken);
            return null;
        }
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) => elementDataTypeCode is 
        ((uint)DataType.Int2) or
        ((uint)DataType.Int4) or
        ((uint)DataType.Int8) or
        ((uint)DataType.Numeric) or
        ((uint)DataType.Float4) or
        ((uint)DataType.Float8);

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is 
        ((uint)DataType.Int2Array) or
        ((uint)DataType.Int4Array) or
        ((uint)DataType.Int8Array) or
        ((uint)DataType.NumericArray) or
        ((uint)DataType.Float4Array) or
        ((uint)DataType.Float8Array);

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.Int2Array => (uint)DataType.Int2,
        (uint)DataType.Int4Array => (uint)DataType.Int4,
        (uint)DataType.Int8Array => (uint)DataType.Int8,
        (uint)DataType.NumericArray => (uint)DataType.Numeric,
        (uint)DataType.Float4Array => (uint)DataType.Float4,
        (uint)DataType.Float8Array => (uint)DataType.Float8,
        _ => 0
    };
}
