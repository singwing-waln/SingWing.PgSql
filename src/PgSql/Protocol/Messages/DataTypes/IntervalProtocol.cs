using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.IntervalArray"/> parameters.
/// </summary>
/// <remarks>
/// <para>
/// <see href="https://doxygen.postgresql.org/structInterval.html"/> and
/// <see href="https://www.postgresql.org/docs/current/datatype-datetime.html"/>.
/// </para>
/// </remarks>
internal sealed class IntervalProtocol : IDataTypeProtocol<TimeSpan, IntervalProtocol>
{
    /// <summary>
    /// The parameter whose value is TimeSpan.Zero.
    /// </summary>
    private static readonly PrimitiveParameter<TimeSpan, IntervalProtocol> Zero = new(TimeSpan.Zero);

    /// <summary>
    /// The parameter whose value is TimeSpan.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<TimeSpan, IntervalProtocol> Min = new(TimeSpan.MinValue);

    /// <summary>
    /// The parameter whose value is TimeSpan.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<TimeSpan, IntervalProtocol> Max = new(TimeSpan.MaxValue);

    /// <summary>
    /// The binary length of the interval in PostgreSQL.
    /// </summary>
    private const int ElementBinaryLength =
        // The time part in microseconds.
        sizeof(long) +
        // The day part.
        sizeof(int) +
        // The month part (not used).
        sizeof(int);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="IntervalProtocol"/> class from outside.
    /// </summary>
    private IntervalProtocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.IntervalArray;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Interval;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in TimeSpan element) => ElementBinaryLength;

    /// <inheritdoc />
    public static async ValueTask WriteElementAsync(
        TimeSpan element,
        Writer writer,
        CancellationToken cancellationToken)
    {
        // time part
        await writer.WriteInt64Async(
            (element.Ticks - (element.Days * TimeSpan.TicksPerDay)) / 10,
            cancellationToken);
        // day part
        await writer.WriteInt32Async(element.Days, cancellationToken);
        // month part
        // PostgreSQL interval can range from -178000000 yeas to 178000000 yeas, 
        // while .NET TimeSpan has a much smaller range (about -29247 yeas to 29247 yeas) than interval,
        // so the month part is not used when sending data to the backend.
        await writer.WriteInt32Async(0, cancellationToken);
    }

    /// <inheritdoc />
    public static TimeSpan? ToElement(object? value) => Conversion.ToTimeSpan(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<TimeSpan, IntervalProtocol> parameter) =>
        ReferenceEquals(parameter, Zero) ||
        ReferenceEquals(parameter, Min) ||
        ReferenceEquals(parameter, Max);

    /// <inheritdoc />
    public static PrimitiveParameter<TimeSpan, IntervalProtocol>? PredefinedParameterFor(in TimeSpan value)
    {
        if (value == TimeSpan.Zero)
        {
            return Zero;
        }

        if (value == TimeSpan.MinValue)
        {
            return Min;
        }

        if (value == TimeSpan.MaxValue)
        {
            return Max;
        }

        return null;
    }

    /// <inheritdoc />
    public static async ValueTask<TimeSpan?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        if (elementDataTypeCode != (uint)DataType.Interval)
        {
            await reader.DiscardAsync(binaryLength, cancellationToken);
            return null;
        }

        Debug.Assert(binaryLength == ElementBinaryLength);

        var time = await reader.ReadInt64Async(cancellationToken);
        var days = await reader.ReadInt32Async(cancellationToken);
        var months = await reader.ReadInt32Async(cancellationToken);

        // https://doxygen.postgresql.org/datatype_2timestamp_8h_source.html
        // 30 days per month, 365.25 days per year, so 5.25 days need to be added every 12 months.
        var years = months / 12;
        var totalDays = days + (months * 30d) + (years * 5.25d);

        var ticks = (totalDays * TimeSpan.TicksPerDay) + (time * 10d);

        if (ticks >= TimeSpan.MaxValue.Ticks)
        {
            return TimeSpan.MaxValue;
        }

        if (ticks <= TimeSpan.MinValue.Ticks)
        {
            return TimeSpan.MinValue;
        }

        return new TimeSpan((long)ticks);
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) =>
        elementDataTypeCode == (uint)DataType.Interval;

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) =>
        arrayDataTypeCode == (uint)DataType.IntervalArray;

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => 
        arrayDataTypeCode == (uint)DataType.IntervalArray
        ? (uint)DataType.Interval
        : 0;
}
