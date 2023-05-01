namespace SingWing.PgSql;

/// <summary>
/// Provides extension methods to <see cref="IRow"/>.
/// </summary>
public static class RowExtensions
{
    /// <summary>
    /// Writes the row to the specified writer as a JSON object.
    /// The column names are used as JSON property names.
    /// </summary>
    /// <param name="row">The <see cref="IRow"/> to be written as a JSON object.</param>
    /// <param name="writer">The writer to which row will be written.</param>
    /// <param name="propertyName">The property name of the JSON object.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <remarks>
    /// <para>
    /// If the column names are quoted by double-quotes (""), 
    /// the camel-case names are used as property names.
    /// </para>
    /// <para>
    /// If the column names are not quoted by double-quotes (""), 
    /// then the property names follow the rules of the database, 
    /// usually all letters in the names are lowercase.
    /// </para>
    /// <para>
    /// If a column does not have a name, then the property name follows the rules of the database, 
    /// usually the name may be "?column?". 
    /// If more than one columns do not have names, the output JSON object will have multiple properties with the same name.
    /// </para>
    /// <para>
    /// The NaN, Infinity, and -Infinity of <see langword="float"/> and <see langword="double"/> are written as 
    /// the strings "NaN", "Infinity", and "-Infinity", respectively.
    /// </para>
    /// <para>
    /// This method does not call <paramref name="writer"/>.FlushAsync.
    /// </para>
    /// </remarks>
    public static ValueTask WriteAsync(
        this IRow row,
        Utf8JsonWriter writer,
        ReadOnlySpan<char> propertyName,
        CancellationToken cancellationToken = default)
    {
        writer.WritePropertyName(propertyName);
        return row.WriteValueAsync(writer, cancellationToken);
    }

    /// <summary>
    /// Writes the row to the specified writer as a JSON object.
    /// The column names (in camel case) are used as JSON property names.
    /// </summary>
    /// <param name="row">The <see cref="IRow"/> to be written as a JSON object.</param>
    /// <param name="writer">The writer to which row will be written.</param>
    /// <param name="propertyName">The property name of the JSON object.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <remarks>
    /// <para>
    /// If the column names are quoted by double-quotes (""), 
    /// the camel-case names are used as property names.
    /// </para>
    /// <para>
    /// If the column names are not quoted by double-quotes (""), 
    /// then the property names follow the rules of the database, 
    /// usually all letters in the names are lowercase.
    /// </para>
    /// <para>
    /// If a column does not have a name, then the property name follows the rules of the database, 
    /// usually the name may be "?column?". 
    /// If more than one columns do not have names, the output JSON object will have multiple properties with the same name.
    /// </para>
    /// <para>
    /// The NaN, Infinity, and -Infinity of <see langword="float"/> and <see langword="double"/> are written as 
    /// the strings "NaN", "Infinity", and "-Infinity", respectively.
    /// </para>
    /// <para>
    /// This method does not call <paramref name="writer"/>.FlushAsync.
    /// </para>
    /// </remarks>
    public static ValueTask WriteAsync(
        this IRow row,
        Utf8JsonWriter writer,
        string propertyName,
        CancellationToken cancellationToken = default)
    {
        writer.WritePropertyName(propertyName);
        return row.WriteValueAsync(writer, cancellationToken);
    }

    /// <summary>
    /// Writes the row to the specified writer as a JSON object.
    /// The column names (in camel case) are used as JSON property names.
    /// </summary>
    /// <param name="row">The <see cref="IRow"/> to be written as a JSON object.</param>
    /// <param name="writer">The writer to which row will be written.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <remarks>
    /// <para>
    /// If the column names are quoted by double-quotes (""), 
    /// the camel-case names are used as property names.
    /// </para>
    /// <para>
    /// If the column names are not quoted by double-quotes (""), 
    /// then the property names follow the rules of the database, 
    /// usually all letters in the names are lowercase.
    /// </para>
    /// <para>
    /// If a column does not have a name, then the property name follows the rules of the database, 
    /// usually the name may be "?column?". 
    /// If more than one columns do not have names, the output JSON object will have multiple properties with the same name.
    /// </para>
    /// <para>
    /// The NaN, Infinity, and -Infinity of <see langword="float"/> and <see langword="double"/> are written as 
    /// the strings "NaN", "Infinity", and "-Infinity", respectively.
    /// </para>
    /// <para>
    /// This method does not call <paramref name="writer"/>.FlushAsync.
    /// </para>
    /// </remarks>
    public static async ValueTask WriteValueAsync(
        this IRow row,
        Utf8JsonWriter writer,
        CancellationToken cancellationToken = default)
    {
        writer.WriteStartObject();

        await foreach (var column in row)
        {
            await column.WriteAsync(writer, cancellationToken);
        }

        writer.WriteEndObject();
    }
}
