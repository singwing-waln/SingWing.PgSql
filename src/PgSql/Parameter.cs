using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.DataTypes;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;
using System.Globalization;

namespace SingWing.PgSql;

/// <summary>
/// Represents a parameter used by PostgreSQL commands.
/// </summary>
/// <remarks>
/// <para>
/// .NET types can be implicitly converted to <see cref="Parameter"/>, 
/// so when calling the methods of <see cref="IExecutor"/>, 
/// the caller can pass the values of .NET type directly.
/// </para>
/// </remarks>
public abstract partial class Parameter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Parameter"/> class.
    /// </summary>
    protected internal Parameter() { }

    /// <summary>
    /// Gets or sets the value of the parameter.
    /// </summary>
    public abstract object? Value { get; set; }

    /// <summary>
    /// Gets the data type of the parameter.
    /// </summary>
    public abstract DataType DataType { get; }

    /// <summary>
    /// Gets the length (in bytes) of the parameter value in binary format.
    /// </summary>
    /// <value>-1 if the <see cref="Value"/> is <see langword="null"/>.</value>
    internal abstract int BinaryLength { get; }

    /// <summary>
    /// Makes a copy of the current parameter.
    /// </summary>
    /// <returns>
    /// A <see cref="Parameter"/> instance that may not be newly created, 
    /// for example, when the parameter value is null, the predefined Null may be returned.
    /// </returns>
    public abstract Parameter Clone();

    /// <summary>
    /// Resets this parameter so that the <see cref="Parameter"/> instance can be reused in the future.
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// Writes the binary data of <see cref="Value"/> as part of a Bind message to the specified output stream.
    /// The length of the binary data will not be written to the output stream.
    /// </summary>
    /// <param name="writer">An output stream used to send data to the PostgreSQL database.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <remarks>
    /// For more information about Bind message, see <see href="https://www.postgresql.org/docs/current/protocol-message-formats.html"/>.
    /// </remarks>
    internal abstract ValueTask WriteAsync(
        Writer writer,
        CancellationToken cancellationToken);

    /// <summary>
    /// Creates a parameter instance of the specified data type.
    /// </summary>
    /// <param name="dataType">The data type of the parameter.</param>
    /// <returns>A new instance of the <see cref="Parameter"/> subclass.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="dataType"/> is not supported.</exception>
    public static Parameter Create(DataType dataType)
    {
        return dataType switch
        {
            DataType.Int2 => new PrimitiveParameter<short, Int2Protocol>(null),
            DataType.Int2Array => new NullableArrayParameter<short, Int2Protocol>(null),
            DataType.Int4 => new PrimitiveParameter<int, Int4Protocol>(null),
            DataType.Int4Array => new NullableArrayParameter<int, Int4Protocol>(null),
            DataType.Int8 => new PrimitiveParameter<long, Int8Protocol>(null),
            DataType.Int8Array => new NullableArrayParameter<long, Int8Protocol>(null),
            DataType.Float4 => new PrimitiveParameter<float, Float4Protocol>(null),
            DataType.Float4Array => new NullableArrayParameter<float, Float4Protocol>(null),
            DataType.Float8 => new PrimitiveParameter<double, Float8Protocol>(null),
            DataType.Float8Array => new NullableArrayParameter<double, Float8Protocol>(null),
            DataType.Numeric => new PrimitiveParameter<decimal, NumericProtocol>(null),
            DataType.NumericArray => new NullableArrayParameter<decimal, NumericProtocol>(null),
            DataType.Boolean => new PrimitiveParameter<bool, BooleanProtocol>(null),
            DataType.BooleanArray => new NullableArrayParameter<bool, BooleanProtocol>(null),
            DataType.Varchar => new PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>(null),
            DataType.VarcharArray => new NullableArrayParameter<ReadOnlyMemory<char>, VarcharProtocol>(null),
            DataType.Bytea => new PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol>(null),
            DataType.ByteaArray => new NullableArrayParameter<ReadOnlyMemory<byte>, ByteaProtocol>(null),
            DataType.Date => new PrimitiveParameter<DateOnly, DateProtocol>(null),
            DataType.DateArray => new NullableArrayParameter<DateOnly, DateProtocol>(null),
            DataType.Time => new PrimitiveParameter<TimeOnly, TimeProtocol>(null),
            DataType.TimeArray => new NullableArrayParameter<TimeOnly, TimeProtocol>(null),
            DataType.TimestampTz => new PrimitiveParameter<DateTime, TimestampTzProtocol>(null),
            DataType.TimestampTzArray => new NullableArrayParameter<DateTime, TimestampTzProtocol>(null),
            DataType.Interval => new PrimitiveParameter<TimeSpan, IntervalProtocol>(null),
            DataType.IntervalArray => new NullableArrayParameter<TimeSpan, IntervalProtocol>(null),
            DataType.Uuid => new PrimitiveParameter<Guid, UuidProtocol>(null),
            DataType.UuidArray => new NullableArrayParameter<Guid, UuidProtocol>(null),
            DataType.Jsonb => new ByteMemoryJsonParameter(null),
            _ => throw new ArgumentOutOfRangeException(
                nameof(dataType),
                string.Format(CultureInfo.CurrentCulture, Strings.DataTypeNotSupported, dataType)),
        };
    }
}
