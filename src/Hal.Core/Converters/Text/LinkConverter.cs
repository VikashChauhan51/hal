using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hal.Core.Converters.Text;
public class LinkConverter : JsonConverter<Link>
{
    public override Link Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException("Deserialization is not implemented for Link.");
    }

    public override void Write(Utf8JsonWriter writer, Link value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();
        writer.WriteString(value.Rel, value.Href);
        writer.WriteString("method", value.Method.ToString());
        writer.WriteEndObject();
    }

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Link).IsAssignableFrom(typeToConvert);
    }
}
