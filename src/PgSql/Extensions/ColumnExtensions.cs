namespace SingWing.PgSql;

/// <summary>
/// Provides extension methods to <see cref="IColumn"/>.
/// </summary>
public static class ColumnExtensions
{
    #region Information

    /// <summary>
    /// Determines whether the name of the column is equal to the specified name, 
    /// the comparison is case-insensitive.
    /// </summary>
    /// <param name="column">The <see cref="IColumn"/> to compare its name.</param>
    /// <param name="name">The unquoted name to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the column name is equal to <paramref name="name"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool NameIs(this IColumn column, in ReadOnlySpan<char> name) =>
        column.Name.Equals(name, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Determines whether the name of the column is equal to the specified name, 
    /// the comparison is case-insensitive.
    /// </summary>
    /// <param name="column">The <see cref="IColumn"/> to compare its name.</param>
    /// <param name="name">The unquoted name to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the column name is equal to <paramref name="name"/>,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool NameIs(this IColumn column, string name) =>
        column.Name.Equals(name, StringComparison.OrdinalIgnoreCase);

    #endregion

    #region Binary

    /// <summary>
    /// Reads the binary data of the column, and converts it to a ReadOnlyMemory&lt;<see langword="byte"/>&gt; value.
    /// The column data type must be <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="defaultValue">The default value returned if the column value is a database null or conversion fails.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read ReadOnlyMemory&lt;<see langword="byte"/>&gt; value.
    /// <paramref name="defaultValue"/> if the value is a database null, or the data type is not <see cref="DataType.Bytea"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If the caller wants to write the binary data to a buffer or stream, 
    /// it is recommended to use one of the WriteAsync methods.
    /// </para>
    /// <para>
    /// The returned memory may be overwritten the next time the new data is read, 
    /// so if the caller wants to keep the returned data for a long time or reuse it, 
    /// they should be copied to the caller's buffer.
    /// </para>
    /// </remarks>
    public static async ValueTask<ReadOnlyMemory<byte>> ToByteMemoryAsync(
        this IColumn column,
        ReadOnlyMemory<byte> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsByteMemoryAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the binary data of the column, and converts it to a ReadOnlyMemory&lt;<see langword="byte"/>&gt; value.
    /// The column data type must be <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read ReadOnlyMemory&lt;<see langword="byte"/>&gt; value.
    /// An empty collection if the value is a database null, or the data type is not <see cref="DataType.ByteaArray"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If the caller wants to write the binary data to a buffer or stream, 
    /// it is recommended to use one of the WriteAsync methods.
    /// </para>
    /// <para>
    /// The returned memory may be overwritten the next time the new data is read, 
    /// so if the caller wants to keep the returned data for a long time or reuse it, 
    /// they should be copied to the caller's buffer.
    /// </para>
    /// </remarks>
    public static async ValueTask<ReadOnlyMemory<byte>> ToByteMemoryAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsByteMemoryAsync(cancellationToken) ?? ReadOnlyMemory<byte>.Empty;

    /// <summary>
    /// Reads the binary data of the column, 
    /// and converts it to an asynchronous collection of ReadOnlyMemory&lt;<see langword="byte"/>&gt;?.
    /// The column data type must be <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="defaultValue">The default value returned if the column value is a database null or conversion fails.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over ReadOnlyMemory&lt;<see langword="byte"/>&gt;?.
    /// <paramref name="defaultValue"/> if the value is a database null, or the data type is not <see cref="DataType.ByteaArray"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The returned memory may be overwritten the next time the new data is read, 
    /// so if the caller wants to keep the returned data for a long time or reuse it, 
    /// they should be copied to the caller's buffer.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<ReadOnlyMemory<byte>?>> ToByteMemoriesAsync(
        this IColumn column,
        IAsyncEnumerable<ReadOnlyMemory<byte>?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsByteMemoriesAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the binary data of the column, 
    /// and converts it to an asynchronous collection of ReadOnlyMemory&lt;<see langword="byte"/>&gt;?.
    /// The column data type must be <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over ReadOnlyMemory&lt;<see langword="byte"/>&gt;?.
    /// An empty collection if the value is a database null, or the data type is not <see cref="DataType.ByteaArray"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The returned memory may be overwritten the next time the new data is read, 
    /// so if the caller wants to keep the returned data for a long time or reuse it, 
    /// they should be copied to the caller's buffer.
    /// </para>
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<ReadOnlyMemory<byte>?>> ToByteMemoriesAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsByteMemoriesAsync(cancellationToken) ?? AsyncEmptyArray<ReadOnlyMemory<byte>?>.Shared;

    /// <summary>
    /// Reads the binary data of the column, and converts it to a <see langword="byte"/>[]? value. 
    /// The column data type must be <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read <see langword="byte"/>[]? value.
    /// <see langword="null"/> if the value is a database null, or the data type is not <see cref="DataType.Bytea"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If the caller wants to write the binary data to a buffer or stream, 
    /// it is recommended to use one of the WriteAsync methods.
    /// </para>
    /// </remarks>
    public static async ValueTask<byte[]?> AsByteArrayAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        (await column.AsByteMemoryAsync(cancellationToken))?.ToArray();

    /// <summary>
    /// Reads the binary data of the column, and converts it to a <see langword="byte"/>[] value.
    /// The column data type must be <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="defaultValue">The default value returned if the column value is a database null or conversion fails.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read <see langword="byte"/>[] value.
    /// <paramref name="defaultValue"/> if the value is a database null, or the data type is not <see cref="DataType.Bytea"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If the caller wants to write the binary data to a buffer or stream, 
    /// it is recommended to use one of the WriteAsync methods.
    /// </para>
    /// </remarks>
    public static async ValueTask<byte[]> ToByteArrayAsync(
        this IColumn column,
        byte[] defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsByteArrayAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the binary data of the column, and converts it to a <see langword="byte"/>[] value.
    /// The column data type must be <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read <see langword="byte"/>[] value.
    /// An empty collection if the value is a database null, or the data type is not <see cref="DataType.ByteaArray"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If the caller wants to write the binary data to a buffer or stream, 
    /// it is recommended to use one of the WriteAsync methods.
    /// </para>
    /// </remarks>
    public static async ValueTask<byte[]> ToByteArrayAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsByteArrayAsync(cancellationToken) ?? Array.Empty<byte>();

    /// <summary>
    /// Reads the binary data of the column, 
    /// and converts it to a nullable asynchronous collection of <see langword="byte"/>[]?.
    /// The column data type must be <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over <see langword="byte"/>[]?.
    /// <see langword="null"/> if the value is a database null, or the data type is not <see cref="DataType.ByteaArray"/>.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<byte[]?>?> AsByteArraysAsync(
        this IColumn column,
        CancellationToken cancellationToken = default)
    {
        var memories = await column.AsByteMemoriesAsync(cancellationToken);

        return memories is null ? null : ToByteArraysAsync(memories);

        static async IAsyncEnumerable<byte[]?> ToByteArraysAsync(
            IAsyncEnumerable<ReadOnlyMemory<byte>?> memories)
        {
            await foreach (var item in memories)
            {
                yield return item?.ToArray();
            }
        }
    }

    /// <summary>
    /// Reads the binary data of the column, 
    /// and converts it to an asynchronous collection of <see langword="byte"/>[]?.
    /// The column data type must be <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="defaultValue">The default value returned if the column value is a database null or conversion fails.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="byte"/>[]?.
    /// <paramref name="defaultValue"/> if the value is a database null, or the data type is not <see cref="DataType.ByteaArray"/>.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<byte[]?>> ToByteArraysAsync(
        this IColumn column,
        IAsyncEnumerable<byte[]?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsByteArraysAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the binary data of the column, 
    /// and converts it to an asynchronous collection of <see langword="byte"/>[]?.
    /// The column data type must be <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="byte"/>[]?.
    /// An empty collection if the value is a database null, or the data type is not <see cref="DataType.ByteaArray"/>.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<byte[]?>> ToByteArraysAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsByteArraysAsync(cancellationToken) ?? AsyncEmptyArray<byte[]?>.Shared;

    #endregion

    #region Boolean

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Boolean"/>,
    /// and converts it to a <see langword="bool"/> value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not <see cref="DataType.Boolean"/> or number, or the value is a database null.</param>
    /// <returns>
    /// The read <see langword="bool"/> value.
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.Boolean"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<bool> ToBooleanAsync(
        this IColumn column,
        bool defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsBooleanAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Boolean"/>,
    /// and converts it to a <see langword="bool"/> value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read <see langword="bool"/> value.
    /// <see langword="false"/> if the data type is not <see cref="DataType.Boolean"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<bool> ToBooleanAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsBooleanAsync(cancellationToken) ?? false;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.BooleanArray"/>,
    /// and converts it to a nullable asynchronous collection of <see langword="bool"/>? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not <see cref="DataType.BooleanArray"/> or number array, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="bool"/>? values.
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.BooleanArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<bool?>> ToBooleansAsync(
        this IColumn column,
        IAsyncEnumerable<bool?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsBooleansAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.BooleanArray"/> or number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="bool"/>s.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="bool"/>? values.
    /// An empty collection if the data type is not <see cref="DataType.BooleanArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<bool?>> ToBooleansAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsBooleansAsync(cancellationToken) ?? AsyncEmptyArray<bool?>.Shared;

    #endregion

    #region Date

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>,
    /// and converts it to a DateOnly value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, or the value is a database null.</param>
    /// <returns>
    /// The read DateOnly value.
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<DateOnly> ToDateAsync(
        this IColumn column,
        DateOnly defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDateAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>,
    /// and converts it to a DateOnly value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read DateOnly value.
    /// Date.MinValue if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<DateOnly> ToDateAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDateAsync(cancellationToken) ?? DateOnly.MinValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>,
    /// and converts it to an asynchronous collection of DateOnly? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over DateOnly? values. 
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<DateOnly?>> ToDatesAsync(
        this IColumn column,
        IAsyncEnumerable<DateOnly?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDatesAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>,
    /// and converts it to an asynchronous collection of DateOnly? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over DateOnly values. 
    /// An empty collection if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<DateOnly?>> ToDatesAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDatesAsync(cancellationToken) ?? AsyncEmptyArray<DateOnly?>.Shared;

    #endregion

    #region DateTime

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>,
    /// and converts it to a local DateTime value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, or the value is a database null.</param>
    /// <returns>
    /// A DateTime value representing the local date time (DateTime.Kind property is DateTimeKind.Local).
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<DateTime> ToDateTimeAsync(
        this IColumn column,
        DateTime defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDateTimeAsync(cancellationToken) ?? defaultValue.ToLocalTime();

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>,
    /// and converts it to a local DateTime value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A DateTime value representing the local date time (DateTime.Kind property is DateTimeKind.Local).
    /// DateTime.MinValue if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<DateTime> ToDateTimeAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDateTimeAsync(cancellationToken) ?? DateTime.MinValue.ToLocalTime();

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>,
    /// and converts it to an asynchronous collection of local DateTime? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over local DateTime? values. 
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<DateTime?>> ToDateTimesAsync(
        this IColumn column,
        IAsyncEnumerable<DateTime?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDateTimesAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>,
    /// and converts it to an asynchronous collection of local DateTime? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over local DateTime? values. 
    /// An empty collection if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<DateTime?>> ToDateTimesAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDateTimesAsync(cancellationToken) ?? AsyncEmptyArray<DateTime?>.Shared;

    #endregion

    #region DateTimeOffset

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>,
    /// and converts it to a local DateTimeOffset value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A local DateTimeOffset? value.
    /// <see langword="null"/> if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<DateTimeOffset?> AsDateTimeOffsetAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDateTimeAsync(cancellationToken);

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>,
    /// and converts it to a local DateTimeOffset value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, or the value is a database null.</param>
    /// <returns>
    /// A DateTimeOffset value representing the local date time.
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<DateTimeOffset> ToDateTimeOffsetAsync(
        this IColumn column,
        DateTimeOffset defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDateTimeOffsetAsync(cancellationToken) ?? defaultValue.ToLocalTime();

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>,
    /// and converts it to a local DateTimeOffset value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A DateTimeOffset value representing the local date time.
    /// DateTimeOffset.MinValue if the data type is not <see cref="DataType.Date"/> or <see cref="DataType.TimestampTz"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<DateTimeOffset> ToDateTimeOffsetAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDateTimeOffsetAsync(cancellationToken) ?? DateTimeOffset.MinValue.ToLocalTime();

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>,
    /// and converts it to a nullable asynchronous collection of local DateTimeOffset? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over local DateTimeOffset? values. 
    /// <see langword="null"/> if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<DateTimeOffset?>?> AsDateTimeOffsetsAsync(
        this IColumn column,
        CancellationToken cancellationToken = default)
    {
        var dateTimes = await column.AsDateTimesAsync(cancellationToken);

        return dateTimes is null ? null : ToDateTimesAsync(dateTimes);

        static async IAsyncEnumerable<DateTimeOffset?> ToDateTimesAsync(
            IAsyncEnumerable<DateTime?> dateTimes)
        {
            await foreach (var item in dateTimes)
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>,
    /// and converts it to an asynchronous collection of local DateTimeOffset? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over local DateTimeOffset? values. 
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<DateTimeOffset?>> ToDateTimeOffsetsAsync(
        this IColumn column,
        IAsyncEnumerable<DateTimeOffset?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDateTimeOffsetsAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>,
    /// and converts it to an asynchronous collection of local DateTimeOffset? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over local DateTimeOffset? values. 
    /// An empty collection if the data type is not <see cref="DataType.DateArray"/> or <see cref="DataType.TimestampTzArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<DateTimeOffset?>> ToDateTimeOffsetsAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDateTimeOffsetsAsync(cancellationToken) ?? AsyncEmptyArray<DateTimeOffset?>.Shared;

    #endregion

    #region Decimal

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="decimal"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not number, or the value is a database null.</param>
    /// <returns>
    /// A <see langword="decimal"/> value.
    /// <paramref name="defaultValue"/> if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<decimal> ToDecimalAsync(
        this IColumn column,
        decimal defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDecimalAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="decimal"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="decimal"/> value.
    /// 0 if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<decimal> ToDecimalAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDecimalAsync(cancellationToken) ?? 0m;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="decimal"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not number array, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="decimal"/> values. 
    /// <paramref name="defaultValue"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<decimal?>> ToDecimalsAsync(
        this IColumn column,
        IAsyncEnumerable<decimal?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDecimalsAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="decimal"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="decimal"/> values. 
    /// An empty collection if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<decimal?>> ToDecimalsAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDecimalsAsync(cancellationToken) ?? AsyncEmptyArray<decimal?>.Shared;

    #endregion

    #region Double

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="double"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not number, or the value is a database null.</param>
    /// <returns>
    /// A <see langword="double"/> value.
    /// <paramref name="defaultValue"/> if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<double> ToDoubleAsync(
        this IColumn column,
        double defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDoubleAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="double"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="double"/> value.
    /// 0 if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<double> ToDoubleAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDoubleAsync(cancellationToken) ?? 0d;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="double"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned if the data type is not number array, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="double"/> values. 
    /// <paramref name="defaultValue"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<double?>> ToDoublesAsync(
        this IColumn column,
        IAsyncEnumerable<double?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsDoublesAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="double"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="double"/> values. 
    /// An empty collection if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<double?>> ToDoublesAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsDoublesAsync(cancellationToken) ?? AsyncEmptyArray<double?>.Shared;

    #endregion

    #region Guid

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Uuid"/>, and converts it to a Guid value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not <see cref="DataType.Uuid"/>, or the value is a database null.</param>
    /// <returns>
    /// A Guid value.
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.Uuid"/>, or the value is a database null.
    /// </returns>
    public static async ValueTask<Guid> ToGuidAsync(
        this IColumn column,
        Guid defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsGuidAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Uuid"/>, and converts it to a Guid value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A Guid value.
    /// Guid.Empty if the data type is not <see cref="DataType.Uuid"/>, or the value is a database null.
    /// </returns>
    public static async ValueTask<Guid> ToGuidAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsGuidAsync(cancellationToken) ?? Guid.Empty;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.UuidArray"/>,
    /// and converts it to an asynchronous collection of Guid? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not <see cref="DataType.UuidArray"/>, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over Guid? values. 
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.UuidArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<Guid?>> ToGuidsAsync(
        this IColumn column,
        IAsyncEnumerable<Guid?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsGuidsAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.UuidArray"/>,
    /// and converts it to an asynchronous collection of Guid? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over Guid? values. 
    /// An empty collection if the data type is not <see cref="DataType.UuidArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<Guid?>> ToGuidsAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsGuidsAsync(cancellationToken) ?? AsyncEmptyArray<Guid?>.Shared;

    #endregion

    #region Int16

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="short"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="defaultValue">The default value returned when the data type is not number, or the value is a database null.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="short"/> value.
    /// <paramref name="defaultValue"/> if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<short> ToInt16Async(
        this IColumn column,
        short defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsInt16Async(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="short"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="short"/> value.
    /// 0 if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<short> ToInt16Async(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsInt16Async(cancellationToken) ?? 0;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="short"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not number array, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="short"/>? values. 
    /// <paramref name="defaultValue"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<short?>> ToInt16sAsync(
        this IColumn column,
        IAsyncEnumerable<short?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsInt16sAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="short"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="short"/>? values. 
    /// An empty collection if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<short?>> ToInt16sAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsInt16sAsync(cancellationToken) ?? AsyncEmptyArray<short?>.Shared;

    #endregion

    #region Int32

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="int"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not number, or the value is a database null.</param>
    /// <returns>
    /// An <see langword="int"/> value.
    /// <paramref name="defaultValue"/> if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<int> ToInt32Async(
        this IColumn column,
        int defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsInt32Async(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="int"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An <see langword="int"/> value.
    /// 0 if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<int> ToInt32Async(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsInt32Async(cancellationToken) ?? 0;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="int"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not number array, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="int"/>? values. 
    /// <paramref name="defaultValue"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<int?>> ToInt32sAsync(
        this IColumn column,
        IAsyncEnumerable<int?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsInt32sAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="int"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="int"/>? values. 
    /// An empty collection if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<int?>> ToInt32sAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsInt32sAsync(cancellationToken) ?? AsyncEmptyArray<int?>.Shared;

    #endregion

    #region Int64

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="long"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not number, or the value is a database null.</param>
    /// <returns>
    /// A <see langword="long"/> value.
    /// <paramref name="defaultValue"/> if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<long> ToInt64Async(
        this IColumn column,
        long defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsInt64Async(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="long"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="long"/> value.
    /// 0 if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<long> ToInt64Async(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsInt64Async(cancellationToken) ?? 0L;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="long"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not number array, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="long"/>? values. 
    /// <paramref name="defaultValue"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<long?>> ToInt64sAsync(
        this IColumn column,
        IAsyncEnumerable<long?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsInt64sAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="long"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="long"/>? values. 
    /// An empty collection if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<long?>> ToInt64sAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsInt64sAsync(cancellationToken) ?? AsyncEmptyArray<long?>.Shared;

    #endregion

    #region Single

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="float"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not number, or the value is a database null.</param>
    /// <returns>
    /// A <see langword="float"/> value.
    /// <paramref name="defaultValue"/> if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<float> ToSingleAsync(
        this IColumn column,
        float defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsSingleAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number (
    /// <see cref="DataType.Int2"/>, <see cref="DataType.Int4"/>, <see cref="DataType.Int8"/>, 
    /// <see cref="DataType.Float4"/>, <see cref="DataType.Float8"/>, <see cref="DataType.Numeric"/>), 
    /// and converts it to a <see langword="float"/> value in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="float"/> value.
    /// 0 if the data type is not number, or the value is a database null.
    /// </returns>
    public static async ValueTask<float> ToSingleAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsSingleAsync(cancellationToken) ?? 0f;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="float"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not number array, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="float"/>? values. 
    /// <paramref name="defaultValue"/> if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<float?>> ToSinglesAsync(
        this IColumn column,
        IAsyncEnumerable<float?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsSinglesAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type number array (
    /// <see cref="DataType.Int2Array"/>, <see cref="DataType.Int4Array"/>, <see cref="DataType.Int8Array"/>, 
    /// <see cref="DataType.Float4Array"/>, <see cref="DataType.Float8Array"/>, <see cref="DataType.NumericArray"/>),
    /// and converts it to an asynchronous collection of <see langword="float"/>? values in the unchecked context.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="float"/>? values. 
    /// An empty collection if the data type is not number array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<float?>> ToSinglesAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsSinglesAsync(cancellationToken) ?? AsyncEmptyArray<float?>.Shared;

    #endregion

    #region String

    /// <summary>
    /// Reads the value of the column of data type string, 
    /// and converts it to a ReadOnlyMemory&lt;<see langword="char"/>&gt; value.
    /// The column data type must be a string type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not string, or the value is a database null.</param>
    /// <returns>
    /// A ReadOnlyMemory&lt;<see langword="char"/>&gt; value.
    /// <paramref name="defaultValue"/> if the data type is not string, or the value is a database null.
    /// </returns>
    public static async ValueTask<ReadOnlyMemory<char>> ToCharMemoryAsync(
        this IColumn column,
        ReadOnlyMemory<char> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsCharMemoryAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type string, 
    /// and converts it to a ReadOnlyMemory&lt;<see langword="char"/>&gt; value.
    /// The column data type must be a string type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A ReadOnlyMemory&lt;<see langword="char"/>&gt; value.
    /// An empty string if the data type is not string, or the value is a database null.
    /// </returns>
    public static async ValueTask<ReadOnlyMemory<char>> ToCharMemoryAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsCharMemoryAsync(cancellationToken) ?? ReadOnlyMemory<char>.Empty;

    /// <summary>
    /// Reads the value of the column of data type string array,
    /// and converts it to an asynchronous collection of ReadOnlyMemory&lt;<see langword="char"/>&gt;? values.
    /// The column data type must be a string array type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not string array, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over ReadOnlyMemory&lt;<see langword="char"/>&gt;? values. 
    /// <paramref name="defaultValue"/> if the data type is not string array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<ReadOnlyMemory<char>?>> ToCharMemoriesAsync(
        this IColumn column,
        IAsyncEnumerable<ReadOnlyMemory<char>?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsCharMemoriesAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type string array,
    /// and converts it to an asynchronous collection of ReadOnlyMemory&lt;<see langword="char"/>&gt;? values.
    /// The column data type must be a string array type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over ReadOnlyMemory&lt;<see langword="char"/>&gt;? values. 
    /// An empty collection if the data type is not string array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<ReadOnlyMemory<char>?>> ToCharMemoriesAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsCharMemoriesAsync(cancellationToken) ?? AsyncEmptyArray<ReadOnlyMemory<char>?>.Shared;

    /// <summary>
    /// Reads the value of the column of data type string, 
    /// and converts it to a <see langword="string"/>? value.
    /// The column data type must be a string type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="string"/>? value.
    /// <see langword="null"/> if the data type is not string, or the value is a database null.
    /// </returns>
    public static async ValueTask<string?> AsStringAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        (await column.AsCharMemoryAsync(cancellationToken))?.ToString();

    /// <summary>
    /// Reads the value of the column of data type string, 
    /// and converts it to a <see langword="string"/> value.
    /// The column data type must be a string type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not string, or the value is a database null.</param>
    /// <returns>
    /// A <see langword="string"/> value.
    /// <paramref name="defaultValue"/> if the data type is not string, or the value is a database null.
    /// </returns>
    public static async ValueTask<string> ToStringAsync(
        this IColumn column,
        string defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsStringAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type string, 
    /// and converts it to a <see langword="string"/> value.
    /// The column data type must be a string type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A <see langword="string"/> value.
    /// The empty string if the data type is not string, or the value is a database null.
    /// </returns>
    public static async ValueTask<string> ToStringAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsStringAsync(cancellationToken) ?? string.Empty;

    /// <summary>
    /// Reads the value of the column of data type string array,
    /// and converts it to a nullable asynchronous collection of <see langword="string"/>? values.
    /// The column data type must be a string array type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A nullable asynchronous iteration over <see langword="string"/>? values. 
    /// <see langword="null"/> if the data type is not string array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<string?>?> AsStringsAsync(
        this IColumn column,
        CancellationToken cancellationToken = default)
    {
        var memories = await column.AsCharMemoriesAsync(cancellationToken);

        return memories is null ? null : ToStringsAsync(memories);

        static async IAsyncEnumerable<string?> ToStringsAsync(
            IAsyncEnumerable<ReadOnlyMemory<char>?> memories)
        {
            await foreach (var item in memories)
            {
                yield return item?.ToString();
            }
        }
    }

    /// <summary>
    /// Reads the value of the column of data type string array,
    /// and converts it to an asynchronous collection of <see langword="string"/>? values.
    /// The column data type must be a string array type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not string array, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="string"/>? values. 
    /// <paramref name="defaultValue"/> if the data type is not string array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<string?>> ToStringsAsync(
        this IColumn column,
        IAsyncEnumerable<string?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsStringsAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type string array,
    /// and converts it to an asynchronous collection of <see langword="string"/>? values.
    /// The column data type must be a string array type code.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over <see langword="string"/>? values. 
    /// An empty collection if the data type is not string array, or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<string?>> ToStringsAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsStringsAsync(cancellationToken) ?? AsyncEmptyArray<string?>.Shared;

    #endregion

    #region Time

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Time"/>, 
    /// and converts it to a TimeOnly value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not <see cref="DataType.Time"/>, or the value is a database null.</param>
    /// <returns>
    /// The read TimeOnly value.
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.Time"/>, or the value is a database null.
    /// </returns>
    /// <remarks>
    /// Due to the difference in precision, the value returned for TimeOnly.MaxValue (23:59:59.9999999) is: 23:59:59.999999.
    /// </remarks>
    public static async ValueTask<TimeOnly> ToTimeAsync(
        this IColumn column,
        TimeOnly defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsTimeAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Time"/>, 
    /// and converts it to a TimeOnly value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read TimeOnly value.
    /// TimeOnly.MinValue if the data type is not <see cref="DataType.Time"/>, or the value is a database null.
    /// </returns>
    /// <remarks>
    /// Due to the difference in precision, the value returned for TimeOnly.MaxValue (23:59:59.9999999) is: 23:59:59.999999.
    /// </remarks>
    public static async ValueTask<TimeOnly> ToTimeAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsTimeAsync(cancellationToken) ?? TimeOnly.MinValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.TimeArray"/>,
    /// and converts it to an asynchronous collection of TimeOnly? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not <see cref="DataType.TimeArray"/>, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over TimeOnly? values. 
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.TimeArray"/>, 
    /// or the value is a database null.
    /// </returns>
    /// <remarks>
    /// Due to the difference in precision, the value returned for TimeOnly.MaxValue (23:59:59.9999999) is: 23:59:59.999999.
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<TimeOnly?>> ToTimesAsync(
        this IColumn column,
        IAsyncEnumerable<TimeOnly?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsTimesAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.TimeArray"/>,
    /// and converts it to an asynchronous collection of TimeOnly? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over TimeOnly? values. 
    /// An empty collection if the data type is not <see cref="DataType.TimeArray"/>, 
    /// or the value is a database null.
    /// </returns>
    /// <remarks>
    /// Due to the difference in precision, the value returned for TimeOnly.MaxValue (23:59:59.9999999) is: 23:59:59.999999.
    /// </remarks>
    public static async ValueTask<IAsyncEnumerable<TimeOnly?>> ToTimesAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsTimesAsync(cancellationToken) ?? AsyncEmptyArray<TimeOnly?>.Shared;

    #endregion

    #region TimeSpan

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Interval"/>, and converts it to a TimeSpan value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="defaultValue">The default value returned when the data type is not <see cref="DataType.Interval"/>, or the value is a database null.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A TimeSpan value.
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.Interval"/>, or the value is a database null.
    /// </returns>
    public static async ValueTask<TimeSpan> ToTimeSpanAsync(
        this IColumn column,
        TimeSpan defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsTimeSpanAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.Interval"/>, and converts it to a TimeSpan value.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// A TimeSpan value.
    /// TimeSpan.Zero if the data type is not <see cref="DataType.Interval"/>, or the value is a database null.
    /// </returns>
    public static async ValueTask<TimeSpan> ToTimeSpanAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsTimeSpanAsync(cancellationToken) ?? TimeSpan.Zero;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.IntervalArray"/>,
    /// and converts it to an asynchronous collection of TimeSpan? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <param name="defaultValue">The default value returned when the data type is not <see cref="DataType.IntervalArray"/>, or the value is a database null.</param>
    /// <returns>
    /// An asynchronous iteration over TimeSpan? values. 
    /// <paramref name="defaultValue"/> if the data type is not <see cref="DataType.IntervalArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<TimeSpan?>> ToTimeSpansAsync(
        this IColumn column,
        IAsyncEnumerable<TimeSpan?> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsTimeSpansAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the value of the column of data type <see cref="DataType.IntervalArray"/>,
    /// and converts it to an asynchronous collection of TimeSpan? values.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// An asynchronous iteration over TimeSpan? values. 
    /// An empty collection if the data type is not <see cref="DataType.IntervalArray"/>, 
    /// or the value is a database null.
    /// </returns>
    public static async ValueTask<IAsyncEnumerable<TimeSpan?>> ToTimeSpansAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsTimeSpansAsync(cancellationToken) ?? AsyncEmptyArray<TimeSpan?>.Shared;

    #endregion

    #region Json

    /// <summary>
    /// Reads the binary data of the column of data type JSON.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="defaultValue">The default value returned when the data type is not JSON, or the value is a database null.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read ReadOnlyMemory&lt;<see langword="byte"/>&gt; value.
    /// <paramref name="defaultValue"/> if the value is a database null, or the data type is not JSON.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The returned memory may be overwritten the next time the new data is read, 
    /// so if the caller wants to keep the returned data for a long time or reuse it, 
    /// they should be copied to the caller's buffer.
    /// </para>
    /// </remarks>
    public static async ValueTask<ReadOnlyMemory<byte>> ToJsonBinaryAsync(
        this IColumn column,
        ReadOnlyMemory<byte> defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsJsonBinaryAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the binary data of the column of data type JSON.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read ReadOnlyMemory&lt;<see langword="byte"/>&gt; value.
    /// ReadOnlyMemory&lt;<see langword="byte"/>&gt;.Empty if the value is a database null, or the data type is not JSON.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The returned memory may be overwritten the next time the new data is read, 
    /// so if the caller wants to keep the returned data for a long time or reuse it, 
    /// they should be copied to the caller's buffer.
    /// </para>
    /// </remarks>
    public static async ValueTask<ReadOnlyMemory<byte>> ToJsonBinaryAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsJsonBinaryAsync(cancellationToken) ?? ReadOnlyMemory<byte>.Empty;

    /// <summary>
    /// Reads the text of the column of data type JSON.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read <see langword="string"/>? value.
    /// <see langword="null"/> if the value is a database null, or the data type is not JSON.
    /// </returns>
    public static async ValueTask<string?> AsJsonStringAsync(
        this IColumn column,
        CancellationToken cancellationToken = default)
    {
        var binary = await column.AsJsonBinaryAsync(cancellationToken);
        if (binary is null)
        {
            return null;
        }

        if (binary.Value.IsEmpty)
        {
            return string.Empty;
        }

        return Encoding.UTF8.GetString(binary.Value.Span);
    }

    /// <summary>
    /// Reads the binary data of the column of data type JSON.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="defaultValue">The default value returned when the data type is not json, or the value is a database null.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read ReadOnlyMemory&lt;<see langword="byte"/>&gt; value.
    /// <paramref name="defaultValue"/> if the value is a database null, or the data type is not JSON.
    /// </returns>
    public static async ValueTask<string> ToJsonStringAsync(
        this IColumn column,
        string defaultValue,
        CancellationToken cancellationToken = default) =>
        await column.AsJsonStringAsync(cancellationToken) ?? defaultValue;

    /// <summary>
    /// Reads the text of the column of data type JSON.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to read its value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <returns>
    /// The read ReadOnlyMemory&lt;<see langword="byte"/>&gt; value.
    /// <see langword="string"/>.Empty if the value is a database null, or the data type is not JSON.
    /// </returns>
    public static async ValueTask<string> ToJsonStringAsync(
        this IColumn column,
        CancellationToken cancellationToken = default) =>
        await column.AsJsonStringAsync(cancellationToken) ?? string.Empty;

    /// <summary>
    /// Writes the column to the specified writer as a JSON property 
    /// with the specified property name.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to be written as a JSON property.</param>
    /// <param name="writer">The JSON writer to which property will be written.</param>
    /// <param name="propertyName">The property name, which can be empty, in which case the column camel-case name is used.</param>
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
    public static ValueTask WriteAsync(
        this IColumn column,
        Utf8JsonWriter writer,
        in ReadOnlySpan<char> propertyName,
        CancellationToken cancellationToken = default)
    {
        if (propertyName.Length == 0)
        {
            writer.WriteCamelPropertyName(column.Name);
        }
        else
        {
            writer.WritePropertyName(propertyName);
        }

        return column.WriteValueAsync(writer, cancellationToken);
    }

    /// <summary>
    /// Writes the column to the specified writer as a JSON property
    /// with the specified property name.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to be written as a JSON property.</param>
    /// <param name="writer">The JSON writer to which property will be written.</param>
    /// <param name="propertyName">The property name, which can be empty, in which case the column camel-case name is used.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <remarks>
    /// <para>
    /// The NaN, Infinity, and -Infinity of <see langword="float"/> and <see langword="double"/> are written as 
    /// the strings "NaN", "Infinity", and "-Infinity", respectively.
    /// </para>
    /// <para>
    /// The binary values are written as base64 encoded JSON strings.
    /// </para>
    /// <para>This method does not call <paramref name="writer"/>.FlushAsync.</para>
    /// </remarks>
    public static ValueTask WriteAsync(
        this IColumn column,
        Utf8JsonWriter writer,
        string propertyName,
        CancellationToken cancellationToken = default)
    {
        if (propertyName.Length == 0)
        {
            writer.WriteCamelPropertyName(column.Name);
        }
        else
        {
            writer.WritePropertyName(propertyName);
        }

        return column.WriteValueAsync(writer, cancellationToken);
    }

    /// <summary>
    /// Writes the column to the specified writer as a JSON property 
    /// with the column name as the property name.
    /// </summary>
    /// <param name="column">The <see cref="IColumn" /> to be written as a JSON property.</param>
    /// <param name="writer">The JSON writer to which property will be written.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous read.</param>
    /// <remarks>
    /// <para>
    /// If the column name is quoted by double-quotes (""), the camel-case name is used as property name.
    /// </para>
    /// <para>
    /// If the column name is not quoted by double-quotes (""), 
    /// the property name follows the rules of the database, 
    /// usually all letters in the name are lowercase.
    /// </para>
    /// <para>
    /// If the column does not have a name, then the property name follows the rules of the database, 
    /// usually the name is "?column?".
    /// </para>
    /// <para>
    /// The NaN, Infinity, and -Infinity of <see langword="float"/> and <see langword="double"/> are written as 
    /// the strings "NaN", "Infinity", and "-Infinity", respectively.
    /// </para>
    /// <para>
    /// The binary values are written as base64 encoded JSON strings.
    /// </para>
    /// <para>
    /// This method does not call <paramref name="writer"/>.FlushAsync.
    /// </para>
    /// </remarks>
    public static ValueTask WriteAsync(
        this IColumn column,
        Utf8JsonWriter writer,
        CancellationToken cancellationToken = default)
    {
        writer.WriteCamelPropertyName(column.Name);
        return column.WriteValueAsync(writer, cancellationToken);
    }

    #endregion
}
