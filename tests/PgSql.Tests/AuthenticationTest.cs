namespace SingWing.PgSql.Tests;

public class AuthenticationTest : TestBase
{
    [Theory]
    [InlineData("localhost", true)]
    [InlineData("localhost:5432", true)]
    [InlineData("127.0.0.1", true)]
    [InlineData("127.0.0.1:5432", true)]
    [InlineData("192.168.0.108", true)]
    [InlineData("192.168.0.108:5432", true)]
    public async void Authenticate_Success(string hosts, bool allowRemote)
    {
        var connectionString = $"hosts={hosts}&database={DatabaseName}&user={UserName}&password={Password}";
        var db = Db.Get(connectionString, id: Guid.NewGuid());

        if (allowRemote)
        {
            await using var connection = await db.AcquireAsync();
        }
        else
        {
            await Assert.ThrowsAsync<OpeningFailedException>(async () =>
            {
                await using var connection = await db.AcquireAsync();
            });
        }
    }

    [Theory]
    [InlineData($"hosts=invalid_hosts&database={DatabaseName}&user={UserName}&password={Password}")]
    [InlineData($"hosts={Hosts}&database=invalid_database&user={UserName}&password={Password}")]
    [InlineData($"hosts={Hosts}&database={DatabaseName}&user=invalid_user&password={Password}")]
    public async void Authenticate_ThrowsAuthenticationException(string connectionString)
    {
        var db = Db.Get(connectionString, id: Guid.NewGuid());

        await Assert.ThrowsAsync<OpeningFailedException>(async () =>
        {
            await using var connection = await db.AcquireAsync();
        });
    }

    [Theory]
    [InlineData("SingWing")]
    [InlineData("SINGWING")]
    [InlineData("singwing-*632&4")]
    public async void InvalidPassword_ThrowsAuthenticationException(string invalidPassword)
    {
        var connectionString = $"hosts={Hosts}&database={DatabaseName}&user={UserName}&password={invalidPassword}";
        var db = Db.Get(connectionString, id: Guid.NewGuid());

        await Assert.ThrowsAsync<OpeningFailedException>(async () =>
        {
            await using var connection = await db.AcquireAsync();
        });
    }
}
