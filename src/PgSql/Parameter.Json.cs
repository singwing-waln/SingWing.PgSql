using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;
using System.Text.Json.Nodes;

namespace SingWing.PgSql;

public abstract partial class Parameter
{
    /// <summary>
    /// Implicitly converts a JsonDocument? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JsonDocument? value to convert.</param>
    /// <remarks>
    /// The parameter object returned by this method does not reference the <paramref name="value"/> object.
    /// </remarks>
    public static implicit operator Parameter(JsonDocument? value)
    {
        return value?.RootElement;
    }

    /// <summary>
    /// Implicitly converts a JsonElement? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JsonElement? value to convert.</param>
    /// <remarks>
    /// The parameter object returned by this method does not reference the document of the <paramref name="value"/>.
    /// </remarks>
    public static implicit operator Parameter(JsonElement? value)
    {
        if (value is null)
        {
            return StreamJsonParameter.Null;
        }

        var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new()
        {
            Indented = false,
            SkipValidation = true
        });

        value.Value.WriteTo(writer);
        writer.Flush();
        stream.Position = 0;
        return stream.ToJsonParameter(ownsStream: true);
    }

    /// <summary>
    /// Implicitly converts a JsonNode? value 
    /// to a <see cref="Parameter"/> whose data type is <see cref="DataType.Jsonb"/>.
    /// </summary>
    /// <param name="value">The JsonNode? value to convert.</param>
    /// <remarks>
    /// The parameter object returned by this method does not reference the <paramref name="value"/> object.
    /// </remarks>
    public static implicit operator Parameter(JsonNode? value)
    {
        if (value is null)
        {
            return StreamJsonParameter.Null;
        }

        var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new()
        {
            Indented = false,
            SkipValidation = true
        });

        value.WriteTo(writer);
        writer.Flush();
        stream.Position = 0;
        return stream.ToJsonParameter(ownsStream: true);
    }
}
