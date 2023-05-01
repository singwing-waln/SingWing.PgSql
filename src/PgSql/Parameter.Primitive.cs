using SingWing.PgSql.Protocol.Messages.DataTypes;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql;

public abstract partial class Parameter
{
    #region Boolean

    /// <summary>
    /// Implicitly converts a <see langword="bool"/>? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Boolean"/>.
    /// </summary>
    /// <param name="value">The <see langword="bool"/>? value to convert.</param>
    public static implicit operator Parameter(bool? value)
    {
        return PrimitiveParameter<bool, BooleanProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="bool"/> value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Boolean"/>.
    /// </summary>
    /// <param name="value">The <see langword="bool"/> value to convert.</param>
    public static implicit operator Parameter(bool value)
    {
        return PrimitiveParameter<bool, BooleanProtocol>.For(value);
    }

    #endregion

    #region Bytea

    /// <summary>
    /// Implicitly converts a <see langword="byte"/>[]? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="value">The <see langword="byte"/>[]? value to convert.</param>
    public static implicit operator Parameter(byte[]? value)
    {
        return PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol>.For(
            value is null ? (ReadOnlyMemory<byte>?)null : value.AsMemory());
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyMemory&lt;<see langword="byte"/>&gt;? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="value">The ReadOnlyMemory&lt;<see langword="byte"/>&gt;? value to convert.</param>
    public static implicit operator Parameter(in ReadOnlyMemory<byte>? value)
    {
        return PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyMemory&lt;<see langword="byte"/>&gt; value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="value">The ReadOnlyMemory&lt;<see langword="byte"/>&gt; value to convert.</param>
    public static implicit operator Parameter(in ReadOnlyMemory<byte> value)
    {
        return PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a Memory&lt;<see langword="byte"/>&gt;? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="value">The Memory&lt;<see langword="byte"/>&gt;? value to convert.</param>
    public static implicit operator Parameter(in Memory<byte>? value)
    {
        return PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol>.For(
            value is null ? (ReadOnlyMemory<byte>?)null : value);
    }

    /// <summary>
    /// Implicitly converts a Memory&lt;<see langword="byte"/>&gt; value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="value">The Memory&lt;<see langword="byte"/>&gt; value to convert.</param>
    public static implicit operator Parameter(in Memory<byte> value)
    {
        return PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a ArraySegment&lt;<see langword="byte"/>&gt;? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="value">The ArraySegment&lt;<see langword="byte"/>&gt;? value to convert.</param>
    public static implicit operator Parameter(in ArraySegment<byte>? value)
    {
        return PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol>.For(
            value is null ? (ReadOnlyMemory<byte>?)null : value.Value.AsMemory());
    }

    /// <summary>
    /// Implicitly converts a ArraySegment&lt;<see langword="byte"/>&gt; value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Bytea"/>.
    /// </summary>
    /// <param name="value">The ArraySegment&lt;<see langword="byte"/>&gt; value to convert.</param>
    public static implicit operator Parameter(in ArraySegment<byte> value)
    {
        return PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol>.For(value);
    }

    #endregion

    #region Date

    /// <summary>
    /// Implicitly converts a DateOnly? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Date"/>.
    /// </summary>
    /// <param name="value">The DateOnly? value to convert.</param>
    public static implicit operator Parameter(DateOnly? value)
    {
        return PrimitiveParameter<DateOnly, DateProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a DateOnly value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Date"/>.
    /// </summary>
    /// <param name="value">The DateOnly value to convert.</param>
    public static implicit operator Parameter(DateOnly value)
    {
        return PrimitiveParameter<DateOnly, DateProtocol>.For(value);
    }

    #endregion

    #region Time

    /// <summary>
    /// Implicitly converts a TimeOnly? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Time"/>.
    /// </summary>
    /// <param name="value">The TimeOnly? value to convert.</param>
    public static implicit operator Parameter(TimeOnly? value)
    {
        return PrimitiveParameter<TimeOnly, TimeProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a TimeOnly value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Time"/>.
    /// </summary>
    /// <param name="value">The TimeOnly value to convert.</param>
    public static implicit operator Parameter(TimeOnly value)
    {
        return PrimitiveParameter<TimeOnly, TimeProtocol>.For(value);
    }

    #endregion

    #region Interval

    /// <summary>
    /// Implicitly converts a nullable TimeSpan
    /// to a <see cref="Parameter"/> whose type is interval.
    /// </summary>
    /// <param name="value">The nullable TimeSpan to convert.</param>
    public static implicit operator Parameter(TimeSpan? value)
    {
        return PrimitiveParameter<TimeSpan, IntervalProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a TimeSpan
    /// to a <see cref="Parameter"/> whose type is interval.
    /// </summary>
    /// <param name="value">The TimeSpan to convert.</param>
    public static implicit operator Parameter(TimeSpan value)
    {
        return PrimitiveParameter<TimeSpan, IntervalProtocol>.For(value);
    }

    #endregion

    #region Float

    /// <summary>
    /// Implicitly converts a <see langword="float"/>? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4"/>.
    /// </summary>
    /// <param name="value">The <see langword="float"/>? value to convert.</param>
    public static implicit operator Parameter(float? value)
    {
        return PrimitiveParameter<float, Float4Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="float"/> value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float4"/>.
    /// </summary>
    /// <param name="value">The <see langword="float"/> value to convert.</param>
    public static implicit operator Parameter(float value)
    {
        return PrimitiveParameter<float, Float4Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="double"/>? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8"/>.
    /// </summary>
    /// <param name="value">The <see langword="double"/>? to convert.</param>
    public static implicit operator Parameter(double? value)
    {
        return PrimitiveParameter<double, Float8Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="double"/> value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Float8"/>.
    /// </summary>
    /// <param name="value">The <see langword="double"/> value to convert.</param>
    public static implicit operator Parameter(double value)
    {
        return PrimitiveParameter<double, Float8Protocol>.For(value);
    }

    #endregion

    #region Int

    /// <summary>
    /// Implicitly converts a nullable <see langword="sbyte"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="sbyte"/> to convert.</param>
    public static implicit operator Parameter(sbyte? value)
    {
        return PrimitiveParameter<short, Int2Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="sbyte"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2"/>.
    /// </summary>
    /// <param name="value">The <see langword="sbyte"/> to convert.</param>
    public static implicit operator Parameter(sbyte value)
    {
        return PrimitiveParameter<short, Int2Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a nullable <see langword="byte"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="byte"/> to convert.</param>
    public static implicit operator Parameter(byte? value)
    {
        return PrimitiveParameter<short, Int2Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="byte"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2"/>.
    /// </summary>
    /// <param name="value">The <see langword="byte"/> to convert.</param>
    public static implicit operator Parameter(byte value)
    {
        return PrimitiveParameter<short, Int2Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a nullable <see langword="short"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="short"/> to convert.</param>
    public static implicit operator Parameter(short? value)
    {
        return PrimitiveParameter<short, Int2Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="short"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2"/>.
    /// </summary>
    /// <param name="value">The <see langword="short"/> to convert.</param>
    public static implicit operator Parameter(short value)
    {
        return PrimitiveParameter<short, Int2Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a nullable <see langword="ushort"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="ushort"/> to convert.</param>
    public static implicit operator Parameter(ushort? value)
    {
        return value is null
            ? PrimitiveParameter<short, Int2Protocol>.For(null)
            : PrimitiveParameter<short, Int2Protocol>.For(unchecked((short)value.Value));
    }

    /// <summary>
    /// Implicitly converts a <see langword="ushort"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int2"/>.
    /// </summary>
    /// <param name="value">The <see langword="ushort"/> to convert.</param>
    public static implicit operator Parameter(ushort value)
    {
        return PrimitiveParameter<short, Int2Protocol>.For(unchecked((short)value));
    }

    /// <summary>
    /// Implicitly converts a nullable <see langword="int"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="int"/> to convert.</param>
    public static implicit operator Parameter(int? value)
    {
        return PrimitiveParameter<int, Int4Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="int"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4"/>.
    /// </summary>
    /// <param name="value">The <see langword="int"/> to convert.</param>
    public static implicit operator Parameter(int value)
    {
        return PrimitiveParameter<int, Int4Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a nullable <see langword="uint"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="uint"/> to convert.</param>
    public static implicit operator Parameter(uint? value)
    {
        return value is null
            ? PrimitiveParameter<int, Int4Protocol>.For(null)
            : PrimitiveParameter<int, Int4Protocol>.For(unchecked((int)value.Value));
    }

    /// <summary>
    /// Implicitly converts a <see langword="uint"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int4"/>.
    /// </summary>
    /// <param name="value">The <see langword="uint"/> to convert.</param>
    public static implicit operator Parameter(uint value)
    {
        return PrimitiveParameter<int, Int4Protocol>.For(unchecked((int)value));
    }

    /// <summary>
    /// Implicitly converts a nullable <see langword="long"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="long"/> to convert.</param>
    public static implicit operator Parameter(long? value)
    {
        return PrimitiveParameter<long, Int8Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="long"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8"/>.
    /// </summary>
    /// <param name="value">The <see langword="long"/> to convert.</param>
    public static implicit operator Parameter(long value)
    {
        return PrimitiveParameter<long, Int8Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a nullable <see langword="ulong"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="ulong"/> to convert.</param>
    public static implicit operator Parameter(ulong? value)
    {
        return value is null
            ? PrimitiveParameter<long, Int8Protocol>.For(null)
            : PrimitiveParameter<long, Int8Protocol>.For(unchecked((long)value.Value));
    }

    /// <summary>
    /// Implicitly converts a <see langword="ulong"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8"/>.
    /// </summary>
    /// <param name="value">The <see langword="ulong"/> to convert.</param>
    public static implicit operator Parameter(ulong value)
    {
        return PrimitiveParameter<long, Int8Protocol>.For(unchecked((long)value));
    }

    /// <summary>
    /// Implicitly converts a nullable <see langword="nint"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="nint"/> to convert.</param>
    public static implicit operator Parameter(nint? value)
    {
        return PrimitiveParameter<long, Int8Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="nint"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8"/>.
    /// </summary>
    /// <param name="value">The <see langword="nint"/> to convert.</param>
    public static implicit operator Parameter(nint value)
    {
        return PrimitiveParameter<long, Int8Protocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a nullable <see langword="nuint"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8"/>.
    /// </summary>
    /// <param name="value">The nullable <see langword="nuint"/> to convert.</param>
    public static implicit operator Parameter(nuint? value)
    {
        return value is null
            ? PrimitiveParameter<long, Int8Protocol>.For(null)
            : PrimitiveParameter<long, Int8Protocol>.For(unchecked((long)value.Value));
    }

    /// <summary>
    /// Implicitly converts a <see langword="nuint"/>
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Int8"/>.
    /// </summary>
    /// <param name="value">The <see langword="nuint"/> to convert.</param>
    public static implicit operator Parameter(nuint value)
    {
        return PrimitiveParameter<long, Int8Protocol>.For(unchecked((long)value));
    }

    #endregion

    #region Numeric

    /// <summary>
    /// Implicitly converts a <see langword="decimal"/>? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Numeric"/>.
    /// </summary>
    /// <param name="value">The <see langword="decimal"/>? value to convert.</param>
    public static implicit operator Parameter(decimal? value)
    {
        return PrimitiveParameter<decimal, NumericProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a <see langword="decimal"/> value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Numeric"/>.
    /// </summary>
    /// <param name="value">The <see langword="decimal"/> value to convert.</param>
    public static implicit operator Parameter(decimal value)
    {
        return PrimitiveParameter<decimal, NumericProtocol>.For(value);
    }

    #endregion

    #region TimestampTz

    /// <summary>
    /// Implicitly converts a DateTime? value (UTC)
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTz"/>.
    /// </summary>
    /// <param name="value">The DateTime? value (UTC) to convert.</param>
    public static implicit operator Parameter(DateTime? value)
    {
        return PrimitiveParameter<DateTime, TimestampTzProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a DateTime value (UTC)
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTz"/>.
    /// </summary>
    /// <param name="value">The DateTime value (UTC) to convert.</param>
    public static implicit operator Parameter(DateTime value)
    {
        return PrimitiveParameter<DateTime, TimestampTzProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a DateTimeOffset? value (UTC)
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTz"/>.
    /// </summary>
    /// <param name="value">The DateTimeOffset? value (UTC) to convert.</param>
    public static implicit operator Parameter(DateTimeOffset? value)
    {
        return PrimitiveParameter<DateTime, TimestampTzProtocol>.For(value?.LocalDateTime);
    }

    /// <summary>
    /// Implicitly converts a DateTimeOffset value (UTC)
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.TimestampTz"/>.
    /// </summary>
    /// <param name="value">The DateTimeOffset value (UTC) to convert.</param>
    public static implicit operator Parameter(DateTimeOffset value)
    {
        return PrimitiveParameter<DateTime, TimestampTzProtocol>.For(value.LocalDateTime);
    }

    #endregion

    #region Uuid

    /// <summary>
    /// Implicitly converts a Guid? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Uuid"/>.
    /// </summary>
    /// <param name="value">The Guid? value to convert.</param>
    public static implicit operator Parameter(Guid? value)
    {
        return PrimitiveParameter<Guid, UuidProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a Guid value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Uuid"/>.
    /// </summary>
    /// <param name="value">The Guid value to convert.</param>
    public static implicit operator Parameter(Guid value)
    {
        return PrimitiveParameter<Guid, UuidProtocol>.For(value);
    }

    #endregion

    #region Varchar

    /// <summary>
    /// Implicitly converts a <see langword="char"/>[]? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Varchar"/>.
    /// </summary>
    /// <param name="value">The <see langword="char"/>[]? value to convert.</param>
    public static implicit operator Parameter(char[]? value)
    {
        return PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>.For(
            value is null ? (ReadOnlyMemory<char>?)null : value.AsMemory());
    }

    /// <summary>
    /// Implicitly converts a <see langword="string"/>? value
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Varchar"/>.
    /// </summary>
    /// <param name="value">The <see langword="string"/>? value to convert.</param>
    public static implicit operator Parameter(string? value)
    {
        return PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>.For(
            value is null ? (ReadOnlyMemory<char>?)null : value.AsMemory());
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyMemory&lt;<see langword="char"/>&gt;? value
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Varchar"/>.
    /// </summary>
    /// <param name="value">The ReadOnlyMemory&lt;<see langword="char"/>&gt;? value to convert.</param>
    public static implicit operator Parameter(ReadOnlyMemory<char>? value)
    {
        return PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a ReadOnlyMemory&lt;<see langword="char"/>&gt; value
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Varchar"/>.
    /// </summary>
    /// <param name="value">The ReadOnlyMemory&lt;<see langword="char"/>&gt; value to convert.</param>
    public static implicit operator Parameter(ReadOnlyMemory<char> value)
    {
        return PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a Memory&lt;<see langword="char"/>&gt;? value
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Varchar"/>.
    /// </summary>
    /// <param name="value">The Memory&lt;<see langword="char"/>&gt;? value to convert.</param>
    public static implicit operator Parameter(Memory<char>? value)
    {
        return PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>.For(
            value is null ? (ReadOnlyMemory<char>?)null : value);
    }

    /// <summary>
    /// Implicitly converts a Memory&lt;<see langword="char"/>&gt; value
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Varchar"/>.
    /// </summary>
    /// <param name="value">The Memory&lt;<see langword="char"/>&gt; value to convert.</param>
    public static implicit operator Parameter(Memory<char> value)
    {
        return PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>.For(value);
    }

    /// <summary>
    /// Implicitly converts a ArraySegment&lt;<see langword="char"/>&gt;? value
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Varchar"/>.
    /// </summary>
    /// <param name="value">The ArraySegment&lt;<see langword="char"/>&gt;? value to convert.</param>
    public static implicit operator Parameter(ArraySegment<char>? value)
    {
        return PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>.For(
            value is null ? (ReadOnlyMemory<char>?)null : value);
    }

    /// <summary>
    /// Implicitly converts a ArraySegment&lt;<see langword="char"/>&gt; value
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Varchar"/>.
    /// </summary>
    /// <param name="value">The ArraySegment&lt;<see langword="char"/>&gt; value to convert.</param>
    public static implicit operator Parameter(ArraySegment<char> value)
    {
        return PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>.For(value);
    }

    #endregion
}
