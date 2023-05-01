namespace SingWing.PgSql;

/// <summary>
/// Defines the data type of <see cref="Parameter"/>s.
/// </summary>
/// <remarks>
/// <para>
/// </para>
/// <para>
/// The underlying integer values are PostgreSQL data type OIDs:
/// <see href="https://github.com/postgres/postgres/blob/master/src/include/catalog/pg_type.dat"/>.
/// </para>
/// <para>
/// For PostgreSQL data types, see: 
/// <see href="https://www.postgresql.org/docs/current/datatype.html"/>.
/// </para>
/// </remarks>
public enum DataType : uint
{
    /// <summary>
    /// 16-bit signed integer (smallint).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-numeric.html#DATATYPE-INT"/>.
    /// </remarks>
    Int2 = 21,

    /// <summary>
    /// One-dimensional array of 16-bit signed integers (smallint[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    Int2Array = 1005,

    /// <summary>
    /// 32-bit signed integer (int).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-numeric.html#DATATYPE-INT"/>.
    /// </remarks>
    Int4 = 23,

    /// <summary>
    /// One-dimensional array of 32-bit signed integers (int[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    Int4Array = 1007,

    /// <summary>
    /// 64-bit signed integer (bigint).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-numeric.html#DATATYPE-INT"/>.
    /// </remarks>
#pragma warning disable CA1720
    Int8 = 20,
#pragma warning restore CA1720

    /// <summary>
    /// One-dimensional array of 64-bit signed integers (bigint[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    Int8Array = 1016,

    /// <summary>
    /// Single-precision floating-point number (real).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-numeric.html#DATATYPE-FLOAT"/>.
    /// </remarks>
    Float4 = 700,

    /// <summary>
    /// One-dimensional array of single-precision floating-point numbers (real[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    Float4Array = 1021,

    /// <summary>
    /// Double-precision floating-point number (double precision).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-numeric.html#DATATYPE-FLOAT"/>.
    /// </remarks>
    Float8 = 701,

    /// <summary>
    /// One-dimensional array of double-precision floating-point numbers (double precision).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    Float8Array = 1022,

    /// <summary>
    /// Arbitrary precision number (numeric).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-numeric.html#DATATYPE-NUMERIC-DECIMAL"/>.
    /// </remarks>
    Numeric = 1700,

    /// <summary>
    /// One-dimensional array of arbitrary precision numbers (numeric[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    NumericArray = 1231,

    /// <summary>
    /// Boolean (true or false) value (boolean).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-boolean.html"/>.
    /// </remarks>
    Boolean = 16,

    /// <summary>
    /// One-dimensional array of boolean (true or false) values (boolean[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    BooleanArray = 1000,

    /// <summary>
    /// Variable-length character string (varchar).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-character.html"/>.
    /// </remarks>
    Varchar = 1043,

    /// <summary>
    /// One-dimensional array of variable-length character strings (varchar[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    VarcharArray = 1015,

    /// <summary>
    /// Variable-length binary string (bytea).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-binary.html"/>.
    /// </remarks>
    Bytea = 17,

    /// <summary>
    /// One-dimensional array of variable-length binary strings (bytea[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    ByteaArray = 1001,

    /// <summary>
    /// Date (no time of day) (date).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-datetime.html"/>.
    /// </remarks>
    Date = 1082,

    /// <summary>
    /// One-dimensional array of dates (no time of day) (date[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    DateArray = 1182,

    /// <summary>
    /// Time of day (no date, no time zone) (time).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-datetime.html"/>.
    /// </remarks>
    Time = 1083,

    /// <summary>
    /// One-dimensional array of times (time of day, no date, no time zone) (time[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    TimeArray = 1183,

    /// <summary>
    /// Datetime with time zone (timestamptz).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-datetime.html"/>.
    /// </remarks>
    TimestampTz = 1184,

    /// <summary>
    /// One-dimensional array of datetimes (with time zone) (timestamptz[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    TimestampTzArray = 1185,

    /// <summary>
    /// Time interval (interval).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-datetime.html"/>.
    /// </remarks>
    Interval = 1186,

    /// <summary>
    /// One-dimensional array of time intervals (interval[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    IntervalArray = 1187,

    /// <summary>
    /// Universally unique identifier (uuid).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-uuid.html"/>.
    /// </remarks>
    Uuid = 2950,

    /// <summary>
    /// One-dimensional array of universally unique identifiers (uuid[]).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/arrays.html"/>.
    /// </remarks>
    UuidArray = 2951,

    /// <summary>
    /// JSON data stored in a decomposed binary format (jsonb).
    /// </summary>
    /// <remarks>
    /// <see href="https://www.postgresql.org/docs/current/datatype-json.html"/>.
    /// </remarks>
    Jsonb = 3802
}
