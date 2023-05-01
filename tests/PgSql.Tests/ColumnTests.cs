using System.Text;
using System.Text.Json;

namespace SingWing.PgSql.Tests;

public class ColumnTests : TestBase
{
    #region Information

    [Theory]
    [InlineData("Id", 0)]
    [InlineData("Name", 1)]
    public async void GetOrdinalAndName_Success(string name, int expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("select 0 as \"Id\", '' as \"Name\"");
        var ordinal = 0;

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var columnName = col.Name.ToString();

                if (columnName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    ordinal = col.Ordinal;
                }
            }
        }

        // Assert
        Assert.Equal(expected, ordinal);
    }

    [Theory]
    [InlineData(0, DataType.Int2)]
    [InlineData(1, DataType.Int2Array)]
    [InlineData(2, DataType.Int4)]
    [InlineData(3, DataType.Int4Array)]
    [InlineData(4, DataType.Int8)]
    [InlineData(5, DataType.Int8Array)]
    [InlineData(6, DataType.Numeric)]
    [InlineData(7, DataType.NumericArray)]
    [InlineData(8, DataType.Float4)]
    [InlineData(9, DataType.Float4Array)]
    [InlineData(10, DataType.Float8)]
    [InlineData(11, DataType.Float8Array)]
    [InlineData(12, DataType.Boolean)]
    [InlineData(13, DataType.BooleanArray)]
    [InlineData(14, DataType.Varchar)]
    [InlineData(15, DataType.VarcharArray)]
    [InlineData(16, DataType.Bytea)]
    [InlineData(17, DataType.ByteaArray)]
    [InlineData(18, DataType.Date)]
    [InlineData(19, DataType.DateArray)]
    [InlineData(20, DataType.Time)]
    [InlineData(21, DataType.TimeArray)]
    [InlineData(22, DataType.TimestampTz)]
    [InlineData(23, DataType.TimestampTzArray)]
    [InlineData(24, DataType.Interval)]
    [InlineData(25, DataType.IntervalArray)]
    [InlineData(26, DataType.Uuid)]
    [InlineData(27, DataType.UuidArray)]
    [InlineData(28, DataType.Jsonb)]
    public async void GetDataType_Success(int columnOrdinal, DataType expectedDataType)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select
            2::smallint as "Int16",
            '{1,2,3,null}'::smallint[] as "Int16Array",
            4::int as "Int32",
            '{4,5,6,null}'::int[] as "Int32Array",
            8::bigint as "Int64",
            '{8,9,10,null}'::bigint[] as "Int64Array",
            0.1::numeric as "Decimal",
            '{0.1,0.2,null}'::numeric[] as "DecimalArray",
            0.2::real as "Single",
            '{0.2,Infinity,-Infinity,NaN,null}'::real[] as "SingleArray",
            0.4::float8 as "Double",
            '{0.4,Infinity,-Infinity,NaN,null}'::float8[] as "DoubleArray",
            true as "Boolean",
            '{true,false,0,1,"yes","no","on","off",null}'::boolean[] as "BooleanArray",
            'singwing'::varchar as "String",
            '{"singwing","waln","",null}'::varchar[] as "StringArray",
            '\xDEADBEEF'::bytea as "Binary",
            '{"\\xDEADBEEF","\\x7d9b6002d4004fe39d2d9fa8cb00a867","",null}'::bytea[] as "BinaryArray",
            '2023-03-11'::date as "Date",
            '{"2023-03-11","2023-03-12",null}'::date[] as "DateArray",
            '13:20:52'::time as "Time",
            '{"13:20:52","14:22:53",null}'::time[] as "TimeArray",
            '2023-10-19 10:23:54+02'::timestamptz as "DateTime",
            '{"2023-10-19 10:23:54+02","2024-11-29 12:23:06+08",null}'::timestamptz[] as "DateTimeArray",
            '3 4:05:06'::interval as "TimeSpan",
            '{"3 4:05:06","1-2",null}'::interval[] as "TimeSpanArray",
            'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11'::uuid as "Guid",
            '{"a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11","680c1b6802274dac8bddc16b9d80a11e",null}'::uuid[] as "GuidArray",
            '{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}'::jsonb as "Json"
            """);
        var dataType = 0u;

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                if (col.Ordinal == columnOrdinal)
                {
                    dataType = col.DataType;
                }
            }
        }

        // Assert
        Assert.Equal((uint)expectedDataType, dataType);
    }

    [Theory]
    [InlineData("select '' as \"Name\"", "Name", true)]
    [InlineData("select '' as \"Name\"", "name", true)]
    [InlineData("select '' as \"Name\"", "NAME", true)]
    [InlineData("select '' as \"Name\"", " Name ", false)]
    [InlineData("select '' as \"Name\"", " No ", false)]
    [InlineData("select '' as \"Name\"", "\"Name\"", false)]
    public async void NameIs(string command, string name, bool expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync(command);
        var yes = false;

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                yes = col.NameIs(name);
            }
        }

        // Assert
        Assert.Equal(expected, yes);
    }

    #endregion

    #region IsNull

    [Fact]
    public async void IsNull_Success()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.True(col.IsNull);
            }
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("''")]
    public async void IsNotNull_Success(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.False(col.IsNull);
            }
        }
    }

    #endregion

    #region ArrayLength

    [Theory]
    [InlineData(0, -1)]
    [InlineData(1, 4)]
    [InlineData(2, -1)]
    [InlineData(3, 4)]
    [InlineData(4, -1)]
    [InlineData(5, 4)]
    [InlineData(6, -1)]
    [InlineData(7, 3)]
    [InlineData(8, -1)]
    [InlineData(9, 5)]
    [InlineData(10, -1)]
    [InlineData(11, 5)]
    [InlineData(12, -1)]
    [InlineData(13, 9)]
    [InlineData(14, -1)]
    [InlineData(15, 4)]
    [InlineData(16, -1)]
    [InlineData(17, 4)]
    [InlineData(18, -1)]
    [InlineData(19, 3)]
    [InlineData(20, -1)]
    [InlineData(21, 3)]
    [InlineData(22, -1)]
    [InlineData(23, 3)]
    [InlineData(24, -1)]
    [InlineData(25, 3)]
    [InlineData(26, -1)]
    [InlineData(27, 3)]
    [InlineData(28, -1)]
    [InlineData(29, -1)]
    public async void GetArrayLength_Success(int columnOrdinal, int expectedLength)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select
            2::smallint as "Int16",
            '{1,2,3,null}'::smallint[] as "Int16Array",
            4::int as "Int32",
            '{4,5,6,null}'::int[] as "Int32Array",
            8::bigint as "Int64",
            '{8,9,10,null}'::bigint[] as "Int64Array",
            0.1::numeric as "Decimal",
            '{0.1,0.2,null}'::numeric[] as "DecimalArray",
            0.2::real as "Single",
            '{0.2,Infinity,-Infinity,NaN,null}'::real[] as "SingleArray",
            0.4::float8 as "Double",
            '{0.4,Infinity,-Infinity,NaN,null}'::float8[] as "DoubleArray",
            true as "Boolean",
            '{true,false,0,1,"yes","no","on","off",null}'::boolean[] as "BooleanArray",
            'singwing'::varchar as "String",
            '{"singwing","waln","",null}'::varchar[] as "StringArray",
            '\xDEADBEEF'::bytea as "Binary",
            '{"\\xDEADBEEF","\\x7d9b6002d4004fe39d2d9fa8cb00a867","",null}'::bytea[] as "BinaryArray",
            '2023-03-11'::date as "Date",
            '{"2023-03-11","2023-03-12",null}'::date[] as "DateArray",
            '13:20:52'::time as "Time",
            '{"13:20:52","14:22:53",null}'::time[] as "TimeArray",
            '2023-10-19 10:23:54+02'::timestamptz as "DateTime",
            '{"2023-10-19 10:23:54+02","2024-11-29 12:23:06+08",null}'::timestamptz[] as "DateTimeArray",
            '3 4:05:06'::interval as "TimeSpan",
            '{"3 4:05:06","1-2",null}'::interval[] as "TimeSpanArray",
            'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11'::uuid as "Guid",
            '{"a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11","680c1b6802274dac8bddc16b9d80a11e",null}'::uuid[] as "GuidArray",
            '{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}'::jsonb as "Json",
            null as "Null"
            """);
        var arrayLength = 0;

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                if (col.Ordinal == columnOrdinal)
                {
                    arrayLength = col.ArrayLength;
                }
            }
        }

        // Assert
        Assert.Equal(expectedLength, arrayLength);
    }

    #endregion

    #region Discard

    [Fact]
    public async void Discard_Success()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("select 0 as id, '' as name");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                await col.DiscardAsync();
            }
        }
    }

    #endregion

    #region Binary

    [Theory]
    [InlineData("")]
    [InlineData("f041e3f2e54047878886a13143cd8355")]
    public async void QueryBinary_ValidHex_Success(string expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select '\\x{expected}'::bytea");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(
                    expected,
                    BitConverter.ToString(await col.ToByteArrayAsync()).Replace("-", ""),
                    ignoreCase: true);
            }
        }
    }

    [Theory]
    [InlineData("some string")]
    [InlineData("f041e3f2e54047878886a13143cd835")]
    public async void QueryBinary_InvalidHex_ThrowsServerException(string expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select '\\x{expected}'::bytea");

        // Assert
        await Assert.ThrowsAsync<ServerException>(async () =>
        {
            await foreach (var row in rows)
            {
                await foreach (var col in row)
                {
                    await col.AsByteArrayAsync();
                }
            }
        });
    }

    [Fact]
    public async void QueryBinary_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsByteArrayAsync());
            }
        }
    }

    [Theory]
    [InlineData("0", -1)]
    [InlineData("'some string'", -1)]
    public async void QueryBinary_OtherType_ReturnsNull(string value, int length)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var data = await col.AsByteArrayAsync();
                Assert.Equal(length, data?.Length ?? -1);
            }
        }
    }

    [Fact]
    public async void QueryBinaries_ValidHex_Success()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{"\\xcfb3f0c0611e4a119e125fbb8fb05a24",null,"\\x"}'::bytea[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var arrays = await col.ToByteArraysAsync();

                var i = 0;
                await foreach (var array in arrays)
                {
                    if (i == 0)
                    {
                        Assert.Equal("cfb3f0c0611e4a119e125fbb8fb05a24", BitConverter.ToString(array ?? Array.Empty<byte>()).Replace("-", ""), ignoreCase: true);
                    }
                    else if (i == 1)
                    {
                        Assert.Null(array);
                    }
                    else if (i == 2)
                    {
                        Assert.Equal(0, array?.Length ?? -1);
                    }
                    i++;
                }
            }
        }
    }

    [Fact]
    public async void QueryBinaries_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsByteArraysAsync());
            }
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryBinaries_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsByteArraysAsync());
            }
        }
    }

    #endregion

    #region Boolean

    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    [InlineData("'yes'", true)]
    [InlineData("'no'", false)]
    [InlineData("'on'", true)]
    [InlineData("'off'", false)]
    [InlineData("1", true)]
    [InlineData("0", false)]
    public async void QueryBoolean_Valid_Success(string column, bool expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::boolean");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, await col.ToBooleanAsync());
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("123")]
    public async void QueryBoolean_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsBooleanAsync());
            }
        }
    }

    [Fact]
    public async void QueryBoolean_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsBooleanAsync());
            }
        }
    }

    [Fact]
    public async void QueryBooleans_Valid_Success()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{true,false,"yes","no","on","off",1,0,null}'::boolean[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToBooleansAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    if (i == 8)
                    {
                        Assert.Null(item);
                    }
                    else
                    {
                        Assert.Equal((i % 2) == 0, item);
                    }

                    i++;
                }
            }
        }
    }

    [Fact]
    public async void QueryBooleans_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsBooleansAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryBooleans_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsBooleansAsync());
            }
        }
    }

    #endregion

    #region Date

    [Theory]
    [InlineData("'1999-01-08'", "1999-01-08")]
    [InlineData("'January 8, 1999'", "1999-01-08")]
    [InlineData("'1999-Jan-08'", "1999-01-08")]
    [InlineData("'19990108'", "1999-01-08")]
    [InlineData("'Jan-08-1999'", "1999-01-08")]
    public async void QueryDate_Valid_Success(string column, string expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::date");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, (await col.ToDateAsync()).ToString("yyyy-MM-dd"));
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("123")]
    [InlineData("'01:02:03'")]
    [InlineData("'1/8/1999'")]
    [InlineData("'January 8, 99 BC'")]
    public async void QueryDate_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDateAsync());
            }
        }
    }

    [Fact]
    public async void QueryDate_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDateAsync());
            }
        }
    }

    [Fact]
    public async void QueryDates_Valid_Success()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{"1999-01-08","January 8, 1999","1999-Jan-08","19990108","Jan-08-1999"}'::date[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDatesAsync();

                await foreach (var item in items)
                {
                    Assert.Equal("1999-01-08", item?.ToString("yyyy-MM-dd"));
                }
            }
        }
    }

    [Fact]
    public async void QueryDates_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDatesAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryDates_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDatesAsync());
            }
        }
    }

    #endregion

    #region DateTime

    [Theory]
    [InlineData("'1999-01-08 04:05:06'", "1999-01-08 04:05:06")]
    [InlineData("'1999-01-08 04:05:06 -8:00'", "1999-01-08 20:05:06")]
    [InlineData("'January 8 04:05:06 1999 PST'", "1999-01-08 20:05:06")]
    [InlineData("'1999-01-08 04:05:06+02'", "1999-01-08 10:05:06")]
    public async void QueryDateTime_Valid_Success(string column, string expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::timestamptz");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, (await col.ToDateTimeAsync()).ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("123")]
    [InlineData("'01:02:03'")]
    [InlineData("'1/8/1999'")]
    [InlineData("'January 8, 99 BC'")]
    public async void QueryDateTime_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDateTimeAsync());
            }
        }
    }

    [Fact]
    public async void QueryDateTime_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDateTimeAsync());
            }
        }
    }

    [Fact]
    public async void QueryDateTimes_Valid_Success()
    {
        // Arrange
        var expected = new string[] { "1999-01-08 04:05:06", "1999-01-08 20:05:06", "1999-01-08 20:05:06", "1999-01-08 10:05:06" };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{"1999-01-08 04:05:06","1999-01-08 04:05:06 -8:00","January 8 04:05:06 1999 PST","1999-01-08 04:05:06+02"}'::timestamptz[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDateTimesAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(expected[i++], item?.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
        }
    }

    [Fact]
    public async void QueryDateTimes_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDateTimesAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryDateTimes_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDateTimesAsync());
            }
        }
    }

    #endregion

    #region Decimal

    public static IEnumerable<object?[]> QueryDecimalTestData()
    {
        yield return new object?[] { "0", 0m };
        yield return new object?[] { "0.1", 0.1m };
        yield return new object?[] { "+12345.67890", 12345.6789m };
        yield return new object?[] { "12345.67890", 12345.6789m };
        yield return new object?[] { "-12345.67890", -12345.6789m };
        yield return new object?[] { "79228162514264337593543950335", 79228162514264337593543950335m };
        yield return new object?[] { "-79228162514264337593543950335", -79228162514264337593543950335m };
        yield return new object?[] { "79228162514264337593543950336", 79228162514264337593543950335m };
        yield return new object?[] { "-79228162514264337593543950336", -79228162514264337593543950335m };
        yield return new object?[] { "999999999999999999999999999999.12345678901234", 79228162514264337593543950335m };
        yield return new object?[] { "-999999999999999999999999999999.12345678901234", -79228162514264337593543950335m };
        yield return new object?[] { "9.9999999999999999999999999999912345678901234", null };
        yield return new object?[] { "-9.9999999999999999999999999999912345678901234", null };
    }

    [Theory, MemberData(nameof(QueryDecimalTestData))]
    public async void QueryDecimal_Valid_Success(string column, decimal? expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::numeric");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, await col.AsDecimalAsync());
            }
        }
    }

    public static IEnumerable<object?[]> QueryDecimalIntTestData()
    {
        yield return new object?[] { "0", 0m };
        yield return new object?[] { "1", 1m };
        yield return new object?[] { "+12345", 12345m };
        yield return new object?[] { "12345", 12345m };
        yield return new object?[] { "-12345", -12345m };
        yield return new object?[] { "79228162514264337593543950335", 79228162514264337593543950335m };
        yield return new object?[] { "-79228162514264337593543950335", -79228162514264337593543950335m };
        yield return new object?[] { "79228162514264337593543950336", 79228162514264337593543950335m };
        yield return new object?[] { "-79228162514264337593543950336", -79228162514264337593543950335m };
        yield return new object?[] { "9999999999999999999999999999999999999999", 79228162514264337593543950335m };
        yield return new object?[] { "-9999999999999999999999999999999999999999", -79228162514264337593543950335m };
    }

    [Theory, MemberData(nameof(QueryDecimalIntTestData))]
    public async void QueryDecimal_Int_Success(string column, decimal expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, await col.AsDecimalAsync());
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("'123'")]
    [InlineData("'NaN'")]
    [InlineData("'Infinity'")]
    [InlineData("'-Infinity'")]
    public async void QueryDecimal_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDecimalAsync());
            }
        }
    }

    [Fact]
    public async void QueryDecimal_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDecimalAsync());
            }
        }
    }

    [Fact]
    public async void QueryDecimals_Valid_Success()
    {
        // Arrange
        var expected = new decimal?[] { 0m, 0.1m, 12345.6789m, -12345.6789m, 79228162514264337593543950335m, -79228162514264337593543950335m, 79228162514264337593543950335m, -79228162514264337593543950335m, 79228162514264337593543950335m, -79228162514264337593543950335m, null, null, null };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{0,0.1,12345.67890,-12345.67890,79228162514264337593543950335,-79228162514264337593543950335,79228162514264337593543950336,-79228162514264337593543950336,999999999999999999999999999999.12345678901234,-999999999999999999999999999999.12345678901234,9.9999999999999999999999999999912345678901234,-9.9999999999999999999999999999912345678901234,null}'::numeric[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDecimalsAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(expected[i++], item);
                }
            }
        }
    }

    [Fact]
    public async void QueryDecimals_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDecimalsAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryDecimals_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDecimalsAsync());
            }
        }
    }

    #endregion

    #region Double

    public static IEnumerable<object?[]> QueryDoubleTestData()
    {
        yield return new object?[] { "0", (Func<double, bool>)(d => d == 0d) };
        yield return new object?[] { "0.1", (Func<double, bool>)(d => d == 0.1d) };
        yield return new object?[] { "+12345.67890", (Func<double, bool>)(d => d == 12345.6789d) };
        yield return new object?[] { "12345.67890", (Func<double, bool>)(d => d == 12345.6789d) };
        yield return new object?[] { "-12345.67890", (Func<double, bool>)(d => d == -12345.6789d) };
        yield return new object?[] { "79228162514264337593543950335", (Func<double, bool>)(d => d == 79228162514264337593543950335d) };
        yield return new object?[] { "-79228162514264337593543950335", (Func<double, bool>)(d => d == -79228162514264337593543950335d) };
        yield return new object?[] { "79228162514264337593543950336", (Func<double, bool>)(d => d == 79228162514264337593543950335d) };
        yield return new object?[] { "-79228162514264337593543950336", (Func<double, bool>)(d => d == -79228162514264337593543950335d) };
        yield return new object?[] { "999999999999999999999999999999.12345678901234", (Func<double, bool>)(d => d == 999999999999999999999999999999.12345678901234d) };
        yield return new object?[] { "-999999999999999999999999999999.12345678901234", (Func<double, bool>)(d => d == -999999999999999999999999999999.12345678901234d) };
        yield return new object?[] { "9.9999999999999999999999999999912345678901234", (Func<double, bool>)(d => d == 9.9999999999999999999999999999912345678901234d) };
        yield return new object?[] { "-9.9999999999999999999999999999912345678901234", (Func<double, bool>)(d => d == -9.9999999999999999999999999999912345678901234d) };
        yield return new object?[] { "'Infinity'", (Func<double, bool>)(d => double.IsPositiveInfinity(d)) };
        yield return new object?[] { "'-Infinity'", (Func<double, bool>)(d => double.IsNegativeInfinity(d)) };
        yield return new object?[] { "'NaN'", (Func<double, bool>)(d => double.IsNaN(d)) };
        yield return new object?[] { "1E+308", (Func<double, bool>)(d => d == 1E+308) };
        yield return new object?[] { "1E-307", (Func<double, bool>)(d => d == 1E-307) };
    }

    [Theory, MemberData(nameof(QueryDoubleTestData))]
    public async void QueryDouble_Valid_Success(string column, Func<double, bool> predicate)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::float8");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.True(predicate(await col.ToDoubleAsync()));
            }
        }
    }

    public static IEnumerable<object?[]> QueryDoubleIntTestData()
    {
        yield return new object?[] { "0", (Func<double, bool>)(d => d == 0d) };
        yield return new object?[] { "1", (Func<double, bool>)(d => d == 1d) };
        yield return new object?[] { "+12345", (Func<double, bool>)(d => d == 12345d) };
        yield return new object?[] { "12345", (Func<double, bool>)(d => d == 12345d) };
        yield return new object?[] { "-12345", (Func<double, bool>)(d => d == -12345d) };
        yield return new object?[] { "79228162514264337593543950335", (Func<double, bool>)(d => d == 79228162514264337593543950335d) };
        yield return new object?[] { "-79228162514264337593543950335", (Func<double, bool>)(d => d == -79228162514264337593543950335d) };
        yield return new object?[] { "79228162514264337593543950336", (Func<double, bool>)(d => d == 79228162514264337593543950335d) };
        yield return new object?[] { "-79228162514264337593543950336", (Func<double, bool>)(d => d == -79228162514264337593543950335d) };
        yield return new object?[] { "99999999999999999999999999999912345678901234", (Func<double, bool>)(d => d == 79228162514264337593543950335d) };
        yield return new object?[] { "-99999999999999999999999999999912345678901234", (Func<double, bool>)(d => d == -79228162514264337593543950335d) };
    }

    [Theory, MemberData(nameof(QueryDoubleIntTestData))]
    public async void QueryDouble_Int_Success(string column, Func<double, bool> predicate)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.True(predicate(await col.ToDoubleAsync()));
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("'123'")]
    [InlineData("'NaN'")]
    [InlineData("'Infinity'")]
    [InlineData("'-Infinity'")]
    public async void QueryDouble_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDoubleAsync());
            }
        }
    }

    [Fact]
    public async void QueryDouble_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDoubleAsync());
            }
        }
    }

    [Fact]
    public async void QueryDoubles_Valid_Success()
    {
        // Arrange
        var expected = new double?[] { 0d, 0.1d, 12345.6789d, -12345.6789d, 79228162514264337593543950335d, -79228162514264337593543950335d, 79228162514264337593543950335d, -79228162514264337593543950335d, 1e+30d, -1e+30d, 9.9999999999999999999999999999912345678901234d, -9.9999999999999999999999999999912345678901234d, double.PositiveInfinity, double.NegativeInfinity, double.NaN, 1E+308, 1E-307 };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{0,0.1,12345.67890,-12345.67890,79228162514264337593543950335,-79228162514264337593543950335,79228162514264337593543950336,-79228162514264337593543950336,999999999999999999999999999999.12345678901234,-999999999999999999999999999999.12345678901234,9.9999999999999999999999999999912345678901234,-9.9999999999999999999999999999912345678901234,"Infinity","-Infinity","NaN",1E+308,1E-307}'::float8[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDoublesAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    var e = expected[i++];
                    Assert.Equal(e, item);
                }
            }
        }
    }

    [Fact]
    public async void QueryDoubles_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDoublesAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryDoubles_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsDoublesAsync());
            }
        }
    }

    #endregion

    #region Guid

    [Theory]
    [InlineData("'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11'", "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11")]
    [InlineData("'A0EEBC99-9C0B-4EF8-BB6D-6BB9BD380A11'", "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11")]
    [InlineData("'{a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11}'", "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11")]
    [InlineData("'{A0EEBC99-9C0B-4EF8-BB6D-6BB9BD380A11}'", "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11")]
    [InlineData("'a0eebc999c0b4ef8bb6d6bb9bd380a11'", "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11")]
    [InlineData("'A0EEBC999C0B4EF8BB6D6BB9BD380A11'", "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11")]
    [InlineData("'a0ee-bc99-9c0b-4ef8-bb6d-6bb9-bd38-0a11'", "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11")]
    [InlineData("'A0EE-BC99-9C0B-4EF8-BB6D-6BB9-BD38-0A11'", "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11")]
    public async void QueryGuid_Valid_Success(string column, string expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::uuid");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, (await col.ToGuidAsync()).ToString("D"));
            }
        }
    }

    [Theory]
    [InlineData("''")]
    [InlineData("'some string'")]
    [InlineData("123")]
    [InlineData("true")]
    [InlineData("'01:02:03'")]
    [InlineData("'1/8/1999'")]
    [InlineData("'January 8, 99 BC'")]
    [InlineData("'a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11'")]
    public async void QueryGuid_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsGuidAsync());
            }
        }
    }

    [Fact]
    public async void QueryGuid_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsGuidAsync());
            }
        }
    }

    [Fact]
    public async void QueryGuids_Valid_Success()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{"a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11","A0EEBC99-9C0B-4EF8-BB6D-6BB9BD380A11","{a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11}","{A0EEBC99-9C0B-4EF8-BB6D-6BB9BD380A11}","a0eebc999c0b4ef8bb6d6bb9bd380a11","A0EEBC999C0B4EF8BB6D6BB9BD380A11","a0ee-bc99-9c0b-4ef8-bb6d-6bb9-bd38-0a11","A0EE-BC99-9C0B-4EF8-BB6D-6BB9-BD38-0A11",null}'::uuid[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToGuidsAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    if (i == 8)
                    {
                        Assert.Null(item);
                    }
                    else
                    {
                        Assert.Equal("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11", item?.ToString("D"));
                    }
                    i++;
                }
            }
        }
    }

    [Fact]
    public async void QueryGuids_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsGuidsAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryGuids_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsGuidsAsync());
            }
        }
    }

    #endregion

    #region Int16

    [Theory]
    [InlineData("0", 0)]
    [InlineData("-128", -128)]
    [InlineData("127", 127)]
    [InlineData("-32767", -32767)]
    [InlineData("32767", 32767)]
    // https://www.postgresql.org/docs/current/datatype-numeric.html
    // The documentation says that smallint range is -32768 to 32767,
    // but this test case will raise numeric_value_out_of_range (22003).
    // pgAdmin 4 raises the same error.
    // [InlineData("-32768", -32768)]
    public async void QueryInt16_Valid_Success(string column, short expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::smallint");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, await col.ToInt16Async());
            }
        }
    }

    [Theory]
    [InlineData("0.1", 0)]
    [InlineData("123.456", 123)]
    [InlineData("32767", 32767)]
    [InlineData("-32767", -32767)]
    [InlineData("32768", unchecked((short)32768))]
    [InlineData("-32768", -32768)]
    [InlineData("32769", unchecked((short)32769))]
    [InlineData("-32769", unchecked((short)-32769))]
    public async void QueryInt16_OtherNumeric_Success(string column, short expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, await col.ToInt16Async());
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("'123'")]
    [InlineData("'NaN'")]
    [InlineData("'Infinity'")]
    [InlineData("'-Infinity'")]
    public async void QueryInt16_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt16Async());
            }
        }
    }

    [Fact]
    public async void QueryInt16_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt16Async());
            }
        }
    }

    [Fact]
    public async void QueryInt16s_Valid_Success()
    {
        // Arrange
        var expected = new short?[] { 0, -128, 127, 32767, -32767, null };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{0, -128, 127, 32767, -32767, null}'::smallint[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToInt16sAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(expected[i++], item);
                }
            }
        }
    }

    [Fact]
    public async void QueryInt16s_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt16sAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryInt16s_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt16sAsync());
            }
        }
    }

    #endregion

    #region Int32

    [Theory]
    [InlineData("0", 0)]
    [InlineData("-128", -128)]
    [InlineData("127", 127)]
    [InlineData("-32767", -32767)]
    [InlineData("32767", 32767)]
    [InlineData("-2147483647", -2147483647)]
    [InlineData("2147483647", 2147483647)]
    // https://www.postgresql.org/docs/current/datatype-numeric.html
    // The documentation says that integer range is -2147483648 to 2147483647,
    // but this test case will raise numeric_value_out_of_range (22003).
    // pgAdmin 4 raises the same error.
    //[InlineData("-2147483648", -2147483648)]
    public async void QueryInt32_Valid_Success(string column, int expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::int");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, await col.ToInt32Async());
            }
        }
    }

    [Theory]
    [InlineData("0.1", 0)]
    [InlineData("123.456", 123)]
    [InlineData("123456", 123456)]
    [InlineData("-123456", -123456)]
    [InlineData("2147483648", unchecked((int)2147483648))]
    [InlineData("-2147483648", -2147483648)]
    [InlineData("2147483649", unchecked((int)2147483649))]
    [InlineData("-2147483649", unchecked((int)-2147483649))]
    public async void QueryInt32_OtherNumeric_Success(string column, int expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, await col.ToInt32Async());
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("'123'")]
    [InlineData("'NaN'")]
    [InlineData("'Infinity'")]
    [InlineData("'-Infinity'")]
    public async void QueryInt32_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt32Async());
            }
        }
    }

    [Fact]
    public async void QueryInt32_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt32Async());
            }
        }
    }

    [Fact]
    public async void QueryInt32s_Valid_Success()
    {
        // Arrange
        var expected = new int?[] { 0, -128, 127, 32767, -32767, 2147483647, -2147483647, null };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{0, -128, 127, 32767, -32767, 2147483647, -2147483647, null}'::int[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToInt32sAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(expected[i++], item);
                }
            }
        }
    }

    [Fact]
    public async void QueryInt32s_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt32sAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryInt32s_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt32sAsync());
            }
        }
    }

    #endregion

    #region Int64

    [Theory]
    [InlineData("0", 0)]
    [InlineData("-128", -128)]
    [InlineData("127", 127)]
    [InlineData("-32767", -32767)]
    [InlineData("32767", 32767)]
    [InlineData("-2147483647", -2147483647)]
    [InlineData("2147483647", 2147483647)]
    [InlineData("-9223372036854775807", -9223372036854775807)]
    [InlineData("9223372036854775807", 9223372036854775807)]
    // https://www.postgresql.org/docs/current/datatype-numeric.html
    // The documentation says that bigint range is -9223372036854775808 to 9223372036854775807,
    // but this test case will raise numeric_value_out_of_range (22003).
    // pgAdmin 4 raises the same error.
    //[InlineData("-9223372036854775808", -9223372036854775808)]
    public async void QueryInt64_Valid_Success(string column, long expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::bigint");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, await col.ToInt64Async());
            }
        }
    }

    [Theory]
    [InlineData("0.1", 0)]
    [InlineData("123.456", 123)]
    [InlineData("123456", 123456)]
    [InlineData("-123456", -123456)]
    [InlineData("2147483648", 2147483648)]
    [InlineData("-2147483648", -2147483648)]
    [InlineData("2147483649", 2147483649)]
    [InlineData("-2147483649", -2147483649)]
    [InlineData("-9223372036854775808", -9223372036854775808)]
    public async void QueryInt64_OtherNumeric_Success(string column, long expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, await col.ToInt64Async());
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("'123'")]
    [InlineData("'NaN'")]
    [InlineData("'Infinity'")]
    [InlineData("'-Infinity'")]
    public async void QueryInt64_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt64Async());
            }
        }
    }

    [Fact]
    public async void QueryInt64_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt64Async());
            }
        }
    }

    [Fact]
    public async void QueryInt64s_Valid_Success()
    {
        // Arrange
        var expected = new long?[] { 0, -128, 127, 32767, -32767, 2147483647, -2147483647, 9223372036854775807, -9223372036854775807, null };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{0, -128, 127, 32767, -32767, 2147483647, -2147483647, 9223372036854775807, -9223372036854775807, null}'::bigint[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToInt64sAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(expected[i++], item);
                }
            }
        }
    }

    [Fact]
    public async void QueryInt64s_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt64sAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryInt64s_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsInt64sAsync());
            }
        }
    }

    #endregion

    #region Single

    static bool IsSingleApproximatelyEqual(float value1, float value2, float epsilon)
    {
        // If they are equal anyway, just return True.
        if (value1.Equals(value2))
            return true;

        // Handle NaN, Infinity.
        if (Double.IsInfinity(value1) | Double.IsNaN(value1))
            return value1.Equals(value2);
        else if (Double.IsInfinity(value2) | Double.IsNaN(value2))
            return value1.Equals(value2);

        // Handle zero to avoid division by zero
        double divisor = Math.Max(value1, value2);
        if (divisor.Equals(0))
            divisor = Math.Min(value1, value2);

        return Math.Abs(value1 - value2) / divisor <= epsilon;
    }

    public static IEnumerable<object?[]> QuerySingleTestData()
    {
        yield return new object?[] { "0", (Func<float, bool>)(d => d == 0f) };
        yield return new object?[] { "0.1", (Func<float, bool>)(d => d == 0.1f) };
        yield return new object?[] { "+12345.67890", (Func<float, bool>)(d => d == 12345.6789f) };
        yield return new object?[] { "12345.67890", (Func<float, bool>)(d => d == 12345.6789f) };
        yield return new object?[] { "-12345.67890", (Func<float, bool>)(d => d == -12345.6789f) };
        yield return new object?[] { "79228162514264337593543950335", (Func<float, bool>)(d => d == 79228162514264337593543950335f) };
        yield return new object?[] { "-79228162514264337593543950335", (Func<float, bool>)(d => d == -79228162514264337593543950335f) };
        yield return new object?[] { "79228162514264337593543950336", (Func<float, bool>)(d => d == 79228162514264337593543950335f) };
        yield return new object?[] { "-79228162514264337593543950336", (Func<float, bool>)(d => d == -79228162514264337593543950335f) };
        yield return new object?[] { "999999999999999999999999999999.12345678901234", (Func<float, bool>)(d => d == 999999999999999999999999999999.12345678901234f) };
        yield return new object?[] { "-999999999999999999999999999999.12345678901234", (Func<float, bool>)(d => d == -999999999999999999999999999999.12345678901234f) };
        yield return new object?[] { "9.9999999999999999999999999999912345678901234", (Func<float, bool>)(d => d == 9.9999999999999999999999999999912345678901234f) };
        yield return new object?[] { "-9.9999999999999999999999999999912345678901234", (Func<float, bool>)(d => d == -9.9999999999999999999999999999912345678901234f) };
        yield return new object?[] { "'Infinity'", (Func<float, bool>)(d => float.IsPositiveInfinity(d)) };
        yield return new object?[] { "'-Infinity'", (Func<float, bool>)(d => float.IsNegativeInfinity(d)) };
        yield return new object?[] { "'NaN'", (Func<float, bool>)(d => float.IsNaN(d)) };
        yield return new object?[] { "1E+37", (Func<float, bool>)(d => IsSingleApproximatelyEqual(d, 1E+37f, 0.00000001f)) };
        yield return new object?[] { "1E-37", (Func<float, bool>)(d => IsSingleApproximatelyEqual(d, 1E-37f, 0.00000001f)) };
    }

    [Theory, MemberData(nameof(QuerySingleTestData))]
    public async void QuerySingle_Valid_Success(string column, Func<float, bool> predicate)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::float4");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var f = await col.ToSingleAsync();
                Assert.True(predicate(f));
            }
        }
    }

    public static IEnumerable<object?[]> QuerySingleIntTestData()
    {
        yield return new object?[] { "0", (Func<float, bool>)(d => d == 0f) };
        yield return new object?[] { "1", (Func<float, bool>)(d => d == 1f) };
        yield return new object?[] { "+12345", (Func<float, bool>)(d => d == 12345f) };
        yield return new object?[] { "12345", (Func<float, bool>)(d => d == 12345f) };
        yield return new object?[] { "-12345", (Func<float, bool>)(d => d == -12345f) };
        yield return new object?[] { "79228162514264337593543950335", (Func<float, bool>)(d => d == 79228162514264337593543950335f) };
        yield return new object?[] { "-79228162514264337593543950335", (Func<float, bool>)(d => d == -79228162514264337593543950335f) };
        yield return new object?[] { "79228162514264337593543950336", (Func<float, bool>)(d => d == 79228162514264337593543950335f) };
        yield return new object?[] { "-79228162514264337593543950336", (Func<float, bool>)(d => d == -79228162514264337593543950335f) };
        yield return new object?[] { "99999999999999999999999999999912345678901234", (Func<float, bool>)(d => d == 79228162514264337593543950335f) };
        yield return new object?[] { "-99999999999999999999999999999912345678901234", (Func<float, bool>)(d => d == -79228162514264337593543950335f) };
    }

    [Theory, MemberData(nameof(QuerySingleIntTestData))]
    public async void QuerySingle_Int_Success(string column, Func<float, bool> predicate)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var d = await col.ToSingleAsync();
                Assert.True(predicate(d));
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("'123'")]
    [InlineData("'NaN'")]
    [InlineData("'Infinity'")]
    [InlineData("'-Infinity'")]
    public async void QuerySingle_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsSingleAsync());
            }
        }
    }

    [Fact]
    public async void QuerySingle_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsSingleAsync());
            }
        }
    }

    [Fact]
    public async void QuerySingles_Valid_Success()
    {
        // Arrange
        var expected = new float[] { 0f, 0.1f, 12345.6789f, -12345.6789f, 79228162514264337593543950335f, -79228162514264337593543950335f, 79228162514264337593543950335f, -79228162514264337593543950335f, 1e+30f, -1e+30f, 9.9999999999999999999999999999912345678901234f, -9.9999999999999999999999999999912345678901234f, float.PositiveInfinity, float.NegativeInfinity, float.NaN, 1E+37f, 1E-37f };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{0,0.1,12345.67890,-12345.67890,79228162514264337593543950335,-79228162514264337593543950335,79228162514264337593543950336,-79228162514264337593543950336,999999999999999999999999999999.12345678901234,-999999999999999999999999999999.12345678901234,9.9999999999999999999999999999912345678901234,-9.9999999999999999999999999999912345678901234,"Infinity","-Infinity","NaN",1E+37,1E-37}'::float4[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToSinglesAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    var e = expected[i++];
                    Assert.True(IsSingleApproximatelyEqual(e, item ?? 0f, 0.0000001f));
                }
            }
        }
    }

    [Fact]
    public async void QuerySingles_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsSinglesAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QuerySingles_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsSinglesAsync());
            }
        }
    }

    #endregion

    #region String

    [Theory]
    [InlineData("", "")]
    [InlineData("''", "'")]
    [InlineData("PostgreSQL 数据库。", "PostgreSQL 数据库。")]
    [InlineData("PostgreSQL データベース。", "PostgreSQL データベース。")]
    [InlineData("PostgreSQL 數據庫。", "PostgreSQL 數據庫。")]
    [InlineData("Base de données PostgreSQL.", "Base de données PostgreSQL.")]
    [InlineData("Base de données ''PostgreSQL''.", "Base de données 'PostgreSQL'.")]
    [InlineData("Base de données \"PostgreSQL\".", "Base de données \"PostgreSQL\".")]
    public async void QueryString_Valid_Success(string value, string expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select '{value}'::varchar");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(
                    expected,
                    await col.ToStringAsync(),
                    ignoreCase: false);
            }
        }
    }

    [Fact]
    public async void QueryString_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsStringAsync());
            }
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("0.12345")]
    [InlineData("true")]
    public async void QueryString_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsStringAsync());
            }
        }
    }

    [Fact]
    public async void QueryStrings_Valid_Success()
    {
        // Arrange
        var expected = new string?[]
        {
            "",
            "'",
            "PostgreSQL 数据库。",
            "PostgreSQL データベース。",
            "PostgreSQL 數據庫。",
            "Base de données PostgreSQL",
            null
        };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{"","''","PostgreSQL 数据库。","PostgreSQL データベース。","PostgreSQL 數據庫。","Base de données PostgreSQL",null}'::varchar[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var arrays = await col.ToStringsAsync();

                var i = 0;
                await foreach (var array in arrays)
                {
                    Assert.Equal(expected[i++], array);
                }
            }
        }
    }

    [Fact]
    public async void QueryStrings_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsStringsAsync());
            }
        }
    }

    [Theory]
    [InlineData("0")]
    [InlineData("0.12345")]
    [InlineData("true")]
    public async void QueryStrings_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsStringsAsync());
            }
        }
    }

    #endregion

    #region Time

    [Theory]
    [InlineData("'00:00:00'", "00:00:00.0000000")]
    [InlineData("'24:00:00'", "23:59:59.9999999")]
    [InlineData("'04:05 AM'", "04:05:00.0000000")]
    [InlineData("'04:05 PM'", "16:05:00.0000000")]
    [InlineData("'04:05:06.789'", "04:05:06.7890000")]
    [InlineData("'04:05'", "04:05:00.0000000")]
    [InlineData("'040506'", "04:05:06.0000000")]
    [InlineData("'04:05:06.789-8'", "04:05:06.7890000")]
    [InlineData("'04:05:06-08:00'", "04:05:06.0000000")]
    [InlineData("'040506+0730'", "04:05:06.0000000")]
    [InlineData("'040506+07:30:00'", "04:05:06.0000000")]
    [InlineData("'04:05:06 PST'", "04:05:06.0000000")]
    [InlineData("'2003-04-12 04:05:06 America/New_York'", "04:05:06.0000000")]
    public async void QueryTime_Valid_Success(string column, string expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::time");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expected, (await col.ToTimeAsync()).ToString("HH:mm:ss.fffffff"));
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("123")]
    [InlineData("'01:02:03'")]
    public async void QueryTime_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsTimeAsync());
            }
        }
    }

    [Fact]
    public async void QueryTime_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsTimeAsync());
            }
        }
    }

    [Fact]
    public async void QueryTimes_Valid_Success()
    {
        // Arrange
        var expected = new string?[]
        {
            "00:00:00.0000000",
            "23:59:59.9999999",
            "04:05:00.0000000",
            "16:05:00.0000000",
            "04:05:06.7890000",
            "04:05:06.0000000",
            null
        };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{"00:00:00","24:00:00","04:05 AM","04:05 PM","04:05:06.789","04:05:06-08:00",null}'::time[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToTimesAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(expected[i++], item?.ToString("HH:mm:ss.fffffff"));
                }
            }
        }
    }

    [Fact]
    public async void QueryTimes_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsTimesAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryTimes_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsTimesAsync());
            }
        }
    }

    #endregion

    #region TimeSpan

    [Theory]
    [InlineData("'3 04:05:06.789'", 273906.789000d)]
    [InlineData("'04:05:06'", 14706.000000d)]
    [InlineData("'04:05:06.789'", 14706.789000d)]
    [InlineData("'80 millenniums'", 922337203685.47754d)]
    [InlineData("'80 centuries'", 252460800000.000000d)]
    [InlineData("'80 decades'", 25246080000.000000d)]
    [InlineData("'80 years'", 2524608000.000000d)]
    [InlineData("'80 months'", 210081600.000000d)]
    [InlineData("'80 weeks'", 48384000.000000d)]
    [InlineData("'80 days'", 6912000.000000d)]
    [InlineData("'80 hours'", 288000.000000d)]
    [InlineData("'80 minutes'", 4800.000000d)]
    [InlineData("'80 seconds'", 80.000000d)]
    [InlineData("'80 milliseconds'", 0.080000d)]
    [InlineData("'80 microseconds'", 0.000080d)]
    [InlineData("'1-2'", 36741600.000000d)]
    [InlineData("'1 year 2 months 3 days 4 hours 5 minutes 6 seconds'", 37015506.000000d)]
    [InlineData("'P1Y2M3DT4H5M6S'", 37015506.000000d)]
    [InlineData("'P0001-02-03T04:05:06'", 37015506.000000d)]
    public async void QueryTimeSpan_Valid_Success(string column, double expectedSeconds)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}::interval");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Equal(expectedSeconds, (await col.ToTimeSpanAsync()).TotalSeconds);
            }
        }
    }

    [Theory]
    [InlineData("'some string'")]
    [InlineData("123")]
    [InlineData("'01:02:03'")]
    public async void QueryTimeSpan_OtherType_ReturnsNull(string column)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        // Assert
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsTimeSpanAsync());
            }
        }
    }

    [Fact]
    public async void QueryTimeSpan_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsTimeSpanAsync());
            }
        }
    }

    [Fact]
    public async void QueryTimeSpans_Valid_Success()
    {
        // Arrange
        var expected = new double?[]
        {
            273906.789000d,
            14706.000000d,
            14706.789000d,
            922337203685.47754d,
            252460800000.000000d,
            25246080000.000000d,
            2524608000.000000d,
            210081600.000000d,
            48384000.000000d,
            6912000.000000d,
            288000.000000d,
            4800.000000d,
            80.000000d,
            0.080000d,
            0.000080d,
            36741600.000000d,
            37015506.000000d,
            37015506.000000d,
            37015506.000000d,
            null
        };
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("""
            select '{"3 04:05:06.789","04:05:06","04:05:06.789","80 millenniums","80 centuries","80 decades","80 years","80 months","80 weeks","80 days","80 hours","80 minutes","80 seconds","80 milliseconds","80 microseconds","1-2","1 year 2 months 3 days 4 hours 5 minutes 6 seconds","P1Y2M3DT4H5M6S","P0001-02-03T04:05:06",null}'::interval[]
            """);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToTimeSpansAsync();

                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(expected[i++], item?.TotalSeconds);
                }
            }
        }
    }

    [Fact]
    public async void QueryTimeSpans_Null_ReturnsNull()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select null");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsTimeSpansAsync());
            }
        }
    }

    [Theory]
    [InlineData("true")]
    [InlineData("false")]
    [InlineData("0")]
    [InlineData("'some string'")]
    public async void QueryTimeSpans_OtherType_ReturnsNull(string value)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {value}");
        byte[]? data = Array.Empty<byte>();

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsTimeSpansAsync());
            }
        }
    }

    #endregion

    #region WriteBinary

    public static IEnumerable<object?[]> WriteBinaryTestData()
    {
        yield return new object?[]
        {
            "''::bytea",
            Array.Empty<byte>()
        };

        yield return new object?[]
        {
            "'\\xf041e3f2e54047878886a13143cd8355'::bytea",
            new byte[]{ 0xf0, 0x41, 0xe3, 0xf2, 0xe5, 0x40, 0x47, 0x87, 0x88, 0x86, 0xa1, 0x31, 0x43, 0xcd, 0x83, 0x55 }
        };
    }

    private static bool CompareBinary(byte[]? x, byte[]? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        if (x.Length != y.Length)
        {
            return false;
        }

        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
            {
                return false;
            }
        }

        return true;
    }

    [Theory, MemberData(nameof(WriteBinaryTestData))]
    public async void WriteBinary_Success(string column, byte[]? expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        using var stream = new MemoryStream();
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                stream.Position = 0;
                await col.WriteAsync(stream);
                Assert.True(CompareBinary(expected, stream.ToArray()));
            }
        }
    }

    public static IEnumerable<object?[]> WriteBinaryCountTestData()
    {
        yield return new object?[]
        {
            "''::bytea",
            0
        };

        yield return new object?[]
        {
            "'\\xf041e3f2e54047878886a13143cd8355'::bytea",
            16
        };

        yield return new object?[]
        {
            "null",
            0
        };
    }

    [Theory, MemberData(nameof(WriteBinaryCountTestData))]
    public async void WriteBinary_Count_Success(string column, int expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        using var stream = new MemoryStream();
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                stream.Position = 0;
                var count = await col.WriteAsync(stream);
                Assert.Equal(expected, count);
            }
        }
    }

    #endregion

    #region WriteString

    [Theory]
    [InlineData("''", "")]
    [InlineData("'null'", "null")]
    [InlineData("'PostgreSQL'", "PostgreSQL")]
    [InlineData("'数据库。'", "数据库。")]
    [InlineData("'データベース。'", "データベース。")]
    [InlineData("'數據庫。'", "數據庫。")]
    [InlineData("'Base de données ''PostgreSQL'''", "Base de données 'PostgreSQL'")]
    public async void WriteString_Success(string column, string? expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        using var stream = new MemoryStream();
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                stream.Position = 0;

                using var writer = new StreamWriter(stream, Encoding.UTF8);
                await col.WriteAsync(writer);
                writer.Flush();

                stream.Position = 0;
                using var reader = new StreamReader(stream, Encoding.UTF8);
                var text = reader.ReadToEnd();
                Assert.Equal(expected, text);
            }
        }
    }

    [Theory]
    [InlineData("''", 0)]
    [InlineData("'null'", 4)]
    [InlineData("null", 0)]
    [InlineData("'PostgreSQL'", 10)]
    [InlineData("'数据库。'", 4)]
    [InlineData("'データベース。'", 7)]
    [InlineData("'數據庫。'", 4)]
    [InlineData("'Base de données ''PostgreSQL'''", 28)]
    public async void WriteString_Count_Success(string column, int expected)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        using var stream = new MemoryStream();
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                stream.Position = 0;

                using var writer = new StreamWriter(stream, Encoding.UTF8);
                var count = await col.WriteAsync(writer);
                writer.Flush();
                Assert.Equal(expected, count);
            }
        }
    }

    #endregion

    #region Json

    public static IEnumerable<object?[]> WriteJsonTestData()
    {
        // string
        yield return new object?[]
        {
            "''",
            "EmptyString",
            "",
            """{"emptystring":""}"""
        };

        yield return new object?[]
        {
            "''",
            "\"EmptyString\"",
            "",
            """{"emptyString":""}"""
        };

        yield return new object?[]
        {
            "''",
            "\"EmptyString\"",
            "EmptyString",
            """{"EmptyString":""}"""
        };

        yield return new object?[]
        {
            "'some string'",
            "SomeString",
            "",
            """{"somestring":"some string"}"""
        };

        yield return new object?[]
        {
            "'some string'",
            "\"SomeString\"",
            "",
            """{"someString":"some string"}"""
        };

        yield return new object?[]
        {
            "'some string'",
            "SomeString",
            "SomeString",
            """{"SomeString":"some string"}"""
        };

        // null
        yield return new object?[]
        {
            "null",
            "Null",
            "",
            """{"null":null}"""
        };

        // number
        yield return new object?[]
        {
            "0",
            "Int4",
            "int",
            """{"int":0}"""
        };

        yield return new object?[]
        {
            "123.456",
            "Decimal",
            "DEC",
            """{"DEC":123.456}"""
        };

        yield return new object?[]
        {
            "1e+20",
            "Float",
            "",
            """{"float":100000000000000000000}"""
        };

        yield return new object?[]
        {
            "'1e+300'::float8",
            "Double",
            "",
            """{"double":1E+300}"""
        };

        yield return new object?[]
        {
            "'NaN'::float8",
            "Double",
            "",
            """{"double":"NaN"}"""
        };

        yield return new object?[]
        {
            "'Infinity'::float8",
            "Double",
            "",
            """{"double":"Infinity"}"""
        };

        yield return new object?[]
        {
            "'-Infinity'::float8",
            "Double",
            "",
            """{"double":"-Infinity"}"""
        };

        // datetime
        yield return new object?[]
        {
            "'2023-03-15 18:45:21'::timestamp",
            "DateTime",
            "dateTime",
            """{"dateTime":"2023-03-16T02:45:21.0000000\u002B08:00"}"""
        };

        yield return new object?[]
        {
            "'2023-03-15 18:45:21+08:00'::timestamp",
            "DateTime",
            "dateTime",
            """{"dateTime":"2023-03-16T02:45:21.0000000\u002B08:00"}"""
        };

        yield return new object?[]
        {
            "'2023-03-15 18:45:21+08:00'::timestamptz",
            "\"DateTime\"",
            "",
            """{"dateTime":"2023-03-15T18:45:21.0000000\u002B08:00"}"""
        };

        yield return new object?[]
        {
            "'2023-03-15 18:45:21'::timestamptz",
            "\"DateTime\"",
            "",
            """{"dateTime":"2023-03-15T18:45:21.0000000\u002B08:00"}"""
        };

        // date
        yield return new object?[]
        {
            "'2023-03-15 18:45:21'::date",
            "\"Date\"",
            "",
            """{"date":"2023-03-15"}"""
        };

        yield return new object?[]
        {
            "'2023-03-15'::date",
            "\"Date\"",
            "",
            """{"date":"2023-03-15"}"""
        };

        // time
        yield return new object?[]
        {
            "'18:45:21'::time",
            "\"Time\"",
            "",
            """{"time":"18:45:21"}"""
        };

        yield return new object?[]
        {
            "'18:45:21'::timetz",
            "\"Time\"",
            "",
            """{"time":"18:45:21"}"""
        };

        // guid
        yield return new object?[]
        {
            "'01FA0A39-229C-4A91-96E7-4791C60170A5'::uuid",
            "\"Id\"",
            "",
            """{"id":"01fa0a39-229c-4a91-96e7-4791c60170a5"}"""
        };

        yield return new object?[]
        {
            "'{01fa0a39-229c-4a91-96e7-4791c60170a5}'::uuid",
            "\"Id\"",
            "",
            """{"id":"01fa0a39-229c-4a91-96e7-4791c60170a5"}"""
        };

        yield return new object?[]
        {
            "'01FA0A39229C4A9196E74791C60170A5'::uuid",
            "\"Id\"",
            "",
            """{"id":"01fa0a39-229c-4a91-96e7-4791c60170a5"}"""
        };

        // json
        yield return new object?[]
        {
            """'{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}'::jsonb""",
            "\"Json\"",
            "",
            """{"json":{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}}"""
        };
        yield return new object?[]
        {
            """'{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}'::json""",
            "\"Json\"",
            "",
            """{"json":{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}}"""
        };
    }

    [Theory, MemberData(nameof(WriteJsonTestData))]
    public async void WriteJson(string column, string columnName, string propertyName, string expectedJson)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column} as {columnName}");

        using var stream = new MemoryStream();
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                stream.Position = 0;

                var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
                {
                    Indented = false
                });

                writer.WriteStartObject();
                await col.WriteAsync(writer, propertyName);
                writer.WriteEndObject();
                writer.Flush();

                stream.Position = 0;
                using var reader = new StreamReader(stream, Encoding.UTF8);
                var text = reader.ReadToEnd();
                Assert.Equal(expectedJson, text);
            }
        }
    }

    [Fact]
    public async void Jsonb_ToJsonBinary_Success()
    {
        // Arrange
        var column = """'{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}'::jsonb""";
        var expected = """{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}""";
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var binary = await col.ToJsonBinaryAsync();
                var text = Encoding.UTF8.GetString(binary.Span);
                Assert.Equal(expected, text);
            }
        }
    }

    [Fact]
    public async void Jsonb_ToJsonString_Success()
    {
        // Arrange
        var column = """'{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}'::jsonb""";
        var expected = """{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}""";
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var text = await col.ToJsonStringAsync();
                Assert.Equal(expected, text);
            }
        }
    }

    [Fact]
    public async void Json_ToJsonBinary_Success()
    {
        // Arrange
        var column = """'{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}'::json""";
        var expected = """{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}""";
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var binary = await col.ToJsonBinaryAsync();
                var text = Encoding.UTF8.GetString(binary.Span);
                Assert.Equal(expected, text);
            }
        }
    }

    [Fact]
    public async void Json_ToJsonString_Success()
    {
        // Arrange
        var column = """'{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}'::json""";
        var expected = """{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}""";
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync($"select {column}");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var text = await col.ToJsonStringAsync();
                Assert.Equal(expected, text);
            }
        }
    }

    #endregion
}
