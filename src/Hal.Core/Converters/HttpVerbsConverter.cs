using Newtonsoft.Json;

namespace Hal.Core.Converters;

public class HttpVerbsConverter : JsonConverter<HttpVerbs>
{

    public override HttpVerbs ReadJson(JsonReader reader, Type objectType, HttpVerbs existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string? value = reader?.Value?.ToString();

        return value switch
        {
            "GET" => HttpVerbs.Get,
            "POST" => HttpVerbs.Post,
            "PUT" => HttpVerbs.Put,
            "DELETE" => HttpVerbs.Delete,
            "PATCH" => HttpVerbs.Patch,
            "HEAD" => HttpVerbs.Head,
            "OPTIONS" => HttpVerbs.Options,
            "TRACE" => HttpVerbs.Trace,
            "CONNECT" => HttpVerbs.Connect,
            _ => throw new JsonException($"Unknown HttpVerbs value: {value}")
        };
    }

    public override void WriteJson(JsonWriter writer, HttpVerbs value, JsonSerializer serializer)
    {
        string stringValue = value switch
        {
            HttpVerbs.Get => "GET",
            HttpVerbs.Post => "POST",
            HttpVerbs.Put => "PUT",
            HttpVerbs.Delete => "DELETE",
            HttpVerbs.Patch => "PATCH",
            HttpVerbs.Head => "HEAD",
            HttpVerbs.Options => "OPTIONS",
            HttpVerbs.Trace => "TRACE",
            HttpVerbs.Connect => "CONNECT",
            _ => throw new JsonException($"Unknown HttpVerbs enum value: {value}")
        };

        writer.WriteValue(stringValue);
    }
}
