namespace Hal.Core.Builders;
public interface IResourceBuilder<T>
{
    IResourceBuilder<T> AddLink(string rel, string href, HttpVerbs method);
    IResourceBuilder<T> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource);
    IResource<T> Build();
}

