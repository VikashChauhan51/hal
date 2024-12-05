using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Hal.Core.Converters;

public class LinkConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Link).IsAssignableFrom(objectType);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var link = value as Link;
        if (link == null) return;

        var json = new JObject
        {
            [link.Rel] = link.Href,
            ["method"] = link.Method.ToString(),
        };

        json.WriteTo(writer);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException("Deserialization is not implemented for Resource.");
    }

    public override bool CanRead => false;
}
