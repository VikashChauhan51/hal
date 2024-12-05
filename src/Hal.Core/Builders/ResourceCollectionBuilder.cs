namespace Hal.Core.Builders;
public class ResourceCollectionBuilder<T> : IResourceCollectionBuilder<T>
{
    private readonly ResourceCollection<T> _resourceCollection;

    public ResourceCollectionBuilder(IEnumerable<T> data)
    {
        _resourceCollection = new ResourceCollection<T>(data);
    }

    public IResourceCollectionBuilder<T> AddLink(string rel, string href, HttpVerbs method)
    {
        _resourceCollection.AddLink(new Link { Href = href, Rel = rel, Method = method });
        return this;
    }

    public IResourceCollectionBuilder<T> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource)
    {
        _resourceCollection.AddEmbeddedResource(rel, embeddedResource);
        return this;
    }

    public IResourceCollectionBuilder<T> AddEmbeddedResourceCollection<TEmbedded>(string rel, IEmbeddedResource<IEnumerable<TEmbedded>> embeddedResourceCollection)
    {
        _resourceCollection.AddEmbeddedResourceCollection(rel, embeddedResourceCollection);
        return this;
    }

    public IResourceCollection<T> Build()
    {
        return _resourceCollection;
    }
}

