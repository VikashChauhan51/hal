
namespace Hal.Core.Services;
public class HalService : IHalService
{
    public void AddSelfLink<T>(IResource<T> resource, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = "self", Method = method });
    }

    public void AddSelfLink<T>(IResourceCollection<T> resource, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = "self", Method = method });
    }

    public void AddLink<T>(IResource<T> resource, string rel, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
    }
    public void AddLink<T>(IResourceCollection<T> resource, string rel, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
    }

    public void AddEmbeddedResource<T, TEmbedded>(IResourceMeta<T, TEmbedded> resource, string key, IEmbeddedResource<TEmbedded> embeddedResource)
    {
        resource.AddEmbeddedResource(key, embeddedResource);
    }

    public void AddEmbeddedResourceCollection<T, TEmbedded>(IResourceCollectionMeta<T, TEmbedded> resource, string key, IEmbeddedResource<IEnumerable<TEmbedded>> embeddedResourceCollection)
    {
        resource.AddEmbeddedResourceCollection(key, embeddedResourceCollection);
    }
}

