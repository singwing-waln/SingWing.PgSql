namespace SingWing.PgSql.Tests;

public class CursorTests : TestBase
{
    #region Database

    [Theory]
    [InlineData(1, "Exile", "Kathryn")]
    [InlineData(3, "Ships of Discovery", "William")]
    public async void Database_QueryCursors_SingleRow_Success(long bookId, string expectedBookTitle, string expectedAuthorFirstName)
    {
        var db = GetDatabase();
        await using var transaction = await db.BeginAsync();

        var bookTitle = "";
        var authorFirstName = "";

        try
        {
            // Call the function, pass parameter(s).
            await transaction.QueryCursorsAsync(
                "SELECT public.book_details($1)",
                bookId);

            // Suppose public.book_details returns two cursors, book$ and author$, 
            // which provide information about the book and the author, respectively.

            // Retrieve the book information.
            var rows = await transaction.QueryAsync("FETCH FIRST IN book$");
            await foreach (var row in rows)
            {
                await foreach (var col in row)
                {
                    if (col.NameIs("Title"))
                    {
                        bookTitle = await col.ToStringAsync();
                    }
                }
            }

            // Retrieve the author information.
            rows = await transaction.QueryAsync("FETCH FIRST IN author$");
            await foreach (var row in rows)
            {
                await foreach (var col in row)
                {
                    if (col.NameIs("FirstName"))
                    {
                        authorFirstName = await col.ToStringAsync();
                    }
                }
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        Assert.Multiple(() =>
        {
            Assert.Equal(expectedBookTitle, bookTitle);
        },
        () =>
        {
            Assert.Equal(expectedAuthorFirstName, authorFirstName);
        });
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 2)]
    [InlineData(3, 1)]
    [InlineData(4, 0)]
    public async void Database_QueryCursors_MultipleRows_Success(long minBookId, long expectedRowCount)
    {
        var db = GetDatabase();
        await using var transaction = await db.BeginAsync();

        var bookCount = 0L;

        try
        {
            await transaction.QueryCursorsAsync(
                "SELECT public.books($1)",
                minBookId);

            var rows = await transaction.QueryAsync("FETCH ALL IN books$");
            await foreach (var row in rows)
            {
                bookCount++;
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        Assert.Equal(expectedRowCount, bookCount);
    }

    [Fact]
    public async void Database_QueryCursorNames_Success()
    {
        var db = GetDatabase();
        var transaction = await db.BeginAsync();
        IList<string> names;

        try
        {
            names = await transaction.QueryCursorNamesAsync(
                "SELECT public.book_details($1)",
                1L);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }

        Assert.Multiple(() =>
        {
            Assert.Equal("book$", names[0]);
        },
        () =>
        {
            Assert.Equal("author$", names[1]);
        });
    }

    #endregion

    #region Node

    [Theory]
    [InlineData(1, "Exile", "Kathryn")]
    [InlineData(3, "Ships of Discovery", "William")]
    public async void Node_QueryCursors_SingleRow_Success(long bookId, string expectedBookTitle, string expectedAuthorFirstName)
    {
        var db = GetDatabase();
        await using var transaction = await db.Nodes[0].BeginAsync();

        var bookTitle = "";
        var authorFirstName = "";

        try
        {
            // Call the function, pass parameter(s).
            await transaction.QueryCursorsAsync(
                "SELECT public.book_details($1)",
                bookId);

            // Suppose public.book_details returns two cursors, book$ and author$, 
            // which provide information about the book and the author, respectively.

            // Retrieve the book information.
            var rows = await transaction.QueryAsync("FETCH FIRST IN book$");
            await foreach (var row in rows)
            {
                await foreach (var col in row)
                {
                    if (col.NameIs("Title"))
                    {
                        bookTitle = await col.ToStringAsync();
                    }
                }
            }

            // Retrieve the author information.
            rows = await transaction.QueryAsync("FETCH FIRST IN author$");
            await foreach (var row in rows)
            {
                await foreach (var col in row)
                {
                    if (col.NameIs("FirstName"))
                    {
                        authorFirstName = await col.ToStringAsync();
                    }
                }
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        Assert.Multiple(() =>
        {
            Assert.Equal(expectedBookTitle, bookTitle);
        },
        () =>
        {
            Assert.Equal(expectedAuthorFirstName, authorFirstName);
        });
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 2)]
    [InlineData(3, 1)]
    [InlineData(4, 0)]
    public async void Node_QueryCursors_MultipleRows_Success(long minBookId, long expectedRowCount)
    {
        var db = GetDatabase();
        await using var transaction = await db.Nodes[0].BeginAsync();

        var bookCount = 0L;

        try
        {
            await transaction.QueryCursorsAsync(
                "SELECT public.books($1)",
                minBookId);

            var rows = await transaction.QueryAsync("FETCH ALL IN books$");
            await foreach (var row in rows)
            {
                bookCount++;
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        Assert.Equal(expectedRowCount, bookCount);
    }

    [Fact]
    public async void Node_QueryCursorNames_Success()
    {
        var db = GetDatabase();
        var transaction = await db.Nodes[0].BeginAsync();
        IList<string> names;

        try
        {
            names = await transaction.QueryCursorNamesAsync(
                "SELECT public.book_details($1)",
                1L);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }

        Assert.Multiple(() =>
        {
            Assert.Equal("book$", names[0]);
        },
        () =>
        {
            Assert.Equal("author$", names[1]);
        });
    }

    #endregion

    #region Connection

    [Theory]
    [InlineData(1, "Exile", "Kathryn")]
    [InlineData(3, "Ships of Discovery", "William")]
    public async void Connection_QueryCursors_SingleRow_Success(long bookId, string expectedBookTitle, string expectedAuthorFirstName)
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();
        await using var transaction = await connection.BeginAsync();

        var bookTitle = "";
        var authorFirstName = "";

        try
        {
            // Call the function, pass parameter(s).
            await transaction.QueryCursorsAsync(
                "SELECT public.book_details($1)",
                bookId);

            // Suppose public.book_details returns two cursors, book$ and author$, 
            // which provide information about the book and the author, respectively.

            // Retrieve the book information.
            var rows = await transaction.QueryAsync("FETCH FIRST IN book$");
            await foreach (var row in rows)
            {
                await foreach (var col in row)
                {
                    if (col.NameIs("Title"))
                    {
                        bookTitle = await col.ToStringAsync();
                    }
                }
            }

            // Retrieve the author information.
            rows = await transaction.QueryAsync("FETCH FIRST IN author$");
            await foreach (var row in rows)
            {
                await foreach (var col in row)
                {
                    if (col.NameIs("FirstName"))
                    {
                        authorFirstName = await col.ToStringAsync();
                    }
                }
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        Assert.Multiple(() =>
        {
            Assert.Equal(expectedBookTitle, bookTitle);
        },
        () =>
        {
            Assert.Equal(expectedAuthorFirstName, authorFirstName);
        });
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 2)]
    [InlineData(3, 1)]
    [InlineData(4, 0)]
    public async void Connection_QueryCursors_MultipleRows_Success(long minBookId, long expectedRowCount)
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();
        await using var transaction = await connection.BeginAsync();

        var bookCount = 0L;

        try
        {
            await transaction.QueryCursorsAsync(
                "SELECT public.books($1)",
                minBookId);

            var rows = await transaction.QueryAsync("FETCH ALL IN books$");
            await foreach (var row in rows)
            {
                bookCount++;
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        Assert.Equal(expectedRowCount, bookCount);
    }

    [Fact]
    public async void Connection_QueryCursorNames_Success()
    {
        var db = GetDatabase();
        await using var connection = await db.AcquireAsync();
        var transaction = await connection.BeginAsync();
        IList<string> names;

        try
        {
            names = await transaction.QueryCursorNamesAsync(
                "SELECT public.book_details($1)",
                1L);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }

        Assert.Multiple(() =>
        {
            Assert.Equal("book$", names[0]);
        },
        () =>
        {
            Assert.Equal("author$", names[1]);
        });
    }

    #endregion
}
