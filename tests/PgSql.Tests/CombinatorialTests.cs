using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SingWing.PgSql.Tests;

public class CombinatorialTests : TestBase
{
    private static async ValueTask<IConnection> EnsureContextForPrimitive(string name, string dataType)
    {
        var db = GetDatabase();
        var connection = await db.AcquireAsync();

        await connection.ExecuteAsync($"""
            CREATE TABLE IF NOT EXISTS public.{name}_test
            (
                "id"                   int,
                "{name}"          {dataType}
            );
            """);

        await connection.ExecuteAsync($"""
            TRUNCATE public.{name}_test;
            """);

        return connection;
    }

    public static IEnumerable<object?[]> Primitive_TestData()
    {
        #region int2

        yield return new object?[] 
        { 
            1, 
            "int2", 
            "smallint", 
            new ParameterValidator((short?)null, async col => await col.AsInt16Async() == null) 
        };

        yield return new object?[]
        {
            2,
            "int2",
            "smallint",
            new ParameterValidator((short?)0, async col => await col.AsInt16Async() == 0)
        };

        yield return new object?[]
        {
            3,
            "int2",
            "smallint",
            new ParameterValidator((short?)1, async col => await col.AsInt16Async() == 1)
        };

        yield return new object?[]
        {
            4,
            "int2",
            "smallint",
            new ParameterValidator(short.MinValue, async col => await col.AsInt16Async() == short.MinValue)
        };

        yield return new object?[]
        {
            5,
            "int2",
            "smallint",
            new ParameterValidator(short.MaxValue, async col => await col.AsInt16Async() == short.MaxValue)
        };

        #endregion

        #region int4

        yield return new object?[]
        {
            1,
            "int4",
            "int",
            new ParameterValidator((int?)null, async col => await col.AsInt32Async() == null)
        };

        yield return new object?[]
        {
            2,
            "int4",
            "int",
            new ParameterValidator((int?)0, async col => await col.AsInt32Async() == 0)
        };

        yield return new object?[]
        {
            3,
            "int4",
            "int",
            new ParameterValidator((int?)1, async col => await col.AsInt32Async() == 1)
        };

        yield return new object?[]
        {
            4,
            "int4",
            "int",
            new ParameterValidator(int.MinValue, async col => await col.AsInt32Async() == int.MinValue)
        };

        yield return new object?[]
        {
            5,
            "int4",
            "int",
            new ParameterValidator(int.MaxValue, async col => await col.AsInt32Async() == int.MaxValue)
        };

        #endregion

        #region int8

        yield return new object?[]
        {
            1,
            "int8",
            "bigint",
            new ParameterValidator((long?)null, async col => await col.AsInt64Async() == null)
        };

        yield return new object?[]
        {
            2,
            "int8",
            "bigint",
            new ParameterValidator((long?)0, async col => await col.AsInt64Async() == 0)
        };

        yield return new object?[]
        {
            3,
            "int8",
            "bigint",
            new ParameterValidator((long?)1, async col => await col.AsInt64Async() == 1)
        };

        yield return new object?[]
        {
            4,
            "int8",
            "bigint",
            new ParameterValidator(long.MinValue, async col => await col.AsInt64Async() == long.MinValue)
        };

        yield return new object?[]
        {
            5,
            "int8",
            "bigint",
            new ParameterValidator(long.MaxValue, async col => await col.AsInt64Async() == long.MaxValue)
        };

        #endregion

        #region float4

        yield return new object?[]
        {
            1,
            "float4",
            "float4",
            new ParameterValidator((float?)null, async col => await col.AsSingleAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "float4",
            "float4",
            new ParameterValidator((float?)0, async col => await col.AsSingleAsync() == 0f)
        };

        yield return new object?[]
        {
            3,
            "float4",
            "float4",
            new ParameterValidator((float?)1, async col => await col.AsSingleAsync() == 1f)
        };

        yield return new object?[]
        {
            4,
            "float4",
            "float4",
            new ParameterValidator(float.NaN, async col => float.IsNaN(await col.ToSingleAsync()))
        };

        yield return new object?[]
        {
            5,
            "float4",
            "float4",
            new ParameterValidator(float.NegativeInfinity, async col => float.IsNegativeInfinity(await col.ToSingleAsync()))
        };

        yield return new object?[]
        {
            6,
            "float4",
            "float4",
            new ParameterValidator(float.PositiveInfinity, async col => float.IsPositiveInfinity(await col.ToSingleAsync()))
        };

        yield return new object?[]
        {
            7,
            "float4",
            "float4",
            new ParameterValidator(float.MinValue, async col => await col.AsSingleAsync() == float.MinValue)
        };

        yield return new object?[]
        {
            8,
            "float4",
            "float4",
            new ParameterValidator(float.MaxValue, async col => await col.AsSingleAsync() == float.MaxValue)
        };

        #endregion

        #region float8

        yield return new object?[]
        {
            1,
            "float8",
            "float8",
            new ParameterValidator((double?)null, async col => await col.AsDoubleAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "float8",
            "float8",
            new ParameterValidator((double?)0, async col => await col.AsDoubleAsync() == 0d)
        };

        yield return new object?[]
        {
            3,
            "float8",
            "float8",
            new ParameterValidator((double?)1, async col => await col.AsDoubleAsync() == 1d)
        };

        yield return new object?[]
        {
            4,
            "float8",
            "float8",
            new ParameterValidator(double.NaN, async col => double.IsNaN(await col.ToDoubleAsync()))
        };

        yield return new object?[]
        {
            5,
            "float8",
            "float8",
            new ParameterValidator(double.NegativeInfinity, async col => double.IsNegativeInfinity(await col.ToDoubleAsync()))
        };

        yield return new object?[]
        {
            6,
            "float8",
            "float8",
            new ParameterValidator(double.PositiveInfinity, async col => double.IsPositiveInfinity(await col.ToDoubleAsync()))
        };

        yield return new object?[]
        {
            7,
            "float8",
            "float8",
            new ParameterValidator(double.MinValue, async col => await col.AsDoubleAsync() == double.MinValue)
        };

        yield return new object?[]
        {
            8,
            "float8",
            "float8",
            new ParameterValidator(double.MaxValue, async col => await col.AsDoubleAsync() == double.MaxValue)
        };

        #endregion

        #region numeric

        yield return new object?[]
        {
            1,
            "numeric",
            "numeric",
            new ParameterValidator((decimal?)null, async col => await col.AsDecimalAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "numeric",
            "numeric",
            new ParameterValidator((decimal?)0m, async col => await col.AsDecimalAsync() == 0m)
        };

        yield return new object?[]
        {
            3,
            "numeric",
            "numeric",
            new ParameterValidator((decimal?)1m, async col => await col.AsDecimalAsync() == 1m)
        };

        yield return new object?[]
        {
            4,
            "numeric",
            "numeric",
            new ParameterValidator((decimal?)1.234m, async col => await col.AsDecimalAsync() == 1.234m)
        };

        yield return new object?[]
        {
            5,
            "numeric",
            "numeric",
            new ParameterValidator(decimal.MinValue, async col => await col.AsDecimalAsync() == decimal.MinValue)
        };

        yield return new object?[]
        {
            6,
            "numeric",
            "numeric",
            new ParameterValidator(decimal.MaxValue, async col => await col.AsDecimalAsync() == decimal.MaxValue)
        };

        #endregion

        #region boolean

        yield return new object?[]
        {
            1,
            "boolean",
            "boolean",
            new ParameterValidator((bool?)null, async col => await col.AsBooleanAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "boolean",
            "boolean",
            new ParameterValidator(true, async col => await col.AsBooleanAsync() == true)
        };

        yield return new object?[]
        {
            3,
            "boolean",
            "boolean",
            new ParameterValidator(false, async col => await col.AsBooleanAsync() == false)
        };

        #endregion

        #region varchar

        yield return new object?[]
        {
            1,
            "varchar",
            "varchar",
            new ParameterValidator((string?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "varchar",
            "varchar",
            new ParameterValidator((char[]?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            3,
            "varchar",
            "varchar",
            new ParameterValidator((ReadOnlyMemory<char>?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            4,
            "varchar",
            "varchar",
            new ParameterValidator((Memory<char>?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            5,
            "varchar",
            "varchar",
            new ParameterValidator((ArraySegment<char>?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            6,
            "varchar",
            "varchar",
            new ParameterValidator("SingWing", async col => await col.AsStringAsync() == "SingWing")
        };

        yield return new object?[]
        {
            7,
            "varchar",
            "varchar",
            new ParameterValidator("数据库", async col => await col.AsStringAsync() == "数据库")
        };

        yield return new object?[]
        {
            8,
            "varchar",
            "varchar",
            new ParameterValidator(new char[]{ 'W', 'a', 'l', 'n' }, async col => await col.AsStringAsync() == "Waln")
        };

        #endregion

        #region text

        yield return new object?[]
        {
            1,
            "text",
            "text",
            new ParameterValidator((string?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "text",
            "text",
            new ParameterValidator((char[]?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            3,
            "text",
            "text",
            new ParameterValidator((ReadOnlyMemory<char>?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            4,
            "text",
            "text",
            new ParameterValidator((Memory<char>?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            5,
            "text",
            "text",
            new ParameterValidator((ArraySegment<char>?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            6,
            "text",
            "text",
            new ParameterValidator("SingWing", async col => await col.AsStringAsync() == "SingWing")
        };

        yield return new object?[]
        {
            7,
            "text",
            "text",
            new ParameterValidator("数据库", async col => await col.AsStringAsync() == "数据库")
        };

        yield return new object?[]
        {
            8,
            "text",
            "text",
            new ParameterValidator(new char[]{ 'W', 'a', 'l', 'n' }, async col => await col.AsStringAsync() == "Waln")
        };

        #endregion

        #region char

        yield return new object?[]
        {
            1,
            "char",
            "char(50)",
            new ParameterValidator((string?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "char",
            "char(50)",
            new ParameterValidator((char[]?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            3,
            "char",
            "char(50)",
            new ParameterValidator((ReadOnlyMemory<char>?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            4,
            "char",
            "char(50)",
            new ParameterValidator((Memory<char>?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            5,
            "char",
            "char(50)",
            new ParameterValidator((ArraySegment<char>?)null, async col => await col.AsStringAsync() == null)
        };

        yield return new object?[]
        {
            6,
            "char",
            "char(50)",
            new ParameterValidator("SingWing", async col => (await col.AsStringAsync())?.Trim() == "SingWing")
        };

        yield return new object?[]
        {
            7,
            "char",
            "char(50)",
            new ParameterValidator("数据库", async col => (await col.AsStringAsync())?.Trim() == "数据库")
        };

        yield return new object?[]
        {
            8,
            "char",
            "char(50)",
            new ParameterValidator(new char[]{ 'W', 'a', 'l', 'n' }, async col => (await col.AsStringAsync())?.Trim() == "Waln")
        };

        #endregion

        #region bytea

        yield return new object?[]
        {
            1,
            "bytea",
            "bytea",
            new ParameterValidator((byte[]?)null, async col => await col.AsByteArrayAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "bytea",
            "bytea",
            new ParameterValidator((ReadOnlyMemory<byte>?)null, async col => await col.AsByteArrayAsync() == null)
        };

        yield return new object?[]
        {
            3,
            "bytea",
            "bytea",
            new ParameterValidator((Memory<byte>?)null, async col => await col.AsByteArrayAsync() == null)
        };

        yield return new object?[]
        {
            4,
            "bytea",
            "bytea",
            new ParameterValidator((ArraySegment<byte>?)null, async col => await col.AsByteArrayAsync() == null)
        };

        yield return new object?[]
        {
            5,
            "bytea",
            "bytea",
            new ParameterValidator(Encoding.UTF8.GetBytes("SingWing"), async col => Encoding.UTF8.GetString(await col.ToByteArrayAsync()) == "SingWing")
        };

        yield return new object?[]
        {
            6,
            "bytea",
            "bytea",
            new ParameterValidator(Encoding.UTF8.GetBytes("数据库"), async col => Encoding.UTF8.GetString(await col.ToByteArrayAsync()) == "数据库")
        };

        #endregion

        #region date

        yield return new object?[]
        {
            1,
            "date",
            "date",
            new ParameterValidator((DateOnly?)null, async col => await col.AsDateAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "date",
            "date",
            new ParameterValidator(new DateOnly(2023, 1, 1), async col => await col.AsDateAsync() == new DateOnly(2023, 1, 1))
        };

        yield return new object?[]
        {
            3,
            "date",
            "date",
            new ParameterValidator(DateOnly.MinValue, async col => await col.AsDateAsync() == DateOnly.MinValue)
        };

        yield return new object?[]
        {
            4,
            "date",
            "date",
            new ParameterValidator(DateOnly.MaxValue, async col => await col.AsDateAsync() == DateOnly.MaxValue)
        };

        #endregion

        #region time

        yield return new object?[]
        {
            1,
            "time",
            "time",
            new ParameterValidator((TimeOnly?)null, async col => await col.AsTimeAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "time",
            "time",
            new ParameterValidator(new TimeOnly(1, 2, 3), async col => await col.AsTimeAsync() == new TimeOnly(1, 2, 3))
        };

        yield return new object?[]
        {
            3,
            "time",
            "time",
            new ParameterValidator(TimeOnly.MinValue, async col => await col.AsTimeAsync() == TimeOnly.MinValue)
        };

        yield return new object?[]
        {
            4,
            "time",
            "time",
            new ParameterValidator(TimeOnly.MaxValue, async col => await col.AsTimeAsync() == Db.MaxTime)
        };

        #endregion

        #region timetz

        yield return new object?[]
        {
            1,
            "timetz",
            "timetz",
            new ParameterValidator((TimeOnly?)null, async col => await col.AsTimeAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "timetz",
            "timetz",
            new ParameterValidator(new TimeOnly(1, 2, 3), async col => await col.AsTimeAsync() == new TimeOnly(1, 2, 3))
        };

        yield return new object?[]
        {
            3,
            "timetz",
            "timetz",
            new ParameterValidator(TimeOnly.MinValue, async col => await col.AsTimeAsync() == TimeOnly.MinValue)
        };

        yield return new object?[]
        {
            4,
            "timetz",
            "timetz",
            new ParameterValidator(TimeOnly.MaxValue, async col => await col.AsTimeAsync() == Db.MaxTime)
        };

        #endregion

        #region timestamptz

        yield return new object?[]
        {
            1,
            "timestamptz",
            "timestamptz",
            new ParameterValidator((DateTime?)null, async col => await col.AsDateTimeAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "timestamptz",
            "timestamptz",
            new ParameterValidator((DateTimeOffset?)null, async col => await col.AsDateTimeAsync() == null)
        };

        yield return new object?[]
        {
            3,
            "timestamptz",
            "timestamptz",
            new ParameterValidator(new DateTime(2023, 2, 3, 4, 5, 6, 7), async col => await col.AsDateTimeAsync() == new DateTime(2023, 2, 3, 4, 5, 6, 7))
        };

        yield return new object?[]
        {
            4,
            "timestamptz",
            "timestamptz",
            new ParameterValidator(DateTime.MinValue, async col => await col.AsDateTimeAsync() == DateTime.MinValue)
        };

        yield return new object?[]
        {
            5,
            "timestamptz",
            "timestamptz",
            new ParameterValidator(DateTime.MaxValue, async col => await col.AsDateTimeAsync() == DateTime.MaxValue)
        };

        yield return new object?[]
        {
            6,
            "timestamptz",
            "timestamptz",
            new ParameterValidator(new DateTimeOffset(new DateTime(2023, 2, 3, 4, 5, 6, 7, DateTimeKind.Utc), TimeSpan.FromHours(0)), async col => await col.AsDateTimeAsync() == new DateTime(2023, 2, 3, 12, 5, 6, 7))
        };

        yield return new object?[]
        {
            7,
            "timestamptz",
            "timestamptz",
            new ParameterValidator(DateTimeOffset.MinValue, async col => await col.AsDateTimeAsync() == DateTimeOffset.MinValue.DateTime)
        };

        yield return new object?[]
        {
            8,
            "timestamptz",
            "timestamptz",
            new ParameterValidator(DateTimeOffset.MaxValue, async col => await col.AsDateTimeAsync() == DateTimeOffset.MaxValue.DateTime)
        };

        #endregion

        #region timestamp

        yield return new object?[]
        {
            1,
            "timestamp",
            "timestamp",
            new ParameterValidator((DateTime?)null, async col => await col.AsDateTimeAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "timestamp",
            "timestamp",
            new ParameterValidator((DateTimeOffset?)null, async col => await col.AsDateTimeAsync() == null)
        };

        yield return new object?[]
        {
            3,
            "timestamp",
            "timestamp",
            new ParameterValidator(new DateTime(2023, 2, 3, 4, 5, 6, 7), async col => await col.AsDateTimeAsync() == new DateTime(2023, 2, 3, 12, 5, 6, 7))
        };

        yield return new object?[]
        {
            4,
            "timestamp",
            "timestamp",
            new ParameterValidator(DateTime.MinValue, async col => await col.AsDateTimeAsync() == DateTime.MinValue)
        };

        yield return new object?[]
        {
            5,
            "timestamp",
            "timestamp",
            new ParameterValidator(DateTime.MaxValue, async col => await col.AsDateTimeAsync() == DateTime.MaxValue)
        };

        yield return new object?[]
        {
            6,
            "timestamp",
            "timestamp",
            new ParameterValidator(new DateTimeOffset(new DateTime(2023, 2, 3, 4, 5, 6, 7)), async col => await col.AsDateTimeAsync() == new DateTime(2023, 2, 3, 12, 5, 6, 7))
        };

        yield return new object?[]
        {
            7,
            "timestamp",
            "timestamp",
            new ParameterValidator(DateTimeOffset.MinValue, async col => await col.AsDateTimeAsync() == DateTimeOffset.MinValue.DateTime)
        };

        yield return new object?[]
        {
            8,
            "timestamp",
            "timestamp",
            new ParameterValidator(DateTimeOffset.MaxValue, async col => await col.AsDateTimeAsync() == DateTimeOffset.MaxValue.DateTime)
        };

        #endregion

        #region interval

        yield return new object?[]
        {
            1,
            "interval",
            "interval",
            new ParameterValidator((TimeSpan?)null, async col => await col.AsTimeSpanAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "interval",
            "interval",
            new ParameterValidator(new TimeSpan(1, 2, 3, 4, 5, 6), async col => await col.AsTimeSpanAsync() == new TimeSpan(1, 2, 3, 4, 5, 6))
        };

        yield return new object?[]
        {
            3,
            "interval",
            "interval",
            new ParameterValidator(TimeSpan.MinValue, async col => await col.AsTimeSpanAsync() == TimeSpan.MinValue)
        };

        yield return new object?[]
        {
            4,
            "interval",
            "interval",
            new ParameterValidator(TimeSpan.MaxValue, async col => await col.AsTimeSpanAsync() == TimeSpan.MaxValue)
        };

        #endregion

        #region uuid

        yield return new object?[]
        {
            1,
            "uuid",
            "uuid",
            new ParameterValidator((Guid?)null, async col => await col.AsGuidAsync() == null)
        };

        yield return new object?[]
        {
            2,
            "uuid",
            "uuid",
            new ParameterValidator(new Guid("0C73258D-D52C-4EC9-97FF-4403AAE13683"), async col => await col.AsGuidAsync() == new Guid("0C73258D-D52C-4EC9-97FF-4403AAE13683"))
        };

        yield return new object?[]
        {
            3,
            "uuid",
            "uuid",
            new ParameterValidator(Guid.Empty, async col => await col.AsGuidAsync() == Guid.Empty)
        };

        #endregion
    }

    [Theory, MemberData(nameof(Primitive_TestData))]
    public async void Primitive_Test(int id, string name, string dataType, ParameterValidator validator)
    {
        await using var connection = await EnsureContextForPrimitive(name, dataType);

        await connection.ExecuteAsync(
            $"""INSERT INTO public.{name}_test("id", "{name}") VALUES($1, $2)""",
            id,
            validator.Parameter);

        var rows = await connection.QueryAsync(
            $"""SELECT "{name}" FROM public.{name}_test WHERE "id" = $1""",
            id);

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.True(await validator.Validate(col));
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_test;
            """);
    }

    private static async ValueTask<IConnection> EnsureContextForArray(string name, string dataType)
    {
        var db = GetDatabase();
        var connection = await db.AcquireAsync();

        await connection.ExecuteAsync($"""
            CREATE TABLE IF NOT EXISTS public.{name}_array_test
            (
                "{name}_array"         {dataType}[]
            );
            """);

        await connection.ExecuteAsync($"""
            TRUNCATE public.{name}_array_test;
            """);

        return connection;
    }

    [Fact]
    public async void Int2_Array_Test()
    {
        var name = "int2";
        var dataType = "smallint";
        var array = new short?[] { null, 0, 1, short.MinValue, short.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToInt16sAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Int4_Array_Test()
    {
        var name = "int4";
        var dataType = "int";
        var array = new int?[] { null, 0, 1, int.MinValue, int.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToInt32sAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Int8_Array_Test()
    {
        var name = "int8";
        var dataType = "bigint";
        var array = new long?[] { null, 0, 1, long.MinValue, long.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToInt64sAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Float4_Array_Test()
    {
        var name = "float4";
        var dataType = "float4";
        var array = new float?[] { null, 0f, 1f, float.NaN, float.NegativeInfinity, float.PositiveInfinity, float.MinValue, float.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToSinglesAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    if (i == 3)
                    {
                        Assert.True(float.IsNaN(item!.Value));
                    }
                    else if (i == 4)
                    {
                        Assert.True(float.IsNegativeInfinity(item!.Value));
                    }
                    else if (i == 5)
                    {
                        Assert.True(float.IsPositiveInfinity(item!.Value));
                    }
                    else
                    {
                        Assert.Equal(array[i], item);
                    }
                    i++;
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Float8_Array_Test()
    {
        var name = "float8";
        var dataType = "float8";
        var array = new double?[] { null, 0d, 1d, double.NaN, double.NegativeInfinity, double.PositiveInfinity, double.MinValue, double.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDoublesAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    if (i == 3)
                    {
                        Assert.True(double.IsNaN(item!.Value));
                    }
                    else if (i == 4)
                    {
                        Assert.True(double.IsNegativeInfinity(item!.Value));
                    }
                    else if (i == 5)
                    {
                        Assert.True(double.IsPositiveInfinity(item!.Value));
                    }
                    else
                    {
                        Assert.Equal(array[i], item);
                    }
                    i++;
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Numeric_Array_Test()
    {
        var name = "numeric";
        var dataType = "numeric";
        var array = new decimal?[] { null, 0m, 1m, 1.234m, decimal.MinValue, decimal.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDecimalsAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Boolean_Array_Test()
    {
        var name = "boolean";
        var dataType = "boolean";
        var array = new bool?[] { null, true, false };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToBooleansAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Theory]
    [InlineData("varchar", "varchar")]
    [InlineData("text", "text")]
    [InlineData("char(50)", "char")]
    public async void String_String_Array_Test(string dataType, string dataTypeName)
    {
        var name = $"{dataTypeName}_string";
        var array = new string?[] { null, "", "SingWing", "数据库" };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToStringsAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item?.Trim());
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Theory]
    [InlineData("varchar", "varchar")]
    [InlineData("text", "text")]
    [InlineData("char(50)", "char")]
    public async void String_CharArray_Array_Test(string dataType, string dataTypeName)
    {
        var name = $"{dataTypeName}_char_array";
        var array = new char[]?[] { null, Array.Empty<char>(), "SingWing".ToCharArray(), "数据库".ToCharArray() };
        var values = new string?[] { null, "", "SingWing", "数据库" };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToStringsAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(values[i++], item?.Trim());
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Theory]
    [InlineData("varchar", "varchar")]
    [InlineData("text", "text")]
    [InlineData("char(50)", "char")]
    public async void String_ReadOnlyMemory_Array_Test(string dataType, string dataTypeName)
    {
        var name = $"{dataTypeName}_readonly_memory";
        var array = new ReadOnlyMemory<char>?[] { null, ReadOnlyMemory<char>.Empty, "SingWing".AsMemory(), "数据库".AsMemory() };
        var values = new string?[] { null, "", "SingWing", "数据库" };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToStringsAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(values[i++], item?.Trim());
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Theory]
    [InlineData("varchar", "varchar")]
    [InlineData("text", "text")]
    [InlineData("char(50)", "char")]
    public async void String_Memory_Array_Test(string dataType, string dataTypeName)
    {
        var name = $"{dataTypeName}_memory";
        var array = new Memory<char>?[] { null, Memory<char>.Empty, "SingWing".ToCharArray().AsMemory(), "数据库".ToCharArray().AsMemory() };
        var values = new string?[] { null, "", "SingWing", "数据库" };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToStringsAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(values[i++], item?.Trim());
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Theory]
    [InlineData("varchar", "varchar")]
    [InlineData("text", "text")]
    [InlineData("char(50)", "char")]
    public async void String_ArraySegment_Array_Test(string dataType, string dataTypeName)
    {
        var name = $"{dataTypeName}_arraysegment";
        var array = new ArraySegment<char>?[] { null, ArraySegment<char>.Empty, "SingWing".ToCharArray(), "数据库".ToCharArray() };
        var values = new string?[] { null, "", "SingWing", "数据库" };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToStringsAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(values[i++], item?.Trim());
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Bytea_ByteArray_Array_Test()
    {
        var name = "bytea_byte_array";
        var dataType = "bytea";
        var array = new byte[]?[] { null, Array.Empty<byte>(), Encoding.UTF8.GetBytes("SingWing"), Encoding.UTF8.GetBytes("数据库") };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToByteArraysAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    if (i == 0)
                    {
                        Assert.Null(item);
                    }
                    else if (i == 1)
                    {
                        Assert.Empty(item!);
                    }
                    else if (i == 2)
                    {
                        Assert.Equal("SingWing", Encoding.UTF8.GetString(item!));
                    }
                    else if (i == 3)
                    {
                        Assert.Equal("数据库", Encoding.UTF8.GetString(item!));
                    }
                    i++;
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Bytea_ReadOnlyMemory_Array_Test()
    {
        var name = "bytea_readonly_memory";
        var dataType = "bytea";
        var array = new ReadOnlyMemory<byte>?[] { null, ReadOnlyMemory<byte>.Empty, Encoding.UTF8.GetBytes("SingWing").AsMemory(), Encoding.UTF8.GetBytes("数据库").AsMemory() };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToByteArraysAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    if (i == 0)
                    {
                        Assert.Null(item);
                    }
                    else if (i == 1)
                    {
                        Assert.Empty(item!);
                    }
                    else if (i == 2)
                    {
                        Assert.Equal("SingWing", Encoding.UTF8.GetString(item!));
                    }
                    else if (i == 3)
                    {
                        Assert.Equal("数据库", Encoding.UTF8.GetString(item!));
                    }
                    i++;
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Bytea_Memory_Array_Test()
    {
        var name = "bytea_memory";
        var dataType = "bytea";
        var array = new Memory<byte>?[] { null, Memory<byte>.Empty, Encoding.UTF8.GetBytes("SingWing").AsMemory(), Encoding.UTF8.GetBytes("数据库").AsMemory() };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToByteArraysAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    if (i == 0)
                    {
                        Assert.Null(item);
                    }
                    else if (i == 1)
                    {
                        Assert.Empty(item!);
                    }
                    else if (i == 2)
                    {
                        Assert.Equal("SingWing", Encoding.UTF8.GetString(item!));
                    }
                    else if (i == 3)
                    {
                        Assert.Equal("数据库", Encoding.UTF8.GetString(item!));
                    }
                    i++;
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Bytea_ArraySegment_Array_Test()
    {
        var name = "bytea_arraysegment";
        var dataType = "bytea";
        var array = new ArraySegment<byte>?[] { null, ArraySegment<byte>.Empty, Encoding.UTF8.GetBytes("SingWing"), Encoding.UTF8.GetBytes("数据库") };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToByteArraysAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    if (i == 0)
                    {
                        Assert.Null(item);
                    }
                    else if (i == 1)
                    {
                        Assert.Empty(item!);
                    }
                    else if (i == 2)
                    {
                        Assert.Equal("SingWing", Encoding.UTF8.GetString(item!));
                    }
                    else if (i == 3)
                    {
                        Assert.Equal("数据库", Encoding.UTF8.GetString(item!));
                    }
                    i++;
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Date_Array_Test()
    {
        var name = "date";
        var dataType = "date";
        var array = new DateOnly?[] { null, new DateOnly(2023, 1, 1), DateOnly.MinValue, DateOnly.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDatesAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Theory]
    [InlineData("time")]
    [InlineData("timetz")]
    public async void Time_Array_Test(string dataType)
    {
        var name = "time";
        var array = new TimeOnly?[] { null, new TimeOnly(1, 2, 3), TimeOnly.MinValue, TimeOnly.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToTimesAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    if (i == 3)
                    {
                        Assert.Equal(Db.MaxTime, item);
                    }
                    else
                    {
                        Assert.Equal(array[i++], item);
                    }
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void TimestampTz_DateTime_Array_Test()
    {
        var name = "timestamptz_datetime";
        var dataType = "timestamptz";
        var array = new DateTime?[] { null, new DateTime(2023, 2, 3, 4, 5, 6, 7), DateTime.MinValue, DateTime.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDateTimesAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void TimestampTz_DateTimeOffset_Array_Test()
    {
        var name = "timestamptz_datetimeoffset";
        var dataType = "timestamptz";
        var array = new DateTimeOffset?[] { null, new DateTimeOffset(new DateTime(2023, 2, 3, 4, 5, 6, 7, DateTimeKind.Utc), TimeSpan.FromHours(0)), DateTimeOffset.MinValue, DateTimeOffset.MaxValue };
        var values = new DateTime?[] { null, new DateTime(2023, 2, 3, 12, 5, 6, 7), DateTimeOffset.MinValue.DateTime, DateTimeOffset.MaxValue.DateTime };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDateTimesAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(values[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Timestamp_DateTime_Array_Test()
    {
        var name = "timestamp_datetime";
        var dataType = "timestamp";
        var array = new DateTime?[] { null, new DateTime(2023, 2, 3, 4, 5, 6, 7), DateTime.MinValue, DateTime.MaxValue };
        var values = new DateTime?[] { null, new DateTime(2023, 2, 3, 12, 5, 6, 7), DateTime.MinValue, DateTime.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDateTimesAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(values[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Timestamp_DateTimeOffset_Array_Test()
    {
        var name = "timestamp_datetimeoffset";
        var dataType = "timestamp";
        var array = new DateTimeOffset?[] { null, new DateTimeOffset(new DateTime(2023, 2, 3, 4, 5, 6, 7)), DateTimeOffset.MinValue, DateTimeOffset.MaxValue };
        var values = new DateTime?[] { null, new DateTime(2023, 2, 3, 12, 5, 6, 7), DateTimeOffset.MinValue.DateTime, DateTimeOffset.MaxValue.DateTime };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToDateTimesAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(values[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Interval_Array_Test()
    {
        var name = "interval";
        var dataType = "interval";
        var array = new TimeSpan?[] { null, new TimeSpan(1, 2, 3, 4, 5, 6), TimeSpan.MinValue, TimeSpan.MaxValue };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToTimeSpansAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    [Fact]
    public async void Uuid_Array_Test()
    {
        var name = "uuid";
        var dataType = "uuid";
        var array = new Guid?[] { null, new Guid("0C73258D-D52C-4EC9-97FF-4403AAE13683"), Guid.Empty };
        await using var connection = await EnsureContextForArray(name, dataType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{name}_array_test("{name}_array") VALUES($1);
            """,
            array);

        var rows = await connection.QueryAsync($"""SELECT "{name}_array" FROM public.{name}_array_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                var items = await col.ToGuidsAsync();
                var i = 0;
                await foreach (var item in items)
                {
                    Assert.Equal(array[i++], item);
                }
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{name}_array_test;
            """);
    }

    private static async ValueTask<IConnection> EnsureContextForJson(string dataType, string parameterType)
    {
        var db = GetDatabase();
        var connection = await db.AcquireAsync();

        await connection.ExecuteAsync($"""
            CREATE TABLE IF NOT EXISTS public.{dataType}_{parameterType}_test
            (
                "{dataType}_json"         {dataType}
            );
            """);

        await connection.ExecuteAsync($"""
            TRUNCATE public.{dataType}_{parameterType}_test;
            """);

        return connection;
    }

    private static Parameter MakeJsonParameter(string jsonString, string parameterType)
    {
        switch (parameterType)
        {
            case "string":
                return jsonString.ToJsonParameter();
            case "char_array":
                return jsonString.ToCharArray().ToJsonParameter();
            case "readonlymemory_char":
                return jsonString.AsMemory().ToJsonParameter();
            case "memory_char":
                return jsonString.ToCharArray().AsMemory().ToJsonParameter();
            case "arraysegment_char":
                return new ArraySegment<char>(jsonString.ToCharArray()).ToJsonParameter();
            case "byte_array":
                return Encoding.UTF8.GetBytes(jsonString).ToJsonParameter();
            case "readonlymemory_byte":
                return ((ReadOnlyMemory<byte>)(Encoding.UTF8.GetBytes(jsonString).AsMemory())).ToJsonParameter();
            case "memory_byte":
                return (Encoding.UTF8.GetBytes(jsonString).AsMemory()).ToJsonParameter();
            case "arraysegment_byte":
                return new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonString)).ToJsonParameter();
            case "stream":
                var stream = new MemoryStream();
                stream.Write(Encoding.UTF8.GetBytes(jsonString));
                stream.Position = 0;
                return stream.ToJsonParameter(ownsStream: true);
            case "json_document":
                {
                    using var doc1 = JsonDocument.Parse(jsonString);
                    return (Parameter)doc1;
                }
            case "json_element":
                {
                    using var doc2 = JsonDocument.Parse(jsonString);
                    return (Parameter)doc2.RootElement;
                }
            case "json_node":
                return (Parameter)JsonNode.Parse(jsonString);
            default:
                throw new InvalidDataException(parameterType);
        }
    }

    private static async ValueTask<string> ToJsonStringAsync(IColumn col, string resultType)
    {
        switch (resultType)
        {
            case "binary":
                var binary = await col.ToJsonBinaryAsync();
                return Encoding.UTF8.GetString(binary.Span);
            case "string":
                return await col.ToJsonStringAsync();
            case "json_document":
                {
                    using var doc = JsonDocument.Parse(await col.ToJsonBinaryAsync());
                    return doc.RootElement.GetRawText();
                }
            case "json_node":
                return JsonNode.Parse((await col.ToJsonBinaryAsync()).Span)?.ToJsonString() ?? "";
            case "writer":
                {
                    using var stream = new MemoryStream();
                    using var writer = new Utf8JsonWriter(stream);
                    await col.WriteValueAsync(writer);
                    writer.Flush();
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            default:
                return "";
        }
    }

    public static IEnumerable<object?[]> Json_TestData()
    {
        var dataTypes = new string[] { "jsonb", "json" };
        var parameterTypes = new string[] { 
            "string", "char_array", "readonlymemory_char", "memory_char", "arraysegment_char", 
            "byte_array", "readonlymemory_byte", "memory_byte", "arraysegment_byte", "stream",
            "json_document", "json_element", "json_node"};
        var resultTypes = new string[] { "binary", "string", "json_document", "json_node", "writer" };

        foreach (var dataType in dataTypes)
        {
            foreach (var parameterType in parameterTypes)
            {
                foreach (var resultType in resultTypes)
                {
                    yield return new object?[] { dataType, parameterType, resultType };
                }
            }
        }
    }

    [Theory, MemberData(nameof(Json_TestData))]
    public async void Json_Test(string dataType, string parameterType, string resultType)
    {
        var jsonString = """{"foo":[true,"bar"],"tags":{"a":1,"b":null}}""";
        await using var connection = await EnsureContextForJson(dataType, parameterType);

        await connection.ExecuteAsync($"""
            INSERT INTO public.{dataType}_{parameterType}_test("{dataType}_json") VALUES($1);
            """,
            MakeJsonParameter(jsonString, parameterType));

        var rows = await connection.QueryAsync($"""SELECT "{dataType}_json" FROM public.{dataType}_{parameterType}_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                string trimmed = string.Concat((await ToJsonStringAsync(col, resultType)).Where(c => !char.IsWhiteSpace(c)));
                Assert.Equal(jsonString, trimmed);
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{dataType}_{parameterType}_test;
            """);
    }

    [Theory]
    [InlineData("jsonb", "bytes_null")]
    [InlineData("jsonb", "chars_null")]
    [InlineData("jsonb", "stream_null")]
    [InlineData("json", "bytes_null")]
    [InlineData("json", "chars_null")]
    [InlineData("json", "stream_null")]
    public async void Json_Null_Test(string dataType, string parameterType)
    {
        await using var connection = await EnsureContextForJson(dataType, parameterType);

        Parameter parameter;

        if (parameterType == "stream_null")
        {
            parameter = ((Stream?)null).ToJsonParameter();
        }
        else if (parameterType == "chars_null")
        {
            parameter = ((string?)null).ToJsonParameter();
        }
        else
        {
            parameter = ((byte[]?)null).ToJsonParameter();
        }

        await connection.ExecuteAsync($"""
            INSERT INTO public.{dataType}_{parameterType}_test("{dataType}_json") VALUES($1);
            """,
            parameter);

        var rows = await connection.QueryAsync($"""SELECT "{dataType}_json" FROM public.{dataType}_{parameterType}_test""");

        await foreach (var row in rows)
        {
            await foreach (var col in row)
            {
                Assert.Null(await col.AsJsonBinaryAsync());
            }
        }

        await connection.ExecuteAsync($"""
            DROP TABLE public.{dataType}_{parameterType}_test;
            """);
    }

    public sealed class ParameterValidator
    {
        internal ParameterValidator(Parameter parameter, Func<IColumn, ValueTask<bool>> validate)
        {
            Parameter = parameter;
            Validate = validate;
        }

        public Parameter Parameter { get; }

        public Func<IColumn, ValueTask<bool>> Validate { get; }
    }
}
