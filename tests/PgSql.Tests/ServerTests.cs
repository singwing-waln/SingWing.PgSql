namespace SingWing.PgSql.Tests;

public class ServerTests : TestBase
{
    #region ConnectionsProportion

    [Theory]
    [InlineData("hosts=192.168.2.101:5432|0.2&database=servertests1&user=postgre&password=Js5Y137RzKKHlCzf", 0.2d)]
    [InlineData("hosts=192.168.2.102|0.65&database=servertests2&user=postgre&password=Js5Y137RzKKHlCzf", 0.65d)]
    public void GetConnectionsProportion_Success(string connectionString, double proportion)
    {
        var db = Db.Get(connectionString, Guid.NewGuid());

        Assert.Equal(proportion, db.Nodes[0].Server.ConnectionsProportion);
    }

    [Theory]
    [InlineData(0d, 1d)]
    [InlineData(-1d, 1d)]
    [InlineData(0.7d, 0.7d)]
    [InlineData(2d, 1d)]
    public void SetConnectionsProportion_Success(double proportion, double expected)
    {
        var db = Db.Get("hosts=192.168.2.103:5432|0.2&database=servertests3&user=postgre&password=Js5Y137RzKKHlCzf", Guid.NewGuid());
        var server = db.Nodes[0].Server;
        server.ConnectionsProportion = proportion;

        Assert.Equal(expected, server.ConnectionsProportion);
    }

    #endregion

    #region MaxConnectionCount

    [Fact]
    public void GetMaxConnectionCount_Success()
    {
        var db = Db.Get("hosts=192.168.2.103:5432|0.2&database=servertests4&user=postgre&password=Js5Y137RzKKHlCzf", Guid.NewGuid());
        var server = db.Nodes[0].Server;

        Assert.Equal(-1, server.MaxConnectionCount);
    }

    #endregion

    #region HeartbeatInterval

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(0, 1)]
    [InlineData(3000, 3000)]
    [InlineData(86400, 86400)]
    [InlineData(86401, 86400)]
    public void HeartbeatIntervalSeconds_Success(int seconds, int expected)
    {
        var db = Db.Get("hosts=192.168.2.104:5432|0.2&database=servertests5&user=postgre&password=Js5Y137RzKKHlCzf", Guid.NewGuid());
        var server = db.Nodes[0].Server;
        server.HeartbeatIntervalSeconds = seconds;

        Assert.Equal(expected, server.HeartbeatIntervalSeconds);
    }

    #endregion
}
