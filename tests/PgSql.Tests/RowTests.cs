using System.Text.Json;
using System.Text;

namespace SingWing.PgSql.Tests;

public class RowTests : TestBase
{
    #region Information

    [Theory]
    [InlineData("select 0", 1)]
    [InlineData("select 0 as \"Id\", '' as \"Name\"", 2)]
    public async void GetColumnCount_Success(string command, int columnCount)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync(command);
        var count = 0;

        await foreach (var row in rows)
        {
            count = row.ColumnCount;
        }

        // Assert
        Assert.Equal(columnCount, count);
    }

    [Theory]
    [InlineData("select 0, ''", "?column?")]
    [InlineData("select 0 as id, '' as name", "name")]
    [InlineData("select 0 as \"Id\", '' as \"Name\"", "Name")]
    public async void NameAt_Success(string command, string columnName)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync(command);
        var name = "";

        await foreach (var row in rows)
        {
            name = row.NameAt(1).ToString();
        }

        // Assert
        Assert.Equal(columnName, name);
    }

    [Theory]
    [InlineData("select 0 as \"Id\", '' as \"Name\"", "\"Name\"")]
    [InlineData("select 0 as \"Id\", '' as \"Name\"", "name")]
    [InlineData("select 0 as id, '' as name", "NAME")]
    public async void NameAt_Failed(string command, string columnName)
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync(command);
        var name = "";

        await foreach (var row in rows)
        {
            name = row.NameAt(1).ToString();
        }

        // Assert
        Assert.NotEqual(columnName, name);
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
    public async void DataTypeAt_Success(int columnOrdinal, DataType expectedDataType)
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
            dataType = row.DataTypeAt(columnOrdinal);
        }

        // Assert
        Assert.Equal((uint)expectedDataType, dataType);
    }

    #endregion

    [Fact]
    public async void Discard_Success()
    {
        // Arrange
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync("select 0 as id, '' as name");

        await foreach (var row in rows)
        {
            await row.DiscardAsync();
        }
    }

    [Theory]
    [InlineData("select 0 as id, 'singwing' as USERNAME", "{\"id\":0,\"username\":\"singwing\"}")]
    [InlineData("select 0 as \"Id\", 'singwing' as \"UserName\"", "{\"id\":0,\"userName\":\"singwing\"}")]
    [InlineData("select 0, 'singwing'", "{\"?column?\":0,\"?column?\":\"singwing\"}")]
    public async void WriteJson_Success(string command, string json)
    {
        // Arrange
        await using var stream = new MemoryStream();
        await using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions()
        {
            Indented = false
        });
        var db = GetDatabase();

        // Act
        var rows = await db.QueryAsync(command);
        var text = "";

        await foreach (var row in rows)
        {
            await row.WriteValueAsync(writer);
            await writer.FlushAsync();
            text = Encoding.UTF8.GetString(stream.ToArray());
        }

        // Assert
        Assert.Equal(json, text);
    }

    [Fact]
    public async void WriteJson_AllDataTypes_Success()
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
            '{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}'::json as "JsonText"
            """);
        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "int16": 2,
              "int16Array": [
                1,
                2,
                3,
                null
              ],
              "int32": 4,
              "int32Array": [
                4,
                5,
                6,
                null
              ],
              "int64": 8,
              "int64Array": [
                8,
                9,
                10,
                null
              ],
              "decimal": 0.1,
              "decimalArray": [
                0.1,
                0.2,
                null
              ],
              "single": 0.2,
              "singleArray": [
                0.2,
                "Infinity",
                "-Infinity",
                "NaN",
                null
              ],
              "double": 0.4,
              "doubleArray": [
                0.4,
                "Infinity",
                "-Infinity",
                "NaN",
                null
              ],
              "boolean": true,
              "booleanArray": [
                true,
                false,
                false,
                true,
                true,
                false,
                true,
                false,
                null
              ],
              "string": "singwing",
              "stringArray": [
                "singwing",
                "waln",
                "",
                null
              ],
              "binary": "3q2+7w==",
              "binaryArray": [
                "3q2+7w==",
                "fZtgAtQAT+OdLZ+oywCoZw==",
                "",
                null
              ],
              "date": "2023-03-11",
              "dateArray": [
                "2023-03-11",
                "2023-03-12",
                null
              ],
              "time": "13:20:52",
              "timeArray": [
                "13:20:52",
                "14:22:53",
                null
              ],
              "dateTime": "2023-10-19T16:23:54.0000000\u002B08:00",
              "dateTimeArray": [
                "2023-10-19T16:23:54.0000000\u002B08:00",
                "2024-11-29T12:23:06.0000000\u002B08:00",
                null
              ],
              "timeSpan": "3.04:05:06",
              "timeSpanArray": [
                "3.04:05:06",
                "425.06:00:00",
                null
              ],
              "guid": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
              "guidArray": [
                "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
                "680c1b68-0227-4dac-8bdd-c16b9d80a11e",
                null
              ],
              "json": {"foo": [true, "bar"], "tags": {"a": 1, "b": null}},
              "jsonText": {"foo": [true, "bar"], "tags": {"a": 1, "b": null}}
            }
            """, json);
    }
}
