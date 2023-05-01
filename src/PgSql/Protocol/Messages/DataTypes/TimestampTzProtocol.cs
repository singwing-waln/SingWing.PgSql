using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.TimestampTzArray"/> parameters.
/// </summary>
internal sealed class TimestampTzProtocol : IDataTypeProtocol<DateTime, TimestampTzProtocol>
{
    /// <summary>
    /// Date is days since "2000-01-01", timestamp is microseconds since "2000-01-01 00:00:00".
    /// </summary>
    /// <remarks>
    /// <see href="https://doxygen.postgresql.org/date_8c.html#aedc511e4369bd41f42fb198e70c10827"/>.
    /// </remarks>
    private static readonly DateTime EpochDateTime = new(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// The tick corresponding to EpochDateTime in .NET.
    /// </summary>
    private static readonly long EpochDateTimeTicks = EpochDateTime.Ticks;

    /// <summary>
    /// The ticks for "0001-01-01 00:00:00" relative to EpochDateTime.
    /// </summary>
    private static readonly long MinDateTimeTicks = PgSqlTicksOf(DateTime.MinValue);

    /// <summary>
    /// The ticks for "9999-12-31 23:59:59" relative to EpochDateTime.
    /// </summary>
    private static readonly long MaxDateTimeTicks = PgSqlTicksOf(DateTime.MaxValue);

    /// <summary>
    /// Timestamp without time zone (timestamp).
    /// </summary>
    internal const uint TimestampTypeCode = 1114;

    /// <summary>
    /// One-dimensional array of timestamp without time zone (timestamp) (timestamp[]).
    /// </summary>
    internal const uint TimestampArrayTypeCode = 1115;

    /// <summary>
    /// The parameter whose value is DateTime.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<DateTime, TimestampTzProtocol> Min = new(DateTime.MinValue);

    /// <summary>
    /// The parameter whose value is DateTime.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<DateTime, TimestampTzProtocol> Max = new(DateTime.MaxValue);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="TimestampTzProtocol"/> class from outside.
    /// </summary>
    private TimestampTzProtocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.TimestampTzArray;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.TimestampTz;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in DateTime element) => sizeof(long);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        DateTime element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteInt64Async(PgSqlTicksOf(element), cancellationToken);

    /// <inheritdoc />
    public static DateTime? ToElement(object? value) => Conversion.ToDateTime(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<DateTime, TimestampTzProtocol> parameter) =>
        ReferenceEquals(parameter, Min) || ReferenceEquals(parameter, Max);

    /// <inheritdoc />
    public static PrimitiveParameter<DateTime, TimestampTzProtocol>? PredefinedParameterFor(in DateTime value) =>
        value == DateTime.MinValue ? Min : value == DateTime.MaxValue ? Max : null;

    /// <inheritdoc />
    public static async ValueTask<DateTime?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        if (elementDataTypeCode is ((uint)DataType.TimestampTz) or TimestampTypeCode)
        {
            Debug.Assert(binaryLength == sizeof(long));
            var ticks = await reader.ReadInt64Async(cancellationToken);

            ticks *= 10;

            if (ticks <= MinDateTimeTicks)
            {
                return DateTime.MinValue;
            }

            if (ticks >= MaxDateTimeTicks)
            {
                return DateTime.MaxValue;
            }

            return new DateTime(ticks + EpochDateTimeTicks, DateTimeKind.Utc).ToLocalTime();
        }

        if (elementDataTypeCode == (uint)DataType.Date)
        {
            var date = await DateProtocol.ReadElementAsync(
                elementDataTypeCode, binaryLength, reader, cancellationToken);
            return date.HasValue
                ? new DateTime(
                    date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0, DateTimeKind.Local)
                : null;
        }

        await reader.DiscardAsync(binaryLength, cancellationToken);
        return null;
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) => elementDataTypeCode is
        ((uint)DataType.TimestampTz) or TimestampTypeCode or ((uint)DataType.Date);

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is
        ((uint)DataType.TimestampTzArray) or TimestampArrayTypeCode or ((uint)DataType.DateArray);

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.TimestampTzArray => (uint)DataType.TimestampTz,
        TimestampArrayTypeCode => TimestampTypeCode,
        (uint)DataType.DateArray => (uint)DataType.Date,
        _ => 0
    };

    /// <summary>
    /// Calculates the ticks in PostgreSQL for the specified datetime,
    /// that is, the number of microseconds that have passed since "2000-01-01 00:00:00".
    /// </summary>
    /// <param name="datetime">The datetime to calculate its ticks.</param>
    /// <returns>The number of microseconds since "2000-01-01 00:00:00" for <paramref name="datetime"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long PgSqlTicksOf(DateTime datetime) =>
        (datetime.ToUniversalTime().Ticks - EpochDateTimeTicks) / 10;
}
