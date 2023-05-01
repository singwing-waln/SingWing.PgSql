using SingWing.PgSql.Protocol.Messages.Backend;
using System.Buffers;
using System.Globalization;

namespace SingWing.PgSql;

/// <summary>
/// Provides extension methods for writing UTF-8 encoded JSON text to JSON writers.
/// </summary>
internal static class JsonExtensions
{
    /// <summary>
    /// Writes camel-case JSON property name to the specified writer.
    /// </summary>
    /// <param name="writer">The writer to which the property name will be written.</param>
    /// <param name="propertyName">The property name.</param>
    /// <remarks>If the property name is an empty string, do nothing.</remarks>
    internal static void WriteCamelPropertyName(
        this Utf8JsonWriter writer,
        in ReadOnlySpan<char> propertyName)
    {
        const int StackThreshold = ColumnDescription.MaxNameLength;
        const int PoolThreshold = 1024;

        if (propertyName.IsEmpty)
        {
            return;
        }

        var c = propertyName[0];

        if (c is < 'A' or > 'Z')
        {
            writer.WritePropertyName(propertyName);
            return;
        }

        if (propertyName.Length <= StackThreshold)
        {
            Span<char> camelBuffer = stackalloc char[propertyName.Length];
            WriteCamelPropertyName(writer, propertyName, camelBuffer);
            return;
        }

        if (propertyName.Length <= PoolThreshold)
        {
            var camelBuffer = ArrayPool<char>.Shared.Rent(propertyName.Length);

            try
            {
                WriteCamelPropertyName(writer, propertyName, camelBuffer);
            }
            finally
            {
                ArrayPool<char>.Shared.Return(camelBuffer);
            }
        }
        else
        {
            WriteCamelPropertyName(
                writer,
                propertyName,
                GC.AllocateUninitializedArray<char>(propertyName.Length));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void WriteCamelPropertyName(
            Utf8JsonWriter writer,
            in ReadOnlySpan<char> propertyName,
            in Span<char> camelBuffer)
        {
            propertyName.CopyTo(camelBuffer);
            camelBuffer[0] = (char)(propertyName[0] + 'a' - 'A');
            writer.WritePropertyName(camelBuffer[..propertyName.Length]);
        }
    }

    /// <summary>
    /// Writes the specified DateOnly as a string (format "yyyy-MM-dd") to the JSON output stream.
    /// </summary>
    /// <param name="writer">The JSON output stream.</param>
    /// <param name="value">The DateOnly to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void WriteStringValue(
        this Utf8JsonWriter writer,
        DateOnly value)
    {
        const string Format = "yyyy-MM-dd";

        Span<char> text = stackalloc char[16];
        _ = value.TryFormat(text, out var charCount, Format, CultureInfo.InvariantCulture);
        writer.WriteStringValue(text[..charCount]);
    }

    /// <summary>
    /// Writes the specified TimeOnly as a string (format "HH:mm:ss.FFFFFFF") to the JSON output stream.
    /// </summary>
    /// <param name="writer">The JSON output stream.</param>
    /// <param name="value">The TimeOnly to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void WriteStringValue(
        this Utf8JsonWriter writer,
        TimeOnly value)
    {
        const string Format = "HH:mm:ss.FFFFFFF";

        Span<char> text = stackalloc char[24];
        _ = value.TryFormat(text, out var charCount, Format, CultureInfo.InvariantCulture);
        writer.WriteStringValue(text[..charCount]);
    }

    /// <summary>
    /// Writes the specified TimeSpan as a string (format "c") to the JSON output stream.
    /// </summary>
    /// <param name="writer">The JSON output stream.</param>
    /// <param name="value">The TimeSpan to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void WriteStringValue(
        this Utf8JsonWriter writer,
        TimeSpan value)
    {
        const string Format = "c";

        Span<char> text = stackalloc char[40];
        _ = value.TryFormat(text, out var charCount, Format, CultureInfo.InvariantCulture);
        writer.WriteStringValue(text[..charCount]);
    }

    /// <summary>
    /// Writes the specified DateTime(Local) as a ISO-8601 string 
    /// (format "yyyy-MM-ddTHH:mm:ss.fffffffzzz") to the JSON output stream.
    /// </summary>
    /// <param name="writer">The JSON output stream.</param>
    /// <param name="value">The DateTime to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void WriteIso8601Value(
        this Utf8JsonWriter writer,
        DateTime value)
    {
        const string Format = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";

        Span<char> text = stackalloc char[40];
        _ = value.ToLocalTime().TryFormat(
            text, out var charCount, Format, CultureInfo.InvariantCulture);
        writer.WriteStringValue(text[..charCount]);
    }

    /// <summary>
    /// Writes the specified <see langword="double"/> value as a number 
    /// or string ("NaN", "-Infinity", "Infinity") to the JSON output stream.
    /// </summary>
    /// <param name="writer">The JSON output stream.</param>
    /// <param name="value">The <see langword="double"/> value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void WriteFloatValue(
        this Utf8JsonWriter writer,
        double value)
    {
        if (double.IsNaN(value))
        {
            writer.WriteStringValue("NaN");
        }
        else if (double.IsNegativeInfinity(value))
        {
            writer.WriteStringValue("-Infinity");
        }
        else if (double.IsPositiveInfinity(value))
        {
            writer.WriteStringValue("Infinity");
        }
        else
        {
            writer.WriteNumberValue(value);
        }
    }

    /// <summary>
    /// Writes the specified <see langword="float"/> value as a number 
    /// or string ("NaN", "-Infinity", "Infinity") to the JSON output stream.
    /// </summary>
    /// <param name="writer">The JSON output stream.</param>
    /// <param name="value">The <see langword="float"/> value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void WriteFloatValue(
        this Utf8JsonWriter writer,
        float value)
    {
        if (float.IsNaN(value))
        {
            writer.WriteStringValue("NaN");
        }
        else if (float.IsNegativeInfinity(value))
        {
            writer.WriteStringValue("-Infinity");
        }
        else if (float.IsPositiveInfinity(value))
        {
            writer.WriteStringValue("Infinity");
        }
        else
        {
            writer.WriteNumberValue(value);
        }
    }
}
