using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.DataTypes;
using System;
using System.Buffers;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Provides methods for reading column values from a <see cref="DataRow"/>.
/// </summary>
internal sealed class DataColumn : IColumn
{
    #region Fields

    /// <summary>
    /// The data row to which the column belongs.
    /// </summary>

    /// <summary>
    /// The 0-based position of the column in the current row.
    /// </summary>

    /// <summary>
    /// The number of bytes of the data in this column.
    /// </summary>
    private int _binaryLength;

    /// <summary>
    /// The number of bytes of the data in this column that have not yet been read. 
    /// Less than 0 means the length of the value has not been read.
    /// </summary>
    private int _remainingLength;

    /// <summary>
    /// The number of elements in this column whose data type is an array.
    /// Less than 0 means the column is not an array type, or the array length has not been read.
    /// </summary>
    private int _arrayLength;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="DataColumn"/> class 
    /// with the specified row to which the column belongs.
    /// </summary>
    /// <param name="row">The row to which the column belongs.</param>
    internal DataColumn(DataRow row)
    {
        Row = row;

        // The position unknown.
        Ordinal = -1;
        // The binary length unknown.
        _binaryLength = -1;
        _remainingLength = -1;
        // The array length unknown.
        _arrayLength = -1;
    }

    #endregion

    #region IColumn

    /// <inheritdoc />
    public int Ordinal { get; private set; }

    public ReadOnlySpan<char> Name => Row.Description[Ordinal].Name;

    /// <inheritdoc />
    public uint DataType => Row.Description[Ordinal].DataType;

    /// <inheritdoc />
    bool IColumn.IsNull => _binaryLength < 0;

    /// <inheritdoc />
    int IColumn.ArrayLength => _arrayLength;

    /// <inheritdoc />
    public async ValueTask DiscardAsync(
        CancellationToken cancellationToken = default)
    {
        if (_remainingLength > 0)
        {
            await Row.Reader.DiscardAsync(_remainingLength, cancellationToken);
            Advance(_remainingLength);
        }
    }

    #endregion

    #region Binary

    /// <inheritdoc />
    public ValueTask<ReadOnlyMemory<byte>?> AsByteMemoryAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<ReadOnlyMemory<byte>, ByteaProtocol>(cancellationToken);

    /// <inheritdoc />
    public async ValueTask<IAsyncEnumerable<ReadOnlyMemory<byte>?>?> AsByteMemoriesAsync(
        CancellationToken cancellationToken = default)
    {
        if (DataType != (uint)PgSql.DataType.ByteaArray)
        {
            await DiscardAsync(cancellationToken);
            return null;
        }

        if (_arrayLength <= 0)
        {
            await DiscardAsync(cancellationToken);
            return _arrayLength == 0 ? AsyncEmptyArray<ReadOnlyMemory<byte>?>.Shared : null;
        }

        return ReadByteMemoryAsyncArray();

        async IAsyncEnumerable<ReadOnlyMemory<byte>?> ReadByteMemoryAsyncArray()
        {
            var buffer = Array.Empty<byte>();

            for (var i = 0; i < _arrayLength; i++)
            {
                var elementLength = await ReadElementBinaryLengthAsync(cancellationToken);

                if (elementLength < 0)
                {
                    yield return null;
                    continue;
                }

                if (elementLength == 0)
                {
                    yield return ReadOnlyMemory<byte>.Empty;
                    continue;
                }

                if (elementLength <= Reader.BufferSize)
                {
                    yield return await ReadByteMemoryAsync(null, elementLength, cancellationToken);
                    continue;
                }

                if (buffer.Length < elementLength)
                {
                    buffer = GC.AllocateUninitializedArray<byte>(elementLength);
                }

                yield return await ReadByteMemoryAsync(buffer, elementLength, cancellationToken);
            }

            Debug.Assert(_remainingLength == 0);

            async ValueTask<ReadOnlyMemory<byte>?> ReadByteMemoryAsync(
                byte[]? buffer,
                int length,
                CancellationToken cancellationToken)
            {
                var value = await Row.Reader.ReadBinaryAsync(
                    buffer?.AsMemory(), length, cancellationToken);
                Advance(length);
                return value;
            }
        }
    }

    #endregion

    #region Boolean

    /// <inheritdoc />
    public ValueTask<bool?> AsBooleanAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<bool, BooleanProtocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<bool?>?> AsBooleansAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<bool, BooleanProtocol>(cancellationToken);

