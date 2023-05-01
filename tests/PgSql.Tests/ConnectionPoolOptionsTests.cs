namespace SingWing.PgSql.Tests;

public class ConnectionPoolOptionsTests : TestBase
{
    #region ConnectTimeoutSeconds

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(300, 300)]
    [InlineData(900, 900)]
    [InlineData(1000, 900)]
    public void Database_ConnectTimeoutSeconds_Success(int seconds, int expected)
    {
        var db = GetDatabase();
        db.Options.ConnectTimeoutSeconds = seconds;
        Assert.Equal(expected, db.Options.ConnectTimeoutSeconds);
    }

    [Theory]
    [InlineData(-1, 25)]
    [InlineData(0, 25)]
    [InlineData(1, 1)]
    [InlineData(300, 300)]
    [InlineData(900, 900)]
    [InlineData(1000, 900)]
    public void Node_ConnectTimeoutSeconds_Success(int seconds, int expected)
    {
        var db = GetDatabase();
        db.Options.ConnectTimeoutSeconds = 25;

        var node = db.Nodes[0];
        node.Options.ConnectTimeoutSeconds = seconds;
        Assert.Equal(expected, node.Options.ConnectTimeoutSeconds);
    }

    #endregion

    #region ReceiveTimeoutSeconds

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(300, 300)]
    [InlineData(900, 900)]
    [InlineData(1000, 900)]
    public void Database_ReceiveTimeoutSeconds_Success(int seconds, int expected)
    {
        var db = GetDatabase();
        db.Options.ReceiveTimeoutSeconds = seconds;
        Assert.Equal(expected, db.Options.ReceiveTimeoutSeconds);
    }

    [Theory]
    [InlineData(-1, 25)]
    [InlineData(0, 25)]
    [InlineData(1, 1)]
    [InlineData(300, 300)]
    [InlineData(900, 900)]
    [InlineData(1000, 900)]
    public void Node_ReceiveTimeoutSeconds_Success(int seconds, int expected)
    {
        var db = GetDatabase();
        db.Options.ReceiveTimeoutSeconds = 25;

        var node = db.Nodes[0];
        node.Options.ReceiveTimeoutSeconds = seconds;
        Assert.Equal(expected, node.Options.ReceiveTimeoutSeconds);
    }

    #endregion

    #region SendTimeoutSeconds

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(300, 300)]
    [InlineData(900, 900)]
    [InlineData(1000, 900)]
    public void Database_SendTimeoutSeconds_Success(int seconds, int expected)
    {
        var db = GetDatabase();
        db.Options.SendTimeoutSeconds = seconds;
        Assert.Equal(expected, db.Options.SendTimeoutSeconds);
    }

    [Theory]
    [InlineData(-1, 25)]
    [InlineData(0, 25)]
    [InlineData(1, 1)]
    [InlineData(300, 300)]
    [InlineData(900, 900)]
    [InlineData(1000, 900)]
    public void Node_SendTimeoutSeconds_Success(int seconds, int expected)
    {
        var db = GetDatabase();
        db.Options.SendTimeoutSeconds = 25;

        var node = db.Nodes[0];
        node.Options.SendTimeoutSeconds = seconds;
        Assert.Equal(expected, node.Options.SendTimeoutSeconds);
    }

    #endregion

    #region WaitTimeoutSeconds

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(300, 300)]
    [InlineData(900, 900)]
    [InlineData(1000, 900)]
    public void Database_WaitTimeoutSeconds_Success(int seconds, int expected)
    {
        var db = GetDatabase();
        db.Options.WaitTimeoutSeconds = seconds;
        Assert.Equal(expected, db.Options.WaitTimeoutSeconds);
    }

    [Theory]
    [InlineData(-1, 25)]
    [InlineData(0, 25)]
    [InlineData(1, 1)]
    [InlineData(300, 300)]
    [InlineData(900, 900)]
    [InlineData(1000, 900)]
    public void Node_WaitTimeoutSeconds_Success(int seconds, int expected)
    {
        var db = GetDatabase();
        db.Options.WaitTimeoutSeconds = 25;

        var node = db.Nodes[0];
        node.Options.WaitTimeoutSeconds = seconds;
        Assert.Equal(expected, node.Options.WaitTimeoutSeconds);
    }

    #endregion
}
