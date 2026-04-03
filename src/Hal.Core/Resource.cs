namespace Hal.Core;

/// <summary>
/// Represents a resource that contains a collection of associated links.
/// </summary>
/// <remarks>The Resource class provides a way to manage a set of links related to the resource, supporting
/// scenarios such as hypermedia-driven APIs. Links can be added to the collection using the AddLink method. The class
/// implements the IResource interface.</remarks>
public class Resource : IResource
{
    /// <inheritdoc/>
    public ISet<ILink> Links { get; init; } = new HashSet<ILink>();

    /// <inheritdoc/>
    public void AddLink(ILink link)
    {
        Links.Add(link);
    }
}

/// <summary>
/// Represents a resource that encapsulates data of a specified type and supports embedding additional related
/// resources.
/// </summary>
/// <remarks>This class is commonly used to model resources in hypermedia APIs, allowing for both primary data and
/// embedded related resources. Embedded resources can be accessed or added using the provided properties and methods.
/// The class is immutable except for the contents of the Embedded dictionary, which can be modified after
/// initialization.</remarks>
/// <typeparam name="TData">The type of data contained within the resource.</typeparam>
public class Resource<TData> : Resource, IResource<TData>
{
    /// <inheritdoc/>
    public TData Data { get; init; }

    /// <inheritdoc/>
    public IDictionary<string, dynamic> Embedded { get; init; } = new Dictionary<string, dynamic>();

    /// <summary>
    /// Initializes a new instance of the Resource class with the specified data.
    /// </summary>
    /// <param name="data">The data to associate with this resource. This value is assigned to the Data property.</param>
    public Resource(TData data)
    {
        Data = data;
    }

    /// <inheritdoc/>
    public void AddEmbeddedResource<T>(string key, IEmbeddedResource<T> resource)
    {
        Embedded[key] = resource;
    }
}

public class Resource<TData, TMeta> : Resource<TData>, IResource<TData, TMeta>
{
    public TMeta Meta { get; init; }
    public Resource(TData data, TMeta meta) : base(data)
    {
        Meta = meta;
    }
}
