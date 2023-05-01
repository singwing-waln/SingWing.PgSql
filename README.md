# SingWing.PgSql - a .NET connector for PostgreSQL

SingWing.PgSql is a lightweight, fast and easy-to-use .NET connector for PostgreSQL. It is a good option if you only require the essential PostgreSQL functionalities.

## Get Started

```csharp
using SingWing.PgSql;

// 1. Prepare the connection string in query string format.
var connectionString = "hosts=localhost|0.5,192.168.1.101:5433|0.7&database=dbname&user=dbuser&password=dbpassword";

// 2. Get a database.
var db = Db.Get(connectionString);

// 3. Execute commands.
await db.ExecuteAsync(
    "INSERT INTO public.books(id, title) VALUES($1, $2)",
    1,
    "SingWing Cookbook");

// This command can be run on a different connection than the one above.
var rows = await db.QueryAsync(
    "SELECT id, title FROM public.books WHERE id = $1",
    1);

// 4. Fetch rows and columns.
await foreach (var row in rows)
{
    await foreach (var col in row)
    {
        switch (col.Name)
        {
            case "id":
                Console.WriteLine($"id: {await col.ToInt32Async()}");
                break;
            case "title":
                Console.WriteLine($"title: {await col.ToStringAsync()}");
                break;
            default:
                break;
        }
    }
}
```

## Features

