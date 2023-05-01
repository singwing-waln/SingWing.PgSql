using SingWing.PgSql.Protocol.Messages.Backend;

namespace SingWing.PgSql;

/// <summary>
/// Represents an exception raised by the PostgreSQL backend.
/// </summary>
/// <remarks>
/// <see href="https://www.postgresql.org/docs/current/protocol-error-fields.html"/>.
/// </remarks>
public sealed class ServerException : ApplicationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServerException"/> class 
    /// with the <see cref="ErrorResponse"/> message sent by the backend.
    /// </summary>
    /// <param name="error">The <see cref="ErrorResponse"/> message received from the database.</param>
    internal ServerException(ErrorResponse error) : base(MessageFor(error))
    {
        Debug.Assert(error.Severity >= ProblemSeverity.Error);
        Error = error;
    }

    /// <summary>
    /// Gets the <see cref="ErrorResponse"/> message sent by the backend.
    /// </summary>
    internal ErrorResponse Error { get; }

    /// <summary>
    /// Gets the severity of the error.
    /// </summary>
    public Severity Severity => (Severity)(int)Error.Severity;

    /// <summary>
    /// Gets the SQLSTATE code for the error.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/errcodes-appendix.html"/>.
    /// </remarks>
    public string SqlState => Error.SqlState;

    /// <summary>
    /// Gets an optional secondary error message carrying more detail about the problem. 
    /// Might run to multiple lines.
    /// </summary>
    public string Detail => Error.Detail;

    /// <summary>
    /// Gets an optional suggestion what to do about the problem. 
    /// This is intended to differ from <see cref="Detail"/> in that it offers advice (potentially inappropriate) 
    /// rather than hard facts. 
    /// Might run to multiple lines.
    /// </summary>
    public string Hint => Error.Hint;

    /// <summary>
    /// Gets a decimal ASCII integer value indicating an error cursor position as an index into the original query string. 
    /// The first character has index 1, and positions are measured in characters not bytes.
    /// </summary>
    public int Position => Error.Position;

    /// <summary>
    /// Gets an indication of the context in which the error occurred. 
    /// Presently this includes a call stack traceback of active procedural language functions 
    /// and internally-generated queries. The trace is one entry per line, most recent first.
    /// </summary>
    public string Where => Error.Where;

    /// <summary>
    /// Gets the name of the schema containing that object, 
    /// if the error was associated with a specific database object.
    /// </summary>
    public string SchemaName => Error.SchemaName;

    /// <summary>
    /// Gets the name of the table, if the error was associated with a specific table. 
    /// Refer to the schema name field for the name of the table's schema.
    /// </summary>
    public string TableName => Error.TableName;

    /// <summary>
    /// Gets the name of the column, if the error was associated with a specific table column. 
    /// Refer to the schema and table name fields to identify the table.
    /// </summary>
    public string ColumnName => Error.ColumnName;

    /// <summary>
    /// Gets the name of the data type, if the error was associated with a specific data type. 
    /// Refer to the schema name field for the name of the data type's schema.
    /// </summary>
    public string DataTypeName => Error.DataTypeName;

    /// <summary>
    /// Gets the name of the constraint, if the error was associated with a specific constraint. 
    /// Refer to fields listed above for the associated table or domain. 
    /// (For this purpose, indexes are treated as constraints, even if they weren't created with constraint syntax.)
    /// </summary>
    public string ConstraintName => Error.ConstraintName;

    /// <summary>
    /// Gets the line number of the source-code location where the error was reported.
    /// </summary>
    public string LineNumber => Error.Line;

    /// <summary>
    /// Gets the name of the source-code routine reporting the error.
    /// </summary>
    public string RoutineName => Error.Routine;

    /// <summary>
    /// Generates an error message containing SqlState for the specified <see cref="ErrorResponse"/>.
    /// </summary>
    /// <param name="error">The <see cref="ErrorResponse"/> instance.</param>
    /// <returns>The error message.</returns>
    private static string MessageFor(ErrorResponse error) =>
        $"{error.Message} ({error.SqlState})";
}
