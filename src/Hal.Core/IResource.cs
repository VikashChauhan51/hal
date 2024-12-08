
namespace Hal.Core;

public interface IResource
{
    ISet<ILink> Links { get; }
    void AddLink(ILink link);
}

public interface IResource<out TData> : IResource
{
    TData Data { get; }
}

public interface IResourceMeta<out TData, out TMeta> : IResource<TData>
{
    TMeta Meta { get; }
    void AddEmbeddedResource<T>(string key, IEmbeddedResource<T> resource);
}
