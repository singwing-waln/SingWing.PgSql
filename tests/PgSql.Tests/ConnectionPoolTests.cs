namespace SingWing.PgSql.Tests;

public class ConnectionPoolTests : TestBase
{
    [Fact]
    public async void DatabaseAsConnectionPool_Success()
    {
        var pool = GetDatabase();

        var connection1 = await pool.AcquireAsync();

        try
        {
            await using var connection2 = await pool.AcquireAsync();

            Assert.NotSame(connection1, connection2);
        }
        finally
        {
            await connection1.DisposeAsync();
        }
    }

    [Fact]
    public async void NodeAsConnectionPool_Success()
    {
        var pool = GetDatabase().Nodes[0];

        var connection1 = await pool.AcquireAsync();

        try
        {
            await using var connection2 = await pool.AcquireAsync();

            Assert.NotSame(connection1, connection2);
        }
        finally
        {
            await connection1.DisposeAsync();
        }
    }
}
