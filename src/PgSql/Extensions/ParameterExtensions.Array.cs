using SingWing.PgSql.Protocol.Messages.DataTypes;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql;

public static partial class ParameterExtensions
{
    #region BooleanArray

    /// <summary>
    /// Converts a collection of <see langword="bool"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="bool"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<bool?>? values) =>
        NullableArrayParameter<bool, BooleanProtocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="bool"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.BooleanArray"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="bool"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<bool>? values) =>
        ArrayParameter<bool, BooleanProtocol>.From(values);

    #endregion

    #region ByteaArray

    /// <summary>
    /// Converts a collection of <see langword="byte"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="byte"/>[]? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<byte[]?>? values)
    {
        return NullableArrayParameter<ReadOnlyMemory<byte>, ByteaProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<byte>?> ToEnumerable(IEnumerable<byte[]?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? (ReadOnlyMemory<byte>?)null : value.AsMemory();
            }
        }
    }

    /// <summary>
    /// Converts a collection of ReadOnlyMemory&lt;<see langword="byte"/>&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The collection of ReadOnlyMemory&lt;<see langword="byte"/>&gt;? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ReadOnlyMemory<byte>?>? values) =>
        NullableArrayParameter<ReadOnlyMemory<byte>, ByteaProtocol>.From(values);

    /// <summary>
    /// Converts a collection of ReadOnlyMemory&lt;<see langword="byte"/>&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The collection of ReadOnlyMemory&lt;<see langword="byte"/>&gt; values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ReadOnlyMemory<byte>>? values) =>
        ArrayParameter<ReadOnlyMemory<byte>, ByteaProtocol>.From(values);

    /// <summary>
    /// Converts a collection of Memory&lt;<see langword="byte"/>&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The collection of Memory&lt;<see langword="byte"/>&gt;? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<Memory<byte>?>? values)
    {
        return NullableArrayParameter<ReadOnlyMemory<byte>, ByteaProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<byte>?> ToEnumerable(IEnumerable<Memory<byte>?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? (ReadOnlyMemory<byte>?)null : value.Value;
            }
        }
    }

    /// <summary>
    /// Converts a collection of Memory&lt;<see langword="byte"/>&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The collection of Memory&lt;<see langword="byte"/>&gt; values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<Memory<byte>>? values)
    {
        return ArrayParameter<ReadOnlyMemory<byte>, ByteaProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<byte>> ToEnumerable(IEnumerable<Memory<byte>> values)
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }
    }

    /// <summary>
    /// Converts a collection of ArraySegment&lt;<see langword="byte"/>&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The collection of ArraySegment&lt;<see langword="byte"/>&gt;? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ArraySegment<byte>?>? values)
    {
        return NullableArrayParameter<ReadOnlyMemory<byte>, ByteaProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<byte>?> ToEnumerable(IEnumerable<ArraySegment<byte>?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? (ReadOnlyMemory<byte>?)null : value.Value.AsMemory();
            }
        }
    }

    /// <summary>
    /// Converts a collection of ArraySegment&lt;<see langword="byte"/>&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.ByteaArray"/>.
    /// </summary>
    /// <param name="values">The collection of ArraySegment&lt;<see langword="byte"/>&gt; values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ArraySegment<byte>>? values)
    {
        return ArrayParameter<ReadOnlyMemory<byte>, ByteaProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<byte>> ToEnumerable(IEnumerable<ArraySegment<byte>> values)
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }
    }

    #endregion

    #region DateArray

    /// <summary>
    /// Converts a collection of DateOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The collection of DateOnly? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<DateOnly?>? values) =>
        NullableArrayParameter<DateOnly, DateProtocol>.From(values);

    /// <summary>
    /// Converts a collection of DateOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.DateArray"/>.
    /// </summary>
    /// <param name="values">The collection of DateOnly values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<DateOnly>? values) =>
        ArrayParameter<DateOnly, DateProtocol>.From(values);

    #endregion

    #region FloatArray

    /// <summary>
    /// Converts a collection of <see langword="float"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="float"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<float?>? values) =>
        NullableArrayParameter<float, Float4Protocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="float"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="float"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<float>? values) =>
        ArrayParameter<float, Float4Protocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="double"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="double"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<double?>? values) =>
        NullableArrayParameter<double, Float8Protocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="double"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="double"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<double>? values) =>
        ArrayParameter<double, Float8Protocol>.From(values);

    #endregion

    #region IntArray

    /// <summary>
    /// Converts a collection of <see langword="short"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="short"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<short?>? values) =>
        NullableArrayParameter<short, Int2Protocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="short"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="short"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<short>? values) =>
        ArrayParameter<short, Int2Protocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="ushort"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="ushort"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ushort?>? values)
    {
        return NullableArrayParameter<short, Int2Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<short?> ToSignedEnumerable(IEnumerable<ushort?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? null : unchecked((short)value);
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="ushort"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="ushort"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ushort>? values)
    {
        return ArrayParameter<short, Int2Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<short> ToSignedEnumerable(IEnumerable<ushort> values)
        {
            foreach (var value in values)
            {
                yield return unchecked((short)value);
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="int"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="int"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<int?>? values) =>
        NullableArrayParameter<int, Int4Protocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="int"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="int"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<int>? values) =>
        ArrayParameter<int, Int4Protocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="uint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="uint"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<uint?>? values)
    {
        return NullableArrayParameter<int, Int4Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<int?> ToSignedEnumerable(IEnumerable<uint?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? null : unchecked((int)value);
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="uint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="uint"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<uint>? values)
    {
        return ArrayParameter<int, Int4Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<int> ToSignedEnumerable(IEnumerable<uint> values)
        {
            foreach (var value in values)
            {
                yield return unchecked((int)value);
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="long"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="long"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<long?>? values) =>
        NullableArrayParameter<long, Int8Protocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="long"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="long"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<long>? values) =>
        ArrayParameter<long, Int8Protocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="ulong"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="ulong"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ulong?>? values)
    {
        return NullableArrayParameter<long, Int8Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<long?> ToSignedEnumerable(IEnumerable<ulong?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? null : unchecked((long)value);
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="ulong"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="ulong"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ulong>? values)
    {
        return ArrayParameter<long, Int8Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<long> ToSignedEnumerable(IEnumerable<ulong> values)
        {
            foreach (var value in values)
            {
                yield return unchecked((long)value);
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="nint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="nint"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<nint?>? values)
    {
        return NullableArrayParameter<long, Int8Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<long?> ToSignedEnumerable(IEnumerable<nint?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? null : unchecked((long)value);
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="nint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="nint"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<nint>? values)
    {
        return ArrayParameter<long, Int8Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<long> ToSignedEnumerable(IEnumerable<nint> values)
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="nuint"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="nuint"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<nuint?>? values)
    {
        return NullableArrayParameter<long, Int8Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<long?> ToSignedEnumerable(IEnumerable<nuint?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? null : unchecked((long)value);
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="nuint"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8Array"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="nuint"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<nuint>? values)
    {
        return ArrayParameter<long, Int8Protocol>.From(values is null ? null : ToSignedEnumerable(values));

        static IEnumerable<long> ToSignedEnumerable(IEnumerable<nuint> values)
        {
            foreach (var value in values)
            {
                yield return unchecked((long)value);
            }
        }
    }

    #endregion

    #region IntervalArray

    /// <summary>
    /// Converts a collection of TimeSpan? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The collection of TimeSpan? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<TimeSpan?>? values) =>
        NullableArrayParameter<TimeSpan, IntervalProtocol>.From(values);

    /// <summary>
    /// Converts a collection of TimeSpan values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.IntervalArray"/>.
    /// </summary>
    /// <param name="values">The collection of TimeSpan values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<TimeSpan>? values) =>
        ArrayParameter<TimeSpan, IntervalProtocol>.From(values);

    #endregion

    #region NumericArray

    /// <summary>
    /// Converts a collection of <see langword="decimal"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="decimal"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<decimal?>? values) =>
        NullableArrayParameter<decimal, NumericProtocol>.From(values);

    /// <summary>
    /// Converts a collection of <see langword="decimal"/> values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.NumericArray"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="decimal"/> values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<decimal>? values) =>
        ArrayParameter<decimal, NumericProtocol>.From(values);

    #endregion

    #region TimeArray

    /// <summary>
    /// Converts a collection of TimeOnly? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The collection of TimeOnly? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<TimeOnly?>? values) =>
        NullableArrayParameter<TimeOnly, TimeProtocol>.From(values);

    /// <summary>
    /// Converts a collection of TimeOnly values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimeArray"/>.
    /// </summary>
    /// <param name="values">The collection of TimeOnly values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<TimeOnly>? values) =>
        ArrayParameter<TimeOnly, TimeProtocol>.From(values);

    #endregion

    #region TimestampTzArray

    /// <summary>
    /// Converts a collection of DateTime? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The collection of DateTime? values (UTC).</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<DateTime?>? values) =>
        NullableArrayParameter<DateTime, TimestampTzProtocol>.From(values);

    /// <summary>
    /// Converts a collection of DateTime values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The collection of DateTime values (UTC).</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<DateTime>? values) =>
        ArrayParameter<DateTime, TimestampTzProtocol>.From(values);

    /// <summary>
    /// Converts a collection of DateTimeOffset? values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The collection of DateTimeOffset? values (UTC).</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<DateTimeOffset?>? values)
    {
        return NullableArrayParameter<DateTime, TimestampTzProtocol>.From(values is null ? null : ToDateTimeEnumerable(values));

        static IEnumerable<DateTime?> ToDateTimeEnumerable(IEnumerable<DateTimeOffset?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? (DateTime?)null : value.Value.LocalDateTime;
            }
        }
    }

    /// <summary>
    /// Converts a collection of DateTimeOffset values (UTC) 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTzArray"/>.
    /// </summary>
    /// <param name="values">The collection of DateTimeOffset values (UTC).</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<DateTimeOffset>? values)
    {
        return ArrayParameter<DateTime, TimestampTzProtocol>.From(values is null ? null : ToDateTimeEnumerable(values));

        static IEnumerable<DateTime> ToDateTimeEnumerable(IEnumerable<DateTimeOffset> values)
        {
            foreach (var value in values)
            {
                yield return value.LocalDateTime;
            }
        }
    }

    #endregion

    #region UuidArray

    /// <summary>
    /// Converts a collection of Guid? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The collection of Guid? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<Guid?>? values) =>
        NullableArrayParameter<Guid, UuidProtocol>.From(values);

    /// <summary>
    /// Converts a collection of Guid values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.UuidArray"/>.
    /// </summary>
    /// <param name="values">The collection of Guid values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<Guid>? values) =>
        ArrayParameter<Guid, UuidProtocol>.From(values);

    #endregion

    #region VarcharArray

    /// <summary>
    /// Converts a collection of <see langword="string"/>? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="string"/>? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<string?>? values)
    {
        return NullableArrayParameter<ReadOnlyMemory<char>, VarcharProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<char>?> ToEnumerable(IEnumerable<string?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? (ReadOnlyMemory<char>?)null : value.AsMemory();
            }
        }
    }

    /// <summary>
    /// Converts a collection of <see langword="char"/>[]? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The collection of <see langword="char"/>[]? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<char[]?>? values)
    {
        return NullableArrayParameter<ReadOnlyMemory<char>, VarcharProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<char>?> ToEnumerable(IEnumerable<char[]?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? (ReadOnlyMemory<char>?)null : value.AsMemory();
            }
        }
    }

    /// <summary>
    /// Converts a collection of ReadOnlyMemory&lt;<see langword="char"/>&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The collection of ReadOnlyMemory&lt;<see langword="char"/>&gt;? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ReadOnlyMemory<char>?>? values) =>
        NullableArrayParameter<ReadOnlyMemory<char>, VarcharProtocol>.From(values);

    /// <summary>
    /// Converts a collection of ReadOnlyMemory&lt;<see langword="char"/>&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The collection of ReadOnlyMemory&lt;<see langword="char"/>&gt; values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ReadOnlyMemory<char>>? values) =>
        ArrayParameter<ReadOnlyMemory<char>, VarcharProtocol>.From(values);

    /// <summary>
    /// Converts a collection of Memory&lt;<see langword="char"/>&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The collection of Memory&lt;<see langword="char"/>&gt;? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<Memory<char>?>? values)
    {
        return NullableArrayParameter<ReadOnlyMemory<char>, VarcharProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<char>?> ToEnumerable(IEnumerable<Memory<char>?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? (ReadOnlyMemory<char>?)null : value.Value;
            }
        }
    }

    /// <summary>
    /// Converts a collection of Memory&lt;<see langword="char"/>&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The collection of Memory&lt;<see langword="char"/>&gt; values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<Memory<char>>? values)
    {
        return ArrayParameter<ReadOnlyMemory<char>, VarcharProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<char>> ToEnumerable(IEnumerable<Memory<char>> values)
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }
    }

    /// <summary>
    /// Converts a collection of ArraySegment&lt;<see langword="char"/>&gt;? values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The collection of ArraySegment&lt;<see langword="char"/>&gt;? values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ArraySegment<char>?>? values)
    {
        return NullableArrayParameter<ReadOnlyMemory<char>, VarcharProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<char>?> ToEnumerable(IEnumerable<ArraySegment<char>?> values)
        {
            foreach (var value in values)
            {
                yield return value is null ? (ReadOnlyMemory<char>?)null : value.Value.AsMemory();
            }
        }
    }

    /// <summary>
    /// Converts a collection of ArraySegment&lt;<see langword="char"/>&gt; values 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.VarcharArray"/>.
    /// </summary>
    /// <param name="values">The collection of ArraySegment&lt;<see langword="char"/>&gt; values.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    public static Parameter ToArrayParameter(this IEnumerable<ArraySegment<char>>? values)
    {
        return ArrayParameter<ReadOnlyMemory<char>, VarcharProtocol>.From(values is null ? null : ToEnumerable(values));

        static IEnumerable<ReadOnlyMemory<char>> ToEnumerable(IEnumerable<ArraySegment<char>> values)
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }
    }

    #endregion
}
