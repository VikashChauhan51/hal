namespace Hal.Core.Services;
public interface IHalService
{
    void AddSelfLink<T>(IResource<T> resource, string href, HttpVerbs method);
    void AddSelfLink<T>(IResourceCollection<T> resource, string href, HttpVerbs method);
    void AddLink<T>(IResourceCollection<T> resource, string rel, string href, HttpVerbs method);
    void AddLink<T>(IResource<T> resource, string rel, string href, HttpVerbs method);
    void AddEmbeddedResource<T, TEmbedded>(IResource<T, TEmbedded> resource, string key, IEmbeddedResource<TEmbedded> embeddedResource);
    void AddEmbeddedResourceCollection<T, TEmbedded>(IResourceCollection<T, TEmbedded> resource, string key, IEmbeddedResourceCollection<TEmbedded> embeddedResourceCollection);

}
