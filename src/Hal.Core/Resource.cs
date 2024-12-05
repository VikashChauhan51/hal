using Hal.Core.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hal.Core;

public class Resource : IResource
{
    [JsonProperty("_links")]
    public ISet<Link> Links { get; init; } = new HashSet<Link>();

    [JsonProperty("_embedded", NullValueHandling = NullValueHandling.Ignore)]
    public IDictionary<string, object> Embedded { get; init; } = new Dictionary<string, object>();
    public void AddLink(Link link)
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
            Converters = new List<JsonConverter>()
            {
                new LinkConverter(),
                new ResourceConverter()
            },
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
    public T Data { get; init; }

    public Resource(T data)
    {
        Data = data;
    }
}

public class Resource<TData, TMeta> : Resource<TData>
{
    public TMeta Meta { get; init; }
    public Resource(TData data, TMeta meta) : base(data)
    {
        Meta = meta;
    }
}
