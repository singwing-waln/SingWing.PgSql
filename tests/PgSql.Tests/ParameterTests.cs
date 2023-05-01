using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SingWing.PgSql.Tests;

public class ParameterTests
{
    #region Helper

    private static bool ArrayEquals<T>(T?[]? expected, object? parameterValue) where T : struct
    {
        if (ReferenceEquals(expected, parameterValue))
        {
            return true;
        }

        if (expected is null && parameterValue is null)
        {
            return true;
        }

        if (expected is null || parameterValue is null)
        {
            return false;
        }

        if (parameterValue is not IEnumerable<T?> enumerable)
        {
            return false;
        }

        int i = 0;
        foreach (var item in enumerable)
        {
            if (Comparer<T?>.Default.Compare(expected[i], item) != 0)
            {
                return false;
            }
            i++;
        }

        return true;
    }

    #endregion

    #region Base

    [Theory]
    [InlineData(DataType.Boolean)]
    [InlineData(DataType.BooleanArray)]
    [InlineData(DataType.Bytea)]
    [InlineData(DataType.ByteaArray)]
    [InlineData(DataType.Date)]
    [InlineData(DataType.DateArray)]
    [InlineData(DataType.Float4)]
    [InlineData(DataType.Float4Array)]
    [InlineData(DataType.Float8)]
    [InlineData(DataType.Float8Array)]
    [InlineData(DataType.Int2)]
    [InlineData(DataType.Int2Array)]
    [InlineData(DataType.Int4)]
    [InlineData(DataType.Int4Array)]
    [InlineData(DataType.Int8)]
    [InlineData(DataType.Int8Array)]
    [InlineData(DataType.Interval)]
    [InlineData(DataType.IntervalArray)]
    [InlineData(DataType.Jsonb)]
    [InlineData(DataType.Numeric)]
    [InlineData(DataType.NumericArray)]
    [InlineData(DataType.Time)]
    [InlineData(DataType.TimeArray)]
    [InlineData(DataType.TimestampTz)]
    [InlineData(DataType.TimestampTzArray)]
    [InlineData(DataType.Uuid)]
    [InlineData(DataType.UuidArray)]
    [InlineData(DataType.Varchar)]
    [InlineData(DataType.VarcharArray)]
    public void Create(DataType dataType)
    {
        var parameter = Parameter.Create(dataType);
        Assert.Equal(dataType, parameter.DataType);
    }

    [Theory]
    [InlineData(DataType.Boolean)]
    [InlineData(DataType.BooleanArray)]
    [InlineData(DataType.Bytea)]
    [InlineData(DataType.ByteaArray)]
    [InlineData(DataType.Date)]
    [InlineData(DataType.DateArray)]
    [InlineData(DataType.Float4)]
    [InlineData(DataType.Float4Array)]
    [InlineData(DataType.Float8)]
    [InlineData(DataType.Float8Array)]
    [InlineData(DataType.Int2)]
    [InlineData(DataType.Int2Array)]
    [InlineData(DataType.Int4)]
    [InlineData(DataType.Int4Array)]
    [InlineData(DataType.Int8)]
    [InlineData(DataType.Int8Array)]
    [InlineData(DataType.Interval)]
    [InlineData(DataType.IntervalArray)]
    [InlineData(DataType.Jsonb)]
    [InlineData(DataType.Numeric)]
    [InlineData(DataType.NumericArray)]
    [InlineData(DataType.Time)]
    [InlineData(DataType.TimeArray)]
    [InlineData(DataType.TimestampTz)]
    [InlineData(DataType.TimestampTzArray)]
    [InlineData(DataType.Uuid)]
    [InlineData(DataType.UuidArray)]
    [InlineData(DataType.Varchar)]
    [InlineData(DataType.VarcharArray)]
    public void Clone_ReferenceNotEquals(DataType dataType)
    {
        var parameter = Parameter.Create(dataType);
        Assert.False(ReferenceEquals(parameter, parameter.Clone()));
    }

