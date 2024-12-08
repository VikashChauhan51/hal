namespace Hal.Core.Builders;
public class ResourceCollectionBuilder<T> : IResourceCollectionBuilder<T>
{
    private readonly IResourceCollection<T> _resourceCollection;

    public ResourceCollectionBuilder(IEnumerable<T> data)
    {
        _resourceCollection = new ResourceCollection<T>(data);
    }

    public IResourceCollectionBuilder<T> AddLink(string rel, string href, HttpVerbs method)
    {
        _resourceCollection.AddLink(new Link { Href = href, Rel = rel, Method = method });
        return this;
    }

    public IResourceCollectionBuilder<T> AddEmbeddedResourceCollection<TEmbedded>(string rel, IEmbeddedResourceCollection<TEmbedded> embeddedResourceCollection)
    {
        _resourceCollection.AddEmbeddedResourceCollection(rel, embeddedResourceCollection);
        return this;
    }

    public IResourceCollection<T> Build()
    {
        return _resourceCollection;
    } 
}

