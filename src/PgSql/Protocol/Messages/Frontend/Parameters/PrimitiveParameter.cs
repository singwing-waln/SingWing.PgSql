using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

/// <summary>
/// Represents a parameter whose value is a primitive type value.
/// </summary>
/// <typeparam name="TElement">The type of primitive value.</typeparam>
/// <typeparam name="TProtocol">The type that implements <see cref="IDataTypeProtocol{TElement, TProtocol}"/>.</typeparam>
internal sealed class PrimitiveParameter<TElement, TProtocol> : Parameter
    where TElement : struct
    where TProtocol : IDataTypeProtocol<TElement, TProtocol>
{
    /// <summary>
    /// The parameter whose value is <see langword="null"/>.
    /// </summary>
    private static readonly PrimitiveParameter<TElement, TProtocol> Null = new(null);

    /// <summary>
    /// The parameter value.
    /// </summary>
    private TElement? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveParameter{TElement, TProtocol}"/> class
    /// with the specified value.
    /// </summary>
    /// <param name="value">The parameter value.</param>
    internal PrimitiveParameter(TElement? value) => _value = value;

    /// <inheritdoc />
    public override object? Value
    {
        get => _value;
        set
        {
            if (ReferenceEquals(this, Null) || TProtocol.IsPredefinedParameter(this))
            {
                return;
            }

            _value = TProtocol.ToElement(value);
        }
    }

    /// <inheritdoc />
    public override DataType DataType => TProtocol.ElementDataType;

    /// <inheritdoc />
    internal override int BinaryLength =>
        _value is null ? -1 : TProtocol.ElementBinaryLengthOf(_value.Value);

    /// <inheritdoc />
    public override Parameter Clone() =>
        ReferenceEquals(this, Null) || TProtocol.IsPredefinedParameter(this)
        ? this
        : (Parameter)new PrimitiveParameter<TElement, TProtocol>(_value);

    /// <inheritdoc />
    public override void Reset()
    {
        if (ReferenceEquals(this, Null) || TProtocol.IsPredefinedParameter(this))
        {
            return;
        }

        _value = null;
    }

    /// <inheritdoc />
    internal override ValueTask WriteAsync(
        Writer writer,
        CancellationToken cancellationToken) =>
        _value is null
        ? ValueTask.CompletedTask
        : TProtocol.WriteElementAsync(_value.Value, writer, cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="PrimitiveParameter{TElement, TProtocol}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The parameter value.</param>
    /// <returns>
    /// <see cref="Null"/> if <paramref name="value"/> is <see langword="null"/>, 
    /// a predefined or newly created <see cref="PrimitiveParameter{TElement, TProtocol}"/> instance otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static PrimitiveParameter<TElement, TProtocol> For(TElement? value) =>
        value is null
        ? Null
        : TProtocol.PredefinedParameterFor(value.Value)
        ?? new PrimitiveParameter<TElement, TProtocol>(value);
}
