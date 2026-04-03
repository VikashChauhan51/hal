namespace Hal.Core;

public static class HalExtensions
{
    public static void AddSelfLink<T>(this IResource<T> resource, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = "self", Method = method });
    }

    public static void AddSelfLink<T>(this IResourceCollection<T> resource, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = "self", Method = method });
    }

    public static void AddLink<T>(this IResource<T> resource, string rel, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
    }
    public static void AddLink<T>(this IResourceCollection<T> resource, string rel, string href, HttpVerbs method)
    {
        resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
    }

    public static void AddEmbeddedResource<T, TEmbedded>(this IResource<T, TEmbedded> resource, string key, IEmbeddedResource<TEmbedded> embeddedResource)
    {
        resource.AddEmbeddedResource(key, embeddedResource);
    }

    public static void AddEmbeddedResourceCollection<T, TEmbedded>(this IResourceCollection<T, TEmbedded> resource, string key, IEmbeddedResourceCollection<TEmbedded> embeddedResourceCollection)
    {
        resource.AddEmbeddedResourceCollection(key, embeddedResourceCollection);
    }
}
