using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.VarcharArray"/> parameters.
/// </summary>
internal sealed class VarcharProtocol : IDataTypeProtocol<ReadOnlyMemory<char>, VarcharProtocol>
{
    /// <summary>
    /// text.
    /// </summary>
    internal const uint TextTypeCode = 25;

    /// <summary>
    /// One-dimensional array of text (text[]).
    /// </summary>
    internal const uint TextArrayTypeCode = 1009;

    /// <summary>
    /// bpchar, char(length), blank-padded string, fixed storage length.
    /// </summary>
    internal const uint BpCharTypeCode = 1042;

    /// <summary>
    /// One-dimensional array of bpchar (bpchar[]).
    /// </summary>
    internal const uint BpCharArrayTypeCode = 1014;

    /// <summary>
    /// refcursor.
    /// </summary>
    /// <remarks>
    /// <see href="https://github.com/postgres/postgres/blob/master/src/include/catalog/pg_type.dat#L355"/>.
    /// </remarks>
    internal const uint RefCursorTypeCode = 1790;

    /// <summary>
    /// One-dimensional array of refcursor (refcursor[]).
    /// </summary>
    internal const uint RefCursorArrayTypeCode = 2201;

    /// <summary>
    /// name, 63-byte type for storing system identifiers.
    /// </summary>
    internal const uint NameTypeCode = 19;

    /// <summary>
    /// One-dimensional array of name (name[]).
    /// </summary>
    internal const uint NameArrayTypeCode = 1003;

    /// <summary>
    /// The parameter whose value is ReadOnlyMemory&lt;<see langword="char"/>&gt;.Empty.
    /// </summary>
    private static readonly PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol> Empty = new(ReadOnlyMemory<char>.Empty);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="VarcharProtocol"/> class from outside.
    /// </summary>
    private VarcharProtocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.VarcharArray;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Varchar;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => false;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in ReadOnlyMemory<char> element) =>
        Encoding.UTF8.GetByteCount(element.Span);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        ReadOnlyMemory<char> element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteStringAsync(element, cancellationToken);

    /// <inheritdoc />
    public static ReadOnlyMemory<char>? ToElement(object? value) =>
        Conversion.ToCharMemory(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol> parameter) =>
        ReferenceEquals(parameter, Empty);

    /// <inheritdoc />
    public static PrimitiveParameter<ReadOnlyMemory<char>, VarcharProtocol>? PredefinedParameterFor(in ReadOnlyMemory<char> value) =>
        value.IsEmpty ? Empty : null;

    /// <inheritdoc />
    public static async ValueTask<ReadOnlyMemory<char>?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        if (!CanConvertElementFrom(elementDataTypeCode))
        {
            await reader.DiscardAsync(binaryLength, cancellationToken);
            return null;
        }

        if (binaryLength == 0)
        {
            return ReadOnlyMemory<char>.Empty;
        }

        return await reader.ReadCharMemoryAsync(
            null, binaryLength, cancellationToken)
            ?? ReadOnlyMemory<char>.Empty;
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) => elementDataTypeCode is
        ((uint)DataType.Varchar) or
        TextTypeCode or
        BpCharTypeCode or
        RefCursorTypeCode or
        NameTypeCode;

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is
        ((uint)DataType.VarcharArray) or
        TextArrayTypeCode or
        BpCharArrayTypeCode or
        RefCursorArrayTypeCode or
        NameArrayTypeCode;

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.VarcharArray => (uint)DataType.Varchar,
        TextArrayTypeCode => TextTypeCode,
        BpCharArrayTypeCode => BpCharTypeCode,
        RefCursorArrayTypeCode => RefCursorTypeCode,
        NameArrayTypeCode => NameTypeCode,
        _ => 0
    };
}
