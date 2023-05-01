namespace SingWing.PgSql.Tests;

public class DbTests : TestBase
{
    #region Get

    [Theory]
    [InlineData("192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5", "example1", "postgre", "Js5Y137RzKKHlCzf", 3)]
    [InlineData("192.168.1.101:5432|0.2,/var/run/postgresql:5432|0.5", "example2", "postgre", "Js5Y137RzKKHlCzf", 2)]
    [InlineData("/var/run/postgresql:5432|0.5", "example3", "postgre", "Js5Y137RzKKHlCzf", 1)]
    public void GetDatabase_Parameters_Success(string hosts, string database, string user, string password, int nodeCount)
    {
        var db = Db.Get(hosts, database, user, password);

        Assert.Multiple(() =>
        {
            Assert.Equal(database, db.Name);
        }, 
        () =>
        {
            Assert.Equal(user, db.UserName);
        },
        () =>
        {
            Assert.Equal(nodeCount, db.Nodes.Count);
        });
    }

    [Theory]
    [InlineData("", "example4", "postgre", "Js5Y137RzKKHlCzf")]
    [InlineData(" \r\n\t", "example5", "postgre", "Js5Y137RzKKHlCzf")]
    [InlineData(",,,", "example6", "postgre", "Js5Y137RzKKHlCzf")]
    [InlineData(",\t, ,\r\n", "example7", "postgre", "Js5Y137RzKKHlCzf")]
    [InlineData("192.168.1.101", "", "postgre", "Js5Y137RzKKHlCzf")]
    [InlineData("192.168.1.101", "example8", "", "Js5Y137RzKKHlCzf")]
    [InlineData("192.168.1.101", "VeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryLongDatabaseName", "postgre", "Js5Y137RzKKHlCzf")]
    [InlineData("192.168.1.101", "example9", "VeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryLongUserName", "Js5Y137RzKKHlCzf")]
    [InlineData("192.168.1.101", "example10", "postgre", "VeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryLongPassword")]
    public void GetDatabase_Parameters_ThrowsArgumentException(string hosts, string database, string user, string password)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Db.Get(hosts, database, user, password);
        });
    }

    [Theory]
    [InlineData("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example11&user=postgre&password=Js5Y137RzKKHlCzf", "example11", "postgre", 3)]
    public void GetDatabase_ConnectionString_Success(string connectionString, string database, string user, int nodeCount)
    {
        var db = Db.Get(connectionString);

        Assert.Multiple(() =>
        {
            Assert.Equal(database, db.Name);
        },
        () =>
        {
            Assert.Equal(user, db.UserName);
        },
        () =>
        {
            Assert.Equal(nodeCount, db.Nodes.Count);
        });
    }

    public static IEnumerable<object?[]> GetDatabase_EncodedConnectionString_TestData()
    {
        yield return new object?[] { $"hosts={Uri.EscapeDataString("192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5")}&database={Uri.EscapeDataString("example 100")}&user={Uri.EscapeDataString("postgre 101")}&password={Uri.EscapeDataString("Js5Y137-RzKKHlCzf")}", "example 100", "postgre 101", 3 };
    }

    [Theory, MemberData(nameof(GetDatabase_EncodedConnectionString_TestData))]
    public void GetDatabase_EncodedConnectionString_Success(string connectionString, string database, string user, int nodeCount)
    {
        var db = Db.Get(connectionString);

        Assert.Multiple(() =>
        {
            Assert.Equal(database, db.Name);
        },
        () =>
        {
            Assert.Equal(user, db.UserName);
        },
        () =>
        {
            Assert.Equal(nodeCount, db.Nodes.Count);
        });
    }

    [Theory]
    [InlineData("hosts=&database=example12&user=postgre&password=Js5Y137RzKKHlCzf")]
    [InlineData("hosts= \r\n\t&database=example13&user=postgre&password=Js5Y137RzKKHlCzf")]
    [InlineData("hosts=,,,&database=example14&user=postgre&password=Js5Y137RzKKHlCzf")]
    [InlineData("hosts=,\t, ,\r\n&database=example15&user=postgre&password=Js5Y137RzKKHlCzf")]
    [InlineData("hosts=192.168.1.101&database=&user=postgre&password=Js5Y137RzKKHlCzf")]
    [InlineData("hosts=192.168.1.101&database=example16&user=&password=Js5Y137RzKKHlCzf")]
    [InlineData("hosts=192.168.1.101&database=VeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryLongDatabaseName2&user=postgre&password=Js5Y137RzKKHlCzf")]
    [InlineData("hosts=192.168.1.101&database=example17&user=VeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryLongUserName&password=Js5Y137RzKKHlCzf")]
    [InlineData("hosts=192.168.1.101&database=example18&user=postgre&password=VeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryVeryLongPassword")]
    public void GetDatabase_ConnectionString_ThrowsArgumentException(string connectionString)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Db.Get(connectionString);
        });
    }

    #endregion

    #region Find

    [Theory]
    [InlineData(Hosts, DatabaseName, UserName, Password)]
    [InlineData("192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5", "example1", "postgre", "Js5Y137RzKKHlCzf")]
    [InlineData("192.168.1.101:5432|0.2,/var/run/postgresql:5432|0.5", "example2", "postgre", "Js5Y137RzKKHlCzf")]
    [InlineData("/var/run/postgresql:5432|0.5", "example3", "postgre", "Js5Y137RzKKHlCzf")]
    public void Find_Success(string hosts, string database, string user, string password)
    {
        var db1 = Db.Get(hosts, database, user, password);
        var db2 = Db.Find(db1.Id);

        Assert.Same(db1, db2);
    }

    #endregion

    #region ApplicationName

    [Fact]
    public void GetApplicationName_Success()
    {
        Assert.Equal(ApplicationName, Db.ApplicationName);
    }

    [Theory]
    [InlineData("SingWing", "SingWing")]
    [InlineData("SingWing.PgSql", "SingWing.PgSql")]
    [InlineData("SingWing仙云", "SingWing??")]
    [InlineData(" \r\n\t\b", "?????")]
    [InlineData("There are two paths you can take, Enola. Yours or the path others choose for you.", "There?are?two?paths?you?can?take,?Enola.?Yours?or?the?path?othe")]
    public void SetApplicationName_Success(string name, string expected)
    {
        var old = Db.ApplicationName;

        Db.ApplicationName = name;

        Assert.Equal(expected, Db.ApplicationName);

        Db.ApplicationName = old;
    }

    #endregion

    #region Logging

    [Fact]
    public void GetLogger_Success()
    {
        Assert.Same(FileLogger.Shared, Db.Logger);
    }

    #endregion
}
