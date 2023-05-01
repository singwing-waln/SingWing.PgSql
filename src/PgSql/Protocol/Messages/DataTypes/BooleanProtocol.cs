using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.BooleanArray"/> parameters.
/// </summary>
internal sealed class BooleanProtocol : IDataTypeProtocol<bool, BooleanProtocol>
{
    /// <summary>
    /// The parameter whose value is <see langword="true"/>.
    /// </summary>
    private static readonly PrimitiveParameter<bool, BooleanProtocol> True = new(true);

    /// <summary>
    /// The parameter whose value is <see langword="false"/>.
    /// </summary>
    private static readonly PrimitiveParameter<bool, BooleanProtocol> False = new(false);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="BooleanProtocol"/> class from outside.
    /// </summary>
    private BooleanProtocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.BooleanArray;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Boolean;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in bool element) => sizeof(byte);

    /// <inheritdoc />
    public static ValueTask WriteElementAsync(
        bool element,
        Writer writer,
        CancellationToken cancellationToken) =>
        writer.WriteByteAsync(element ? (byte)1 : (byte)0, cancellationToken);

    /// <inheritdoc />
    public static bool? ToElement(object? value) => Conversion.ToBoolean(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<bool, BooleanProtocol> parameter) =>
        ReferenceEquals(parameter, True) || ReferenceEquals(parameter, False);

    /// <inheritdoc />
    public static PrimitiveParameter<bool, BooleanProtocol>? PredefinedParameterFor(in bool value) =>
        value ? True : False;

    /// <inheritdoc />
    public static async ValueTask<bool?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        if (elementDataTypeCode != (uint)DataType.Boolean)
        {
            await reader.DiscardAsync(binaryLength, cancellationToken);
            return null;
        }

        return await reader.ReadByteAsync(cancellationToken) != 0;
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) =>
        elementDataTypeCode == (uint)DataType.Boolean;

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) =>
        arrayDataTypeCode == (uint)DataType.BooleanArray;

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) =>
        arrayDataTypeCode == (uint)DataType.BooleanArray ? (uint)DataType.Boolean : 0;
}
