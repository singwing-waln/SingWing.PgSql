namespace SingWing.PgSql.Tests;

public class DatabaseTests : TestBase
{
    #region Id

    [Fact]
    public void GetId_Specified_Success()
    {
        var id = Guid.NewGuid();
        var db = Db.Get(
            "hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example30&user=postgre&password=Js5Y137RzKKHlCzf", 
            id);

        Assert.Equal(id, db.Id);
    }

    [Fact]
    public void GetId_Equals_Success()
    {
        var db1 = Db.Get(
            "hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example31&user=postgre&password=Js5Y137RzKKHlCzf");

        var db2 = Db.Get(
            "hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3&database=example31&user=postgre&password=Js5Y137RzKKHlCzf");

        Assert.Equal(db1.Id, db2.Id);
    }

    [Fact]
    public void GetId_NotEquals_Success()
    {
        var db1 = Db.Get(
            "hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example32&user=postgre&password=Js5Y137RzKKHlCzf");

        var db2 = Db.Get(
            "hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example33&user=postgre&password=Js5Y137RzKKHlCzf");

        Assert.NotEqual(db1.Id, db2.Id);
    }

    #endregion

    #region Name

    [Theory]
    [InlineData(DefaultConnectionString, DatabaseName)]
    [InlineData("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example34&user=postgre&password=Js5Y137RzKKHlCzf", "example34")]
    public void GetName_Success(string connectionString, string databaseName)
    {
        var db = Db.Get(connectionString);

        Assert.Equal(databaseName, db.Name);
    }

    #endregion

    #region UserName

    [Theory]
    [InlineData(DefaultConnectionString, UserName)]
    [InlineData("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example35&user=postgre&password=Js5Y137RzKKHlCzf", "postgre")]
    public void GetUserName_Success(string connectionString, string userName)
    {
        var db = Db.Get(connectionString);

        Assert.Equal(userName, db.UserName);
    }

    #endregion

    #region Nodes

    [Theory]
    [InlineData("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example36&user=postgre&password=Js5Y137RzKKHlCzf", 3)]
    [InlineData("hosts=192.168.1.101:5432|0.2,192.168.1.101|0.3,192.168.1.102|0.5&database=example37&user=postgre&password=Js5Y137RzKKHlCzf", 2)]
    public void GetNodes_Success(string connectionString, int nodeCount)
    {
        var db = Db.Get(connectionString);

        Assert.Equal(nodeCount, db.Nodes.Count);
    }

    #endregion

    #region MaxTextLengthOfCachedExtendedQuery

    [Fact]
    public void GetMaxTextLengthOfCachedExtendedQuery_Default_Success()
    {
        var db = GetDatabase();
        Assert.Equal(256, db.MaxTextLengthOfCachedExtendedQuery);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(1024, 1024)]
    public void MaxTextLengthOfCachedExtendedQuery_Success(int length, int expected)
    {
        var db = GetDatabase();
        db.MaxTextLengthOfCachedExtendedQuery = length;
        Assert.Equal(expected, db.MaxTextLengthOfCachedExtendedQuery);
    }

    #endregion

    #region Use

    [Fact]
    public void Use_Same_Success()
    {
        var db = GetDatabase();
        var other = db.Use(db.Name);
        Assert.Same(db, other);
    }

    [Fact]
    public void Use_Success()
    {
        var otherName = "other_database";
        var db = GetDatabase();
        var other = db.Use(otherName);
        Assert.Multiple(() =>
        {
            Assert.NotSame(db, other);
        }, () =>
        {
            Assert.Equal(otherName, other.Name);
        });
    }

    [Fact]
    public void Use_EmptyName_ThrowsArgumentException()
    {
        var db = GetDatabase();

        Assert.Throws<ArgumentException>(() =>
        {
            db.Use(string.Empty);
        });
    }

    #endregion
}
