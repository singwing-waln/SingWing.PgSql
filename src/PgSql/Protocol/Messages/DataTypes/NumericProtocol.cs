using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;
using System.Runtime.InteropServices;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.NumericArray"/> parameters.
/// </summary>
internal sealed class NumericProtocol : IDataTypeProtocol<decimal, NumericProtocol>
{
    /// <summary>
    /// The parameter whose value is 0.
    /// </summary>
    private static readonly PrimitiveParameter<decimal, NumericProtocol> Zero = new(0);

    /// <summary>
    /// The parameter whose value is decimal.MinValue.
    /// </summary>
    private static readonly PrimitiveParameter<decimal, NumericProtocol> Min = new(decimal.MinValue);

    /// <summary>
    /// The parameter whose value is decimal.MaxValue.
    /// </summary>
    private static readonly PrimitiveParameter<decimal, NumericProtocol> Max = new(decimal.MaxValue);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="NumericProtocol"/> class from outside.
    /// </summary>
    private NumericProtocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.NumericArray;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Numeric;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => false;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in decimal element) =>
        DecimalBinary.BinaryLengthOf(element);

    /// <inheritdoc />
    public static async ValueTask WriteElementAsync(
        decimal element,
        Writer writer,
        CancellationToken cancellationToken)
    {
        var binaryLength = DecimalBinary.BinaryLengthOf(element);
        var groupCount = (binaryLength >> 1 /* / sizeof(short) */) - 4;
        await writer.EnsureSpaceAsync(binaryLength, cancellationToken);
        new DecimalBinary(element).Write(writer, groupCount);
    }

    /// <inheritdoc />
    public static decimal? ToElement(object? value) => Conversion.ToDecimal(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<decimal, NumericProtocol> parameter) =>
        ReferenceEquals(parameter, Zero) ||
        ReferenceEquals(parameter, Min) ||
        ReferenceEquals(parameter, Max);

    /// <inheritdoc />
    public static PrimitiveParameter<decimal, NumericProtocol>? PredefinedParameterFor(in decimal value)
    {
        if (value == 0)
        {
            return Zero;
        }

        if (value == decimal.MinValue)
        {
            return Min;
        }

        if (value == decimal.MaxValue)
        {
            return Max;
        }

        return null;
    }

    /// <inheritdoc />
    public static async ValueTask<decimal?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        return elementDataTypeCode switch
        {
            (uint)DataType.Numeric => await DecimalBinary.ReadAsync(binaryLength, reader, cancellationToken),
            (uint)DataType.Float4 => unchecked((decimal?)await Float4Protocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            (uint)DataType.Float8 => unchecked((decimal?)await Float8Protocol.ReadElementAsync(
                                elementDataTypeCode, binaryLength, reader, cancellationToken)),
            (uint)DataType.Int2 => await reader.ReadInt16Async(cancellationToken),
            (uint)DataType.Int4 => await reader.ReadInt32Async(cancellationToken),
            (uint)DataType.Int8 => await reader.ReadInt64Async(cancellationToken),
            _ => await DiscardAsync(),
        };

        async ValueTask<decimal?> DiscardAsync()
        {
            await reader.DiscardAsync(binaryLength, cancellationToken);
            return null;
        }
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) => elementDataTypeCode is 
        ((uint)DataType.Numeric) or
        ((uint)DataType.Float4) or
        ((uint)DataType.Float8) or
        ((uint)DataType.Int2) or
        ((uint)DataType.Int4) or
        ((uint)DataType.Int8);

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) => arrayDataTypeCode is 
        ((uint)DataType.NumericArray) or
        ((uint)DataType.Float4Array) or
        ((uint)DataType.Float8Array) or
        ((uint)DataType.Int2Array) or
        ((uint)DataType.Int4Array) or
        ((uint)DataType.Int8Array);

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) => arrayDataTypeCode switch
    {
        (uint)DataType.NumericArray => (uint)DataType.Numeric,
        (uint)DataType.Float4Array => (uint)DataType.Float4,
        (uint)DataType.Float8Array => (uint)DataType.Float8,
        (uint)DataType.Int2Array => (uint)DataType.Int2,
        (uint)DataType.Int4Array => (uint)DataType.Int4,
        (uint)DataType.Int8Array => (uint)DataType.Int8,
        _ => 0
    };

    /// <summary>
    /// Defines the binary representation of <see langword="decimal"/> in PostgreSQL.
    /// </summary>
    /// <remarks>
    /// <see href="https://docs.microsoft.com/en-us/dotnet/api/system.decimal.-ctor?view=net-7.0#system-decimal-ctor(system-int32())"/>.
    /// <see href="https://github.com/npgsql/npgsql/blob/main/src/Npgsql/Internal/TypeHandlers/NumericHandlers/DecimalRaw.cs"/>.
    /// <see href="https://github.com/npgsql/npgsql/blob/main/src/Npgsql/Internal/TypeHandlers/NumericHandlers/NumericHandler.cs"/>.
    /// PostgreSQL License:
    /// <see href="https://github.com/npgsql/npgsql/blob/main/LICENSE"/>.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    private struct DecimalBinary
    {
        private const int SignPositive = 0x0000;
        private const int SignNegative = 0x4000;
        private const int SignMask = unchecked((int)0x80000000);
        private const int ScaleMask = 0x00FF0000;
        private const int ScaleShift = 16;
        private const int SignSpecialMask = 0xC000;
        private const int MaxScale = 28;

        private const int MaxGroupCount = 8;
        private const int MaxGroupScale = 4;

        /// <summary>
        /// Fast access for 10^n where n is 0-9.
        /// </summary>
        private static readonly uint[] Powers10 = new uint[]
        {
            1,
            10,
            100,
            1000,
            10000,
            100000,
            1000000,
            10000000,
            100000000,
            1000000000
        };

        /// <summary>
        /// The maximum power of 10 that a 32 bit unsigned integer can store.
        /// </summary>
        private static readonly int MaxUInt32Scale = Powers10.Length - 1;

        /// <summary>
        /// The maximum group of a <see langword="decimal"/>'s binary format in PostgreSQL.
        /// </summary>
        private static readonly uint MaxGroupSize = Powers10[MaxGroupScale];

        /// <summary>
        /// The original value.
        /// </summary>
        [FieldOffset(0)]
        private readonly decimal _value;

        /// <summary>
        /// Sign, scale factor, etc.
        /// </summary>
        /// <remarks>
        /// <para>
        /// bits [3] contains the scale factor and sign, and consists of following parts:
        /// </para>
        /// <para>
        /// Bits 0 to 15, the lower word, are unused and must be zero.
        /// </para>
        /// <para>
        /// Bits 16 to 23 must contain an exponent between 0 and 28, 
        /// which indicates the power of 10 to divide the integer number.
        /// </para>
        /// <para>
        /// Bits 24 to 30 are unused and must be zero.
        /// </para>
        /// <para>
        /// Bit 31 contains the sign; 0 meaning positive, and 1 meaning negative.
        /// </para>
        /// </remarks>
        [FieldOffset(0)]
        private int _flags;

        /// <summary>
        /// The high 32 bits of the 96-bit integer number.
        /// </summary>
        [FieldOffset(4)]
        private uint _high;

        /// <summary>
        /// The low 32 bits of the 96-bit integer number.
        /// </summary>
        [FieldOffset(8)]
        private uint _low;

        /// <summary>
        /// The middle 32 bits of the 96-bit integer number.
        /// </summary>
        [FieldOffset(12)]
        private uint _mid;

        /// <summary>
        /// Gets a value that indicates whether the current number is a negative number.
        /// </summary>
        private bool Negative => (_flags & SignMask) != 0;

        /// <summary>
        /// Gets or sets the number of digits after the decimal point.
        /// </summary>
        private int Scale
        {
            get => (_flags & ScaleMask) >> ScaleShift;
            set => _flags = (_flags & SignMask) | ((value << ScaleShift) & ScaleMask);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalBinary"/> struct with the specified decimal value.
        /// </summary>
        /// <param name="value">The decimal numeric value.</param>
        internal DecimalBinary(decimal value) : this() => _value = value;

        /// <summary>
        /// Writes this binary value to the specified output stream.
        /// </summary>
        /// <param name="writer">The output stream to the database service.</param>
        /// <param name="groupCount">The number of digit groups.</param>
        internal void Write(Writer writer, int groupCount)
        {
            var weight = 0;

            Span<short> groups = groupCount > 0 ? stackalloc short[groupCount] : Array.Empty<short>();
            groups.Clear();
            groupCount = 0;

            if (_low != 0 || _mid != 0 || _high != 0)
            {
                var scale = Scale;
                weight = (-scale / MaxGroupScale) - 1;

                uint remainder;
                var scaleChunk = scale % MaxGroupScale;
                if (scaleChunk > 0)
                {
                    var divisor = Powers10[scaleChunk];
                    var multiplier = Powers10[MaxGroupScale - scaleChunk];
                    remainder = Divide(ref this, divisor) * multiplier;

                    if (remainder != 0)
                    {
                        weight--;
                        goto WriteGroups;
                    }
                }

                while ((remainder = Divide(ref this, MaxGroupSize)) == 0)
                {
                    weight++;
                }

            WriteGroups:
                groups[groupCount++] = (short)remainder;

                while (_low != 0 || _mid != 0 || _high != 0)
                {
                    groups[groupCount++] = (short)Divide(ref this, MaxGroupSize);
                }
            }

            writer.WriteInt16((short)groupCount);
            writer.WriteInt16((short)(groupCount + weight));
            writer.WriteInt16((short)(Negative ? SignNegative : SignPositive));
            writer.WriteInt16((short)Scale);

            while (groupCount > 0)
            {
                writer.WriteInt16(groups[--groupCount]);
            }
        }

        /// <summary>
        /// Reads a <see langword="decimal"/> value from the specified connection input stream.
        /// </summary>
        /// <param name="length">The binary length of the <see langword="DecimalBinary"/> value.</param>
        /// <param name="reader">The input stream to the database service.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
        /// <returns>
        /// The read <see langword="decimal"/> value, or <see langword="null"/> if the conversion is not possible.
        /// </returns>
        internal static async ValueTask<decimal?> ReadAsync(
            int length,
            Reader reader,
            CancellationToken cancellationToken)
        {
            const int MinBinaryLength = sizeof(short) * 4;

            if (length < MinBinaryLength)
            {
                await reader.DiscardAsync(length, cancellationToken);
                return null;
            }

            await reader.EnsureAsync(MinBinaryLength, cancellationToken);
            length -= MinBinaryLength;

            var binary = new DecimalBinary();
            var groups = reader.ReadInt16();
            var weight = reader.ReadInt16() - groups + 1;
            var sign = reader.ReadUInt16();
            var scale = reader.ReadInt16();

            if ((sign & SignSpecialMask) == SignSpecialMask)
            {
                // NaN, Infinity and -Infinity are not supported.
                await reader.DiscardAsync(length, cancellationToken);
                return null;
            }

            if (sign == SignNegative)
            {
                binary._flags ^= SignMask;
            }

            if (scale < 0 is var exponential && exponential)
            {
                scale = (short)-scale;
            }
            else
            {
                binary.Scale = scale;
            }

            if (scale > MaxScale)
            {
                await reader.DiscardAsync(length, cancellationToken);
                return null;
            }

            var scaleDifference = exponential
                ? weight * MaxGroupScale
                : (weight * MaxGroupScale) + scale;

            if (groups > MaxGroupCount)
            {
                await reader.DiscardAsync(length, cancellationToken);
                return binary.Negative ? decimal.MinValue : decimal.MaxValue;
            }

            Debug.Assert(length == groups * sizeof(ushort));
            await reader.EnsureAsync(length, cancellationToken);

            if (groups == MaxGroupCount)
            {
                while (groups-- > 1)
                {
                    Multiply(ref binary, MaxGroupSize);
                    Add(ref binary, reader.ReadUInt16());
                }

                var group = reader.ReadUInt16();
                var groupSize = Powers10[-scaleDifference];
                if (group % groupSize != 0)
                {
                    return null;
                }

                Multiply(ref binary, MaxGroupSize / groupSize);
                Add(ref binary, group / groupSize);
            }
            else
            {
                while (groups-- > 0)
                {
                    Multiply(ref binary, MaxGroupSize);
                    Add(ref binary, reader.ReadUInt16());
                }

                if (scaleDifference < 0)
                {
                    _ = Divide(ref binary, Powers10[-scaleDifference]);
                }
                else
                {
                    while (scaleDifference > 0)
                    {
                        var scaleChunk = Math.Min(MaxUInt32Scale, scaleDifference);
                        Multiply(ref binary, Powers10[scaleChunk]);
                        scaleDifference -= scaleChunk;
                    }
                }
            }

            return binary._value;
        }

        /// <summary>
        /// Adds a <see cref="DecimalBinary"/> and a <see langword="uint"/>.
        /// </summary>
        /// <param name="binary">The <see cref="DecimalBinary"/> value to add.</param>
        /// <param name="addend">The addend.</param>
        private static void Add(ref DecimalBinary binary, uint addend)
        {
            uint integer;
            uint sum;

            integer = binary._low;
            binary._low = sum = integer + addend;

            if (sum >= integer && sum >= addend)
            {
                return;
            }

            integer = binary._mid;
            binary._mid = sum = integer + 1;

            if (sum >= integer && sum >= 1)
            {
                return;
            }

            integer = binary._high;
            binary._high = sum = integer + 1;

            if (sum < integer || sum < 1)
            {
                binary = new DecimalBinary(binary.Negative ? decimal.MinValue : decimal.MaxValue);
            }
        }

        /// <summary>
        /// Multiplies a <see cref="DecimalBinary"/> and a <see langword="uint"/>.
        /// </summary>
        /// <param name="binary">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        private static void Multiply(ref DecimalBinary binary, uint multiplier)
        {
            ulong integer;
            uint remainder;

            integer = (ulong)binary._low * multiplier;
            binary._low = (uint)integer;
            remainder = (uint)(integer >> 32);

            integer = ((ulong)binary._mid * multiplier) + remainder;
            binary._mid = (uint)integer;
            remainder = (uint)(integer >> 32);

            integer = ((ulong)binary._high * multiplier) + remainder;
            binary._high = (uint)integer;
            remainder = (uint)(integer >> 32);

            if (remainder != 0)
            {
                binary = new DecimalBinary(binary.Negative ? decimal.MinValue : decimal.MaxValue);
            }
        }

        /// <summary>
        /// Divides a <see cref="DecimalBinary"/> and a <see langword="uint"/>.
        /// </summary>
        /// <param name="binary">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The remainder after dividing.</returns>
        private static uint Divide(ref DecimalBinary binary, uint divisor)
        {
            ulong integer;
            uint remainder = 0;

            if (binary._high != 0)
            {
                integer = binary._high;
                binary._high = (uint)(integer / divisor);
                remainder = (uint)(integer % divisor);
            }

            if (binary._mid != 0 || remainder != 0)
            {
                integer = ((ulong)remainder << 32) | binary._mid;
                binary._mid = (uint)(integer / divisor);
                remainder = (uint)(integer % divisor);
            }

            if (binary._low != 0 || remainder != 0)
            {
                integer = ((ulong)remainder << 32) | binary._low;
                binary._low = (uint)(integer / divisor);
                remainder = (uint)(integer % divisor);
            }

            return remainder;
        }

        /// <summary>
        /// Gets the binary length of the specified decimal value.
        /// </summary>
        /// <param name="value">The decimal value.</param>
        /// <returns>The binary length of the <paramref name="value"/>.</returns>
        internal static int BinaryLengthOf(decimal value)
        {
            if (value == 0m)
            {
                return 8;
            }

            var groupCount = 0;
            var binary = new DecimalBinary(value);
            if (binary._low != 0 || binary._mid != 0 || binary._high != 0)
            {
                uint remainder = default;
                var scaleChunk = binary.Scale % MaxGroupScale;
                if (scaleChunk > 0)
                {
                    var divisor = Powers10[scaleChunk];
                    var multiplier = Powers10[MaxGroupScale - scaleChunk];
                    remainder = Divide(ref binary, divisor) * multiplier;
                }

                while (remainder == 0)
                {
                    remainder = Divide(ref binary, MaxGroupSize);
                }

                groupCount++;

                while (binary._low != 0 || binary._mid != 0 || binary._high != 0)
                {
                    _ = Divide(ref binary, MaxGroupSize);
                    groupCount++;
                }
            }

            return (4 + groupCount) << 1 /* * sizeof(short) */;
        }
    }
}
