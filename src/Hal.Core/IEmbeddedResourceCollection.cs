
namespace Hal.Core;

/// <summary>
/// Represents a collection of embedded resources of a specified type.
/// </summary>
/// <typeparam name="T">The type of the embedded resources contained in the collection.</typeparam>
public interface IEmbeddedResourceCollection<out T>
{
    /// <summary>
    /// Gets the collection of embedded items of type T.
    /// </summary>
    IEnumerable<T> Embedded { get;}
}
