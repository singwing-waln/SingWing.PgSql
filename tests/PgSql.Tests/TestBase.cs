using System.Text;
using System.Text.Json;

namespace SingWing.PgSql.Tests;

public class TestBase
{
    //public const string Hosts = "localhost,192.168.0.108:5433";
    public const string Hosts = "localhost";

    public const string DatabaseName = "singwing_pgsql_tests";

    public const string UserName = "singwing";

    public const string Password = "singwing";

    public const string ApplicationName = "SingWing";

    public const string DefaultConnectionString = 
        $"hosts={Hosts}&database={DatabaseName}&user={UserName}&password={Password}";

    static TestBase()
    {
        Db.ApplicationName = ApplicationName;
    }

    public static IDatabase GetDatabase(string connectionString = "")
    {
        Db.Logger = FileLogger.Shared;
        return Db.Get(string.IsNullOrEmpty(connectionString) ? DefaultConnectionString : connectionString);
    }

    public static async ValueTask<string> ToJsonStringAsync(IRow row, bool indented)
    {
        await using var stream = new MemoryStream();
        await using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions()
        {
            Indented = indented
        });

        await row.WriteValueAsync(writer);
        await writer.FlushAsync();
        return Encoding.UTF8.GetString(stream.ToArray());
    }
}
