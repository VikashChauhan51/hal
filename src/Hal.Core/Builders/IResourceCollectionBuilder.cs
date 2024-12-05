namespace Hal.Core.Builders;
public interface IResourceCollectionBuilder<T>
{
    IResourceCollectionBuilder<T> AddLink(string rel, string href, HttpVerbs method);
    IResourceCollectionBuilder<T> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource);
    IResourceCollectionBuilder<T> AddEmbeddedResourceCollection<TEmbedded>(string rel, IEmbeddedResource<IEnumerable<TEmbedded>> embeddedResourceCollection);
    IResourceCollection<T> Build();
}
