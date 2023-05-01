using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;
using System.Globalization;

namespace SingWing.PgSql;

/// <summary>
/// Provides extension methods for converting JSON string or binary to <see cref="Parameter"/> whose type is jsonb.
/// </summary>
public static partial class ParameterExtensions
{
    /// <summary>
    /// Converts the specified JSON text represented by a nullable <see langword="string"/> 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON text to convert.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>This method skips structural JSON validation and allows the user to write invalid JSON.</remarks>
    public static Parameter ToJsonParameter(this string? value) =>
        CharMemoryJsonParameter.From(value is null ? (ReadOnlyMemory<char>?)null : value.AsMemory());

    /// <summary>
    /// Converts the specified JSON text represented by a nullable <see langword="char"/>[] 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON text to convert.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>This method skips structural JSON validation and allows the user to write invalid JSON.</remarks>
    public static Parameter ToJsonParameter(this char[]? value) =>
        CharMemoryJsonParameter.From(value is null ? (ReadOnlyMemory<char>?)null : value.AsMemory());

    /// <summary>
    /// Converts the specified JSON text represented by a nullable ReadOnlyMemory&lt;<see langword="char"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON text to convert.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>This method skips structural JSON validation and allows the user to write invalid JSON.</remarks>
    public static Parameter ToJsonParameter(this ReadOnlyMemory<char>? value) =>
        CharMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON text represented by a ReadOnlyMemory&lt;<see langword="char"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON text to convert.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>This method skips structural JSON validation and allows the user to write invalid JSON.</remarks>
    public static Parameter ToJsonParameter(this ReadOnlyMemory<char> value) =>
        CharMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON text represented by a nullable Memory&lt;<see langword="char"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON text to convert.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>This method skips structural JSON validation and allows the user to write invalid JSON.</remarks>
    public static Parameter ToJsonParameter(this Memory<char>? value) =>
        CharMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON text represented by a Memory&lt;<see langword="char"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON text to convert.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>This method skips structural JSON validation and allows the user to write invalid JSON.</remarks>
    public static Parameter ToJsonParameter(this Memory<char> value) =>
        CharMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON text represented by a nullable ArraySegment&lt;<see langword="char"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON text to convert.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>This method skips structural JSON validation and allows the user to write invalid JSON.</remarks>
    public static Parameter ToJsonParameter(this ArraySegment<char>? value) =>
        CharMemoryJsonParameter.From(value is null ? (ReadOnlyMemory<char>?)null : value.Value.AsMemory());

    /// <summary>
    /// Converts the specified JSON text represented by an ArraySegment&lt;<see langword="char"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON text to convert.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>This method skips structural JSON validation and allows the user to write invalid JSON.</remarks>
    public static Parameter ToJsonParameter(this ArraySegment<char> value) =>
        CharMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON binary data represented by a nullable <see langword="byte"/>[] 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON binary data to convert. The binary data must be UTF-8 encoded.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>
    /// The binary data must be UTF-8 encoded.
    /// This method skips structural JSON validation and allows the user to write invalid JSON.
    /// </remarks>
    public static Parameter ToJsonParameter(this byte[]? value) =>
        ByteMemoryJsonParameter.From(value is null ? (ReadOnlyMemory<byte>?)null : value.AsMemory());

