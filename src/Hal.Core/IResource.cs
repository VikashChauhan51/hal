
namespace Hal.Core;

public interface IResource
{
    ISet<Link> Links { get;}

    void AddLink(Link link);
    void AddEmbeddedResource<T>(string key, IEmbeddedResource<T> resource);
    void AddEmbeddedResourceCollection<T>(string key, IEmbeddedResource<IEnumerable<T>> resource);
}

public interface IResource<out T>
{
    T Data { get;}
}