* Uses [IAsyncEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1?view=net-7.0) instead of [DataReader](https://learn.microsoft.com/en-us/dotnet/api/system.data.common.dbdatareader?view=net-7.0&viewFallbackFrom=dotnet-plat-ext-7.0) to retrieve rows and columns.
* Supports for sending and receiving data using [Memory-related types](https://learn.microsoft.com/en-us/dotnet/standard/memory-and-spans/).
* Tries to use as little memory as possible and does not cache the data in result sets of queries.
* Supported [PostgreSQL data types](https://www.postgresql.org/docs/current/datatype.html): bigint, bool, bytea, date, float4/real, float8/double precision, int/integer, json/jsonb, numeric/decimal, smallint, time/timetz, timestamptz/timestamp, uuid, varchar/text/bpchar, and corresponding array types. More data types can be supported by implementing the `IDataTypeProtocol` interface.
* Load balancing, pooling and transactions are supported.
* All operations are asynchronous.
* It is interface-oriented (maybe), from the perspective of callers.

## Notes

* SingWing.PgSql is not an [ADO.NET](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/) data provider.
* Only [SASL SCRAM-SHA-256](https://www.postgresql.org/docs/current/sasl-authentication.html) authentication is supported.
* At the moment, SSL is not supported.

## Prerequisites

* [.NET 7.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-7)
* [C# 11](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history)
* [PostgreSQL Frontend/Backend Protocol 3](https://www.postgresql.org/docs/15/protocol.html)

## Load Balancing and Pooling

SingWing.PgSql uses `IDatabase`, `INode`, `IServer` and `IConnectionPool` for load balancing and pooling.

A database may be distributed, which means that its backend service may run in one or more distributed nodes.
Load balancing is supported by providing multiple node hosts in the connection string or the `hosts` parameter.

So, a database contains one or more nodes, and one node manages a pool of connections internally. A connection pool automatically queries the PostgreSQL server for [*max_connections*](https://www.postgresql.org/docs/15/runtime-config-connection.html#RUNTIME-CONFIG-CONNECTION-SETTINGS) to determine the maximum number of connections in the pool. Callers don't need to specify pool-size parameters in connection strings.

PostgreSQL's architecture is process-based instead of thread-based, the total number of connections available per client may be small. SingWing.PgSQL coordinates and manages all connections to a single server internally. The maximum connection number for a server is shared between multiple databases on that server.

A server may have multiple clients connected to it simultaneously, and the number of connections available to each client may be less than `max_connections - superuser_reserved_connections`.
We can use the format "`host:port|proportion`" in the `hosts` parameter to provide a `proportion` that represents the proportion of the maximum number of connections for the current client. In this case, the maximum number of connections for the server is set to `(max_connections - superuser_reserved_connections) * proportion`. `proportion` is a value greater than 0 and less than or equal to 1, and the default value is 1.

SingWing.PgSql allows clients to dynamically adjust the value of the `proportion` via `IServer`. `IServer` is also responsible for maintaining a heartbeat timer that periodically sends `Sync` messages to the backend to keep connections alive.

In addition, clients can set timeout options for connecting, receiving, sending, and waiting at any time through `IConnectionPoolOptions` of an `IConnectionPool`.

## Connection Strings

A connection string is a url-encoded query string "*name1=value1&name2=value2&name3=value3...*". The names and values are encoded in name-value tuples separated by '*&*', with a '=' between the name and the value. Special characters in both names and values can be encoded by [Uri.EscapeDataString](https://learn.microsoft.com/en-us/dotnet/api/system.uri.escapedatastring?view=net-7.0).

The following table describes the supported parameters (Names are case-insensitive):

| Name        | Required | Description |
| :---------: | -------- | ----------- |
| hosts       | Required | A string of one or more PostreSQL hosts in the format: "*host1:port1\|proportion1,host2:port2\|proportion2,...*". Each host contains the host name (or IP address) and port number separated by ':'. The host name can be a Unix Domain Socket path, such as "*/var/run/postgresql*". If no port number is provided, 5432 will be used. `proportion` is also optional and the default value is 1. |
| database    | Required | The name of the database to connect to. The maximum length is 63. |
| user        | Required | The user name to be used when connecting to PostgreSQL. The maximum length is 63. |
| password    | Optional | The password for the PostgreSQL user. The maximum length is 63. If no password is provided, the empty password will be used. |

If we need to pass the name of the current client application to PostgreSQL, we can set the `Db.ApplicationName` property. If the length of the [application name](https://www.postgresql.org/docs/current/runtime-config-logging.html#GUC-APPLICATION-NAME) exceeds 63, it is truncated. Only ASCII printable characters may be used in the application name, other characters will be replaced with '?'.

Instead of using a connection string, we can also get an `IDatabase` instance as follows:
```csharp
var hosts = "...";
var database = "...";
var user = "...";
var password = "...";

var db = Db.Get(hosts, database, user, password);
```

## Executors and Transactions

In SingWing.PgSql, `IExecutor` is used to execute commands. `IDatabase`, `INode`, `IConnection` and `ITransaction` are all executors.

### IDatabase
  
Use this interface if the caller does not need to care about the order of commands or on which node or connection they will be executed.
  
```csharp
// Db.Get() does not immediately open connections to the database. 
// Connections will be established on demand and managed by the underlying connection pools.
var db = Db.Get(connectionString);
  
await db.QueryAsync("...");
await db.ExecuteAsync("...");
await db.PerformAsync("...");
```

### INode

Use this interface if the caller does not need to care about the order of commands or on which connection they will be executed.
  
```csharp
var node = db.Nodes[0];
// or
var node = db.Nodes["localhost:5433"];
  
await node.QueryAsync("...");
await node.ExecuteAsync("...");
await node.PerformAsync("...");
```

`IDatabase.Nodes` is an `INodeCollection` that can be used to dynamically add or remove nodes.

### IConnection

Use this interface if commands need to be executed sequentially on the same connection and do not need to start transactions.
  
```csharp
// The connection must be returned to the connection pool 
// via the IAsyncDisposable.DisposeAsync().
await using var connection = await db.AcquireAsync();
// or
await using var connection = await node.AcquireAsync();
  
await connection.QueryAsync("...");
await connection.ExecuteAsync("...");
await connection.PerformAsync("...");
```

### ITransaction

Use this interface if commands needs to be executed in a transaction.
  
```csharp
// The underlying connection for the transaction must be returned to the connection pool 
// via the IAsyncDisposable.DisposeAsync().
await using var transaction = await db.BeginAsync();
// or
await using var transaction = await node.BeginAsync();
// or
await using var transaction = await connection.BeginAsync();
  
try
{
    await transaction.QueryAsync("...");
    await transaction.ExecuteAsync("...");
    await transaction.PerformAsync("...");

    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
    throw;
}
```

We can also use transactions like this:
  
```csharp
var transactional = db or node or connection;
  
await transactional.TransactAsync(async transaction =>
{
    await transaction.QueryAsync("...");
    await transaction.ExecuteAsync("...");
    await transaction.PerformAsync("...");
});
```

### Databases and Nodes

`IDatabase` and `INode` executes commands by dispatching them to connections in the underlying pools. If there is more than one connection, it is possible to execute multiple commands or start multiple transactions simultaneously.

### Transactions and Savepoints

Multiple transactions can be started in parallel in one `IDatabase` or `INode`. It is important to note that PostgreSQL does not support nested or parallel transactions.
Once a transaction is started, it monopolizes a connection and must be ended by calling `DisposeAsync` to return the underlying connection to its pool.

```csharp
// Begin a transaction with the specified IsolationLevel.
// If a transaction is not explicitly committed or rolled back, 
// await using will try to roll it back.
await using var firstTransaction = await db.BeginAsync(IsolationLevel.ReadCommitted);

...

// Before firstTransaction ends, start another transaction.
await using var secondTransaction = await db.BeginAsync(IsolationLevel.RepeatableRead);

...

// Commit the first transaction.
await firstTransaction.CommitAsync();

// Roll back the second transaction.
await secondTransaction.RollbackAsync();

```

It's possible to use savepoints in a transaction.

```csharp
var savepointName = "...";

// Define a savepoint.
await transaction.SaveAsync(savepointName);

...

// Destroy a savepoint.
await transaction.ReleaseAsync(savepointName);

// Or roll back to a savepoint.
await transaction.RollbackAsync(savepointName);
```

## Data Types

PostgreSQL provides a very large number of [data types](https://www.postgresql.org/docs/current/datatype.html). At the moment, SingWing.PgSql only supports the following data types:

| #     | PostgreSQL Type             | .NET Type                                                   |
| :---: | --------------------------- | ----------------------------------------------------------- |
| 1     | bigint                      | long?                                                       |
| 2     | bigint[]                    | long?[]?/...                                                |
| 3     | bool                        | bool?                                                       |
| 4     | bool[]                      | bool?[]?/...                                                |
| 5     | bytea                       | byte[]?/ReadOnlyMemory\<byte\>?/Memory\<byte\>?/...         |
| 6     | bytea[]                     | byte[]?[]?/...                                              |
| 7     | date                        | DateOnly?                                                   |
| 8     | date[]                      | DateOnly?[]?                                                |
| 9     | float4/real                 | float?                                                      |
| 10    | float4[]/real[]             | float?[]?                                                   |
| 11    | float8/double precision     | double?                                                     |
| 12    | float8[]/double precision[] | double?[]?                                                  |
| 13    | int/integer                 | int?                                                        |
| 14    | int[]/integer[]             | int?[]?                                                     |
| 15    | jsonb/json                  | byte?[]/string?/ReadOnlyMemory\<byte\>?/Memory\<byte\>?/... |
| 16    | numeric/decimal             | decimal?                                                    |
| 17    | numeric[]/decimal[]         | decimal?[]?                                                 |
| 18    | smallint                    | short?                                                      |
| 19    | smallint[]                  | short?[]?                                                   |
| 20    | time/timetz                 | TimeOnly?                                                   |
| 21    | time[]/timetz[]             | TimeOnly?[]?                                                |
| 22    | timestamptz/timestamp       | DateTime?/DateTimeOffset?                                   |
| 23    | timestamptz[]/timestamp[]   | DateTime?[]?/DateTimeOffset?[]?                             |
| 24    | uuid                        | Guid?                                                       |
| 25    | uuid[]                      | Guid?[]?                                                    |
| 26    | varchar/text/char           | string?/ReadOnlyMemory\<char\>?/Memory\<char\>?/char[]?/... |
| 27    | varchar[]/text[]/char[]     | string?[]?/...                                              |
| 28    | refcursor                   | The cursor name is string.                                  |

`numeric`: NaN, Infinity and -Infinity are not supported, and returns null. If the database value is greater than `decimal.MaxValue`, returns `decimal.MaxValue`. If the value is less than `decimal.MinValue`, returns `decimal.MinValue`.

`timetz`: For timetz (time with time zone), the time zone part is ignored.

`timestamp`: DateTimes sent to PosgreSQL always use UTC value, and read values are converted to local datetime.

## Parameters

In most cases, we don't need to create `Parameter` instances ourselves. SingWing.PgSql provides implicit conversions for most commonly used data types. Some collection types and JSON values may need to explicitly call `ToArrayParameter` and `ToJsonParameter`.

```csharp
// Values are implicitly converted to Parameter instances.
var rows = await db.QueryAsync(
    "SELECT id, title FROM public.books WHERE id = $1",
    1);

await transaction.ExecuteAsync(
    "INSERT INTO public.books(id, title, tags) VALUES($1, $2, $3)",
    1,
    "SingWing Cookbook",
    new string[] { "Secure", "Private Cloud" });

// C# does not support interfaces as the source or destination for implicit conversions, 
// so explicit convertions are required.
IEnumerable<string> tags = ...;
await db.ExecuteAsync(
    "UPDATE public.books SET tags = $1 WHERE id = $2",
    tags.ToArrayParameter(),
    1);

// We can send binary data and strings to PostgreSQL as JSONs, 
// and in order to distinguish them from bytea and varchar/text, 
// we need to explicitly convert them to JSON parameters.
var jsonBytes = new byte[] {...};
var jsonString = "...";

await db.ExecuteAsync(
    "UPDATE public.orders SET books = $1, logistics = $2 WHERE id = $3",
    jsonBytes.ToJsonParameter(),
    jsonString.ToJsonParameter(),
    1);

```

### Object Values

 :bulb: **Experimental**

If we need to pass a value of type `object`, we can use `Parameter.Create(DataType)` to create a new instance of a subclass of `Parameter` and then set its `Value` property to an `object`.

```csharp
// Create a parameter with the specified DataType.
var parameter = Parameter.Create(DataType.Int4);

// idObject is internally converted to a 32-bit integer, 
// and null is used if the conversion fails.
object idObject = ...
parameter.Value = idObject;
```

Using `object` values may introduce additional overhead due to [boxing/unboxing](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing) and type conversions.

### Placeholders

SingWing.PgSql does not provide a `Name` property for the `Parameter` class. Therefore, parameter placeholders in command texts should use numeric syntax `$n`. `$1` represents the first parameter, `$2` represents the second, and so on.

## Retrieving Data

SingWing.PgSql uses `IAsyncEnumerable` instead of `DataReader` to retrieve rows and columns.

```csharp
var rows = await db.QueryAsync("SELECT id, ...");

await foreach (var row in rows)
{
    await foreach (var col in row)
    {
        switch (col.Name)
        {
            case "id":
                break;
            ...
        }
    }
}
```

The `Name` of a column is a `ReadOnlySpan` that can be used in `switch` statements (See [Switching ReadOnlySpan<char> with constant strings](https://github.com/dotnet/csharplang/issues/1881)). 
We can also check the column name using the column's `NameIs` method. This method performs a case-insensitive comparison.

```csharp
await foreach (var row in rows)
{
    await foreach (var col in row)
    {
        // The comparison is case-insensitive.
        if (col.NameIs("id"))
        {
            ...
        }
    }
}
```

### Reading Column Values

There are four categories of methods that we can use to read column values (==XXX== represents a .NET type, such as `Boolean`, `Int32`, `String`, etc.):

1. ToXXXAsync()
   
   Reads a column value and converts it to the .NET type. If the value is database null, or if the conversion fails, the data is discarded and the specified default value is returned.

   ```csharp
   // If the defaultValue parameter is omitted, the `default` of the .NET type is usually returned.
   int id = await col.ToInt32Async(defaultValue: 123);
   ```

2. AsXXXAsync()

   Reads a column value and converts it to the .NET type. If the value is database null, or if the conversion fails, the data is discarded and `null` is returned.
   
   ```csharp
   int? id = await col.AsInt32Async();

   if (id is not null)
   {
       ...
   }
   ```

3. ToXXXsAsync()

   Reads a column value and converts it to the .NET array type. If the value is database null, or if the conversion fails, the data is discarded and the specified collection is returned.
   
   ```csharp
   // If the defaultValue parameter is omitted, an empty collection is returned.
   IAsyncEnumerable<int?> ids = await col.ToInt32sAsync(defaultValue: ...);

   await foreach (var id in ids)
   {
       ...
   }
   ```

4. AsXXXsAsync()

   Reads a column value and converts it to the .NET array type. If the value is database null, or if the conversion fails, the data is discarded and `null` is returned.
   
   ```csharp
   IAsyncEnumerable<int?>? ids = await col.AsInt32sAsync();

   if (ids is not null)
   {
       await foreach (var id in ids)
       {
           ...
       }
   }
   ```

### Binary Data and Strings

For binary data and strings, we can also write them directly to buffers, streams or writers.

```csharp
// Write binary data to a buffer.
var buffer = new byte[1024];
await col.WriteAsync(buffer);

// Write binary data to a stream.
await using var stream = new FileStream(...);
await col.WriteAsync(stream);
```

```csharp
// Write string to a buffer.
var buffer = new char[1024];
await col.WriteAsync(buffer);

// Write string to a writer.
await using var writer = new StreamWriter(...);
await col.WriteAsync(writer);
```

### JSON

SingWing.PgSql supports writing a column as a JSON property to a `Utf8JsonWriter`.

```csharp
// Write a column value as a JSON property to a `Utf8JsonWriter`.
await col.WriteValueAsync(jsonWriter);

// Write a column as a JSON property to a `Utf8JsonWriter` and specify the property name.
await col.WriteAsync(jsonWriter, propertyName: "id");

// Write a column as a JSON property to a `Utf8JsonWriter` without specifying the property name.
// In this case, the column camel-case name is used as the property name.
await col.WriteAsync(jsonWriter);
```

Or writing a row as a JSON object to a `Utf8JsonWriter`.

```csharp
// Write a row value as a JSON object to a `Utf8JsonWriter`.
await row.WriteValueAsync(jsonWriter);

// Write a row as a JSON object to a `Utf8JsonWriter` and specify the property name.
await row.WriteAsync(jsonWriter, propertyName: "book");
```

For `float` numbers, if the value is `NaN`, `Infinity`, or `-Infinity`, the strings "NaN", "Infinity", and "-Infinity" will be written, respectively. SingWing.PgSql does not support `NaN`, `Infinity`, or `-Infinity` for `numeric` and will write `null`.

The formatted strings for date, time, and datetime are "yyyy-MM-dd", "HH:mm:ss.FFFFFFF", and "yyyy-MM-ddTHH:mm:ss.ffffffffzzz", respectively. See [Custom date and time format strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings).

For interval values, use the standard format string ["c"](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-timespan-format-strings) for `TimeSpan`.

```csharp
// e.g. {"date": "2023-02-19"}.
await col.WriteAsync(jsonWriter, propertyName: "date");

// e.g. {"time": "02:12:09"}, {"time": "02:12:09.1234567"}...
await col.WriteAsync(jsonWriter, propertyName: "time");

// e.g. {"datetime": "2023-10-19T16:23:54.1234567\u002B08:00"}.
await col.WriteAsync(jsonWriter, propertyName: "datetime");

// e.g. {"interval": "00:00:00"}, {"interval": "3.17:25:30.1234567"}...
await col.WriteAsync(jsonWriter, propertyName: "interval");
```

## Other Commands

### Non-Query Commands

Use `ExecuteAsync` to execute non-query commands, such as `INSERT`, `UPDATE`, `DELETE`, etc. `ExecuteAsync` may also be used to execute query commands, such as `SELECT`, `FETCH`, in which case all data rows are retrieved and discarded.

In the first case, the number of rows affected is returned. In the second case, the number of rows retrieved is returned. For more information, see the definition of [CommandComplete](https://www.postgresql.org/docs/current/protocol-message-formats.html).

```csharp
// Execute commands in a database.
var insertedCount = await db.ExecuteAsync("INSERT INTO ...");
var selectedCount = await db.ExecuteAsync("SELECT ...");

// Execute commands in a transaction.
try
{
    var insertedCount = await transaction.ExecuteAsync("INSERT INTO ...");
    var updatedCount = await transaction.ExecuteAsync("UPDATE ...");
    var deletedCount = await transaction.ExecuteAsync("DELETE ...");

    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
    throw;
}
```

### Mulitple Statements

If a command contains more than one statements separated by ';', we need to use `PerformAsync`, and we cannot provide parameters in this case. All statements are executed as a single transaction, unless explicit transaction control commands are included.

See [Multiple Statements In A Simple Query](https://www.postgresql.org/docs/current/protocol-flow.html#id-1.10.6.7.4).

```csharp
// Statements are separated by semicolons and executed as a single transaction.
await db.PerformAsync("INSERT INTO ...;UPDATE ...;DELETE ...;...");
```

### Functions and Cursors

SingWing.PgSql provides `QueryCursorsAsync` and `QueryCursorNamesAsync` for `ITransaction` to support [cursor](https://www.postgresql.org/docs/current/plpgsql-cursors.html) operations. For example, we can use them to call a PostgreSQL [Function](https://www.postgresql.org/docs/current/plpgsql-declarations.html) that returns one or more cursors.

```csharp
// Calling function that return cursors, and retrieving rows from cursors.
await db.TransactAsync(async transaction =>
{
    // Call the function, pass parameter(s).
    await transaction.QueryCursorsAsync(
        "SELECT public.book_details($1)", 
        bookId);

    // Suppose public.book_details returns two cursors, book$ and author$.

    // Retrieve the book information.
    var rows = await transaction.QueryAsync("FETCH FIRST IN book$");
    await foreach (var row in rows)
    {
        ...
    }
    
    // Retrieve the author information.
    rows = await transaction.QueryAsync("FETCH FIRST IN author$");
    await foreach (var row in rows)
    {
        ...
    }
});
```

The PostgreSQL function `public.book_details` might be defined as follows:

```sql
CREATE FUNCTION public.book_details("bookId$" bigint) 
RETURNS SETOF refcursor 
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE
    "authorId$" bigint;
    "book$" refcursor := 'book$';
    "author$" refcursor := 'author$';
BEGIN
    SELECT
        "AuthorId"
    INTO
        "authorId$"
    FROM
        public.books
    WHERE
        "Id" = "bookId$"
    LIMIT 1;

    OPEN "book$" FOR
    SELECT
        "Id",
        "Title"
    FROM
        public.books
    WHERE
        "Id" = "bookId$"
    LIMIT 1;
    RETURN NEXT "book$";

    OPEN "author$" FOR
    SELECT
        "Id",
        "FirstName",
        "LastName"
    FROM
        public.authors
    WHERE
        "Id" = "authorId$"
    LIMIT 1;
    RETURN NEXT "author$";
END;
$BODY$;
```

## Authentication

Currently, only the [SASL SCRAM-SHA-256](https://www.postgresql.org/docs/current/sasl-authentication.html) authentication mechanism is supported.

## SSL

SSL is not supported.

## Exceptions

If connection or authentication fails, an `OpeningFailedException` is thrown.

During command execution, SingWing.PgSql may generate a `ServerException` if the PostgreSQL backend sends an [`ErrorResponse`](https://www.postgresql.org/docs/current/protocol-error-fields.html).

## Logging

In some cases, an `ErrorResponse` may be logged instead of generating a `ServerException`. For example, when we receive an `ErrorResponse` while waiting for the `ReadyForQuery` message. When this happens, we can set an `ILogger` to `Db.Logger` to log the error messages.

`Db.Logger` is also used to log `NoticeResponse` and other messages.

For more information about the PostgreSQL message formats, see [https://www.postgresql.org/docs/current/protocol-message-formats.html](https://www.postgresql.org/docs/current/protocol-message-formats.html).

## Other Data Types Implementation Guidelines

### Step 1 - Adding a New DataType Member

Add a member for the data type in the `enum DataType`.

### Step 2 - Implementing the IDataTypeProtocol Interface

Data of a particular data type might be transmitted between the front and back ends in [¡°text¡± or ¡°binary¡±](https://www.postgresql.org/docs/current/protocol-overview.html#PROTOCOL-FORMAT-CODES) format. SingWing.PgSql only supports the binary format.

`IDataTypeProtocol` defines how a data type should send binary data to and read binary data from the PostgreSQL backend. You can find definitions of the data types and the sending and receiving protocols for each data type from [pg_type.dat](https://github.com/postgres/postgres/blob/master/src/include/catalog/pg_type.dat) and [PostgreSQL Source Code](https://doxygen.postgresql.org/index.html).

### Step 3 - Implementing Parameter for the Data Type

In order for callers to pass the specified type of data, a subclass inherited from the `Parameter` class needs to be implemented. 
It is recommended not to make public subclasses of `Parameter`, but provide implicit conversions to `Parameter` instances, or provide extension methods for the conversions, such as `ToArrayParameter()`.

### Step 4 - Providing Methods for Reading Data

Add methods to read data of the new data type in the `IColumn` interface, and implement them in the internal `DataColumn` class.

## License

**MIT**.