    /// <summary>
    /// Converts the specified JSON binary data represented by a nullable ReadOnlyMemory&lt;<see langword="byte"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON binary data to convert. The binary data must be UTF-8 encoded.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>
    /// The binary data must be UTF-8 encoded.
    /// This method skips structural JSON validation and allows the user to write invalid JSON.
    /// </remarks>
    public static Parameter ToJsonParameter(this ReadOnlyMemory<byte>? value) =>
        ByteMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON binary data represented by a ReadOnlyMemory&lt;<see langword="byte"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON binary data to convert. The binary data must be UTF-8 encoded.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>
    /// The binary data must be UTF-8 encoded.
    /// This method skips structural JSON validation and allows the user to write invalid JSON.
    /// </remarks>
    public static Parameter ToJsonParameter(this ReadOnlyMemory<byte> value) =>
        ByteMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON binary data represented by a nullable Memory&lt;<see langword="byte"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON binary data to convert. The binary data must be UTF-8 encoded.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>
    /// The binary data must be UTF-8 encoded.
    /// This method skips structural JSON validation and allows the user to write invalid JSON.
    /// </remarks>
    public static Parameter ToJsonParameter(this Memory<byte>? value) =>
        ByteMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON binary data represented by a Memory&lt;<see langword="byte"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON binary data to convert. The binary data must be UTF-8 encoded.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>
    /// The binary data must be UTF-8 encoded.
    /// This method skips structural JSON validation and allows the user to write invalid JSON.
    /// </remarks>
    public static Parameter ToJsonParameter(this Memory<byte> value) =>
        ByteMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON binary data represented by a nullable ArraySegment&lt;<see langword="byte"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON binary data to convert. The binary data must be UTF-8 encoded.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>
    /// The binary data must be UTF-8 encoded.
    /// This method skips structural JSON validation and allows the user to write invalid JSON.
    /// </remarks>
    public static Parameter ToJsonParameter(this ArraySegment<byte>? value) =>
        ByteMemoryJsonParameter.From(value is null ? (ReadOnlyMemory<byte>?)null : value.Value.AsMemory());

    /// <summary>
    /// Converts the specified JSON binary data represented by a ArraySegment&lt;<see langword="byte"/>&gt; 
    /// to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JSON binary data to convert. The binary data must be UTF-8 encoded.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <remarks>
    /// The binary data must be UTF-8 encoded.
    /// This method skips structural JSON validation and allows the user to write invalid JSON.
    /// </remarks>
    public static Parameter ToJsonParameter(this ArraySegment<byte> value) =>
        ByteMemoryJsonParameter.From(value);

    /// <summary>
    /// Converts the specified JSON stream to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="stream">The JSON stream to convert. The stream data must be UTF-8 encoded.</param>
    /// <param name="length">The length of JSON data in the stream.</param>
    /// <param name="ownsStream">Indicates whether the underlying stream should be closed after the parameter value are sent to PostgreSQL.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The length of the data exceeds the maximum length supported by PostgreSQL.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The binary data must be UTF-8 encoded.
    /// </para>
    /// <para>
    /// If <paramref name="length"/> is less than 0, the length of the stream is used. 
    /// If the length of the stream is unknown, a NotSupportedException is thrown.
    /// </para>
    /// </remarks>
    public static Parameter ToJsonParameter(this Stream? stream, int length = -1, bool ownsStream = false)
    {
        // The maximum length of stream: 1GiB.
        const int MaxLength = 1024 * 1024 * 1024;

        if (length == 0 || stream is null || ReferenceEquals(stream, Stream.Null))
        {
            return StreamJsonParameter.Null;
        }

        length = length < 0 ? (int)stream.Length : length;

        return length > MaxLength
            ? throw new ArgumentOutOfRangeException(
                nameof(stream),
                string.Format(CultureInfo.CurrentCulture, Strings.StreamLengthExceedsLimit, MaxLength))
            : length == 0
            ? StreamJsonParameter.Null
            : (Parameter)StreamJsonParameter.From(stream, length, ownsStream: ownsStream);
    }

    /// <summary>
    /// Converts the specified JSON stream to a <see cref="Parameter"/> whose type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="stream">The JSON stream to convert. The stream data must be UTF-8 encoded.</param>
    /// <param name="ownsStream">Indicates whether the underlying stream should be closed after the parameter value are sent to PostgreSQL.</param>
    /// <returns>The converted <see cref="Parameter"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The length of the data exceeds the maximum length supported by PostgreSQL.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The binary data must be UTF-8 encoded.
    /// </para>
    /// </remarks>
    public static Parameter ToJsonParameter(this Stream? stream, bool ownsStream) => ToJsonParameter(stream, -1, ownsStream);
}
