
namespace Hal.Core;

public interface IEmbeddedResourceCollection<out T>
{
    IEnumerable<T> Embedded { get;}
}