    public static IEnumerable<object?[]> SetValueTestData()
    {
        yield return new object?[] { DataType.Boolean, true };
        yield return new object?[] { DataType.BooleanArray, new bool?[] { true, false, null } };
        yield return new object?[] { DataType.Bytea, new byte[] { 0x01, 0x02, 0x03 } };
        yield return new object?[] { DataType.ByteaArray, new byte[]?[] { new byte[] { 0x01, 0x02, 0x03 }, new byte[] { 0x01, 0x02, 0x03 }, null } };
        yield return new object?[] { DataType.Date, DateOnly.FromDateTime(DateTime.Now) };
        yield return new object?[] { DataType.DateArray, new DateOnly?[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.MaxValue, null } };
        yield return new object?[] { DataType.Float4, 123.456f };
        yield return new object?[] { DataType.Float4Array, new float?[] { 123.456f, -0.2f, float.NaN, float.NegativeInfinity, float.PositiveInfinity, null } };
        yield return new object?[] { DataType.Float8, 123.456d };
        yield return new object?[] { DataType.Float8Array, new double?[] { 123.456d, -0.2d, double.NaN, double.NegativeInfinity, double.PositiveInfinity, null } };
        yield return new object?[] { DataType.Int2, (short)0 };
        yield return new object?[] { DataType.Int2Array, new short?[] { 0, 32767, -32767, null } };
        yield return new object?[] { DataType.Int4, 123 };
        yield return new object?[] { DataType.Int4Array, new int?[] { 0, -1, 3, null } };
        yield return new object?[] { DataType.Int8, 345L };
        yield return new object?[] { DataType.Int8Array, new long?[] { 0, -1, 9, null } };
        yield return new object?[] { DataType.Interval, new TimeSpan(0, 0, 10) };
        yield return new object?[] { DataType.IntervalArray, new TimeSpan?[] { TimeSpan.MaxValue, TimeSpan.Zero, new TimeSpan(0, 0, 10), null } };
        yield return new object?[] { DataType.Numeric, 123.456m };
        yield return new object?[] { DataType.NumericArray, new decimal?[] { 0m, 11.2m, -11.2m, null } };
        yield return new object?[] { DataType.Time, TimeOnly.MinValue };
        yield return new object?[] { DataType.TimeArray, new TimeOnly?[] { TimeOnly.MinValue, TimeOnly.MaxValue, new TimeOnly(1, 2, 3), null } };
        yield return new object?[] { DataType.TimestampTz, DateTime.Now };
        yield return new object?[] { DataType.TimestampTzArray, new DateTime?[] { DateTime.Now, DateTime.MaxValue, DateTime.MinValue, null } };
        yield return new object?[] { DataType.Uuid, Guid.Empty };
        yield return new object?[] { DataType.UuidArray, new Guid?[] { Guid.Empty, new Guid("01FA0A39-229C-4A91-96E7-4791C60170A5"), null } };
        yield return new object?[] { DataType.Varchar, "PostgreSQL" };
        yield return new object?[] { DataType.VarcharArray, new string?[] { "PostgreSQL", "数据库", "", null } };
        yield return new object?[] { DataType.Jsonb, "{}" };
    }

    [Theory, MemberData(nameof(SetValueTestData))]
    public void SetValue(DataType dataType, object? value)
    {
        var parameter = Parameter.Create(dataType);
        parameter.Value = value;

        Assert.NotNull(parameter.Value);
    }

    [Theory, MemberData(nameof(SetValueTestData))]
    public void Reset(DataType dataType, object? value)
    {
        var parameter = Parameter.Create(dataType);
        parameter.Value = value;
        parameter.Reset();

        Assert.Null(parameter.Value);
    }

    #endregion

