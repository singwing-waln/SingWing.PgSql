using SingWing.PgSql;

namespace PgSql.UdsTests
{
    internal class Program
    {
        static async Task Main()
        {
            const string ConnectionString = "hosts=/var/run/postgresql:5433&database=singwing_pgsql_tests&user=singwing&password=singwing";
            const string CommandText = "SELECT 1";

            Console.WriteLine($"SingWing.PgSql Unix Domain Socket Test");
            Console.WriteLine(ConnectionString);
            Console.WriteLine($"Command: \"{CommandText}\", Expected Output: 1");
            var db = Db.Get(ConnectionString);
            var rows = await db.QueryAsync(CommandText);
            await foreach (var row in rows)
            {
                await foreach (var col in row)
                {
                    Console.WriteLine(await col.ToInt32Async());
                }
            }
        }
    }
}