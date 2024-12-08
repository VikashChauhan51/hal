namespace Hal.Core.Builders;
public interface IResourceCollectionBuilder<TData>
{
    IResourceCollectionBuilder<TData> AddLink(string rel, string href, HttpVerbs method);
    IResourceCollectionBuilder<TData> AddEmbeddedResourceCollection<TEmbedded>(string rel, IEmbeddedResourceCollection<TEmbedded> embeddedResourceCollection);
    IResourceCollection<TData> Build();
}

public interface IResourceCollectionBuilder<TData, TMeta>
{
    IResourceCollectionBuilder<TData, TMeta> AddLink(string rel, string href, HttpVerbs method);
    IResourceCollectionBuilder<TData, TMeta> AddEmbeddedResourceCollection<TEmbedded>(string rel, IEmbeddedResourceCollection<TEmbedded> embeddedResourceCollection);
    IResourceCollection<TData, TMeta> Build();
}
