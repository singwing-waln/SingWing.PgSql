using System.Net;

namespace SingWing.PgSql.Tests;

public class HostTests : TestBase
{
    #region Name

    [Fact]
    public void DefaultName_Success()
    {
        var host = new Host();
        Assert.Equal("localhost", host.Name);
    }

    [Theory]
    [InlineData("", "localhost")]
    [InlineData("localhost", "localhost")]
    [InlineData(" \t\r\n", "localhost")]
    [InlineData(" localhost ", "localhost")]
    [InlineData("192.168.1.101", "192.168.1.101")]
    [InlineData("\t192.168.1.101\r\n", "192.168.1.101")]
    [InlineData("/var/run/postgresql", "/var/run/postgresql")]
    public void GetName_Success(string name, string expected)
    {
        var host = new Host(name);
        Assert.Equal(expected, host.Name);
    }

    #endregion

    #region Port

    [Fact]
    public void DefaultPort_Success()
    {
        var host = new Host();
        Assert.Equal(Db.DefaultPort, host.Port);
    }

    [Theory]
    [InlineData(Db.DefaultPort, Db.DefaultPort)]
    [InlineData(IPEndPoint.MinPort - 1, Db.DefaultPort)]
    [InlineData(IPEndPoint.MaxPort + 1, Db.DefaultPort)]
    [InlineData(IPEndPoint.MinPort, Db.DefaultPort)]
    [InlineData(IPEndPoint.MaxPort, IPEndPoint.MaxPort)]
    [InlineData(5433, 5433)]
    public void GetPort_Success(int port, int expected)
    {
        var host = new Host("localhost", port);
        Assert.Equal(expected, host.Port);
    }

    #endregion

    #region ToString

    [Theory]
    [InlineData("", 0, "localhost:5432")]
    [InlineData("localhost", 1234, "localhost:1234")]
    [InlineData(" \t\r\n", Db.DefaultPort, "localhost:5432")]
    [InlineData("192.168.1.101", 5433, "192.168.1.101:5433")]
    [InlineData("\t192.168.1.101\r\n", IPEndPoint.MaxPort + 1, "192.168.1.101:5432")]
    [InlineData("/var/run/postgresql", 5678, "/var/run/postgresql:5678")]
    public void ToString_Success(string name, int port, string expected)
    {
        var host = new Host(name, port);
        Assert.Equal(expected, host.ToString());
    }

    #endregion

    #region Equals and NotEquals

    public static IEnumerable<object?[]> Equals_Success_TestData()
    {
        yield return new object?[] { new Host(), new Host() };
        yield return new object?[] { new Host("localhost"), new Host() };
        yield return new object?[] { new Host("localhost", 5432), new Host() };
    }

    [Theory, MemberData(nameof(Equals_Success_TestData))]
    public void Equals_Success(Host x, Host y)
    {
        Assert.True(x == y);
    }

    public static IEnumerable<object?[]> NotEquals_Success_TestData()
    {
        yield return new object?[] { new Host("127.0.0.1"), new Host() };
        yield return new object?[] { new Host("192.168.0.101"), new Host() };
        yield return new object?[] { new Host("localhost", 5433), new Host() };
    }

    [Theory, MemberData(nameof(NotEquals_Success_TestData))]
    public void NotEquals_Success(Host x, Host y)
    {
        Assert.True(x != y);
    }

    #endregion

    #region FromString

    public static IEnumerable<object?[]> FromString_Success_TestData()
    {
        yield return new object?[] { "", new Host() };
        yield return new object?[] { "localhost", new Host() };
        yield return new object?[] { "localhost:5432", new Host() };
        yield return new object?[] { "192.168.0.101:5433", new Host("192.168.0.101", 5433) };
        yield return new object?[] { "/var/run/postgresql", new Host("/var/run/postgresql") };
        yield return new object?[] { "/var/run/postgresql", new Host("/var/run/postgresql", 5432) };
        yield return new object?[] { "/var/run/postgresql:5433", new Host("/var/run/postgresql", 5433) };
    }

    [Theory, MemberData(nameof(FromString_Success_TestData))]
    public void FromString_Success(string s, Host expected)
    {
        Assert.Equal(expected, s);
    }

    #endregion
}
