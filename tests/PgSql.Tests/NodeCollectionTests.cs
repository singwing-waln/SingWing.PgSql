namespace SingWing.PgSql.Tests;

public class NodeCollectionTests : TestBase
{
    #region Indexer

    public static IEnumerable<object?[]> GetItem_Success_TestData()
    {
        yield return new object?[] {
            "hosts=localhost&database=example51&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host()
            }
        };

        yield return new object?[] {
            "hosts=192.168.1.101&database=example52&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("192.168.1.101")
            }
        };

        yield return new object?[] {
            "hosts=/var/run/postgresql&database=example53&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("/var/run/postgresql")
            }
        };

        yield return new object?[] {
            "hosts=/var/run/postgresql:5433&database=example54&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("/var/run/postgresql", 5433)
            }
        };

        yield return new object?[] {
            "hosts=localhost,192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5,/var/run/postgresql&database=example55&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host(),
                new Host("192.168.1.101"),
                new Host("192.168.1.101", 5433),
                new Host("192.168.1.102", 5432),
                new Host("/var/run/postgresql")
            }
        };

        yield return new object?[] {
            "hosts=localhost,192.168.1.101:5432|0.2,192.168.1.101|0.3,192.168.1.102|0.5,/var/run/postgresql&database=example55&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host(),
                new Host("192.168.1.101"),
                new Host("192.168.1.102", 5432),
                new Host("/var/run/postgresql")
            }
        };
    }

    [Theory, MemberData(nameof(GetItem_Success_TestData))]
    public void GetItem_Success(string connectionString, Host[] expectedHosts)
    {
        var nodes = Db.Get(connectionString).Nodes;

        Assert.All(expectedHosts, host =>
        {
            Assert.NotNull(nodes[host]);
        });
    }

    #endregion

    #region Contains

    public static IEnumerable<object?[]> Contains_Success_TestData()
    {
        yield return new object?[] {
            "hosts=localhost&database=example51&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host()
            }
        };

        yield return new object?[] {
            "hosts=192.168.1.101&database=example52&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("192.168.1.101")
            }
        };

        yield return new object?[] {
            "hosts=/var/run/postgresql&database=example53&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("/var/run/postgresql")
            }
        };

        yield return new object?[] {
            "hosts=/var/run/postgresql:5433&database=example54&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("/var/run/postgresql", 5433)
            }
        };

        yield return new object?[] {
            "hosts=localhost,192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5,/var/run/postgresql&database=example55&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host(),
                new Host("192.168.1.101"),
                new Host("192.168.1.101", 5433),
                new Host("192.168.1.102", 5432),
                new Host("/var/run/postgresql")
            }
        };

        yield return new object?[] {
            "hosts=localhost,192.168.1.101:5432|0.2,192.168.1.101|0.3,192.168.1.102|0.5,/var/run/postgresql&database=example55&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host(),
                new Host("192.168.1.101"),
                new Host("192.168.1.102", 5432),
                new Host("/var/run/postgresql")
            }
        };
    }

    [Theory, MemberData(nameof(Contains_Success_TestData))]
    public void Contains_Success(string connectionString, Host[] expectedHosts)
    {
        var nodes = Db.Get(connectionString).Nodes;

        Assert.All(expectedHosts, host =>
        {
            Assert.True(nodes.Contains(host));
        });
    }

    public static IEnumerable<object?[]> NotContains_Success_TestData()
    {
        yield return new object?[] {
            "hosts=localhost&database=example51&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("192.168.2.101", 5433)
            }
        };

        yield return new object?[] {
            "hosts=192.168.1.101&database=example52&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("192.168.2.101", 5433)
            }
        };

        yield return new object?[] {
            "hosts=/var/run/postgresql&database=example53&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("/var/lib/postgresql", 5433)
            }
        };

        yield return new object?[] {
            "hosts=/var/run/postgresql:5433&database=example54&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("/var/lib/postgresql", 5433)
            }
        };

        yield return new object?[] {
            "hosts=localhost,192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5,/var/run/postgresql&database=example55&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("www.pittypat.work"),
                new Host("192.168.0.101"),
                new Host("192.168.0.101", 5433),
                new Host("192.168.0.102", 5432),
                new Host("/var/run/postgresql", 5434)
            }
        };

        yield return new object?[] {
            "hosts=localhost,192.168.1.101:5432|0.2,192.168.1.101|0.3,192.168.1.102|0.5,/var/run/postgresql&database=example55&user=postgre&password=Js5Y137RzKKHlCzf",
            new Host[]
            {
                new Host("local"),
                new Host("192.168.0.101"),
                new Host("192.168.0.102", 5432),
                new Host("/var/lib/postgresql")
            }
        };
    }

    [Theory, MemberData(nameof(NotContains_Success_TestData))]
    public void NotContains_Success(string connectionString, Host[] expectedHosts)
    {
        var nodes = Db.Get(connectionString).Nodes;

        Assert.All(expectedHosts, host =>
        {
            Assert.False(nodes.Contains(host));
        });
    }

    #endregion

    #region Add

    [Fact]
    public void Add_Success()
    {
        var db = Db.Get("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example61&user=postgre&password=Js5Y137RzKKHlCzf");
        var nodes = db.Nodes;
        nodes.Add("/var/run/postgresql:5678");

        Assert.Multiple(() =>
        {
            Assert.Equal(4, nodes.Count);
        },
        () =>
        {
            var host = new Host("/var/run/postgresql", 5678);
            Assert.True(nodes.Contains(host));
        });
    }

    #endregion

    #region Remove

    [Fact]
    public void Remove_Exists_Success()
    {
        var db = Db.Get("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example62&user=postgre&password=Js5Y137RzKKHlCzf");
        var nodes = db.Nodes;
        var yes = nodes.Remove("192.168.1.101");

        Assert.Multiple(() =>
        {
            Assert.Equal(2, nodes.Count);
        },
        () =>
        {
            Assert.True(yes);
        });
    }

    [Fact]
    public void Remove_NotExists_Success()
    {
        var db = Db.Get("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example63&user=postgre&password=Js5Y137RzKKHlCzf");
        var nodes = db.Nodes;
        var yes = nodes.Remove("/var/run/postgresql:5678");

        Assert.Multiple(() =>
        {
            Assert.Equal(3, nodes.Count);
        },
        () =>
        {
            Assert.False(yes);
        });
    }

    #endregion

    #region Clear

    [Fact]
    public void Clear_Success()
    {
        var db = Db.Get("hosts=192.168.1.101:5432|0.2,192.168.1.101:5433|0.3,192.168.1.102|0.5&database=example64&user=postgre&password=Js5Y137RzKKHlCzf");
        var nodes = db.Nodes;
        nodes.Clear();

        Assert.Equal(0, nodes.Count);
    }

    #endregion
}
