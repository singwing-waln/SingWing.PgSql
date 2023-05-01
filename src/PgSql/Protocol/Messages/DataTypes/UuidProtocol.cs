using SingWing.PgSql.Protocol.Connections;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;
using System.Runtime.InteropServices;

namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// Provides the data type, binary length, and method for writing elements to 
/// output streams for <see cref="DataType.UuidArray"/> parameters.
/// </summary>
internal sealed class UuidProtocol : IDataTypeProtocol<Guid, UuidProtocol>
{
    /// <summary>
    /// The parameter whose value is Guid.Empty.
    /// </summary>
    private static readonly PrimitiveParameter<Guid, UuidProtocol> Empty = new(Guid.Empty);

    /// <summary>
    /// Prevents the creation of new instances of the <see cref="UuidProtocol"/> class from outside.
    /// </summary>
    private UuidProtocol() { }

    /// <inheritdoc />
    public static DataType ArrayDataType => DataType.UuidArray;

    /// <inheritdoc />
    public static DataType ElementDataType => DataType.Uuid;

    /// <inheritdoc />
    public static bool IsElementBinaryLengthFixed => true;

    /// <inheritdoc />
    public static int ElementBinaryLengthOf(in Guid element) => GuidBinary.BinaryLength;

    /// <inheritdoc />
    public static async ValueTask WriteElementAsync(
        Guid element,
        Writer writer,
        CancellationToken cancellationToken)
    {
        await writer.EnsureSpaceAsync(GuidBinary.BinaryLength, cancellationToken);
        new GuidBinary(element).Write(writer);
    }

    /// <inheritdoc />
    public static Guid? ToElement(object? value) => Conversion.ToGuid(value);

    /// <inheritdoc />
    public static bool IsPredefinedParameter(PrimitiveParameter<Guid, UuidProtocol> parameter) =>
        ReferenceEquals(parameter, Empty);

    /// <inheritdoc />
    public static PrimitiveParameter<Guid, UuidProtocol>? PredefinedParameterFor(in Guid value) =>
        value == Guid.Empty ? Empty : null;

    /// <inheritdoc />
    public static async ValueTask<Guid?> ReadElementAsync(
        uint elementDataTypeCode,
        int binaryLength,
        Reader reader,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        if (elementDataTypeCode != (uint)DataType.Uuid)
        {
            await reader.DiscardAsync(binaryLength, cancellationToken);
            return null;
        }

        await reader.EnsureAsync(GuidBinary.BinaryLength, cancellationToken);
        return GuidBinary.Read(reader);
    }

    /// <inheritdoc />
    public static bool CanConvertElementFrom(uint elementDataTypeCode) =>
        elementDataTypeCode == (uint)DataType.Uuid;

    /// <inheritdoc />
    public static bool CanConvertArrayFrom(uint arrayDataTypeCode) =>
        arrayDataTypeCode == (uint)DataType.UuidArray;

    /// <inheritdoc />
    public static uint ElementTypeOfArrayType(uint arrayDataTypeCode) =>
        arrayDataTypeCode == (uint)DataType.UuidArray
        ? (uint)DataType.Uuid
        : 0;

    /// <summary>
    /// Defines the binary representation of Guid in PostgreSQL.
    /// </summary>
    /// <remarks>
    /// <para>
    /// .NET uses the following format
    /// (<see href="https://docs.microsoft.com/en-us/windows/win32/api/guiddef/ns-guiddef-guid"/>):
    /// </para>
    /// <code>
    /// typedef struct _GUID 
    /// {
    ///   unsigned long             Data1;
    ///   unsigned short            Data2;
    ///   unsigned short            Data3;
    ///   unsigned char             Data4[8];
    /// } GUID;
    /// </code>
    /// 
    /// <para>
    /// PostgreSQL uses the RFC 4122 
    /// (<see href="https://datatracker.ietf.org/doc/html/rfc4122#section-4.1.2"/>) format:
    /// </para>
    /// <code>
    /// {
    ///   time_low                  uint32
    ///   time_mid                  uint16
    ///   time_hi_and_version       uint16
    ///   clock_seq_hi_and_reserved uint8
    ///   clock_seq_low             uint8
    ///   node                      uint8[6]
    /// }
    /// </code>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    private readonly struct GuidBinary
    {
        /// <summary>
        /// The binary length of Guid.
        /// </summary>
        internal const int BinaryLength = 16;

        /// <summary>
        /// The native value represented by .NET.
        /// </summary>
        [FieldOffset(00)]
        private readonly Guid Value;

        /// <summary>
        /// The first part represented by a 32-bit integer.
        /// </summary>
        [FieldOffset(00)]
        private readonly int Data1;

        /// <summary>
        /// The second part represented by a 16-bit integer.
        /// </summary>
        [FieldOffset(04)]
        private readonly short Data2;

        /// <summary>
        /// The third part represented by a 16-bit integer.
        /// </summary>
        [FieldOffset(06)]
        private readonly short Data3;

        /// <summary>
        /// The fourth part represented by a 64-bit integer.
        /// </summary>
        /// <remarks>
        /// This part of the GUID on Windows is char[8], so it is not a strict long integer.
        /// </remarks>
        [FieldOffset(08)]
        private readonly long Data4;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidBinary"/> struct
        /// with the specified Guid value.
        /// </summary>
        /// <param name="value">A Guid value.</param>
        internal GuidBinary(Guid value) : this() => Value = value;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuidBinary"/> struct
        /// with the specified parts.
        /// </summary>
        /// <param name="data1">The first part.</param>
        /// <param name="data2">The second part.</param>
        /// <param name="data3">The third part.</param>
        /// <param name="data4">The fourth part.</param>
        private GuidBinary(int data1, short data2, short data3, long data4) : this() =>
            (Data1, Data2, Data3, Data4) = (data1, data2, data3, data4);

        /// <summary>
        /// Writes this binary value to the specified output stream.
        /// </summary>
        /// <param name="writer">The output stream to the database service.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Write(Writer writer)
        {
            writer.WriteInt32(Data1);
            writer.WriteInt16(Data2);
            writer.WriteInt16(Data3);
            writer.WriteRawInt64(Data4);
        }

        /// <summary>
        /// Reads a Guid value from the specified connection input stream.
        /// </summary>
        /// <param name="reader">The input stream to the database service.</param>
        /// <returns>The read Guid value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Guid Read(Reader reader)
        {
            var data1 = reader.ReadInt32();
            var data2 = reader.ReadInt16();
            var data3 = reader.ReadInt16();
            var data4 = reader.ReadRawInt64();

            return new GuidBinary(data1, data2, data3, data4).Value;
        }
    }
}
