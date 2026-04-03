namespace Hal.Core;

/// <summary>
/// Represents a collection of embedded resources of a specified type.
/// </summary>
/// <typeparam name="T">The type of the embedded resources contained in the collection.</typeparam>
public class EmbeddedResourceCollection<T> : IEmbeddedResourceCollection<T>
{
    /// <inheritdoc/>
    public IEnumerable<T> Embedded { get; init; }

    /// <summary>
    /// Initializes a new instance of the EmbeddedResourceCollection class with the specified embedded resources.
    /// </summary>
    /// <param name="embedded">The collection of embedded resources to include in the collection. Cannot be null.</param>
    public EmbeddedResourceCollection(IEnumerable<T> embedded)
    {
        Embedded = embedded;
    }

}
