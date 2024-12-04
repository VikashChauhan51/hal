namespace Hal.Core.Services;
public interface IHalService
{
    void AddSelfLink<T>(Resource<T> resource, string href, HttpVerbs method);
    void AddLink<T>(Resource<T> resource, string rel, string href, HttpVerbs method);
    void AddEmbeddedResource<T, TEmbedded>(Resource<T> resource, string rel, TEmbedded embeddedResource);
    void AddEmbeddedCollection<T, TEmbedded>(Resource<T> resource, string rel, IDictionary<string, TEmbedded> embeddedResources);
    void AddSelfLink<T>(ResourceCollection<T> resource, string href, HttpVerbs method);
    void AddLink<T>(ResourceCollection<T> resource, string rel, string href, HttpVerbs method);
}


