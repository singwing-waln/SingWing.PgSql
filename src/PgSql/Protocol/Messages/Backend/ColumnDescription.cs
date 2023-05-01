using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Backend;

/// <summary>
/// Represents the message that describes column information.
/// </summary>
internal sealed class ColumnDescription
{
    /// <summary>
    /// The description of empty columns.
    /// </summary>
    internal static readonly ColumnDescription Empty = new();

    /// <summary>
    /// The maximum length of the column name buffer.
    /// </summary>
    /// <remarks>
    /// <para>
    /// At compile time, the default maximum length of an identifier in PostgreSQL is 63.
    /// <see href="https://www.postgresql.org/docs/current/sql-syntax-lexical.html#SQL-SYNTAX-IDENTIFIERS"/>.
    /// </para>
    /// <para>
    /// If a column name is longer than <see cref="MaxNameLength"/>, it is truncated.
    /// </para>
    /// </remarks>
    internal const int MaxNameLength = 64;

    /// <summary>
    /// The buffer for column name.
    /// </summary>
    private readonly char[] _name;

    /// <summary>
    /// The actual length of the column name.
    /// </summary>
    private byte _nameLength;

    /// <summary>
    /// The data type code of the column.
    /// </summary>
    private uint _dataType;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColumnDescription"/> class.
    /// </summary>
    internal ColumnDescription()
    {
        Ordinal = 0;
        _name = new char[MaxNameLength];
        _nameLength = 0;
        _dataType = 0;
    }

    /// <inheritdoc />
    internal int Ordinal { get; private set; }

    /// <inheritdoc />
    internal ReadOnlySpan<char> Name => _name.AsSpan(0, _nameLength);

    /// <inheritdoc />
    internal uint DataType => _dataType;

    /// <summary>
    /// Reads the information of the column from the input stream.
    /// </summary>
    /// <param name="reader">The input stream to read.</param>
    /// <param name="maxBytes">The maximum number of bytes read.</param>
    /// <param name="ordinal">The 0-based position of the column in the row.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The number of bytes actually read.</returns>
    internal async ValueTask<int> ReadAsync(
        Reader reader,
        int maxBytes,
        int ordinal,
        CancellationToken cancellationToken)
    {
        // If the column name is longer than MaxNameLength, it is truncated.
        var (charCount, byteCount) = await reader.ReadColumnNameAsync(
            _name, maxBytes, cancellationToken);
        _nameLength = (byte)Math.Min(charCount, MaxNameLength);
        Ordinal = ordinal;

        // (Int32): If the field can be identified as a column of a specific table,
        // the object ID of the table; otherwise zero.
        // 
        // (Int16): If the field can be identified as a column of a specific table,
        // the attribute number of the column; otherwise zero.
        await reader.DiscardAsync(sizeof(int) + sizeof(short), cancellationToken);
        byteCount += sizeof(int) + sizeof(short);

        // The object ID of the field's data type.
        _dataType = await reader.ReadUInt32Async(cancellationToken);
        byteCount += sizeof(uint);

        // (Int16): The data type size (see pg_type.typlen).
        // Note that negative values denote variable-width types.
        //
        // (Int32): The type modifier (see pg_attribute.atttypmod).
        // The meaning of the modifier is type-specific.
        await reader.DiscardAsync(sizeof(short) + sizeof(int), cancellationToken);
        byteCount += sizeof(short) + sizeof(int);

        // The format code being used for the field.
        // Currently will be zero (text) or one (binary).
        // In a RowDescription returned from the statement variant of Describe,
        // the format code is not yet known and will always be zero.
        var formatCode = await reader.ReadInt16Async(cancellationToken);
        byteCount += sizeof(short);

        return byteCount;
    }
}
