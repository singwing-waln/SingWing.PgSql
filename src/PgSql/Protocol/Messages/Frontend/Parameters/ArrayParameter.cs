using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.DataTypes;

namespace SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

/// <summary>
/// Represents a parameter whose value is an array type value.
/// </summary>
/// <typeparam name="TElement">The type of array elements.</typeparam>
/// <typeparam name="TProtocol">The type that implements <see cref="IDataTypeProtocol{TElement, TProtocol}"/>.</typeparam>
internal sealed class ArrayParameter<TElement, TProtocol> : Parameter
    where TElement : struct
    where TProtocol : IDataTypeProtocol<TElement, TProtocol>
{
    /// <summary>
    /// The parameter whose value is <see langword="null"/>.
    /// </summary>
    private static readonly ArrayParameter<TElement, TProtocol> Null = new(null);

    /// <summary>
    /// The parameter whose value is an empty array.
    /// </summary>
    private static readonly ArrayParameter<TElement, TProtocol> Empty = new(Array.Empty<TElement>());

    /// <summary>
    /// The binary length (in bytes) of the parameter value.
    /// </summary>
    private int _binaryLength;

    /// <summary>
    /// The parameter value.
    /// </summary>
    private IEnumerable<TElement>? _elements;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayParameter{TElement, TProtocol}"/> class 
    /// with a collection of TElement? values.
    /// </summary>
    /// <param name="elements">The collection of TElement? values.</param>
    internal ArrayParameter(IEnumerable<TElement>? elements)
    {
        _binaryLength = GetArrayBinaryLengthForSending(elements);
        _elements = elements;
    }

    /// <inheritdoc />
    public override object? Value
    {
        get => _elements;
        set
        {
            if (ReferenceEquals(this, Null) || ReferenceEquals(this, Empty))
            {
                return;
            }

            if (value is null)
            {
                _binaryLength = -1;
                _elements = null;
                return;
            }

            if (value is IEnumerable<TElement> enumerable)
            {
                _binaryLength = GetArrayBinaryLengthForSending(enumerable);
                _elements = enumerable;
                return;
            }

            if (value is IEnumerable<TElement?> nullableElements)
            {
                var elements = ToNonNullable(nullableElements);
                _binaryLength = GetArrayBinaryLengthForSending(elements);
                _elements = elements;
                return;
            }

            if (value is IEnumerable objects)
            {
                var elements = ObjectsToElements(objects);
                _binaryLength = GetArrayBinaryLengthForSending(elements);
                _elements = elements;
                return;
            }

            _binaryLength = -1;
            _elements = null;

            static IEnumerable<TElement> ToNonNullable(IEnumerable<TElement?> enumerable)
            {
                foreach (var item in enumerable)
                {
                    if (item is not null)
                    {
                        yield return item.Value;
                    }
                }
            }

            static IEnumerable<TElement> ObjectsToElements(IEnumerable enumerable)
            {
                var dataType = TProtocol.ElementDataType;
                foreach (var item in enumerable)
                {
                    var element = Conversion.ToElement(item, dataType);
                    if (element is not null)
                    {
                        yield return (TElement)element;
                    }
                }
            }
        }
    }

    /// <inheritdoc />
    public override DataType DataType => TProtocol.ArrayDataType;

    /// <inheritdoc />
    internal override int BinaryLength => _binaryLength;

    /// <inheritdoc />
    public override Parameter Clone() =>
        ReferenceEquals(this, Null) || ReferenceEquals(this, Empty)
        ? this
        : (Parameter)new ArrayParameter<TElement, TProtocol>(_elements);

    /// <inheritdoc />
    public override void Reset()
    {
        if (ReferenceEquals(this, Null) || ReferenceEquals(this, Empty))
        {
            return;
        }

        _binaryLength = -1;
        _elements = null;
    }

    /// <inheritdoc />
    internal override async ValueTask WriteAsync(
        Writer writer,
        CancellationToken cancellationToken)
    {
        if (_elements is null)
        {
            return;
        }

        await writer.WriteArrayHeaderAsync(
            arrayLength: _elements.Count(),
            elementDataType: TProtocol.ElementDataType,
            cancellationToken);

        if (TProtocol.IsElementBinaryLengthFixed)
        {
            var elementBinaryLength = TProtocol.ElementBinaryLengthOf(default);
            foreach (var element in _elements)
            {
                await writer.WriteInt32Async(elementBinaryLength, cancellationToken);
                await TProtocol.WriteElementAsync(element, writer, cancellationToken);
            }
        }
        else
        {
            foreach (var element in _elements)
            {
                await writer.WriteInt32Async(TProtocol.ElementBinaryLengthOf(element), cancellationToken);
                await TProtocol.WriteElementAsync(element, writer, cancellationToken);
            }
        }
    }

    /// <summary>
    /// Creates or returns an instance of the <see cref="ArrayParameter{TElement, TProtocol}"/> class 
    /// with a collection of <typeparamref name="TElement"/> values.
    /// </summary>
    /// <param name="elements">The collection of <typeparamref name="TElement"/> values.</param>
    /// <returns>
    /// <see cref="Null"/> if <paramref name="elements"/> is <see langword="null"/>, 
    /// <see cref="Empty"/> if <paramref name="elements"/> is an empty collection, 
    /// a new instance of the <see cref="ArrayParameter{TElement, TProtocol}"/> class otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ArrayParameter<TElement, TProtocol> From(IEnumerable<TElement>? elements) =>
        elements is null
        ? Null
        : elements.IsEmpty()
        ? Empty
        : new ArrayParameter<TElement, TProtocol>(elements);

    /// <summary>
    /// Gets the total binary length of the array and its elements for sending data to PostgreSQL.
    /// </summary>
    /// <param name="elements">The collection of array elements.</param>
    /// <returns>The sum of the binary lengths of the array header information and all elements.</returns>
    private static int GetArrayBinaryLengthForSending(in IEnumerable<TElement>? elements)
    {
        if (elements is null)
        {
            return -1;
        }

        var length = ArrayHelper.HeaderBinaryLengthForSending;

        if (TProtocol.IsElementBinaryLengthFixed)
        {
            length += (sizeof(int) + TProtocol.ElementBinaryLengthOf(default)) * elements.Count();
        }
        else
        {
            foreach (var element in elements)
            {
                length += sizeof(int) + TProtocol.ElementBinaryLengthOf(element);
            }
        }

        return length;
    }
}
