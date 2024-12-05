namespace Hal.Core.Builders;
public class ResourceBuilder<T> : IResourceBuilder<T>
{
    private readonly Resource<T> _resource;

    public ResourceBuilder(T data)
    {
        _resource = new Resource<T>(data);
    }

    public IResourceBuilder<T> AddLink(string rel, string href, HttpVerbs method)
    {
        _resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
        return this;
    }

    public IResourceBuilder<T> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource)
    {
        _resource.AddEmbeddedResource(rel, embeddedResource);
        return this;
    }

    public IResourceBuilder<T> AddEmbeddedResourceCollection<TEmbedded>(string rel, IEmbeddedResource<IEnumerable<TEmbedded>> embeddedResourceCollection)
    {
        _resource.AddEmbeddedResourceCollection(rel, embeddedResourceCollection);
        return this;
    }

    public IResource<T> Build()
    {
        return _resource;
    }
}

