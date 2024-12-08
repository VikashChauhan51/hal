namespace Hal.Core.Builders;
public class ResourceCollectionBuilder<TData> : IResourceCollectionBuilder<TData>
{
    private readonly IResourceCollection<TData> _resourceCollection;

    public ResourceCollectionBuilder(IEnumerable<TData> data)
    {
        _resourceCollection = new ResourceCollection<TData>(data);
    }

    public IResourceCollectionBuilder<TData> AddLink(string rel, string href, HttpVerbs method)
    {
        _resourceCollection.AddLink(new Link { Href = href, Rel = rel, Method = method });
        return this;
    }

    public IResourceCollectionBuilder<TData> AddEmbeddedResourceCollection<TEmbedded>(string rel, IEmbeddedResourceCollection<TEmbedded> embeddedResourceCollection)
    {
        _resourceCollection.AddEmbeddedResourceCollection(rel, embeddedResourceCollection);
        return this;
    }

    public IResourceCollection<TData> Build()
    {
        return _resourceCollection;
    }
}


public class ResourceCollectionBuilder<TData, TMeta> : IResourceCollectionBuilder<TData, TMeta>
{
    private readonly IResourceCollection<TData, TMeta> _resourceCollection;

    public ResourceCollectionBuilder(IEnumerable<TData> data, TMeta meta)
    {
        _resourceCollection = new ResourceCollection<TData, TMeta>(data, meta);
    }

    public IResourceCollectionBuilder<TData, TMeta> AddLink(string rel, string href, HttpVerbs method)
    {
        _resourceCollection.AddLink(new Link { Href = href, Rel = rel, Method = method });
        return this;
    }

    public IResourceCollectionBuilder<TData, TMeta> AddEmbeddedResourceCollection<TEmbedded>(string rel, IEmbeddedResourceCollection<TEmbedded> embeddedResourceCollection)
    {
        _resourceCollection.AddEmbeddedResourceCollection(rel, embeddedResourceCollection);
        return this;
    }

    public IResourceCollection<TData, TMeta> Build()
    {
        return _resourceCollection;
    }
}
