namespace Hal.Core.Services;
public class HalService : IHalService
{
    public void AddSelfLink<T>(Resource<T> resource, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = "self", Method = method });
    }

    public void AddLink<T>(Resource<T> resource, string rel, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
    }

    public void AddEmbeddedResource<T, TEmbedded>(Resource<T> resource, string rel, TEmbedded embeddedResource)
    {
        var embedded = new EmbeddedResource<TEmbedded>(embeddedResource);
        resource.AddLink(new Link { Href = string.Empty, Rel = rel, Method = HttpVerbs.Get });
    }

    public void AddEmbeddedCollection<T, TEmbedded>(Resource<T> resource, string rel, IDictionary<string, TEmbedded> embeddedResources)
    {
        var embedded = new EmbeddedResource<IDictionary<string, TEmbedded>>(embeddedResources);
        resource.AddLink(new Link { Href = string.Empty, Rel = rel, Method = HttpVerbs.Get });
    }

    public void AddSelfLink<T>(ResourceCollection<T> resource, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = "self", Method = method });
    }

    public void AddLink<T>(ResourceCollection<T> resource, string rel, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
    }
}

