using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hal.Core.Converters.Text;
public class ResourceConverter : JsonConverter<Resource>
{
    public override Resource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException("Deserialization is not implemented for Resource.");
    }

    public override void Write(Utf8JsonWriter writer, Resource value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();
        writer.WritePropertyName("_links");
        JsonSerializer.Serialize(writer, value.Links, options);

        if (value is Resource<object> res)
        {
            if (res.Embedded?.Count > 0)
            {
                writer.WritePropertyName("_embedded");
                JsonSerializer.Serialize(writer, res.Embedded, options);
            }

            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, res.Data, options);
        }

        if (value is Resource<object, object> resMeta)
        {
            writer.WritePropertyName("meta");
            JsonSerializer.Serialize(writer, resMeta.Meta, options);
        }

        if (value is ResourceCollection<object> resColl)
        {
            if (resColl.Embedded?.Count > 0)
            {
                writer.WritePropertyName("_embedded");
                JsonSerializer.Serialize(writer, resColl.Embedded, options);
            }

            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, resColl.Data, options);
        }

        if (value is ResourceCollection<object, object> resCollMeta)
        {
            writer.WritePropertyName("meta");
            JsonSerializer.Serialize(writer, resCollMeta.Meta, options);
        }

        writer.WriteEndObject();
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Resource).IsAssignableFrom(typeToConvert);
    }
}
