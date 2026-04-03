namespace Hal.Core.Builders;

/// <summary>
/// This interface defines a builder for constructing resource objects that encapsulate data of type TData, along with associated links and embedded resources. It provides a fluent API for adding links and embedded resources to the resource being built, allowing for flexible and customizable resource construction. The Build method finalizes the construction process and returns the resulting resource object. This interface is commonly used in scenarios where resources need to be dynamically constructed based on varying data and relationships, such as in RESTful APIs or hypermedia-driven applications.
/// </summary>
/// <typeparam name="TData">The data.</typeparam>
public interface IResourceBuilder<TData>
{
    IResourceBuilder<TData> AddLink(string rel, string href, HttpVerbs method);
    IResourceBuilder<TData> AddLink(Func<ILinkBuilder, ILink> link);
    IResourceBuilder<TData> AddLink(ILink link);
    IResourceBuilder<TData> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource);
    IResource<TData> Build();
}


public interface IResourceBuilder<TData, TMeta>
{
    IResourceBuilder<TData, TMeta> AddLink(string rel, string href, HttpVerbs method);
    IResourceBuilder<TData, TMeta> AddLink(Func<ILinkBuilder, ILink> link);
    IResourceBuilder<TData, TMeta> AddLink(ILink link);
    IResourceBuilder<TData, TMeta> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource);
    IResource<TData, TMeta> Build();
}
