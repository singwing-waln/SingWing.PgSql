using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.Float8Array"/> parameters.
/// </summary>
internal sealed class Float8Protocol : IDataTypeProtocol<double, Float8Protocol>
{
    /// <summary>
    /// The parameter whose value is 0.
    /// </summary>
    private static readonly PrimitiveParameter<double, Float8Protocol> Zero = new(0f);

    /// <summary>
    /// The parameter whose value is double.NaN.
    /// </summary>
    private static readonly PrimitiveParameter<double, Float8Protocol> NaN = new(double.NaN);

    /// <summary>
    /// The parameter whose value is double.NegativeInfinity.
    /// </summary>
    private static readonly PrimitiveParameter<double, Float8Protocol> NegativeInfinity = new(double.NegativeInfinity);

    /// <summary>
    /// The parameter whose value is double.PositiveInfinity.
    /// </summary>
    private static readonly PrimitiveParameter<double, Float8Protocol> PositiveInfinity = new(double.PositiveInfinity);

    /// <summary>
    /// The parameter whose value is double.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<double, Float8Protocol> Min = new(double.MinValue);

    /// <summary>
    /// The parameter whose value is double.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<double, Float8Protocol> Max = new(double.MaxValue);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="Float8Protocol"/> class from outside.
    /// </summary>
    private Float8Protocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.Float8Array;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Float8;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in double element) => sizeof(double);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        double element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteInt64Async(
            Unsafe.As<double, long>(ref element),
            cancellationToken);

    /// <inheritdoc />
    public static double? ToElement(object? value) => Conversion.ToDouble(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<double, Float8Protocol> parameter) =>
        ReferenceEquals(parameter, Zero) ||
        ReferenceEquals(parameter, NaN) ||
        ReferenceEquals(parameter, Min) ||
        ReferenceEquals(parameter, Max) ||
        ReferenceEquals(parameter, NegativeInfinity) ||
        ReferenceEquals(parameter, PositiveInfinity);

    /// <inheritdoc />
    public static PrimitiveParameter<double, Float8Protocol>? PredefinedParameterFor(in double value)
    {
        if (value == 0f)
        {
            return Zero;
        }

        if (double.IsNaN(value))
        {
            return NaN;
        }

        if (double.IsNegativeInfinity(value))
        {
            return NegativeInfinity;
        }

        if (double.IsPositiveInfinity(value))
        {
            return PositiveInfinity;
        }

        if (value == double.MinValue)
        {
            return Min;
        }

        if (value == double.MaxValue)
        {
            return Max;
        }

        return null;
    }

    /// <inheritdoc />
    public static async ValueTask<double?> ReadElementAsync(
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
            case (uint)DataType.Float8:
                var n = await reader.ReadInt64Async(cancellationToken);
                return Unsafe.As<long, double>(ref n);
            case (uint)DataType.Float4:
                return await Float4Protocol.ReadElementAsync(
                    elementDataTypeCode, binaryLength, reader, cancellationToken);
            case (uint)DataType.Numeric:
                return unchecked((double?)await NumericProtocol.ReadElementAsync(
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
        ((uint)DataType.Float8) or
        ((uint)DataType.Float4) or
        ((uint)DataType.Numeric) or
        ((uint)DataType.Int2) or
        ((uint)DataType.Int4) or
        ((uint)DataType.Int8);

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is 
        ((uint)DataType.Float8Array) or
        ((uint)DataType.Float4Array) or
        ((uint)DataType.NumericArray) or
        ((uint)DataType.Int2Array) or
        ((uint)DataType.Int4Array) or
        ((uint)DataType.Int8Array);

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.Float8Array => (uint)DataType.Float8,
        (uint)DataType.Float4Array => (uint)DataType.Float4,
        (uint)DataType.NumericArray => (uint)DataType.Numeric,
        (uint)DataType.Int2Array => (uint)DataType.Int2,
        (uint)DataType.Int4Array => (uint)DataType.Int4,
        (uint)DataType.Int8Array => (uint)DataType.Int8,
        _ => 0
    };
}
