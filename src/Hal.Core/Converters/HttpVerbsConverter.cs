using Newtonsoft.Json;

namespace Hal.Core.Converters;

public class HttpVerbsConverter : JsonConverter<HttpVerbs>
{

    public override HttpVerbs ReadJson(JsonReader reader, Type objectType, HttpVerbs existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var enumString = reader.Value?.ToString();
        if (string.IsNullOrEmpty(enumString))
            throw new JsonException($"Unable to convert '{enumString}' to {typeof(HttpVerbs).Name}.");

        return Enum.Parse<HttpVerbs>(enumString, true);
    }

    public override void WriteJson(JsonWriter writer, HttpVerbs value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}