    #endregion

    #region Date

    /// <inheritdoc />
    public ValueTask<DateOnly?> AsDateAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<DateOnly, DateProtocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<DateOnly?>?> AsDatesAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<DateOnly, DateProtocol>(cancellationToken);

    #endregion

    #region DateTime

    /// <inheritdoc />
    public ValueTask<DateTime?> AsDateTimeAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<DateTime, TimestampTzProtocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<DateTime?>?> AsDateTimesAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<DateTime, TimestampTzProtocol>(cancellationToken);

    #endregion

    #region Decimal

    /// <inheritdoc />
    public ValueTask<decimal?> AsDecimalAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<decimal, NumericProtocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<decimal?>?> AsDecimalsAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<decimal, NumericProtocol>(cancellationToken);

    #endregion

    #region Double

    /// <inheritdoc />
    public ValueTask<double?> AsDoubleAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<double, Float8Protocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<double?>?> AsDoublesAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<double, Float8Protocol>(cancellationToken);

    #endregion

    #region Guid

    /// <inheritdoc />
    public ValueTask<Guid?> AsGuidAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<Guid, UuidProtocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<Guid?>?> AsGuidsAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<Guid, UuidProtocol>(cancellationToken);

    #endregion

    #region Int16

    /// <inheritdoc />
    public ValueTask<short?> AsInt16Async(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<short, Int2Protocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<short?>?> AsInt16sAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<short, Int2Protocol>(cancellationToken);

    #endregion

    #region Int32

    /// <inheritdoc />
    public ValueTask<int?> AsInt32Async(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<int, Int4Protocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<int?>?> AsInt32sAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<int, Int4Protocol>(cancellationToken);

    #endregion

    #region Int64

    /// <inheritdoc />
    public ValueTask<long?> AsInt64Async(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<long, Int8Protocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<long?>?> AsInt64sAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<long, Int8Protocol>(cancellationToken);

    #endregion

    #region Single

    /// <inheritdoc />
    public ValueTask<float?> AsSingleAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<float, Float4Protocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<float?>?> AsSinglesAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<float, Float4Protocol>(cancellationToken);

    #endregion

    #region String

    /// <inheritdoc />
    public async ValueTask<ReadOnlyMemory<char>?> AsCharMemoryAsync(
        CancellationToken cancellationToken = default)
    {
        if (!VarcharProtocol.CanConvertElementFrom(DataType))
        {
            await DiscardAsync(cancellationToken);
            return null;
        }

        return await ReadCharMemoryAsync(Array.Empty<char>(), _remainingLength, cancellationToken);
    }

    /// <inheritdoc />
    public async ValueTask<IAsyncEnumerable<ReadOnlyMemory<char>?>?> AsCharMemoriesAsync(
        CancellationToken cancellationToken = default)
    {
        if (!VarcharProtocol.CanConvertArrayFrom(DataType))
        {
            await DiscardAsync(cancellationToken);
            return null;
        }

        if (_arrayLength <= 0)
        {
            await DiscardAsync(cancellationToken);
            return _arrayLength == 0 ? AsyncEmptyArray<ReadOnlyMemory<char>?>.Shared : null;
        }

        return ReadArrayAsync(cancellationToken);

        async IAsyncEnumerable<ReadOnlyMemory<char>?> ReadArrayAsync(
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var buffer = Array.Empty<char>();
            for (var i = 0; i < _arrayLength; i++)
            {
                var elementLength = await ReadElementBinaryLengthAsync(cancellationToken);

                if (elementLength < 0)
                {
                    yield return null;
                    continue;
                }

                if (elementLength == 0)
                {
                    yield return ReadOnlyMemory<char>.Empty;
                    continue;
                }

                if (elementLength > buffer.Length)
                {
                    buffer = GC.AllocateUninitializedArray<char>(elementLength);
                }

                yield return await ReadCharMemoryAsync(buffer, elementLength, cancellationToken);
            }

            Debug.Assert(_remainingLength == 0);
        }
    }

    /// <summary>
    /// Reads binary data of the specified length, and converts the data to a memory of <see langword="char"/>s.
    /// </summary>
    /// <param name="buffer">The buffer into which characters are to be written.</param>
    /// <param name="length">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The read memory value. Returns <see langword="null"/> if the conversion fails, or if the value is a database null value.</returns>
    private async ValueTask<ReadOnlyMemory<char>?> ReadCharMemoryAsync(
        char[] buffer,
        int length,
        CancellationToken cancellationToken)
    {
        var value = await Row.Reader.ReadCharMemoryAsync(
            buffer.AsMemory(), length, cancellationToken);
        Advance(length);
        return value;
    }

    #endregion

    #region Time

    /// <inheritdoc />
    public ValueTask<TimeOnly?> AsTimeAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<TimeOnly, TimeProtocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<TimeOnly?>?> AsTimesAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<TimeOnly, TimeProtocol>(cancellationToken);

    #endregion

    #region TimeSpan

    /// <inheritdoc />
    public ValueTask<TimeSpan?> AsTimeSpanAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementAsync<TimeSpan, IntervalProtocol>(cancellationToken);

    /// <inheritdoc />
    public ValueTask<IAsyncEnumerable<TimeSpan?>?> AsTimeSpansAsync(
        CancellationToken cancellationToken = default) =>
        ReadElementsAsync<TimeSpan, IntervalProtocol>(cancellationToken);

    #endregion

    #region WriteBinary

    /// <inheritdoc />
    public async ValueTask<int> WriteAsync(
        Memory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        var binaryLength = _remainingLength;
        if (binaryLength < 0)
        {
            return 0;
        }

        if (!ByteaProtocol.CanConvertElementFrom(DataType))
        {
            await DiscardAsync(cancellationToken);
            return -1;
        }

        if (binaryLength == 0)
        {
            return 0;
        }

        await Row.Reader.ReadBinaryAsync(
            buffer, binaryLength, cancellationToken);
        Advance(binaryLength);
        return binaryLength;
    }

    /// <inheritdoc />
    public async ValueTask<int> WriteAsync(
        IBufferWriter<byte> writer,
        CancellationToken cancellationToken = default)
    {
        var binaryLength = _remainingLength;
        if (binaryLength < 0)
        {
            return 0;
        }

        if (!ByteaProtocol.CanConvertElementFrom(DataType))
        {
            await DiscardAsync(cancellationToken);
            return -1;
        }

        if (binaryLength == 0)
        {
            return 0;
        }

        await Row.Reader.ReadBinaryAsync(
            writer, binaryLength, cancellationToken);
        Advance(binaryLength);
        return binaryLength;
    }

    /// <inheritdoc />
    public async ValueTask<int> WriteAsync(
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        var binaryLength = _remainingLength;
        if (binaryLength < 0)
        {
            return 0;
        }

        if (!ByteaProtocol.CanConvertElementFrom(DataType))
        {
            await DiscardAsync(cancellationToken);
            return -1;
        }

        if (binaryLength == 0)
        {
            return 0;
        }

        await Row.Reader.ReadBinaryAsync(
            stream, binaryLength, cancellationToken);
        Advance(binaryLength);
        return binaryLength;
    }

    #endregion

    #region WriteString

    /// <inheritdoc />
    public async ValueTask<int> WriteAsync(
        Memory<char> buffer,
        CancellationToken cancellationToken = default)
    {
        if (_remainingLength < 0)
        {
            return 0;
        }

        if (!VarcharProtocol.CanConvertElementFrom(DataType))
        {
            await DiscardAsync(cancellationToken);
            return -1;
        }

        if (_remainingLength == 0)
        {
            return 0;
        }

        var charCount = await Row.Reader.ReadStringAsync(
            buffer, _remainingLength, cancellationToken);
        Advance(_remainingLength);
        return charCount;
    }

    /// <inheritdoc />
    public async ValueTask<int> WriteAsync(
        IBufferWriter<char> writer,
        CancellationToken cancellationToken = default)
    {
        if (_remainingLength < 0)
        {
            return 0;
        }

        if (!VarcharProtocol.CanConvertElementFrom(DataType))
        {
            await DiscardAsync(cancellationToken);
            return -1;
        }

        if (_remainingLength == 0)
        {
            return 0;
        }

        var charCount = await Row.Reader.ReadStringAsync(
            writer, _remainingLength, cancellationToken);
        Advance(_remainingLength);
        return charCount;
    }

    /// <inheritdoc />
    public async ValueTask<int> WriteAsync(
        TextWriter writer,
        CancellationToken cancellationToken = default)
    {
        if (_remainingLength < 0)
        {
            return 0;
        }

        if (!VarcharProtocol.CanConvertElementFrom(DataType))
        {
            await DiscardAsync(cancellationToken);
            return -1;
        }

        if (_remainingLength == 0)
        {
            return 0;
        }

        var charCount = await Row.Reader.ReadStringAsync(
            writer, _remainingLength, cancellationToken);
        Advance(_remainingLength);
        return charCount;
    }

    #endregion

    #region Json

    /// <summary>
    /// PostgreSQL json text data type.
    /// </summary>
    private const uint JsonTypeCode = 114u;

    /// <inheritdoc />
    public async ValueTask<ReadOnlyMemory<byte>?> AsJsonBinaryAsync(
        CancellationToken cancellationToken = default)
    {
        if (DataType != (uint)PgSql.DataType.Jsonb && DataType != JsonTypeCode)
        {
            await DiscardAsync(cancellationToken);
            return null;
        }

        var value = await Row.Reader.ReadBinaryAsync(
            (Memory<byte>?)null, _remainingLength, cancellationToken);
        Advance(_remainingLength);

        if (value is null || value.Value.IsEmpty)
        {
            return value;
        }

        if (DataType == (uint)PgSql.DataType.Jsonb)
        {
            // Skip jsonb version.
            return value.Value[1..];
        }

        return value;
    }

    /// <inheritdoc />
    public async ValueTask WriteValueAsync(
        Utf8JsonWriter writer,
        CancellationToken cancellationToken = default)
    {
        var typeCode = DataType;
        switch (typeCode)
        {
            case (uint)PgSql.DataType.Int2:
                var int2 = await AsInt16Async(cancellationToken);

                if (int2 is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteNumberValue(int2.Value);
                }
                break;
            case (uint)PgSql.DataType.Int2Array:
                var int2s = await AsInt16sAsync(cancellationToken);

                if (int2s is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in int2s)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteNumberValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Int4:
                var int4 = await AsInt32Async(cancellationToken);

                if (int4 is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteNumberValue(int4.Value);
                }
                break;
            case (uint)PgSql.DataType.Int4Array:
                var int4s = await AsInt32sAsync(cancellationToken);

                if (int4s is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in int4s)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteNumberValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Int8:
                var int8 = await AsInt64Async(cancellationToken);
                if (int8 is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteNumberValue(int8.Value);
                }
                break;
            case (uint)PgSql.DataType.Int8Array:
                var int8s = await AsInt64sAsync(cancellationToken);

                if (int8s is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in int8s)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteNumberValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Float4:
                var float4 = await AsSingleAsync(cancellationToken);

                if (float4 is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteFloatValue(float4.Value);
                }
                break;
            case (uint)PgSql.DataType.Float4Array:
                var float4s = await AsSinglesAsync(cancellationToken);

                if (float4s is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in float4s)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteFloatValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Float8:
                var float8 = await AsDoubleAsync(cancellationToken);

                if (float8 is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteFloatValue(float8.Value);
                }
                break;
            case (uint)PgSql.DataType.Float8Array:
                var float8s = await AsDoublesAsync(cancellationToken);

                if (float8s is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in float8s)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteFloatValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Numeric:
                var numeric = await AsDecimalAsync(cancellationToken);

                if (numeric is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteNumberValue(numeric.Value);
                }
                break;
            case (uint)PgSql.DataType.NumericArray:
                var numerics = await AsDecimalsAsync(cancellationToken);

                if (numerics is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in numerics)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteNumberValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Boolean:
                var @bool = await AsBooleanAsync(cancellationToken);

                if (@bool is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteBooleanValue(@bool.Value);
                }
                break;
            case (uint)PgSql.DataType.BooleanArray:
                var bools = await AsBooleansAsync(cancellationToken);

                if (bools is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in bools)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteBooleanValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Varchar:
            case VarcharProtocol.TextTypeCode:
            case VarcharProtocol.BpCharTypeCode:
                var varchar = await AsCharMemoryAsync(cancellationToken);

                if (varchar is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(varchar.Value.Span);
                }
                break;
            case (uint)PgSql.DataType.VarcharArray:
            case VarcharProtocol.TextArrayTypeCode:
            case VarcharProtocol.BpCharArrayTypeCode:
                var varchars = await AsCharMemoriesAsync(cancellationToken);

                if (varchars is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in varchars)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteStringValue(item.Value.Span);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Bytea:
                var bytea = await AsByteMemoryAsync(cancellationToken);

                if (bytea is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteBase64StringValue(bytea.Value.Span);
                }
                break;
            case (uint)PgSql.DataType.ByteaArray:
                var byteas = await AsByteMemoriesAsync(cancellationToken);

                if (byteas is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in byteas)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteBase64StringValue(item.Value.Span);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Date:
                var date = await AsDateAsync(cancellationToken);

                if (date is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(date.Value);
                }
                break;
            case (uint)PgSql.DataType.DateArray:
                var dates = await AsDatesAsync(cancellationToken);

                if (dates is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in dates)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteStringValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Time:
            case TimeProtocol.TimeTzTypeCode:
                var time = await AsTimeAsync(cancellationToken);

                if (time is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(time.Value);
                }
                break;
            case (uint)PgSql.DataType.TimeArray:
            case TimeProtocol.TimeTzArrayTypeCode:
                var times = await AsTimesAsync(cancellationToken);

                if (times is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in times)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteStringValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.TimestampTz:
            case TimestampTzProtocol.TimestampTypeCode:
                var timestamp = await AsDateTimeAsync(cancellationToken);

                if (timestamp is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteIso8601Value(timestamp.Value);
                }
                break;
            case (uint)PgSql.DataType.TimestampTzArray:
            case TimestampTzProtocol.TimestampArrayTypeCode:
                var timestamps = await AsDateTimesAsync(cancellationToken);

                if (timestamps is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in timestamps)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteIso8601Value(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Interval:
                var interval = await AsTimeSpanAsync(cancellationToken);

                if (interval is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(interval.Value);
                }
                break;
            case (uint)PgSql.DataType.IntervalArray:
                var intervals = await AsTimeSpansAsync(cancellationToken);

                if (intervals is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in intervals)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteStringValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Uuid:
                var uuid = await AsGuidAsync(cancellationToken);

                if (uuid is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(uuid.Value);
                }
                break;
            case (uint)PgSql.DataType.UuidArray:
                var uuids = await AsGuidsAsync(cancellationToken);

                if (uuids is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStartArray();

                    await foreach (var item in uuids)
                    {
                        if (item is null)
                        {
                            writer.WriteNullValue();
                        }
                        else
                        {
                            writer.WriteStringValue(item.Value);
                        }
                    }

                    writer.WriteEndArray();
                }
                break;
            case (uint)PgSql.DataType.Jsonb:
                await Row.Reader.ReadJsonAsync(writer, _remainingLength, cancellationToken);
                break;
            case JsonTypeCode:
                // json as text
                await Row.Reader.ReadJsonTextAsync(writer, _remainingLength, cancellationToken);
                break;
            default:
                // For unsupported data types, discard the data in the column and write a null to the JSON writer.
                await DiscardAsync(cancellationToken);
                writer.WriteNullValue();
                break;
        }

        Advance(_remainingLength);
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Gets the row where the column is located.
    /// </summary>
    internal DataRow Row { get; }

    /// <summary>
    /// Resets the state of the column.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Reset()
    {
        Ordinal = -1;
        _binaryLength = -1;
        _remainingLength = -1;
        _arrayLength = -1;
    }

    /// <summary>
    /// Advances the enumerator asynchronously to the next column of the row.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the enumerator was successfully advanced to the next column, 
    /// or <see langword="false"/> if the enumerator has passed the end of the row.
    /// </returns>
    internal async ValueTask<bool> MoveNextAsync()
    {
        var columnCount = Row.Description.ColumnCount;
        if (Ordinal >= columnCount)
        {
            // No more columns.
            return false;
        }

        // If the previous column still has data remaining and has not been read,
        // read and discard the data for that column first.
        if (Ordinal >= 0 && _remainingLength > 0)
        {
            await DiscardAsync();
            _remainingLength = -1;
        }

        if (++Ordinal == columnCount)
        {
            return false;
        }

        _binaryLength = await Row.Reader.ReadInt32Async(default);
        Row.Advance(sizeof(int));
        _remainingLength = _binaryLength;
        _arrayLength = -1;

        if (IsArray(DataType))
        {
            (_arrayLength, var consumed) = await Row.Reader.ReadArrayHeaderAsync(
               maxBytes: _remainingLength,
               CancellationToken.None);
            Advance(consumed);
        }

        return true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool IsArray(uint dataTypeCode) => dataTypeCode is
            ((uint)PgSql.DataType.BooleanArray) or
            ((uint)PgSql.DataType.ByteaArray) or
            ((uint)PgSql.DataType.DateArray) or
            ((uint)PgSql.DataType.Float4Array) or
            ((uint)PgSql.DataType.Float8Array) or
            ((uint)PgSql.DataType.Int2Array) or
            ((uint)PgSql.DataType.Int4Array) or
            ((uint)PgSql.DataType.Int8Array) or
            ((uint)PgSql.DataType.IntervalArray) or
            ((uint)PgSql.DataType.NumericArray) or
            ((uint)PgSql.DataType.TimeArray) or
            ((uint)PgSql.DataType.TimestampTzArray) or
            ((uint)PgSql.DataType.UuidArray) or
            ((uint)PgSql.DataType.VarcharArray) or
            VarcharProtocol.TextArrayTypeCode or
            VarcharProtocol.BpCharArrayTypeCode or
            TimestampTzProtocol.TimestampArrayTypeCode or
            TimeProtocol.TimeTzArrayTypeCode;
    }

    /// <summary>
    /// Advances the read position after processing a certain number of bytes.
    /// </summary>
    /// <param name="byteCount">The number of bytes processed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Advance(int byteCount)
    {
        if (byteCount > 0)
        {
            Debug.Assert(byteCount <= _remainingLength);
            _remainingLength -= byteCount;
            Row.Advance(byteCount);
        }
    }

    /// <summary>
    /// Reads the binary length of the next element in the current array.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The number of bytes of the next element in the current array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async ValueTask<int> ReadElementBinaryLengthAsync(CancellationToken cancellationToken)
    {
        var length = await Row.Reader.ReadInt32Async(cancellationToken);
        Advance(sizeof(int));
        return length;
    }

    /// <summary>
    /// Reads the value of the current column using the specified protocol.
    /// </summary>
    /// <typeparam name="TElement">The data type of the value.</typeparam>
    /// <typeparam name="TProtocol">The protocol for reading data.</typeparam>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The read value.
    /// Or <see langword="null"/> if the value is a database null, or the conversion is not possible.
    /// </returns>
    private async ValueTask<TElement?> ReadElementAsync<TElement, TProtocol>(
        CancellationToken cancellationToken)
        where TElement : struct
        where TProtocol : IDataTypeProtocol<TElement, TProtocol>
    {
        var dataTypeCode = DataType;
        if (!TProtocol.CanConvertElementFrom(dataTypeCode))
        {
            await DiscardAsync(cancellationToken);
            return null;
        }

        var value = await TProtocol.ReadElementAsync(
            dataTypeCode, _remainingLength, Row.Reader, cancellationToken);
        Advance(_remainingLength);
        return value;
    }

    /// <summary>
    /// Reads the value of the current column using the specified protocol.
    /// </summary>
    /// <typeparam name="TElement">The data type of the value.</typeparam>
    /// <typeparam name="TProtocol">The protocol for reading data.</typeparam>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The read value.
    /// Or <see langword="null"/> if the value is a database null, or the conversion is not possible.
    /// </returns>
    private async ValueTask<IAsyncEnumerable<TElement?>?> ReadElementsAsync<TElement, TProtocol>(
        CancellationToken cancellationToken)
        where TElement : struct
        where TProtocol : IDataTypeProtocol<TElement, TProtocol>
    {
        var arrayDataTypeCode = DataType;
        if (!TProtocol.CanConvertArrayFrom(arrayDataTypeCode))
        {
            await DiscardAsync(cancellationToken);
            return null;
        }

        if (_arrayLength <= 0)
        {
            await DiscardAsync(cancellationToken);
            return _arrayLength == 0 ? AsyncEmptyArray<TElement?>.Shared : null;
        }

        return ReadAsyncArray();

        async IAsyncEnumerable<TElement?> ReadAsyncArray()
        {
            var elementDataTypeCode = TProtocol.ElementTypeOfArrayType(arrayDataTypeCode);
            for (var i = 0; i < _arrayLength; i++)
            {
                var elementBinaryLength = await ReadElementBinaryLengthAsync(cancellationToken);
                if (elementBinaryLength < 0)
                {
                    yield return null;
                }
                else
                {
                    var value = await TProtocol.ReadElementAsync(
                        elementDataTypeCode, elementBinaryLength, Row.Reader, cancellationToken);
                    Advance(elementBinaryLength);
                    yield return value;
                }
            }

            Debug.Assert(_remainingLength == 0);
        }
    }

    #endregion
}
