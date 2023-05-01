using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

namespace SingWing.PgSql.Protocol.Messages;

/// <summary>
/// Provides the data type, binary length, and methods for reading and writing values.
/// </summary>
/// <typeparam name="TElement">The type of array elements.</typeparam>
/// <typeparam name="TProtocol">The type that implements this interface.</typeparam>
internal interface IDataTypeProtocol<TElement, TProtocol>
    where TElement : struct
    where TProtocol : IDataTypeProtocol<TElement, TProtocol>
{
    /// <summary>
    /// Gets the data type code of the array.
    /// </summary>
    static abstract DataType ArrayDataType { get; }

    /// <summary>
    /// Gets the data type code of the array elements.
    /// </summary>
    static abstract DataType ElementDataType { get; }

    /// <summary>
    /// Gets a value that indicates whether the binary length of a single array element is a fixed value.
    /// </summary>
    static abstract bool IsElementBinaryLengthFixed { get; }

    /// <summary>
    /// Gets the binary length of the specified element.
    /// </summary>
    /// <param name="element">The element in the array.</param>
    /// <returns>The binary length (in bytes) of the <paramref name="element"/>.</returns>
    static abstract int ElementBinaryLengthOf(in TElement element);

    /// <summary>
    /// Writes binary data of an array element as part of a Bind message to the specified output stream.
    /// </summary>
    /// <param name="element">The array element to write.</param>
    /// <param name="writer">The output stream used to send data to the PostgreSQL database.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    static abstract ValueTask WriteElementAsync(
        TElement element,
        Writer writer,
        CancellationToken cancellationToken);

    /// <summary>
    /// Converts an object to a value of the <typeparamref name="TElement"/> type.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>The converted value, <see langword="null"/> if no conversion is possible.</returns>
    static abstract TElement? ToElement(object? value);

    /// <summary>
    /// Checks whether the specified parameter instance is a predefined parameter.
    /// </summary>
    /// <param name="parameter">The parameter to check.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="parameter"/> is predefined, 
    /// <see langword="false"/> otherwise.
    /// </returns>
    static abstract bool IsPredefinedParameter(PrimitiveParameter<TElement, TProtocol> parameter);

    /// <summary>
    /// Gets the predefined parameter for the specified parameter value.
    /// </summary>
    /// <param name="value">The parameter value.</param>
    /// <returns>
    /// The predefined parameter for <paramref name="value"/>, 
    /// or <see langword="null"/> if there's no predefined parameter for the <paramref name="value"/>.
    /// </returns>
    static abstract PrimitiveParameter<TElement, TProtocol>? PredefinedParameterFor(in TElement value);

    /// <summary>
    /// Reads an array element from the specified input stream.
    /// </summary>
    /// <param name="elementDataTypeCode">The data type of the column.</param>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="reader">The input stream used to receive data from the PostgreSQL database.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The read element.
    /// Or <see langword="null"/> if <paramref name="binaryLength"/> is less than 0, or the conversion is not possible.
    /// </returns>
    static abstract ValueTask<TElement?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken);

    /// <summary>
    /// Checks whether a value of the specified element type can be converted to a value of the current type.
    /// </summary>
    /// <param name="elementDataTypeCode">The element data type to check.</param>
    /// <returns>
    /// <see langword="true"/> if it can be converted, <see langword="false"/> otherwise.
    /// </returns>
    static abstract bool CanConvertElementFrom(uint elementDataTypeCode);

    /// <summary>
    /// Checks whether an array of the specified array type can be converted to a value of the current array type.
    /// </summary>
    /// <param name="arrayDataTypeCode">The array data type to check.</param>
    /// <returns>
    /// <see langword="true"/> if it can be converted, <see langword="false"/> otherwise.
    /// </returns>
    static abstract bool CanConvertArrayFrom(uint arrayDataTypeCode);

    /// <summary>
    /// Gets the data type of the element in an array of the specified array type.
    /// </summary>
    /// <param name="arrayDataTypeCode">The array data type.</param>
    /// <returns>The element data type, or 0 if the conversion is not possible.</returns>
    static abstract uint ElementTypeOfArrayType(uint arrayDataTypeCode);
}
