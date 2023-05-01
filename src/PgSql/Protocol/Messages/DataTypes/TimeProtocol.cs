using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.TimeArray"/> parameters.
/// </summary>
internal sealed class TimeProtocol : IDataTypeProtocol<TimeOnly, TimeProtocol>
{
    /// <summary>
    /// Time of day (with time zone) (timetz).
    /// </summary>
    internal const uint TimeTzTypeCode = 1266;

    /// <summary>
    /// One-dimensional array of timetzs (time of day, with time zone) (timetz[]).
    /// </summary>
    internal const uint TimeTzArrayTypeCode = 1270;

    /// <summary>
    /// The parameter whose value is TimeOnly.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<TimeOnly, TimeProtocol> Min = new(TimeOnly.MinValue);

    /// <summary>
    /// The parameter whose value is TimeOnly.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<TimeOnly, TimeProtocol> Max = new(TimeOnly.MaxValue);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="TimeProtocol"/> class from outside.
    /// </summary>
    private TimeProtocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.TimeArray;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Time;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in TimeOnly element) => sizeof(long);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        TimeOnly element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteInt64Async(element.Ticks / 10, cancellationToken);

    /// <inheritdoc />
    public static TimeOnly? ToElement(object? value) => Conversion.ToTimeOnly(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<TimeOnly, TimeProtocol> parameter) =>
        ReferenceEquals(parameter, Min) || ReferenceEquals(parameter, Max);

    /// <inheritdoc />
    public static PrimitiveParameter<TimeOnly, TimeProtocol>? PredefinedParameterFor(in TimeOnly value) =>
        value == TimeOnly.MinValue ? Min : value == TimeOnly.MaxValue ? Max : null;

    /// <inheritdoc />
    public static async ValueTask<TimeOnly?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        if (elementDataTypeCode == (uint)DataType.Time)
        {
            Debug.Assert(binaryLength == sizeof(long));

            var microseconds = await reader.ReadInt64Async(cancellationToken);
            return TicksToTimeOnly(microseconds * 10);
        }

        if (elementDataTypeCode == TimeTzTypeCode)
        {
            Debug.Assert(binaryLength == (sizeof(long) + sizeof(int)));

            var microseconds = await reader.ReadInt64Async(cancellationToken);
            // zone is ignored.
            _ = await reader.ReadInt32Async(cancellationToken);

            return TicksToTimeOnly(microseconds * 10);
        }

        await reader.DiscardAsync(binaryLength, cancellationToken);
        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static TimeOnly TicksToTimeOnly(long ticks)
        {
            return ticks > TimeOnly.MaxValue.Ticks
                ? TimeOnly.MaxValue
                : ticks < TimeOnly.MinValue.Ticks ? TimeOnly.MinValue : new TimeOnly(ticks);
        }
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) => elementDataTypeCode is
        ((uint)DataType.Time) or TimeTzTypeCode;

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is
        ((uint)DataType.TimeArray) or TimeTzArrayTypeCode;

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.TimeArray => (uint)DataType.Time,
        TimeTzArrayTypeCode => TimeTzTypeCode,
        _ => 0
    };
}
