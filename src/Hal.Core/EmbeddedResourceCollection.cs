using Hal.Core.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Hal.Core;
public class EmbeddedResourceCollection<T> : IEmbeddedResourceCollection<T>
{

    [JsonProperty("_embedded")]
    [JsonPropertyName("_embedded")]
    public IEnumerable<T> Embedded { get; init; }
    public EmbeddedResourceCollection(IEnumerable<T> embedded)
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
