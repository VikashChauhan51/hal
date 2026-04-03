namespace Hal.Core;

/// <summary>
/// Represents a collection of resources with associated embedded resources, supporting hypermedia scenarios.
/// </summary>
/// <remarks>Use this class to model a set of resources along with any related embedded resources, such as for HAL
/// or other hypermedia-driven APIs. The collection supports adding embedded resource collections by key, enabling
/// flexible representation of complex resource graphs.</remarks>
/// <typeparam name="TData">The type of data contained in the resource collection.</typeparam>
public class ResourceCollection<TData> : Resource, IResourceCollection<TData>
{
    /// <inheritdoc/>
    public IEnumerable<TData> Data { get; init; }

    /// <inheritdoc/>
    public IDictionary<string, dynamic> Embedded { get; init; } = new Dictionary<string, dynamic>();


    /// <summary>
    /// Initializes a new instance of the ResourceCollection class with the specified data items.
    /// </summary>
    /// <param name="data">The collection of data items to include in the resource collection. Cannot be null.</param>
    public ResourceCollection(IEnumerable<TData> data)
    {
        Data = data;
    }

    /// <inheritdoc/>
    public void AddEmbeddedResourceCollection<T>(string key, IEmbeddedResourceCollection<T> resource)
    {
        Embedded[key] = resource;
    }
}

/// <summary>
/// Represents a collection of resources with associated metadata.
/// </summary>
/// <remarks>This class extends the base resource collection by including additional metadata, which can be used
/// to provide context or supplementary information about the collection as a whole. It is commonly used in scenarios
/// where both a set of resources and related metadata need to be returned together, such as in paginated API
/// responses.</remarks>
/// <typeparam name="TData">The type of the resource elements contained in the collection.</typeparam>
/// <typeparam name="TMeta">The type of the metadata associated with the resource collection.</typeparam>
public class ResourceCollection<TData, TMeta> : ResourceCollection<TData>, IResourceCollection<TData, TMeta>
{
    /// <inheritdoc/>
    public TMeta Meta { get; init; }

    /// <summary>
    /// Initializes a new instance of the ResourceCollection class with the specified data items and associated
    /// metadata.
    /// </summary>
    /// <param name="data">The collection of data items to include in the resource collection. Cannot be null.</param>
    /// <param name="meta">The metadata associated with the resource collection. Cannot be null.</param>
    public ResourceCollection(IEnumerable<TData> data, TMeta meta) : base(data)
    {
        Meta = meta;
    }
}
