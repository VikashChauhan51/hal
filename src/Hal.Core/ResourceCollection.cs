using Hal.Core.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Text.Json.Serialization;

namespace Hal.Core;
public class ResourceCollection<T> : Resource, IResourceCollection<T>, ICollection<T>
{
    private readonly ICollection<T> _resourceCollection = new List<T>();

    [JsonPropertyName("data")]
    [JsonProperty("data")]
    public IEnumerable<T> Data => _resourceCollection;

    public int Count => _resourceCollection.Count;

    public bool IsReadOnly => _resourceCollection.IsReadOnly;

    public ResourceCollection(IEnumerable<T> data)
    {
        foreach (var item in data ?? [])
        {
            _resourceCollection.Add(item);
        }
    }

    public ResourceCollection(ICollection<T> data)
    {
        _resourceCollection = data;
    }

    public void Add(T item)
    {
        _resourceCollection.Add(item);
    }

    public void Clear()
    {
        _resourceCollection.Clear();
    }

    public bool Contains(T item)
    {
        return _resourceCollection.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _resourceCollection.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return _resourceCollection.Remove(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _resourceCollection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _resourceCollection.GetEnumerator();
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
