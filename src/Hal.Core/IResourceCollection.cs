namespace Hal.Core;
public interface IResourceCollection<out TData> :IResource
{
    IEnumerable<TData> Data { get; }
    void AddEmbeddedResourceCollection<T>(string key, IEmbeddedResourceCollection<T> resource);
}


public interface IResourceCollection<out TData, out TMeta> : IResourceCollection<TData>
{
    TMeta Meta { get; }  
}
