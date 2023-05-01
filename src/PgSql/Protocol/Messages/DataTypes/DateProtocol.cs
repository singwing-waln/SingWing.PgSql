using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.DateArray"/> parameters.
/// </summary>
internal sealed class DateProtocol : IDataTypeProtocol<DateOnly, DateProtocol>
{
    /// <summary>
    /// Date is days since 2000.
    /// </summary>
    /// <remarks>
    /// <code>#define POSTGRES_EPOCH_JDATE    2451545 /* == date2j(2000, 1, 1) */</code>
    /// <see href="https://doxygen.postgresql.org/datatype_2timestamp_8h_source.html"/>.
    /// </remarks>
    private static readonly DateOnly EpochDate = new(2000, 1, 1);

    /// <summary>
    /// The tick corresponding to EpochDate in .NET.
    /// </summary>
    private static readonly int EpochDateDayNumber = EpochDate.DayNumber;

    /// <summary>
    /// The ticks for "0001-01-01" relative to EpochDate.
    /// </summary>
    private static readonly int MinDateTicks = PgSqlTicksOf(DateOnly.MinValue);

    /// <summary>
    /// The ticks for "9999-12-31" relative to EpochDate.
    /// </summary>
    private static readonly int MaxDateTicks = PgSqlTicksOf(DateOnly.MaxValue);

    /// <summary>
    /// The parameter whose value is DateOnly.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<DateOnly, DateProtocol> Min = new(DateOnly.MinValue);

    /// <summary>
    /// The parameter whose value is DateOnly.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<DateOnly, DateProtocol> Max = new(DateOnly.MaxValue);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="DateProtocol"/> class from outside.
    /// </summary>
    private DateProtocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.DateArray;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Date;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in DateOnly element) => sizeof(int);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        DateOnly element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteInt32Async(PgSqlTicksOf(element), cancellationToken);

    /// <inheritdoc />
    public static DateOnly? ToElement(object? value) => Conversion.ToDateOnly(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<DateOnly, DateProtocol> parameter) =>
        ReferenceEquals(parameter, Min) || ReferenceEquals(parameter, Max);

    /// <inheritdoc />
    public static PrimitiveParameter<DateOnly, DateProtocol>? PredefinedParameterFor(in DateOnly value) =>
        value == DateOnly.MinValue ? Min : value == DateOnly.MaxValue ? Max : null;

    /// <inheritdoc />
    public static async ValueTask<DateOnly?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        if (elementDataTypeCode == (uint)DataType.Date)
        {
            Debug.Assert(binaryLength == sizeof(int));
            var ticks = await reader.ReadInt32Async(cancellationToken);
            if (ticks <= MinDateTicks)
            {
                return DateOnly.MinValue;
            }

            if (ticks >= MaxDateTicks)
            {
                return DateOnly.MaxValue;
            }

            return DateOnly.FromDayNumber(ticks + EpochDateDayNumber);
        }

        if (elementDataTypeCode is ((uint)DataType.TimestampTz) or TimestampTzProtocol.TimestampTypeCode)
        {
            var dateTime = await TimestampTzProtocol.ReadElementAsync(
                elementDataTypeCode, binaryLength, reader, cancellationToken);
            return dateTime.HasValue
                ? new DateOnly(
                    dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day)
                : null;
        }

        await reader.DiscardAsync(binaryLength, cancellationToken);
        return null;
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) => elementDataTypeCode is
        ((uint)DataType.Date) or ((uint)DataType.TimestampTz) or TimestampTzProtocol.TimestampTypeCode;

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is
        ((uint)DataType.DateArray) or
        ((uint)DataType.TimestampTzArray) or
        TimestampTzProtocol.TimestampArrayTypeCode;

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.DateArray => (uint)DataType.Date,
        (uint)DataType.TimestampTzArray => (uint)DataType.TimestampTz,
        TimestampTzProtocol.TimestampArrayTypeCode => TimestampTzProtocol.TimestampTypeCode,
        _ => 0
    };

    /// <summary>
    /// Calculates the ticks in PostgreSQL for the specified date, 
    /// that is, the number of days since "2000-01-01".
    /// </summary>
    /// <param name="date">The specified date.</param>
    /// <returns>The number of days since "2000-01-01" for <paramref name="date"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int PgSqlTicksOf(DateOnly date) => date.DayNumber - EpochDateDayNumber;
}
