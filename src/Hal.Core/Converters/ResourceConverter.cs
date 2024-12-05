
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hal.Core.Converters;


public class ResourceJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Resource).IsAssignableFrom(objectType);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var resource = value as Resource;
        if (resource == null) return;

        var json = new JObject
        {
            ["_links"] = JToken.FromObject(resource.Links, serializer)
        };

        if (resource is Resource<object> res)
        {
            json["data"] = JToken.FromObject(res.Data, serializer);
        }

        if (resource.Embedded?.Count > 0)
        {
            json["_embedded"] = JToken.FromObject(resource.Embedded, serializer);
        }

        if (resource is Resource<object, object> resMeta)
        {
            json["meta"] = JToken.FromObject(resMeta.Meta, serializer);
        }

        if (resource is ResourceCollection<object> resColl)
        {
            json["data"] = JToken.FromObject(resColl.Data, serializer);
        }

        if (resource is ResourceCollection<object, object> resCollMeta)
        {
            json["meta"] = JToken.FromObject(resCollMeta.Meta, serializer);
        }

        json.WriteTo(writer);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException("Deserialization is not implemented for Resource.");
    }

    public override bool CanRead => false;
}

