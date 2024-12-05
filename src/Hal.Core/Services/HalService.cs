
namespace Hal.Core.Services;
public class HalService : IHalService
{
    public void AddSelfLink<T>(Resource<T> resource, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = "self", Method = method });
    }

    public void AddSelfLink<T>(ResourceCollection<T> resource, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = "self", Method = method });
    }

    public void AddLink<T>(Resource<T> resource, string rel, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
    }
    public void AddLink<T>(ResourceCollection<T> resource, string rel, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
    }

    public void AddEmbeddedResource<T, TEmbedded>(Resource<T> resource, string key, IEmbeddedResource<TEmbedded> embeddedResource)
    {
        resource.AddEmbeddedResource(key, embeddedResource);
    }

    public void AddEmbeddedResourceCollection<T, TEmbedded>(Resource<T> resource, string key, IEmbeddedResource<IEnumerable<TEmbedded>> embeddedResourceCollection)
    {
        resource.AddEmbeddedResourceCollection(key, embeddedResourceCollection);
    }
}

