using System.Buffers;

namespace SingWing.PgSql;

/// <summary>
/// Provides methods for reading column values from a data row.
/// </summary>
public interface IColumn
{
    #region Information

    /// <summary>
    /// Gets the 0-based position of the column in the row.
    /// </summary>
    int Ordinal { get; }

    /// <summary>
    /// Gets the name of the column.
    /// </summary>
    ReadOnlySpan<char> Name { get; }

    /// <summary>
    /// Gets the OID of the column data type.
    /// </summary>
    /// <remarks>
    /// <para>The data type code returned by this property may not be defined in the <see cref="DataType"/> enum.</para>
    /// <para>If a data type is not supported, the binary data of that type will be discarded.</para>
    /// <para><see href="https://github.com/postgres/postgres/blob/master/src/include/catalog/pg_type.dat"/>.</para>
    /// </remarks>
    uint DataType { get; }

    /// <summary>
    /// Gets a value that indicates whether the value of this column is <see langword="null"/>.
    /// </summary>
    bool IsNull { get; }

    /// <summary>
    /// Gets the number of elements in the column when the data type is an array.
    /// </summary>
    /// <value>Less than 0 means the column is not an array type, or the value is a database null.</value>
    int ArrayLength { get; }

    #endregion

    #region Discard

    /// <summary>
    /// Reads and discards all remaining data in the column.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    ValueTask DiscardAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Binary