    #region Boolean

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(null)]
    public void FromBoolean_DataType(bool? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Boolean, parameter.DataType);
    }

    [Fact]
    public void FromBooleanEnumerable_DataType()
    {
        var parameter = (new bool?[] { true, false, null }).ToArrayParameter();
        Assert.Equal(DataType.BooleanArray, parameter.DataType);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(null)]
    public void FromBoolean_Value(bool? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromBooleanEnumerable_Value()
    {
        var value = new bool?[] { true, false, null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Bytea

    [Theory]
    [InlineData(new byte[] { })]
    [InlineData(new byte[] { 0x01, 0x02, 0x03 })]
    [InlineData(null)]
    public void FromByteArray_DataType(byte[]? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Bytea, parameter.DataType);
    }

    [Fact]
    public void FromByteArrayEnumerable_DataType()
    {
        var parameter = (new byte[]?[] { Array.Empty<byte>(), new byte[] { 0x01, 0x02, 0x03 }, null }).ToArrayParameter();
        Assert.Equal(DataType.ByteaArray, parameter.DataType);
    }

    private static bool BytesEquals(byte[]? value, object? parameterValue)
    {
        if (value == null && parameterValue == null)
        {
            return true;
        }

        if (value == null || parameterValue == null)
        {
            return false;
        }

        if (parameterValue is not ReadOnlyMemory<byte> mem)
        {
            return false;
        }

        if (mem.Length != value.Length)
        {
            return false;
        }

        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] != mem.Span[i])
            {
                return false;
            }
        }

        return true;
    }

    private static bool BytesArrayEquals(byte[]?[] value, object? parameterValue)
    {
        if (value == null && parameterValue == null)
        {
            return true;
        }

        if (value == null || parameterValue == null)
        {
            return false;
        }

        if (parameterValue is not IEnumerable<ReadOnlyMemory<byte>?> memArray)
        {
            return false;
        }

        if (memArray.Count() != value.Length)
        {
            return false;
        }

        int i = 0;
        foreach (var item in memArray)
        {
            if (!BytesEquals(value[i], item))
            {
                return false;
            }
            i++;
        }

        return true;
    }

    [Theory]
    [InlineData(new byte[] { })]
    [InlineData(new byte[] { 0x01, 0x02, 0x03 })]
    [InlineData(null)]
    public void FromByteArray_Value(byte[]? value)
    {
        Parameter parameter = value;
        Assert.True(BytesEquals(value, parameter.Value));
    }

    [Fact]
    public void FromByteArrayEnumerable_Value()
    {
        var value = new byte[]?[] { Array.Empty<byte>(), new byte[] { 0x01, 0x02, 0x03 }, null };
        var parameter = value.ToArrayParameter();
        Assert.True(BytesArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Date

    public static IEnumerable<object?[]> FromDateOnlyTestData()
    {
        yield return new object?[] { new DateOnly() };
        yield return new object?[] { DateOnly.MinValue };
        yield return new object?[] { DateOnly.MaxValue };
        yield return new object?[] { DateOnly.FromDateTime(DateTime.Now) };
        yield return new object?[] { null };
    }

    [Theory, MemberData(nameof(FromDateOnlyTestData))]
    public void FromDateOnly_DataType(DateOnly? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Date, parameter.DataType);
    }

    [Fact]
    public void FromDateOnlyEnumerable_DataType()
    {
        var parameter = (new DateOnly?[] { new DateOnly(), DateOnly.MinValue, DateOnly.MaxValue, DateOnly.FromDateTime(DateTime.Now), null }).ToArrayParameter();
        Assert.Equal(DataType.DateArray, parameter.DataType);
    }

    [Theory, MemberData(nameof(FromDateOnlyTestData))]
    public void FromDateOnly_Value(DateOnly? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromDateOnlyEnumerable_Value()
    {
        var value = new DateOnly?[] { new DateOnly(), DateOnly.MinValue, DateOnly.MaxValue, DateOnly.FromDateTime(DateTime.Now), null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Time

    public static IEnumerable<object?[]> FromTimeOnlyTestData()
    {
        yield return new object?[] { new TimeOnly() };
        yield return new object?[] { TimeOnly.MinValue };
        yield return new object?[] { TimeOnly.MaxValue };
        yield return new object?[] { TimeOnly.FromDateTime(DateTime.Now) };
        yield return new object?[] { null };
    }

    [Theory, MemberData(nameof(FromTimeOnlyTestData))]
    public void FromTimeOnly_DataType(TimeOnly? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Time, parameter.DataType);
    }

    [Fact]
    public void FromTimeOnlyEnumerable_DataType()
    {
        var parameter = (new TimeOnly?[] { new TimeOnly(), TimeOnly.MinValue, TimeOnly.MaxValue, TimeOnly.FromDateTime(DateTime.Now), null }).ToArrayParameter();
        Assert.Equal(DataType.TimeArray, parameter.DataType);
    }

    [Theory, MemberData(nameof(FromTimeOnlyTestData))]
    public void FromTimeOnly_Value(TimeOnly? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromTimeOnlyEnumerable_Value()
    {
        var value = new TimeOnly?[] { new TimeOnly(), TimeOnly.MinValue, TimeOnly.MaxValue, TimeOnly.FromDateTime(DateTime.Now), null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Interval

    public static IEnumerable<object?[]> FromTimeSpanTestData()
    {
        yield return new object?[] { new TimeSpan() };
        yield return new object?[] { TimeSpan.MinValue };
        yield return new object?[] { TimeSpan.MaxValue };
        yield return new object?[] { new TimeSpan(1, 2, 3) };
        yield return new object?[] { null };
    }

    [Theory, MemberData(nameof(FromTimeSpanTestData))]
    public void FromTimeSpan_DataType(TimeSpan? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Interval, parameter.DataType);
    }

    [Fact]
    public void FromTimeSpanEnumerable_DataType()
    {
        var parameter = (new TimeSpan?[] { new TimeSpan(), TimeSpan.MinValue, TimeSpan.MaxValue, new TimeSpan(1, 2, 3), null }).ToArrayParameter();
        Assert.Equal(DataType.IntervalArray, parameter.DataType);
    }

    [Theory, MemberData(nameof(FromTimeSpanTestData))]
    public void FromTimeSpan_Value(TimeSpan? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromTimeSpanEnumerable_Value()
    {
        var value = new TimeSpan?[] { new TimeSpan(), TimeSpan.MinValue, TimeSpan.MaxValue, new TimeSpan(1, 2, 3), null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Int2

    [Theory]
    [InlineData((short)0)]
    [InlineData((short)-1)]
    [InlineData((short)1)]
    [InlineData(null)]
    public void FromInt16_DataType(short? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Int2, parameter.DataType);
    }

    [Fact]
    public void FromInt16Enumerable_DataType()
    {
        var parameter = (new short?[] { 0, -1, 1, null }).ToArrayParameter();
        Assert.Equal(DataType.Int2Array, parameter.DataType);
    }

    [Theory]
    [InlineData((short)0)]
    [InlineData((short)-1)]
    [InlineData((short)1)]
    [InlineData(null)]
    public void FromInt16_Value(short? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromInt16Enumerable_Value()
    {
        var value = new short?[] { 0, -1, 1, null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Int4

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1)]
    [InlineData(null)]
    public void FromInt32_DataType(int? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Int4, parameter.DataType);
    }

    [Fact]
    public void FromInt32Enumerable_DataType()
    {
        var parameter = (new int?[] { 0, -1, 1, null }).ToArrayParameter();
        Assert.Equal(DataType.Int4Array, parameter.DataType);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1)]
    [InlineData(null)]
    public void FromInt32_Value(int? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromInt32Enumerable_Value()
    {
        var value = new int?[] { 0, -1, 1, null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Int8

    [Theory]
    [InlineData((long)0)]
    [InlineData((long)-1)]
    [InlineData((long)1)]
    [InlineData(null)]
    public void FromInt64_DataType(long? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Int8, parameter.DataType);
    }

    [Fact]
    public void FromInt64Enumerable_DataType()
    {
        var parameter = (new long?[] { 0, -1, 1, null }).ToArrayParameter();
        Assert.Equal(DataType.Int8Array, parameter.DataType);
    }

    [Theory]
    [InlineData((long)0)]
    [InlineData((long)-1)]
    [InlineData((long)1)]
    [InlineData(null)]
    public void FromInt64_Value(long? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromInt64Enumerable_Value()
    {
        var value = new long?[] { 0, -1, 1, null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Float

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    [InlineData(1f)]
    [InlineData(float.NaN)]
    [InlineData(float.NegativeInfinity)]
    [InlineData(float.PositiveInfinity)]
    [InlineData(null)]
    public void FromSingle_DataType(float? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Float4, parameter.DataType);
    }

    [Theory]
    [InlineData(0d)]
    [InlineData(-1d)]
    [InlineData(1d)]
    [InlineData(double.NaN)]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(null)]
    public void FromDouble_DataType(double? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Float8, parameter.DataType);
    }

    [Fact]
    public void FromSingleEnumerable_DataType()
    {
        var parameter = (new float?[] { 0, 1f, 2.3f, -1.2f, float.NaN, float.NegativeInfinity, float.PositiveInfinity, null }).ToArrayParameter();
        Assert.Equal(DataType.Float4Array, parameter.DataType);
    }

    [Fact]
    public void FromDoubleEnumerable_DataType()
    {
        var parameter = (new double?[] { 0, 1d, 2.3d, -1.2d, double.NaN, double.NegativeInfinity, double.PositiveInfinity, null }).ToArrayParameter();
        Assert.Equal(DataType.Float8Array, parameter.DataType);
    }

    [Theory]
    [InlineData(0f)]
    [InlineData(-1f)]
    [InlineData(1f)]
    [InlineData(float.NaN)]
    [InlineData(float.NegativeInfinity)]
    [InlineData(float.PositiveInfinity)]
    [InlineData(null)]
    public void FromSingle_Value(float? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Theory]
    [InlineData(0d)]
    [InlineData(-1d)]
    [InlineData(1d)]
    [InlineData(double.NaN)]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(null)]
    public void FromDouble_Value(double? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromSingleEnumerable_Value()
    {
        var value = new float?[] { 0, 1f, 2.3f, -1.2f, float.NaN, float.NegativeInfinity, float.PositiveInfinity, null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    [Fact]
    public void FromDoubleEnumerable_Value()
    {
        var value = new double?[] { 0, 1d, 2.3d, -1.2d, double.NaN, double.NegativeInfinity, double.PositiveInfinity, null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Numeric

    public static IEnumerable<object?[]> FromDecimalTestData()
    {
        yield return new object?[] { 0m };
        yield return new object?[] { decimal.MaxValue };
        yield return new object?[] { decimal.MinValue };
        yield return new object?[] { 1.23m };
        yield return new object?[] { -1.23m };
        yield return new object?[] { null };
    }

    [Theory, MemberData(nameof(FromDecimalTestData))]
    public void FromDecimal_DataType(decimal? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Numeric, parameter.DataType);
    }

    [Fact]
    public void FromDecimalEnumerable_DataType()
    {
        var parameter = (new decimal?[] { 0m, decimal.MaxValue, decimal.MinValue, 1.23m, -1.23m, null }).ToArrayParameter();
        Assert.Equal(DataType.NumericArray, parameter.DataType);
    }

    [Theory, MemberData(nameof(FromDecimalTestData))]
    public void FromDecimal_Value(decimal? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromDecimalEnumerable_Value()
    {
        var value = new decimal?[] { 0m, decimal.MaxValue, decimal.MinValue, 1.23m, -1.23m, null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region TimestampTz

    private static bool DateTimeOffsetEquals(DateTimeOffset? expected, object? parameterValue)
    {
        if (expected is null && parameterValue is null)
        {
            return true;
        }

        if (expected is null || parameterValue is null)
        {
            return false;
        }

        if (parameterValue is DateTimeOffset dto)
        {
            return expected == dto;
        }

        if (parameterValue is DateTime dt)
        {
            return expected.Value.LocalDateTime == dt.ToLocalTime();
        }

        return false;
    }

    private static bool DateTimeOffsetArrayEquals(DateTimeOffset?[] expected, object? parameterValue)
    {
        if (expected is null && parameterValue is null)
        {
            return true;
        }

        if (expected is null || parameterValue is null)
        {
            return false;
        }

        if (parameterValue is IEnumerable<DateTimeOffset?> dto)
        {
            return ArrayEquals(expected, dto);
        }

        if (parameterValue is IEnumerable<DateTime?> dt)
        {
            if (dt.Count() != expected.Length)
            {
                return false;
            }

            int i = 0;
            foreach (var item in dt)
            {
                if (expected[i]?.LocalDateTime != item?.ToLocalTime())
                {
                    return false;
                }
                i++;
            }

            return true;
        }

        return false;
    }

    public static IEnumerable<object?[]> FromDateTimeTestData()
    {
        yield return new object?[] { DateTime.MinValue };
        yield return new object?[] { DateTime.MaxValue };
        yield return new object?[] { new DateTime(1, 2, 3) };
        yield return new object?[] { DateTime.Now };
        yield return new object?[] { null };
    }

    [Theory, MemberData(nameof(FromDateTimeTestData))]
    public void FromDateTime_DataType(DateTime? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.TimestampTz, parameter.DataType);
    }

    public static IEnumerable<object?[]> FromDateTimeOffsetTestData()
    {
        yield return new object?[] { DateTimeOffset.MinValue };
        yield return new object?[] { DateTimeOffset.MaxValue };
        yield return new object?[] { new DateTimeOffset(2023, 3, 15, 11, 22, 33, TimeSpan.Zero) };
        yield return new object?[] { DateTimeOffset.Now };
        yield return new object?[] { null };
    }

    [Theory, MemberData(nameof(FromDateTimeOffsetTestData))]
    public void FromDateTimeOffset_DataType(DateTimeOffset? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.TimestampTz, parameter.DataType);
    }

    [Fact]
    public void FromDateTimeEnumerable_DataType()
    {
        var parameter = (new DateTime?[] { new DateTime(), DateTime.MinValue, DateTime.MaxValue, new DateTime(1, 2, 3), null }).ToArrayParameter();
        Assert.Equal(DataType.TimestampTzArray, parameter.DataType);
    }

    [Fact]
    public void FromDateTimeOffsetEnumerable_DataType()
    {
        var parameter = (new DateTimeOffset?[] { new DateTimeOffset(), DateTimeOffset.MinValue, DateTimeOffset.MaxValue, new DateTimeOffset(2023, 3, 15, 11, 22, 33, TimeSpan.Zero), null }).ToArrayParameter();
        Assert.Equal(DataType.TimestampTzArray, parameter.DataType);
    }

    [Theory, MemberData(nameof(FromDateTimeTestData))]
    public void FromDateTime_Value(DateTime? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Theory, MemberData(nameof(FromDateTimeOffsetTestData))]
    public void FromDateTimeOffset_Value(DateTimeOffset? value)
    {
        Parameter parameter = value;
        Assert.True(DateTimeOffsetEquals(value, parameter.Value));
    }

    [Fact]
    public void FromDateTimeEnumerable_Value()
    {
        var value = new DateTime?[] { new DateTime(), DateTime.MinValue, DateTime.MaxValue, new DateTime(1, 2, 3), null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    [Fact]
    public void FromDateTimeOffsetEnumerable_Value()
    {
        var value = new DateTimeOffset?[] { new DateTimeOffset(), DateTimeOffset.MinValue, DateTimeOffset.MaxValue, new DateTimeOffset(2023, 3, 15, 11, 22, 33, TimeSpan.Zero), null };
        var parameter = value.ToArrayParameter();
        Assert.True(DateTimeOffsetArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Uuid

    public static IEnumerable<object?[]> FromGuidTestData()
    {
        yield return new object?[] { new Guid() };
        yield return new object?[] { Guid.Empty };
        yield return new object?[] { Guid.NewGuid() };
        yield return new object?[] { new Guid("391FEAA7-BE97-401A-9F41-E7D765BC2F92") };
        yield return new object?[] { null };
    }

    [Theory, MemberData(nameof(FromGuidTestData))]
    public void FromGuid_DataType(Guid? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Uuid, parameter.DataType);
    }

    [Fact]
    public void FromGuidEnumerable_DataType()
    {
        var parameter = (new Guid?[] { new Guid(), Guid.Empty, Guid.NewGuid(), new Guid("391FEAA7-BE97-401A-9F41-E7D765BC2F92"), null }).ToArrayParameter();
        Assert.Equal(DataType.UuidArray, parameter.DataType);
    }

    [Theory, MemberData(nameof(FromGuidTestData))]
    public void FromGuid_Value(Guid? value)
    {
        Parameter parameter = value;
        Assert.Equal(value, parameter.Value);
    }

    [Fact]
    public void FromGuidEnumerable_Value()
    {
        var value = new Guid?[] { new Guid(), Guid.Empty, Guid.NewGuid(), new Guid("391FEAA7-BE97-401A-9F41-E7D765BC2F92"), null };
        var parameter = value.ToArrayParameter();
        Assert.True(ArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Varchar

    [Theory]
    [InlineData("")]
    [InlineData("PostgreSQL")]
    [InlineData("数据库")]
    [InlineData(null)]
    public void FromString_DataType(string? value)
    {
        Parameter parameter = value;
        Assert.Equal(DataType.Varchar, parameter.DataType);
    }

    [Fact]
    public void FromStringEnumerable_DataType()
    {
        var parameter = (new string?[] { "", string.Empty, "PostgreSQL", "数据库", null }).ToArrayParameter();
        Assert.Equal(DataType.VarcharArray, parameter.DataType);
    }

    private static bool StringEquals(string? value, object? parameterValue)
    {
        if (value == null && parameterValue == null)
        {
            return true;
        }

        if (value == null || parameterValue == null)
        {
            return false;
        }

        if (parameterValue is not ReadOnlyMemory<char> mem)
        {
            return false;
        }

        if (mem.Length != value.Length)
        {
            return false;
        }

        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] != mem.Span[i])
            {
                return false;
            }
        }

        return true;
    }

    private static bool StringArrayEquals(string?[] value, object? parameterValue)
    {
        if (value == null && parameterValue == null)
        {
            return true;
        }

        if (value == null || parameterValue == null)
        {
            return false;
        }

        if (parameterValue is not IEnumerable<ReadOnlyMemory<char>?> memArray)
        {
            return false;
        }

        if (memArray.Count() != value.Length)
        {
            return false;
        }

        int i = 0;
        foreach (var item in memArray)
        {
            if (!StringEquals(value[i], item))
            {
                return false;
            }
            i++;
        }

        return true;
    }

    [Theory]
    [InlineData("")]
    [InlineData("PostgreSQL")]
    [InlineData("数据库")]
    [InlineData(null)]
    public void FromString_Value(string? value)
    {
        Parameter parameter = value;
        Assert.True(StringEquals(value, parameter.Value));
    }

    [Fact]
    public void FromStringEnumerable_Value()
    {
        var value = new string?[] { "", string.Empty, "PostgreSQL", "数据库", null };
        var parameter = value.ToArrayParameter();
        Assert.True(StringArrayEquals(value, parameter.Value));
    }

    #endregion

    #region Json

    [Fact]
    public void Json_FromString_DataType()
    {
        var json = """{"name":"singwing","id":1}""";
        var parameter = json.ToJsonParameter();
        Assert.Equal(DataType.Jsonb, parameter.DataType);
    }

    [Fact]
    public void Json_FromByteArray_DataType()
    {
        var json = Encoding.UTF8.GetBytes("""{"name":"singwing","id":1}""");
        var parameter = json.ToJsonParameter();
        Assert.Equal(DataType.Jsonb, parameter.DataType);
    }

    [Fact]
    public void Json_FromStream_DataType()
    {
        var json = Encoding.UTF8.GetBytes("""{"name":"singwing","id":1}""");
        using var stream = new MemoryStream(json);
        var parameter = stream.ToJsonParameter();
        Assert.Equal(DataType.Jsonb, parameter.DataType);
    }

    [Fact]
    public void Json_FromJsonDocument_DataType()
    {
        var json = """{"name":"singwing","id":1}""";
        using var doc = JsonDocument.Parse(json);
        Parameter parameter = doc;
        Assert.Equal(DataType.Jsonb, parameter.DataType);
    }

    [Fact]
    public void Json_FromJsonElement_DataType()
    {
        var json = """{"name":"singwing","id":1}""";
        using var doc = JsonDocument.Parse(json);
        Parameter parameter = doc.RootElement;
        Assert.Equal(DataType.Jsonb, parameter.DataType);
    }

    [Fact]
    public void Json_FromJsonNode_DataType()
    {
        var json = """{"name":"singwing","id":1}""";
        var node = JsonNode.Parse(json);
        Parameter parameter = node;
        Assert.Equal(DataType.Jsonb, parameter.DataType);
    }

    private static string StreamToJsonString(Stream? stream)
    {
        if (stream is null)
        {
            return string.Empty;
        }

        using var s = stream;
        var buffer = new byte[256];
        var n = s.Read(buffer);
        return Encoding.UTF8.GetString(buffer, 0, n);
    }

    [Fact]
    public void Json_FromString_Value()
    {
        var json = """{"name":"singwing","id":1}""";
        var parameter = json.ToJsonParameter();
        Assert.Equal(json, ((ReadOnlyMemory<char>?)parameter.Value).ToString());
    }

    [Fact]
    public void Json_FromByteArray_Value()
    {
        var jsonString = """{"name":"singwing","id":1}""";
        var json = Encoding.UTF8.GetBytes(jsonString);
        var parameter = json.ToJsonParameter();
        Assert.Equal(jsonString, Encoding.UTF8.GetString(((ReadOnlyMemory<byte>?)parameter.Value)!.Value.Span));
    }

    [Fact]
    public void Json_FromStream_Value()
    {
        var jsonString = """{"name":"singwing","id":1}""";
        var json = Encoding.UTF8.GetBytes(jsonString);
        using var stream = new MemoryStream(json);
        var parameter = stream.ToJsonParameter();
        Assert.Equal(jsonString, StreamToJsonString(parameter.Value as Stream));
    }

    [Fact]
    public void Json_FromJsonDocument_Value()
    {
        var json = """{"name":"singwing","id":1}""";
        using var doc = JsonDocument.Parse(json);
        Parameter parameter = doc;
        Assert.Equal(json, StreamToJsonString(parameter.Value as Stream));
    }

    [Fact]
    public void Json_FromJsonElement_Value()
    {
        var json = """{"name":"singwing","id":1}""";
        using var doc = JsonDocument.Parse(json);
        Parameter parameter = doc.RootElement;
        Assert.Equal(json, StreamToJsonString(parameter.Value as Stream));
    }

    [Fact]
    public void Json_FromJsonNode_Value()
    {
        var json = """{"name":"singwing","id":1}""";
        var node = JsonNode.Parse(json);
        Parameter parameter = node;
        Assert.Equal(json, StreamToJsonString(parameter.Value as Stream));
    }

    #endregion
}
