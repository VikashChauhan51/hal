using Hal.Core.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Hal.Core;
public class EmbeddedResource<T> : IEmbeddedResource<T>
{
    [JsonProperty("_embedded")]
    [JsonPropertyName("_embedded")]
    public T Embedded { get; init; }
    public EmbeddedResource(T embedded)
    {
        Embedded = embedded;
    }

    public override string ToString()
    {
        var jsonSerializerSettings = new HalJsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters =
            [
                new LinkConverter(),
                new ResourceConverter()
            ],
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

        return JsonConvert.SerializeObject(this, jsonSerializerSettings);
    }
}
