namespace Hal.Core;

/// <summary>
/// Represents a resource that encapsulates an embedded value of a specified type.
/// </summary>
/// <typeparam name="T">The type of the value to be embedded within the resource.</typeparam>
public class EmbeddedResource<T> : IEmbeddedResource<T>
{
    /// <inheritdoc/>
    public T Embedded { get; init; }

    /// <summary>
    /// Initializes a new instance of the EmbeddedResource class with the specified embedded resource.
    /// </summary>
    /// <param name="embedded">The resource to embed in this instance. Cannot be null.</param>
    public EmbeddedResource(T embedded)
    {
        Embedded = embedded;
    }
}
