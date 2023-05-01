namespace SingWing.PgSql.Tests;

public class TransactionalTests : TestBase
{
    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Database_Begin_IsolationLevel_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();

        await using var transaction = await db.BeginAsync(isolationLevel);

        Assert.Same(db, transaction.Database);
        await transaction.DisposeAsync();
    }

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Node_Begin_IsolationLevel_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();
        var node = db.Nodes[0];

        await using var transaction = await node.BeginAsync(isolationLevel);

        Assert.Same(node, transaction.Node);
    }

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Connection_Begin_IsolationLevel_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        await using var transaction = await connection.BeginAsync(isolationLevel);

        Assert.Same(connection, transaction.Connection);
    }

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Connection_Begin_IsolationLevel_Disposed(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();
        await connection.DisposeAsync();

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await using var transaction = await connection.BeginAsync(isolationLevel);
        });
    }
}
