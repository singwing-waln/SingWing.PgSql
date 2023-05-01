using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.Int4Array"/> parameters.
/// </summary>
internal sealed class Int4Protocol : IDataTypeProtocol<int, Int4Protocol>
{
    /// <summary>
    /// The parameter whose value is 0.
    /// </summary>
    private static readonly PrimitiveParameter<int, Int4Protocol> Zero = new(0);

    /// <summary>
    /// The parameter whose value is int.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<int, Int4Protocol> Min = new(int.MinValue);

    /// <summary>
    /// The parameter whose value is int.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<int, Int4Protocol> Max = new(int.MaxValue);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="Int4Protocol"/> class from outside.
    /// </summary>
    private Int4Protocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.Int4Array;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Int4;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in int element) => sizeof(int);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        int element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteInt32Async(element, cancellationToken);

    /// <inheritdoc />
    public static int? ToElement(object? value) => Conversion.ToInt32(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<int, Int4Protocol> parameter) =>
        ReferenceEquals(parameter, Zero) ||
        ReferenceEquals(parameter, Min) ||
        ReferenceEquals(parameter, Max);

    /// <inheritdoc />
    public static PrimitiveParameter<int, Int4Protocol>? PredefinedParameterFor(in int value)
    {
        if (value == 0)
        {
            return Zero;
        }

        if (value == int.MinValue)
        {
            return Min;
        }

        if (value == int.MaxValue)
        {
            return Max;
        }

        return null;
    }

    /// <inheritdoc />
    public static async ValueTask<int?> ReadElementAsync(
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
            (uint)DataType.Int4 => await reader.ReadInt32Async(cancellationToken),
            (uint)DataType.Int2 => await reader.ReadInt16Async(cancellationToken),
            (uint)DataType.Int8 => unchecked((int?)await reader.ReadInt64Async(cancellationToken)),
            (uint)DataType.Numeric => unchecked((int?)await NumericProtocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            (uint)DataType.Float4 => unchecked((int?)await Float4Protocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            (uint)DataType.Float8 => unchecked((int?)await Float8Protocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            _ => await DiscardAsync(),
        };

        async ValueTask<int?> DiscardAsync()
        {
            await reader.DiscardAsync(binaryLength, cancellationToken);
            return null;
        }
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) => elementDataTypeCode is
        ((uint)DataType.Int4) or
        ((uint)DataType.Int2) or
        ((uint)DataType.Int8) or
        ((uint)DataType.Numeric) or
        ((uint)DataType.Float4) or
        ((uint)DataType.Float8);

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is
        ((uint)DataType.Int4Array) or
        ((uint)DataType.Int2Array) or
        ((uint)DataType.Int8Array) or
        ((uint)DataType.NumericArray) or
        ((uint)DataType.Float4Array) or
        ((uint)DataType.Float8Array);

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.Int4Array => (uint)DataType.Int4,
        (uint)DataType.Int2Array => (uint)DataType.Int2,
        (uint)DataType.Int8Array => (uint)DataType.Int8,
        (uint)DataType.NumericArray => (uint)DataType.Numeric,
        (uint)DataType.Float4Array => (uint)DataType.Float4,
        (uint)DataType.Float8Array => (uint)DataType.Float8,
        _ => 0
    };
}
