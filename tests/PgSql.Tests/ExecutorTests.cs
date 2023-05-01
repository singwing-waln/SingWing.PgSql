using System.Text;

namespace SingWing.PgSql.Tests;

public class ExecutorTests : TestBase
{
    #region Database

    [Fact]
    public async void Database_Query_EmptyCommand_ThrowsArgumentException()
    {
        var db = GetDatabase();

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await db.QueryAsync("");
        });
    }

    [Fact]
    public async void Database_Query_WithPrimitiveParameters()
    {
        var db = GetDatabase();

        var rows = await db.QueryAsync("""
            select
            $1::smallint as "Int16",
            $2::int as "Int32",
            $3::bigint as "Int64",
            $4::numeric as "Decimal",
            $5::real as "Single",
            $6::float8 as "Double",
            $7::bool as "Boolean",
            $8::varchar as "String",
            $9::bytea as "Binary",
            $10::date as "Date",
            $11::time as "Time",
            $12::timestamptz as "DateTime",
            $13::interval as "TimeSpan",
            $14::uuid as "Guid"
            """,
            (short)1,
            2,
            3L,
            1.2m,
            2.3f,
            3.4d,
            true,
            "singwing",
            new byte[] { 0xDE, 0xAD, 0xBE, 0xEF },
            new DateOnly(2023, 3, 16),
            new TimeOnly(12, 34, 56),
            new DateTime(2023, 3, 16, 12, 34, 56),
            TimeSpan.FromSeconds(10),
            new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"));

        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "int16": 1,
              "int32": 2,
              "int64": 3,
              "decimal": 1.2,
              "single": 2.3,
              "double": 3.4,
              "boolean": true,
              "string": "singwing",
              "binary": "3q2+7w==",
              "date": "2023-03-16",
              "time": "12:34:56",
              "dateTime": "2023-03-16T12:34:56.0000000\u002B08:00",
              "timeSpan": "00:00:10",
              "guid": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"
            }
            """, json);
    }

    [Fact]
    public async void Database_Query_WithArrayParameters()
    {
        var db = GetDatabase();

        var rows = await db.QueryAsync("""
            select
            $1::smallint[] as "Int16Array",
            $2::int[] as "Int32Array",
            $3::bigint[] as "Int64Array",
            $4::numeric[] as "DecimalArray",
            $5::real[] as "SingleArray",
            $6::float8[] as "DoubleArray",
            $7::boolean[] as "BooleanArray",
            $8::varchar[] as "StringArray",
            $9::text[] as "TextArray",
            $10::bytea[] as "BinaryArray",
            $11::date[] as "DateArray",
            $12::time[] as "TimeArray",
            $13::timestamptz[] as "DateTimeArray",
            $14::interval[] as "TimeSpanArray",
            $15::uuid[] as "GuidArray"
            """,
            new short?[] { 1, 2, 3, null },
            new int?[] { 4, 5, 6, null },
            new long?[] { 8, 9, 10, null },
            new decimal?[] { 0.1m, 0.2m, null },
            new float?[] { 0.2f, float.PositiveInfinity, float.NegativeInfinity, float.NaN, null },
            new double?[] { 0.4d, double.PositiveInfinity, double.NegativeInfinity, double.NaN, null },
            new bool?[] { true, false, null },
            new string?[] { "PostgreSQL", "数据库", "", null },
            new string?[] { "قاعدة بيانات", "PostgreSQL", "", null },
            new byte[]?[] { new byte[] { 0xDE, 0xAD, 0xBE, 0xEF }, new byte[] { 0x7d, 0x9b, 0x60, 0x02, 0xd4, 0x00, 0x4f, 0xe3, 0x9d, 0x2d, 0x9f, 0xa8, 0xcb, 0x00, 0xa8, 0x67 }, null },
            new DateOnly?[] { new DateOnly(2023, 3, 11), new DateOnly(2023, 3, 12), null },
            new TimeOnly?[] { new TimeOnly(13, 20, 52), new TimeOnly(14, 22, 53), null },
            new DateTimeOffset?[] { new DateTimeOffset(2023, 10, 19, 10, 23, 54, TimeSpan.FromHours(2)), new DateTimeOffset(2024, 11, 29, 12, 23, 06, TimeSpan.FromHours(8)), null },
            new TimeSpan?[] { new TimeSpan(3, 4, 5, 6), new TimeSpan(425, 6, 0, 0), null },
            new Guid?[] { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"), new Guid("680c1b6802274dac8bddc16b9d80a11e"), null });

        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "int16Array": [
                1,
                2,
                3,
                null
              ],
              "int32Array": [
                4,
                5,
                6,
                null
              ],
              "int64Array": [
                8,
                9,
                10,
                null
              ],
              "decimalArray": [
                0.1,
                0.2,
                null
              ],
              "singleArray": [
                0.2,
                "Infinity",
                "-Infinity",
                "NaN",
                null
              ],
              "doubleArray": [
                0.4,
                "Infinity",
                "-Infinity",
                "NaN",
                null
              ],
              "booleanArray": [
                true,
                false,
                null
              ],
              "stringArray": [
                "PostgreSQL",
                "\u6570\u636E\u5E93",
                "",
                null
              ],
              "textArray": [
                "\u0642\u0627\u0639\u062F\u0629 \u0628\u064A\u0627\u0646\u0627\u062A",
                "PostgreSQL",
                "",
                null
              ],
              "binaryArray": [
                "3q2+7w==",
                "fZtgAtQAT+OdLZ+oywCoZw==",
                null
              ],
              "dateArray": [
                "2023-03-11",
                "2023-03-12",
                null
              ],
              "timeArray": [
                "13:20:52",
                "14:22:53",
                null
              ],
              "dateTimeArray": [
                "2023-10-19T16:23:54.0000000\u002B08:00",
                "2024-11-29T12:23:06.0000000\u002B08:00",
                null
              ],
              "timeSpanArray": [
                "3.04:05:06",
                "425.06:00:00",
                null
              ],
              "guidArray": [
                "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
                "680c1b68-0227-4dac-8bdd-c16b9d80a11e",
                null
              ]
            }
            """, json);
    }

    [Fact]
    public async void Database_Query_WithJsonParameters()
    {
        var text = """{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}""";
        var db = GetDatabase();

        var rows = await db.QueryAsync("""
            select
            $1::jsonb as "Json",
            $2::json as "JsonText"
            """,
            Encoding.UTF8.GetBytes(text).ToJsonParameter(),
            text.ToJsonParameter());

        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "json": {"foo": [true, "bar"], "tags": {"a": 1, "b": null}},
              "jsonText": {"foo": [true, "bar"], "tags": {"a": 1, "b": null}}
            }
            """, json);
    }

    [Fact]
    public async void Database_Execute_EmptyCommand_ThrowsArgumentException()
    {
        var db = GetDatabase();

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await db.ExecuteAsync("");
        });
    }

    [Fact]
    public async void Database_Execute_NotExists_ThrowsServerException()
    {
        var command = "update public.tests_database set name='' where 1=2";
        var db = GetDatabase();

        await Assert.ThrowsAsync<ServerException>(async () =>
        {
            await db.ExecuteAsync(command);
        });
    }

    [Fact]
    public async void Database_ExecuteStatements()
    {
        var db = GetDatabase();

        await db.PerformAsync("""
            create table if not exists public.test_database_3(id bigint not null, name varchar(50) not null);
            insert into public.test_database_3(id,name) values(1,'case1');
            drop table public.test_database_3;
            """);
    }

    #endregion

    #region Node

    [Fact]
    public async void Node_Query_EmptyCommand_ThrowsArgumentException()
    {
        var db = GetDatabase();

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await db.Nodes[0].QueryAsync("");
        });
    }

    [Fact]
    public async void Node_Query_WithPrimitiveParameters()
    {
        var db = GetDatabase();

        var rows = await db.Nodes[0].QueryAsync("""
            select
            $1::smallint as "Int16",
            $2::int as "Int32",
            $3::bigint as "Int64",
            $4::numeric as "Decimal",
            $5::real as "Single",
            $6::float8 as "Double",
            $7::bool as "Boolean",
            $8::varchar as "String",
            $9::bytea as "Binary",
            $10::date as "Date",
            $11::time as "Time",
            $12::timestamptz as "DateTime",
            $13::interval as "TimeSpan",
            $14::uuid as "Guid"
            """,
            (short)1,
            2,
            3L,
            1.2m,
            2.3f,
            3.4d,
            true,
            "singwing",
            new byte[] { 0xDE, 0xAD, 0xBE, 0xEF },
            new DateOnly(2023, 3, 16),
            new TimeOnly(12, 34, 56),
            new DateTime(2023, 3, 16, 12, 34, 56),
            TimeSpan.FromSeconds(10),
            new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"));

        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "int16": 1,
              "int32": 2,
              "int64": 3,
              "decimal": 1.2,
              "single": 2.3,
              "double": 3.4,
              "boolean": true,
              "string": "singwing",
              "binary": "3q2+7w==",
              "date": "2023-03-16",
              "time": "12:34:56",
              "dateTime": "2023-03-16T12:34:56.0000000\u002B08:00",
              "timeSpan": "00:00:10",
              "guid": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"
            }
            """, json);
    }

    [Fact]
    public async void Node_Query_WithArrayParameters()
    {
        var db = GetDatabase();

        var rows = await db.Nodes[0].QueryAsync("""
            select
            $1::smallint[] as "Int16Array",
            $2::int[] as "Int32Array",
            $3::bigint[] as "Int64Array",
            $4::numeric[] as "DecimalArray",
            $5::real[] as "SingleArray",
            $6::float8[] as "DoubleArray",
            $7::boolean[] as "BooleanArray",
            $8::varchar[] as "StringArray",
            $9::text[] as "TextArray",
            $10::bytea[] as "BinaryArray",
            $11::date[] as "DateArray",
            $12::time[] as "TimeArray",
            $13::timestamptz[] as "DateTimeArray",
            $14::interval[] as "TimeSpanArray",
            $15::uuid[] as "GuidArray"
            """,
            new short?[] { 1, 2, 3, null },
            new int?[] { 4, 5, 6, null },
            new long?[] { 8, 9, 10, null },
            new decimal?[] { 0.1m, 0.2m, null },
            new float?[] { 0.2f, float.PositiveInfinity, float.NegativeInfinity, float.NaN, null },
            new double?[] { 0.4d, double.PositiveInfinity, double.NegativeInfinity, double.NaN, null },
            new bool?[] { true, false, null },
            new string?[] { "PostgreSQL", "数据库", "", null },
            new string?[] { "قاعدة بيانات", "PostgreSQL", "", null },
            new byte[]?[] { new byte[] { 0xDE, 0xAD, 0xBE, 0xEF }, new byte[] { 0x7d, 0x9b, 0x60, 0x02, 0xd4, 0x00, 0x4f, 0xe3, 0x9d, 0x2d, 0x9f, 0xa8, 0xcb, 0x00, 0xa8, 0x67 }, null },
            new DateOnly?[] { new DateOnly(2023, 3, 11), new DateOnly(2023, 3, 12), null },
            new TimeOnly?[] { new TimeOnly(13, 20, 52), new TimeOnly(14, 22, 53), null },
            new DateTimeOffset?[] { new DateTimeOffset(2023, 10, 19, 10, 23, 54, TimeSpan.FromHours(2)), new DateTimeOffset(2024, 11, 29, 12, 23, 06, TimeSpan.FromHours(8)), null },
            new TimeSpan?[] { new TimeSpan(3, 4, 5, 6), new TimeSpan(425, 6, 0, 0), null },
            new Guid?[] { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"), new Guid("680c1b6802274dac8bddc16b9d80a11e"), null });

        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "int16Array": [
                1,
                2,
                3,
                null
              ],
              "int32Array": [
                4,
                5,
                6,
                null
              ],
              "int64Array": [
                8,
                9,
                10,
                null
              ],
              "decimalArray": [
                0.1,
                0.2,
                null
              ],
              "singleArray": [
                0.2,
                "Infinity",
                "-Infinity",
                "NaN",
                null
              ],
              "doubleArray": [
                0.4,
                "Infinity",
                "-Infinity",
                "NaN",
                null
              ],
              "booleanArray": [
                true,
                false,
                null
              ],
              "stringArray": [
                "PostgreSQL",
                "\u6570\u636E\u5E93",
                "",
                null
              ],
              "textArray": [
                "\u0642\u0627\u0639\u062F\u0629 \u0628\u064A\u0627\u0646\u0627\u062A",
                "PostgreSQL",
                "",
                null
              ],
              "binaryArray": [
                "3q2+7w==",
                "fZtgAtQAT+OdLZ+oywCoZw==",
                null
              ],
              "dateArray": [
                "2023-03-11",
                "2023-03-12",
                null
              ],
              "timeArray": [
                "13:20:52",
                "14:22:53",
                null
              ],
              "dateTimeArray": [
                "2023-10-19T16:23:54.0000000\u002B08:00",
                "2024-11-29T12:23:06.0000000\u002B08:00",
                null
              ],
              "timeSpanArray": [
                "3.04:05:06",
                "425.06:00:00",
                null
              ],
              "guidArray": [
                "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
                "680c1b68-0227-4dac-8bdd-c16b9d80a11e",
                null
              ]
            }
            """, json);
    }

    [Fact]
    public async void Node_Query_WithJsonParameters()
    {
        var text = """{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}""";
        var db = GetDatabase();

        var rows = await db.Nodes[0].QueryAsync("""
            select
            $1::jsonb as "Json",
            $2::json as "JsonText"
            """,
            Encoding.UTF8.GetBytes(text).ToJsonParameter(),
            text.ToJsonParameter());

        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "json": {"foo": [true, "bar"], "tags": {"a": 1, "b": null}},
              "jsonText": {"foo": [true, "bar"], "tags": {"a": 1, "b": null}}
            }
            """, json);
    }

    [Fact]
    public async void Node_Execute_EmptyCommand_ThrowsArgumentException()
    {
        var db = GetDatabase();

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await db.Nodes[0].ExecuteAsync("");
        });
    }

    [Fact]
    public async void Node_Execute_NotExists_ThrowsServerException()
    {
        var command = "update public.tests_node set name='' where 1=2";
        var db = GetDatabase();

        await Assert.ThrowsAsync<ServerException>(async () =>
        {
            await db.Nodes[0].ExecuteAsync(command);
        });
    }

    [Fact]
    public async void Node_ExecuteStatements()
    {
        var db = GetDatabase();

        await db.Nodes[0].PerformAsync("""
            create table if not exists public.test_node_3(id bigint not null, name varchar(50) not null);
            insert into public.test_node_3(id,name) values(1,'case1');
            drop table public.test_node_3;
            """);
    }

    #endregion

    #region Connection

    [Fact]
    public async void Connection_Query_EmptyCommand_ThrowsArgumentException()
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await connection.QueryAsync("");
        });
    }

    [Fact]
    public async void Connection_Query_WithPrimitiveParameters()
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        var rows = await connection.QueryAsync("""
            select
            $1::smallint as "Int16",
            $2::int as "Int32",
            $3::bigint as "Int64",
            $4::numeric as "Decimal",
            $5::real as "Single",
            $6::float8 as "Double",
            $7::bool as "Boolean",
            $8::varchar as "String",
            $9::bytea as "Binary",
            $10::date as "Date",
            $11::time as "Time",
            $12::timestamptz as "DateTime",
            $13::interval as "TimeSpan",
            $14::uuid as "Guid"
            """,
            (short)1,
            2,
            3L,
            1.2m,
            2.3f,
            3.4d,
            true,
            "singwing",
            new byte[] { 0xDE, 0xAD, 0xBE, 0xEF },
            new DateOnly(2023, 3, 16),
            new TimeOnly(12, 34, 56),
            new DateTime(2023, 3, 16, 12, 34, 56),
            TimeSpan.FromSeconds(10),
            new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"));

        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "int16": 1,
              "int32": 2,
              "int64": 3,
              "decimal": 1.2,
              "single": 2.3,
              "double": 3.4,
              "boolean": true,
              "string": "singwing",
              "binary": "3q2+7w==",
              "date": "2023-03-16",
              "time": "12:34:56",
              "dateTime": "2023-03-16T12:34:56.0000000\u002B08:00",
              "timeSpan": "00:00:10",
              "guid": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"
            }
            """, json);
    }

    [Fact]
    public async void Connection_Query_WithArrayParameters()
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        var rows = await connection.QueryAsync("""
            select
            $1::smallint[] as "Int16Array",
            $2::int[] as "Int32Array",
            $3::bigint[] as "Int64Array",
            $4::numeric[] as "DecimalArray",
            $5::real[] as "SingleArray",
            $6::float8[] as "DoubleArray",
            $7::boolean[] as "BooleanArray",
            $8::varchar[] as "StringArray",
            $9::text[] as "TextArray",
            $10::bytea[] as "BinaryArray",
            $11::date[] as "DateArray",
            $12::time[] as "TimeArray",
            $13::timestamptz[] as "DateTimeArray",
            $14::interval[] as "TimeSpanArray",
            $15::uuid[] as "GuidArray"
            """,
            new short?[] { 1, 2, 3, null },
            new int?[] { 4, 5, 6, null },
            new long?[] { 8, 9, 10, null },
            new decimal?[] { 0.1m, 0.2m, null },
            new float?[] { 0.2f, float.PositiveInfinity, float.NegativeInfinity, float.NaN, null },
            new double?[] { 0.4d, double.PositiveInfinity, double.NegativeInfinity, double.NaN, null },
            new bool?[] { true, false, null },
            new string?[] { "PostgreSQL", "数据库", "", null },
            new string?[] { "قاعدة بيانات", "PostgreSQL", "", null },
            new byte[]?[] { new byte[] { 0xDE, 0xAD, 0xBE, 0xEF }, new byte[] { 0x7d, 0x9b, 0x60, 0x02, 0xd4, 0x00, 0x4f, 0xe3, 0x9d, 0x2d, 0x9f, 0xa8, 0xcb, 0x00, 0xa8, 0x67 }, null },
            new DateOnly?[] { new DateOnly(2023, 3, 11), new DateOnly(2023, 3, 12), null },
            new TimeOnly?[] { new TimeOnly(13, 20, 52), new TimeOnly(14, 22, 53), null },
            new DateTimeOffset?[] { new DateTimeOffset(2023, 10, 19, 10, 23, 54, TimeSpan.FromHours(2)), new DateTimeOffset(2024, 11, 29, 12, 23, 06, TimeSpan.FromHours(8)), null },
            new TimeSpan?[] { new TimeSpan(3, 4, 5, 6), new TimeSpan(425, 6, 0, 0), null },
            new Guid?[] { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"), new Guid("680c1b6802274dac8bddc16b9d80a11e"), null });

        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "int16Array": [
                1,
                2,
                3,
                null
              ],
              "int32Array": [
                4,
                5,
                6,
                null
              ],
              "int64Array": [
                8,
                9,
                10,
                null
              ],
              "decimalArray": [
                0.1,
                0.2,
                null
              ],
              "singleArray": [
                0.2,
                "Infinity",
                "-Infinity",
                "NaN",
                null
              ],
              "doubleArray": [
                0.4,
                "Infinity",
                "-Infinity",
                "NaN",
                null
              ],
              "booleanArray": [
                true,
                false,
                null
              ],
              "stringArray": [
                "PostgreSQL",
                "\u6570\u636E\u5E93",
                "",
                null
              ],
              "textArray": [
                "\u0642\u0627\u0639\u062F\u0629 \u0628\u064A\u0627\u0646\u0627\u062A",
                "PostgreSQL",
                "",
                null
              ],
              "binaryArray": [
                "3q2+7w==",
                "fZtgAtQAT+OdLZ+oywCoZw==",
                null
              ],
              "dateArray": [
                "2023-03-11",
                "2023-03-12",
                null
              ],
              "timeArray": [
                "13:20:52",
                "14:22:53",
                null
              ],
              "dateTimeArray": [
                "2023-10-19T16:23:54.0000000\u002B08:00",
                "2024-11-29T12:23:06.0000000\u002B08:00",
                null
              ],
              "timeSpanArray": [
                "3.04:05:06",
                "425.06:00:00",
                null
              ],
              "guidArray": [
                "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
                "680c1b68-0227-4dac-8bdd-c16b9d80a11e",
                null
              ]
            }
            """, json);
    }

    [Fact]
    public async void Connection_Query_WithJsonParameters()
    {
        var text = """{"foo": [true, "bar"], "tags": {"a": 1, "b": null}}""";
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        var rows = await connection.QueryAsync("""
            select
            $1::jsonb as "Json",
            $2::json as "JsonText"
            """,
            Encoding.UTF8.GetBytes(text).ToJsonParameter(),
            text.ToJsonParameter());

        var json = "";

        await foreach (var row in rows)
        {
            json = await ToJsonStringAsync(row, indented: true);
        }

        // Assert
        Assert.Equal("""
            {
              "json": {"foo": [true, "bar"], "tags": {"a": 1, "b": null}},
              "jsonText": {"foo": [true, "bar"], "tags": {"a": 1, "b": null}}
            }
            """, json);
    }

    [Fact]
    public async void Connection_Execute_EmptyCommand_ThrowsArgumentException()
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await connection.ExecuteAsync("");
        });
    }

    [Fact]
    public async void Connection_Execute_NotExists_ThrowsServerException()
    {
        var command = "update public.tests_connection set name='' where 1=2";
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        await Assert.ThrowsAsync<ServerException>(async () =>
        {
            await connection.ExecuteAsync(command);
        });
    }

    [Fact]
    public async void Connection_ExecuteStatements()
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        await connection.PerformAsync("""
            create table if not exists public.test_connection_3(id bigint not null, name varchar(50) not null);
            insert into public.test_connection_3(id,name) values(1,'case1');
            drop table public.test_connection_3;
            """);
    }

    [Fact]
    public async void Connection_Execute_WithoutParameters()
    {
        var commands = new Dictionary<string, long>()
        {
            { "create table if not exists public.test_connection_1(id bigint not null, name varchar(50) not null)", 0L },
            { "update public.test_connection_1 set name='' where 1=2", 0L },
            { "insert into public.test_connection_1(id,name) values(1,'case1')", 1L },
            { "insert into public.test_connection_1(id,name) values(2,'case2')", 1L },
            { "update public.test_connection_1 set name='case' where id>=1", 2L },
            { "delete from public.test_connection_1 where id>=1", 2L },
            { "select id,name from public.test_connection_1 where id>=1", 0L },
            { "drop table public.test_connection_1", 0L }
        };

        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        foreach (var command in commands)
        {
            var rowCount = await connection.ExecuteAsync(command.Key);
            Assert.Equal(command.Value, rowCount);
        }
    }

    [Fact]
    public async void Connection_Execute_WithParameters()
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        await connection.ExecuteAsync("create table if not exists public.test_connection_2(id bigint not null, name varchar(50) not null)");
        var rowCount = await connection.ExecuteAsync(
            "insert into public.test_connection_2(id,name) values($1,$2)",
            1L,
            "case1");

        Assert.Equal(1, rowCount);

        var rows = await connection.QueryAsync("select id,name from public.test_connection_2 where id=$1", 1L);
        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                switch (col.Name)
                {
                    case "id":
                        Assert.Equal(1L, await col.ToInt64Async());
                        break;
                    case "name":
                        Assert.Equal("case1", await col.ToStringAsync());
                        break;
                    default:
                        break;
                }
            }
        }

        await connection.ExecuteAsync("drop table public.test_connection_2");
    }

    #endregion
}
