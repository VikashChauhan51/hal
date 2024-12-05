namespace Hal.Core;

public interface IEmbeddedResource<out T>
{
    T Embedded { get; }
}
