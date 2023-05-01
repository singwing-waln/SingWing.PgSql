namespace SingWing.PgSql.Tests;

public class NodeTests : TestBase
{
    #region Server

    [Fact]
    public void GetServer_Success()
    {
        var db = Db.Get("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=nodetests1&user=postgre&password=Js5Y137RzKKHlCzf");
        var hosts = new Host[] 
        { 
            new Host("192.168.1.101", 5432),
            new Host("192.168.1.101", 5433),
            new Host("192.168.1.102", 5432)
        };

        Assert.Contains(db.Nodes, node => hosts.Contains(node.Server.Host));
    }

    #endregion

    #region Database

    [Fact]
    public void GetDatabase_Success()
    {
        var db = Db.Get("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=nodetests2&user=postgre&password=Js5Y137RzKKHlCzf");

        Assert.All(db.Nodes, node =>
        {
            Assert.Same(db, node.Database);
        });
    }

    #endregion
}
