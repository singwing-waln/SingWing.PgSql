namespace SingWing.PgSql.Tests;

public class ConnectionTests : TestBase
{
    #region Database

    [Fact]
    public async void GetDatabase_Success()
    {
        var db = GetDatabase();

        await using var connection = await db.AcquireAsync();

        Assert.Same(db, connection.Database);
    }

    #endregion

    #region Node

    [Fact]
    public async void GetNode_Success()
    {
        var db = GetDatabase();
        var node = db.Nodes[0];

        await using var connection = await node.AcquireAsync();

        Assert.Same(node, connection.Node);
    }

    #endregion
}
