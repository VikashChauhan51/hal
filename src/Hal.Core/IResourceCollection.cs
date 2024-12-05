namespace Hal.Core;
public interface IResourceCollection<out T>
{
    IEnumerable<T> Data { get;}
}
