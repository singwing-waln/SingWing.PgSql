using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.ByteaArray"/> parameters.
/// </summary>
internal sealed class ByteaProtocol : IDataTypeProtocol<ReadOnlyMemory<byte>, ByteaProtocol>
{
    /// <summary>
    /// The parameter whose value is ReadOnlyMemory&lt;<see langword="byte"/>&gt;.Empty.
    /// </summary>
    private static readonly PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol> Empty = new(ReadOnlyMemory<byte>.Empty);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="ByteaProtocol"/> class from outside.
    /// </summary>
    private ByteaProtocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.ByteaArray;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Bytea;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => false;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in ReadOnlyMemory<byte> element) => element.Length;

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        ReadOnlyMemory<byte> element,
        Writer writer,
        CancellationToken cancellationToken) => writer.WriteBinaryAsync(element, cancellationToken);

    /// <inheritdoc />
    public static ReadOnlyMemory<byte>? ToElement(object? value) => Conversion.ToByteMemory(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol> parameter) =>
        ReferenceEquals(parameter, Empty);

    /// <inheritdoc />
    public static PrimitiveParameter<ReadOnlyMemory<byte>, ByteaProtocol>? PredefinedParameterFor(in ReadOnlyMemory<byte> value) =>
        value.IsEmpty ? Empty : null;

    /// <inheritdoc />
    public static ValueTask<ReadOnlyMemory<byte>?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken) =>
        reader.ReadBinaryAsync((Memory<byte>?)null, binaryLength, cancellationToken);

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) =>
        elementDataTypeCode == (uint)DataType.Bytea;

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) =>
        arrayDataTypeCode == (uint)DataType.ByteaArray;

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) =>
        arrayDataTypeCode == (uint)DataType.ByteaArray
        ? (uint)DataType.Bytea
        : 0;
}
