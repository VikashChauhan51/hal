namespace Hal.Core;

/// <summary>
/// Represents a collection of resources that exposes data items and supports adding embedded resource collections.
/// </summary>
/// <remarks>Use this interface to work with collections of resources that may include embedded resource
/// collections. Implementations should ensure that the data and embedded resources are accessible as intended. This
/// interface is typically used in scenarios where resources are grouped and may have hierarchical
/// relationships.</remarks>
/// <typeparam name="TData">The type of data contained in the resource collection.</typeparam>
public interface IResourceCollection<out TData> : IResource
{
    /// <summary>
    /// Gets the collection of data items of type TData contained in the result.
    /// </summary>
    IEnumerable<TData> Data { get; }

    /// <summary>
    /// Adds an embedded resource collection to the collection with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the resources contained in the collection.</typeparam>
    /// <param name="key">The unique key used to identify the embedded resource collection. Cannot be null or empty.</param>
    /// <param name="resource">The embedded resource collection to add. Cannot be null.</param>
    void AddEmbeddedResourceCollection<T>(string key, IEmbeddedResourceCollection<T> resource);
}


/// <summary>
/// Represents a collection of resources with associated metadata.
/// </summary>
/// <remarks>This interface extends IResourceCollection<TData> by providing access to additional metadata
/// describing the collection as a whole. It is commonly used in scenarios where resource collections are accompanied by
/// summary information, pagination details, or other contextual metadata.</remarks>
/// <typeparam name="TData">The type of the resource elements contained in the collection.</typeparam>
/// <typeparam name="TMeta">The type of the metadata associated with the collection.</typeparam>

public interface IResourceCollection<out TData, out TMeta> : IResourceCollection<TData>
{
    /// <summary>
    /// Gets the metadata associated with the current instance.
    /// </summary>
    TMeta Meta { get; }
}
