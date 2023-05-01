namespace SingWing.PgSql.Tests;

public class TransactionTests : TestBase
{
    #region Database

    [Fact]
    public async void GetDatabase_Success()
    {
        var db = GetDatabase();

        await using var transaction = await db.BeginAsync();

        Assert.Same(db, transaction.Database);
    }

    #endregion

    #region Node

    [Fact]
    public async void GetNode_Success()
    {
        var db = GetDatabase();
        var node = db.Nodes[0];

        await using var transaction = await node.BeginAsync();

        Assert.Same(node, transaction.Node);
    }

    #endregion

    #region Connection

    [Fact]
    public async void GetConnection_Success()
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();
        await using var transaction = await connection.BeginAsync();

        Assert.Same(connection, transaction.Connection);
    }

    #endregion

    #region IsolationLevel

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void GetIsolationLevel_ForDatabase_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();

        await using var transaction = await db.BeginAsync(isolationLevel);

        // Assert
        Assert.Equal(isolationLevel, transaction.IsolationLevel);
    }

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void GetIsolationLevel_ForNode_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();
        var node = db.Nodes[0];

        await using var transaction = await node.BeginAsync(isolationLevel);

        // Assert
        Assert.Equal(isolationLevel, transaction.IsolationLevel);
    }

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void GetIsolationLevel_ForConnection_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        await using var transaction = await connection.BeginAsync(isolationLevel);

        // Assert
        Assert.Equal(isolationLevel, transaction.IsolationLevel);
    }

    #endregion

    #region Commit

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Commit_ForDatabase_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();

        await using var transaction = await db.BeginAsync(isolationLevel);

        // Act
        await transaction.CommitAsync();
    }

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Commit_ForNode_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();
        var node = db.Nodes[0];

        await using var transaction = await node.BeginAsync(isolationLevel);

        // Act
        await transaction.CommitAsync();
    }

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Commit_ForConnection_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        await using var transaction = await connection.BeginAsync(isolationLevel);

        await transaction.CommitAsync();
    }

    #endregion

    #region Rollback

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Rollback_ForDatabase_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();

        await using var transaction = await db.BeginAsync(isolationLevel);

        // Act
        await transaction.RollbackAsync();
    }

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Rollback_ForNode_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();
        var node = db.Nodes[0];

        await using var transaction = await node.BeginAsync(isolationLevel);

        // Act
        await transaction.RollbackAsync();
    }

    [Theory]
    [InlineData(IsolationLevel.ReadUncommitted)]
    [InlineData(IsolationLevel.ReadCommitted)]
    [InlineData(IsolationLevel.RepeatableRead)]
    [InlineData(IsolationLevel.Serializable)]
    public async void Rollback_ForConnection_Success(IsolationLevel isolationLevel)
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();

        await using var transaction = await connection.BeginAsync(isolationLevel);

        // Act
        await transaction.RollbackAsync();
    }

    #endregion

    #region Savepoint

    [Fact]
    public async void Save_EmptyName_ThrowsArgumentException()
    {
        var db = GetDatabase();
        await using var transaction = await db.BeginAsync();

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await transaction.SaveAsync("");
        });
    }

    [Fact]
    public async void Release_EmptyName_ThrowsArgumentException()
    {
        var db = GetDatabase();
        await using var transaction = await db.BeginAsync();
        await transaction.SaveAsync("savepoint_1");

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await transaction.ReleaseAsync("");
        });
    }

    [Fact]
    public async void SaveAndRollback_Success()
    {
        var name = "savepoint_2";
        var db = GetDatabase();
        await using var transaction = await db.BeginAsync();
        await transaction.SaveAsync(name);
        await transaction.RollbackAsync(name);
    }

    [Fact]
    public async void SaveAndRelease_Success()
    {
        var name = "savepoint_3";
        var db = GetDatabase();
        await using var transaction = await db.BeginAsync();
        await transaction.SaveAsync(name);
        await transaction.ReleaseAsync(name);
    }

    #endregion
}
