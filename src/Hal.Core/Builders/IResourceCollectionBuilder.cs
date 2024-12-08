namespace Hal.Core.Builders;
public interface IResourceCollectionBuilder<T>
{
    IResourceCollectionBuilder<T> AddLink(string rel, string href, HttpVerbs method);
    IResourceCollectionBuilder<T> AddEmbeddedResourceCollection<TEmbedded>(string rel, IEmbeddedResourceCollection<TEmbedded> embeddedResourceCollection);
    IResourceCollection<T> Build();
}
