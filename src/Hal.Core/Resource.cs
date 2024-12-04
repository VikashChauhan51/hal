using Newtonsoft.Json;

namespace Hal.Core;

public class Resource
{
    [JsonProperty("_links")]
    public ISet<Link> Links { get; init; } = new HashSet<Link>();

    public void AddLink(Link link)
    {
        Links.Add(link);

    }
}

public class Resource<T> : Resource
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

public class EmbeddedResource<T>
{
    [JsonProperty("_embedded")]
    public T Embedded { get; init; }
    public EmbeddedResource(T embedded)
    {
        Embedded = embedded;
    }
}


public class ResourceCollection<T> : Resource
{
    public IEnumerable<T> Data { get; init; }

    public ResourceCollection(IEnumerable<T> data)
    {
        Data = data;
    }
}

public class ResourceCollection<TData, TMeta> : ResourceCollection<TData>
{
    public TMeta Meta { get; init; }
    public ResourceCollection(IEnumerable<TData> data, TMeta meta) : base(data)
    {
        Meta = meta;
    }
}
