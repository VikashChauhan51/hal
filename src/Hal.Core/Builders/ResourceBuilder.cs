
namespace Hal.Core.Builders;
public class ResourceBuilder<TData> : IResourceBuilder<TData>
{
    private readonly IResource<TData> _resource;

    public ResourceBuilder(TData data)
    {
        _resource = new Resource<TData>(data);
    }

    public IResourceBuilder<TData> AddLink(string rel, string href, HttpVerbs method)
    {
        _resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
        return this;
    }
    public IResourceBuilder<TData> AddLink(Func<ILinkBuilder, ILink> link)
    {
        var linkBuilder = new LinkBuilder();
        var constructedLink = link(linkBuilder);
        _resource.AddLink(constructedLink);
        return this;
    }

    public IResourceBuilder<TData> AddLink(ILink link)
    {
        _resource.AddLink(link);
        return this;
    }
    public IResourceBuilder<TData> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource)
    {
        _resource.AddEmbeddedResource(rel, embeddedResource);
        return this;
    }
    public IResource<TData> Build()
    {
        return _resource;
    }  
}


public class ResourceBuilder<TData, TMeta> : IResourceBuilder<TData, TMeta>
{
    private readonly IResource<TData, TMeta> _resource;

    public ResourceBuilder(TData data, TMeta meta)
    {
        _resource = new Resource<TData, TMeta>(data, meta);
    }

    public IResourceBuilder<TData, TMeta> AddLink(string rel, string href, HttpVerbs method)
    {
        _resource.AddLink(new Link { Href = href, Rel = rel, Method = method });
        return this;
    }

    public IResourceBuilder<TData, TMeta> AddLink(Func<ILinkBuilder, ILink> link)
    {
        var linkBuilder = new LinkBuilder();
        var constructedLink = link(linkBuilder);
        _resource.AddLink(constructedLink);
        return this;
    }

    public IResourceBuilder<TData, TMeta> AddLink(ILink link)
    {
        _resource.AddLink(link);
        return this;
    }
    public IResourceBuilder<TData, TMeta> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource)
    {
        _resource.AddEmbeddedResource(rel, embeddedResource);
        return this;
    }

    public IResource<TData, TMeta> Build()
    {
        return _resource;
    }
}
