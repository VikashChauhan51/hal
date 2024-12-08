using Hal.Core.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Hal.Core;
public class ResourceCollection<TData> : Resource, IResourceCollection<TData>
{

    [JsonPropertyName("data")]
    [JsonProperty("data")]
    public IEnumerable<TData> Data { get; init; }

    [JsonPropertyName("_embedded")]
    [JsonProperty("_embedded", NullValueHandling = NullValueHandling.Ignore)]
    public IDictionary<string, dynamic> Embedded { get; init; } = new Dictionary<string, dynamic>();

    public ResourceCollection(IEnumerable<TData> data)
    {
        Data = data;
    }
    public void AddEmbeddedResourceCollection<T>(string key, IEmbeddedResourceCollection<T> resource)
    {
        Embedded[key] = resource;
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

public class ResourceCollection<TData, TMeta> : ResourceCollection<TData>
{
    [JsonPropertyName("meta")]
    [JsonProperty("meta")]
    public TMeta Meta { get; init; }
    public ResourceCollection(IEnumerable<TData> data, TMeta meta) : base(data)
    {
        Meta = meta;
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
