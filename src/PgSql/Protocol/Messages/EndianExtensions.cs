using System.Buffers.Binary;

namespace SingWing.PgSql.Protocol.Messages;

/// <summary>
/// Provides methods to translate numbers back and forth between the host byte order (Big-Endian or Little-Endian) and network byte order (Big-Endian).
/// </summary>
internal static class EndianExtensions
{
    /// <summary>
    /// Translates a <see langword="short"/> from host byte order (Big-Endian or Little-Endian) to network byte order (Big-Endian).
    /// </summary>
    /// <param name="value">The <see langword="short"/> value to translate, in host byte order (Big-Endian or Little-Endian).</param>
    /// <returns>The translated <see langword="short"/> value in network byte order (Big-Endian).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static short ToNetworkOrder(this short value) => BitConverter.IsLittleEndian
        ? BinaryPrimitives.ReverseEndianness(value)
        : value;

    /// <summary>
    /// Translates a <see langword="int"/> from host byte order (Big-Endian or Little-Endian) to network byte order (Big-Endian).
    /// </summary>
    /// <param name="value">The <see langword="int"/> value to translate, in host byte order (Big-Endian or Little-Endian).</param>
    /// <returns>The translated <see langword="int"/> value in network byte order (Big-Endian).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int ToNetworkOrder(this int value) => BitConverter.IsLittleEndian
        ? BinaryPrimitives.ReverseEndianness(value)
        : value;

    /// <summary>
    /// Translates a <see langword="long"/> from host byte order (Big-Endian or Little-Endian) to network byte order (Big-Endian).
    /// </summary>
    /// <param name="value">The <see langword="long"/> value to translate, in host byte order (Big-Endian or Little-Endian).</param>
    /// <returns>The translated <see langword="long"/> value in network byte order (Big-Endian).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static long ToNetworkOrder(this long value) => BitConverter.IsLittleEndian
        ? BinaryPrimitives.ReverseEndianness(value)
        : value;

    /// <summary>
    /// Translates a <see langword="short"/> from network byte order (Big-Endian) to host byte order (Big-Endian or Little-Endian).
    /// </summary>
    /// <param name="value">The <see langword="short"/> value to translate, in network byte order (Big-Endian).</param>
    /// <returns>The translated <see langword="short"/> value in host byte order (Big-Endian or Little-Endian).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static short ToHostOrder(this short value) => BitConverter.IsLittleEndian
        ? BinaryPrimitives.ReverseEndianness(value)
        : value;

    /// <summary>
    /// Translates a <see langword="ushort"/> from network byte order (Big-Endian) to host byte order (Big-Endian or Little-Endian).
    /// </summary>
    /// <param name="value">The <see langword="ushort"/> value to translate, in network byte order (Big-Endian).</param>
    /// <returns>The translated <see langword="ushort"/> value in host byte order (Big-Endian or Little-Endian).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ushort ToHostOrder(this ushort value) => BitConverter.IsLittleEndian
        ? BinaryPrimitives.ReverseEndianness(value)
        : value;

    /// <summary>
    /// Translates a <see langword="int"/> from network byte order (Big-Endian) to host byte order (Big-Endian or Little-Endian).
    /// </summary>
    /// <param name="value">The <see langword="int"/> value to translate, in network byte order (Big-Endian).</param>
    /// <returns>The translated <see langword="int"/> value in host byte order (Big-Endian or Little-Endian).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int ToHostOrder(this int value) => BitConverter.IsLittleEndian
        ? BinaryPrimitives.ReverseEndianness(value)
        : value;

    /// <summary>
    /// Translates a <see langword="uint"/> from network byte order (Big-Endian) to host byte order (Big-Endian or Little-Endian).
    /// </summary>
    /// <param name="value">The <see langword="uint"/> value to translate, in network byte order (Big-Endian).</param>
    /// <returns>The translated <see langword="uint"/> value in host byte order (Big-Endian or Little-Endian).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static uint ToHostOrder(this uint value) => BitConverter.IsLittleEndian
        ? BinaryPrimitives.ReverseEndianness(value)
        : value;

    /// <summary>
    /// Translates a <see langword="long"/> from network byte order (Big-Endian) to host byte order (Big-Endian or Little-Endian).
    /// </summary>
    /// <param name="value">The <see langword="long"/> value to translate, in network byte order (Big-Endian).</param>
    /// <returns>The translated <see langword="long"/> value in host byte order (Big-Endian or Little-Endian).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static long ToHostOrder(this long value) => BitConverter.IsLittleEndian
        ? BinaryPrimitives.ReverseEndianness(value)
        : value;

    /// <summary>
    /// Translates a <see langword="ulong"/> from network byte order (Big-Endian) to host byte order (Big-Endian or Little-Endian).
    /// </summary>
    /// <param name="value">The <see langword="ulong"/> value to translate, in network byte order (Big-Endian).</param>
    /// <returns>The translated <see langword="ulong"/> value in host byte order (Big-Endian or Little-Endian).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ulong ToHostOrder(this ulong value) => BitConverter.IsLittleEndian
        ? BinaryPrimitives.ReverseEndianness(value)
        : value;
}