    /// <summary>
    /// Reads the binary data of the column, and converts it to a ReadOnlyMemory&lt;<see langword="byte"/>&gt;? value. 
    /// The <see cref="DataType"/> must be <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read ReadOnlyMemory&lt;<see langword="byte"/>&gt;? value.
    /// <see langword="null"/> if the value is a database null, or the data type is not <see cref="DataType.Bytea"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If you want to write the binary data to a buffer or stream, 
    /// you can use one of the WriteAsync methods.
    /// </para>
    /// <para>
    /// The returned memory may be overwritten the next time the new data is read, 
    /// so if the caller wants to keep the returned data for a long time or reuse it, 
    /// they should be copied to the caller's buffer.
    /// </para>
    /// </remarks>
    ValueTask<ReadOnlyMemory<byte>?> AsByteMemoryAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the binary data of the column, 
    /// and converts it to a nullable asynchronous collection of ReadOnlyMemory&lt;<see langword="byte"/>&gt;?.
    /// The <see cref="DataType"/> must be <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over ReadOnlyMemory&lt;<see langword="byte"/>&gt;?.
    /// <see langword="null"/> if the value is a database null, or the data type is not <see cref="DataType.ByteaArray"/>.
    /// </returns>
    ValueTask<IAsyncEnumerable<ReadOnlyMemory<byte>?>?> AsByteMemoriesAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Boolean

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Boolean"/>,
    /// and converts it to a <see langword="bool"/>? value.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read <see langword="bool"/> value.
    /// <see langword="null"/> if the data type is not <see cref="DataType.Boolean"/>, 
    /// or the value is a database null.
    /// </returns>
    ValueTask<bool?> AsBooleanAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.BooleanArray"/>,
    /// and converts it to a nullable asynchronous collection of <see langword="bool"/>? values.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over <see langword="bool"/>? values.
    /// <see langword="null"/> if the data type is not <see cref="DataType.BooleanArray"/>, 
    /// or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<bool?>?> AsBooleansAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Date

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>,
    /// and converts it to a DateOnly? value.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read DateOnly? value.
    /// <see langword="null"/> if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, 
    /// or the value is a database null.
    /// </returns>
    ValueTask<DateOnly?> AsDateAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>,
    /// and converts it to a nullable asynchronous collection of DateOnly? values.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over DateOnly? values. 
    /// <see langword="null"/> if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, 
    /// or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<DateOnly?>?> AsDatesAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region DateTime

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>,
    /// and converts it to a local DateTime? value.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A DateTime? value whose Kind property is Local.
    /// <see langword="null"/> if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, 
    /// or the value is a database null.
    /// </returns>
    ValueTask<DateTime?> AsDateTimeAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>,
    /// and converts it to a nullable asynchronous collection of local DateTime? values.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over local DateTime? values. 
    /// <see langword="null"/> if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, 
    /// or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<DateTime?>?> AsDateTimesAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Decimal

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a nullable <see langword="decimal"/> value in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable <see langword="decimal"/> value.
    /// <see langword="null"/> if the data type is not number, or the value is a database null.
    /// </returns>
    ValueTask<decimal?> AsDecimalAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to a nullable asynchronous collection of <see langword="decimal"/>? values the in unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over <see langword="decimal"/>? values. 
    /// <see langword="null"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<decimal?>?> AsDecimalsAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Double

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="double"/>? value in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable <see langword="double"/> value.
    /// <see langword="null"/> if the data type is not number, or the value is a database null.
    /// </returns>
    ValueTask<double?> AsDoubleAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to a nullable asynchronous collection of <see langword="double"/>? values in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over <see langword="double"/>? values. 
    /// <see langword="null"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<double?>?> AsDoublesAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Guid

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Uuid"/>,
    /// and converts it to a Guid? value.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable Guid value.
    /// <see langword="null"/> if the data type is not <see cref="DataType.Uuid"/>, or the value is a database null.
    /// </returns>
    ValueTask<Guid?> AsGuidAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.UuidArray"/>,
    /// and converts it to a nullable asynchronous collection of Guid? values.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over Guid? values. 
    /// <see langword="null"/> if the data type is not <see cref="DataType.UuidArray"/>, 
    /// or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<Guid?>?> AsGuidsAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Int16

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a nullable <see langword="short"/> value in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="short"/>? value.
    /// <see langword="null"/> if the data type is not number, or the value is a database null.
    /// </returns>
    ValueTask<short?> AsInt16Async(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to a nullable asynchronous collection of <see langword="short"/> values in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over <see langword="short"/>? values. 
    /// <see langword="null"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<short?>?> AsInt16sAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Int32

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a nullable <see langword="int"/> value in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An <see langword="int"/>? value.
    /// <see langword="null"/> if the data type is not number, or the value is a database null.
    /// </returns>
    ValueTask<int?> AsInt32Async(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to a nullable asynchronous collection of <see langword="int"/> values in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over <see langword="int"/>? values. 
    /// <see langword="null"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<int?>?> AsInt32sAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Int64

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a nullable <see langword="long"/> value in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="long"/>? value.
    /// <see langword="null"/> if the data type is not number, or the value is a database null.
    /// </returns>
    ValueTask<long?> AsInt64Async(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to a nullable asynchronous collection of <see langword="long"/> values in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over <see langword="long"/>? values. 
    /// <see langword="null"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<long?>?> AsInt64sAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Single

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="float"/>? value in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="float"/>? value.
    /// <see langword="null"/> if the data type is not number, or the value is a database null.
    /// </returns>
    ValueTask<float?> AsSingleAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to a nullable asynchronous collection of <see langword="float"/>? values in the unchecked context.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over <see langword="float"/>? values. 
    /// <see langword="null"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<float?>?> AsSinglesAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region String

    /// <summary>
    /// Reads the value of the column of data type string, 
    /// and converts it to a ReadOnlyMemory&lt;<see langword="char"/>&gt;? value.
    /// The <see cref="DataType"/> must be a string type code.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A ReadOnlyMemory&lt;<see langword="char"/>&gt;? value.
    /// <see langword="null"/> if the data type is not string, or the value is a database null.
    /// </returns>
    ValueTask<ReadOnlyMemory<char>?> AsCharMemoryAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type string array,
    /// and converts it to a nullable asynchronous collection of ReadOnlyMemory&lt;<see langword="char"/>&gt;? values.
    /// The <see cref="DataType"/> must be a string array type code.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over ReadOnlyMemory&lt;<see langword="char"/>&gt;? values. 
    /// <see langword="null"/> if the data type is not string array, or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<ReadOnlyMemory<char>?>?> AsCharMemoriesAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region Time

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Time"/>,
    /// and converts it to a TimeOnly? value.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read TimeOnly? value.
    /// <see langword="null"/> if the data type is not <see cref="DataType.Time"/>, or the value is a database null.
    /// </returns>
    /// <remarks>
    /// Due to the difference in precision, the value returned for TimeOnly.MaxValue (23:59:59.9999999) is: 23:59:59.999999.
    /// </remarks>
    ValueTask<TimeOnly?> AsTimeAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.TimeArray"/>,
    /// and converts it to a nullable asynchronous collection of TimeOnly? values.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over TimeOnly? values. 
    /// <see langword="null"/> if the data type is not <see cref="DataType.TimeArray"/>, 
    /// or the value is a database null.
    /// </returns>
    /// <remarks>
    /// Due to the difference in precision, the value returned for TimeOnly.MaxValue (23:59:59.9999999) is: 23:59:59.999999.
    /// </remarks>
    ValueTask<IAsyncEnumerable<TimeOnly?>?> AsTimesAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region TimeSpan

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Interval"/>,
    /// and converts it to a TimeSpan? value.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A TimeSpan? value.
    /// <see langword="null"/> if the data type is not <see cref="DataType.Interval"/>, or the value is a database null.
    /// </returns>
    ValueTask<TimeSpan?> AsTimeSpanAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.IntervalArray"/>,
    /// and converts it to a nullable asynchronous collection of TimeSpan? values.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over TimeSpan? values. 
    /// <see langword="null"/> if the data type is not <see cref="DataType.IntervalArray"/>, 
    /// or the value is a database null.
    /// </returns>
    ValueTask<IAsyncEnumerable<TimeSpan?>?> AsTimeSpansAsync(
        CancellationToken cancellationToken = default);

    #endregion

    #region WriteBinary

    /// <summary>
    /// Writes the binary data of the column to the specified buffer.
    /// If the <see cref="DataType"/> is not <see cref="DataType.Bytea"/>, the data is discarded.
    /// </summary>
    /// <param name="buffer">The buffer to which data will be written.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>The number of bytes written to the <paramref name="buffer"/>, -1 if the data type is not <see cref="DataType.Bytea"/>.</returns>
    ValueTask<int> WriteAsync(
        Memory<byte> buffer,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes the binary data of the column to the specified buffer writer.
    /// If the <see cref="DataType"/> is not <see cref="DataType.Bytea"/>, the data is discarded.
    /// </summary>
    /// <param name="writer">The buffer writer to which data will be written.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>The number of bytes written to the buffer <paramref name="writer"/>, -1 if the data type is not <see cref="DataType.Bytea"/>.</returns>
    ValueTask<int> WriteAsync(
        IBufferWriter<byte> writer,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes the binary data of the column to the specified stream.
    /// If the <see cref="DataType"/> is not <see cref="DataType.Bytea"/>, the data is discarded.
    /// </summary>
    /// <param name="stream">The stream to which data will be written.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>The number of bytes written to the <paramref name="stream"/>, -1 if the data type is not <see cref="DataType.Bytea"/>.</returns>
    ValueTask<int> WriteAsync(
        Stream stream,
        CancellationToken cancellationToken = default);

    #endregion

    #region WriteString

    /// <summary>
    /// Writes the binary data of the column as a string to the specified buffer.
    /// If the <see cref="DataType"/> is not a string type code, the data is discarded.
    /// </summary>
    /// <param name="buffer">The buffer to which string will be written.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>The number of characters written to the <paramref name="buffer"/>, -1 if the data type is not a string type.</returns>
    ValueTask<int> WriteAsync(
        Memory<char> buffer,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes the binary data of the column as a string to the specified buffer writer.
    /// If the <see cref="DataType"/> is not a string type code, the data is discarded.
    /// </summary>
    /// <param name="writer">The buffer writer to which string will be written.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>The number of characters written to the buffer <paramref name="writer"/>, -1 if the data type is not a string type.</returns>
    ValueTask<int> WriteAsync(
        IBufferWriter<char> writer,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes the binary data of the column as a string to the specified text writer.
    /// If the <see cref="DataType"/> is not a string type code, the data is discarded.
    /// </summary>
    /// <param name="writer">The text writer to which string will be written.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>The number of characters written to the text <paramref name="writer"/>, -1 if the data type is not a string type.</returns>
    ValueTask<int> WriteAsync(
        TextWriter writer,
        CancellationToken cancellationToken = default);

    #endregion

    #region Json

    /// <summary>
    /// Reads the binary data of the column of data type JSON.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read ReadOnlyMemory&lt;<see langword="byte"/>&gt;? value.
    /// <see langword="null"/> if the value is a database null, or the data type is not JSON.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The returned memory may be overwritten the next time the new data is read, 
    /// so if the caller wants to keep the returned data for a long time or reuse it, 
    /// they should be copied to the caller's buffer.
    /// </para>
    /// </remarks>
    ValueTask<ReadOnlyMemory<byte>?> AsJsonBinaryAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Writes the value of the column to the specified writer as a JSON property value.
    /// </summary>
    /// <param name="writer">The JSON writer to which value will be written.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <remarks>
    /// <para>
    /// The NaN, Infinity, and -Infinity of <see langword="float"/> and <see langword="double"/> are written as 
    /// the strings "NaN", "Infinity", and "-Infinity", respectively.
    /// </para>
    /// <para>
    /// The binary values are written as base64 encoded JSON strings.
    /// </para>
    /// <para>
    /// For unsupported data types, discards the data in the column and writes a null to the JSON writer.
    /// </para>
    /// <para>This method does not call <paramref name="writer"/>.FlushAsync.</para>
    /// </remarks>
    ValueTask WriteValueAsync(
        Utf8JsonWriter writer,
        CancellationToken cancellationToken = default);

    #endregion
}
