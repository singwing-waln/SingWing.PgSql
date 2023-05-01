namespace SingWing.PgSql.Tests;

public class CloudTests
{
    const string ConnectionString = "hosts=localhost&database=singwing&user=singwing&password=singwing";

    //[Fact]
    public async void EnsureNode_Test()
    {
        Db.Logger = FileLogger.Shared;
        var db = Db.Get(ConnectionString);

        await using var transaction = await db.BeginAsync();

        int count = 0;
        try
        {
            // 使用指定的数据库连接字符串，连接到目标数据库，获取私有云和节点信息。
            count = await transaction.QueryCursorsAsync(
                "select public.ensure_node($1,$2,$3,$4,$5)",
                "家",
                "",
                80,
                0,
                0);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        Assert.Equal(3, count);
    }
}
