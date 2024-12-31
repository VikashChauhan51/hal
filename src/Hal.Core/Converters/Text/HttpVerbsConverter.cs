using System.Text.Json;
using System.Text.Json.Serialization;


namespace Hal.Core.Converters.Text;

public class HttpVerbsConverter : JsonConverter<HttpVerbs>
{
    public override HttpVerbs Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var enumString = reader.GetString();
        if (string.IsNullOrEmpty(enumString))
        {
            throw new JsonException($"Unable to convert '{enumString}' to {typeof(HttpVerbs).Name}.");
        }

        if (Enum.TryParse<HttpVerbs>(enumString, ignoreCase: true, out var result))
        {
            return result;
        }

        throw new JsonException($"Unable to convert '{enumString}' to {typeof(HttpVerbs).Name}.");
    }

    public override void Write(Utf8JsonWriter writer, HttpVerbs value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
