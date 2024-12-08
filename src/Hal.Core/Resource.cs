using Hal.Core.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Hal.Core;

public class Resource : IResource
{
    [JsonPropertyName("_links")]
    [JsonProperty("_links")]
    public ISet<ILink> Links { get; init; } = new HashSet<ILink>();

    [JsonPropertyName("_embedded")]
    [JsonProperty("_embedded", NullValueHandling = NullValueHandling.Ignore)]
    public IDictionary<string, object> Embedded { get; init; } = new Dictionary<string, object>();
    public void AddLink(ILink link)
    {
        Links.Add(link);
    }
    public void AddEmbeddedResource<T>(string key, IEmbeddedResource<T> resource)
    {
        Embedded[key] = resource;
    }
    public void AddEmbeddedResourceCollection<T>(string key, IEmbeddedResource<IEnumerable<T>> resource)
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

public class Resource<T> : Resource, IResource<T>
{
    [JsonPropertyName("data")]
    [JsonProperty("data")]
    public T Data { get; init; }

    public Resource(T data)
    {
        Data = data;
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

public class Resource<TData, TMeta> : Resource<TData>
{
    [JsonPropertyName("meta")]
    [JsonProperty("meta")]
    public TMeta Meta { get; init; }
    public Resource(TData data, TMeta meta) : base(data)
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
