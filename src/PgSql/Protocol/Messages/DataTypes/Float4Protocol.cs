using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.Float4Array"/> parameters.
/// </summary>
internal sealed class Float4Protocol : IDataTypeProtocol<float, Float4Protocol>
{
    /// <summary>
    /// The parameter whose value is 0.
    /// </summary>
    private static readonly PrimitiveParameter<float, Float4Protocol> Zero = new(0f);

    /// <summary>
    /// The parameter whose value is float.NaN.
    /// </summary>
    private static readonly PrimitiveParameter<float, Float4Protocol> NaN = new(float.NaN);

    /// <summary>
    /// The parameter whose value is float.NegativeInfinity.
    /// </summary>
    private static readonly PrimitiveParameter<float, Float4Protocol> NegativeInfinity = new(float.NegativeInfinity);

    /// <summary>
    /// The parameter whose value is float.PositiveInfinity.
    /// </summary>
    private static readonly PrimitiveParameter<float, Float4Protocol> PositiveInfinity = new(float.PositiveInfinity);

    /// <summary>
    /// The parameter whose value is float.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<float, Float4Protocol> Min = new(float.MinValue);

    /// <summary>
    /// The parameter whose value is float.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<float, Float4Protocol> Max = new(float.MaxValue);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="Float4Protocol"/> class from outside.
    /// </summary>
    private Float4Protocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.Float4Array;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Float4;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in float element) => sizeof(float);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        float element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteInt32Async(
            Unsafe.As<float, int>(ref element),
            cancellationToken);

    /// <inheritdoc />
    public static float? ToElement(object? value) => Conversion.ToSingle(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<float, Float4Protocol> parameter) =>
        ReferenceEquals(parameter, Zero) ||
        ReferenceEquals(parameter, NaN) ||
        ReferenceEquals(parameter, Min) ||
        ReferenceEquals(parameter, Max) ||
        ReferenceEquals(parameter, NegativeInfinity) ||
        ReferenceEquals(parameter, PositiveInfinity);

    /// <inheritdoc />
    public static PrimitiveParameter<float, Float4Protocol>? PredefinedParameterFor(in float value)
    {
        if (value == 0f)
        {
            return Zero;
        }

        if (float.IsNaN(value))
        {
            return NaN;
        }

        if (float.IsNegativeInfinity(value))
        {
            return NegativeInfinity;
        }

        if (float.IsPositiveInfinity(value))
        {
            return PositiveInfinity;
        }

        if (value == float.MinValue)
        {
            return Min;
        }

        if (value == float.MaxValue)
        {
            return Max;
        }
        
        return null;
    }

    /// <inheritdoc />
    public static async ValueTask<float?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        switch (elementDataTypeCode)
        {
            case (uint)DataType.Float4:
                Debug.Assert(binaryLength == sizeof(float));
                var n = await reader.ReadInt32Async(cancellationToken);
                return Unsafe.As<int, float>(ref n);
            case (uint)DataType.Float8:
                return unchecked((float?)await Float8Protocol.ReadElementAsync(
                    elementDataTypeCode, binaryLength, reader, cancellationToken));
            case (uint)DataType.Numeric:
                return unchecked((float?)await NumericProtocol.ReadElementAsync(
                    elementDataTypeCode, binaryLength, reader, cancellationToken));
            case (uint)DataType.Int2:
                return await reader.ReadInt16Async(cancellationToken);
            case (uint)DataType.Int4:
                return await reader.ReadInt32Async(cancellationToken);
            case (uint)DataType.Int8:
                return await reader.ReadInt64Async(cancellationToken);
            default:
                await reader.DiscardAsync(binaryLength, cancellationToken);
                return null;
        }
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) => elementDataTypeCode is
        ((uint)DataType.Float4) or
        ((uint)DataType.Float8) or
        ((uint)DataType.Numeric) or
        ((uint)DataType.Int2) or
        ((uint)DataType.Int4) or
        ((uint)DataType.Int8);

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is
        ((uint)DataType.Float4Array) or
        ((uint)DataType.Float8Array) or
        ((uint)DataType.NumericArray) or
        ((uint)DataType.Int2Array) or
        ((uint)DataType.Int4Array) or
        ((uint)DataType.Int8Array);

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.Float4Array => (uint)DataType.Float4,
        (uint)DataType.Float8Array => (uint)DataType.Float8,
        (uint)DataType.NumericArray => (uint)DataType.Numeric,
        (uint)DataType.Int2Array => (uint)DataType.Int2,
        (uint)DataType.Int4Array => (uint)DataType.Int4,
        (uint)DataType.Int8Array => (uint)DataType.Int8,
        _ => 0
    };
}
