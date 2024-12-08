namespace Hal.Core;
public interface IResourceCollection<out TData> :IResource
{
    IEnumerable<TData> Data { get; }
}


public interface IResourceCollectionMeta<out TData, out TMeta> : IResource<TData>
{
    IEnumerable<TMeta> Meta { get; }
    void AddEmbeddedResourceCollection<T>(string key, IEmbeddedResource<IEnumerable<T>> resource);
}
