namespace Hal.Core;

/// <summary>
/// Represents a base resource in a RESTful API following HATEOAS (Hypermedia as the Engine of Application State) principles.
/// A resource serves as the foundation for hypermedia-driven APIs, providing links to related resources and actions.
/// </summary>
/// <remarks>
/// This interface implements the core HATEOAS constraint of REST architecture, allowing clients to
/// dynamically discover available actions and navigate through the API without prior knowledge of URIs.
/// Resources can contain links to themselves, related resources, and available state transitions.
/// </remarks>
public interface IResource
{
    /// <summary>
    /// Gets the collection of links associated with this resource.
    /// Links represent hypermedia controls that inform clients about possible actions and relationships.
    /// </summary>
    /// <value>
    /// A set of <see cref="ILink"/> objects that provide hypermedia controls for the resource.
    /// Common links include "self", "related", "collection", "next", "previous", and action links like "update", "delete", or "create".
    /// </value>
    /// <remarks>
    /// The set is typically immutable after resource construction, though implementations may
    /// allow modifications before the resource is returned to the client. Links are essential
    /// for enabling dynamic discovery and reducing client-server coupling.
    /// </remarks>
    ISet<ILink> Links { get; }

    /// <summary>
    /// Adds a hypermedia link to the resource's collection of links.
    /// </summary>
    /// <param name="link">The link to add, containing relation type, URI, and optional metadata such as HTTP method or content type.</param>
    /// <remarks>
    /// Links should be added during resource construction or response formatting. Common link relations include:
    /// <list type="bullet">
    /// <item><description>"self" - A canonical reference to the resource itself</description></item>
    /// <item><description>"collection" - The collection containing this resource</description></item>
    /// <item><description>"related" - A related resource</description></item>
    /// <item><description>"next"/"previous" - For paginated collections</description></item>
    /// <item><description>"create"/"update"/"delete" - Available state transitions</description></item>
    /// </list>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="link"/> is null.</exception>
    void AddLink(ILink link);
}

/// <summary>
/// Represents a typed resource in a RESTful API that contains application data along with hypermedia links.
/// This interface extends the base <see cref="IResource"/> by adding strongly-typed data payload support.
/// </summary>
/// <typeparam name="TData">The type of the resource's data payload. Must be covariant (out) to allow more derived types.</typeparam>
/// <remarks>
/// This interface is suitable for resources that primarily expose business data while maintaining
/// HATEOAS hypermedia controls. The data payload typically contains the actual resource attributes
/// (e.g., product details, user profile, order information), while links provide navigation and action options.
/// </remarks>
/// <example>
/// <code>
/// public class ProductResource : IResource&lt;ProductDto&gt;
/// {
///     public ProductDto Data { get; }
///     public ISet&lt;ILink&gt; Links { get; }
///     
///     public void AddLink(ILink link) { ... }
///     public void AddEmbeddedResource&lt;T&gt;(string key, IEmbeddedResource&lt;T&gt; resource) { ... }
/// }
/// </code>
/// </example>
public interface IResource<out TData> : IResource
{
    /// <summary>
    /// Gets the strongly-typed data payload of the resource.
    /// The data represents the actual content or state of the resource being exposed by the API.
    /// </summary>
    /// <value>
    /// An instance of <typeparamref name="TData"/> containing the resource's business data.
    /// This could be a DTO (Data Transfer Object), entity, or any other application-specific type.
    /// </value>
    /// <remarks>
    /// The data payload is covariant (out), meaning you can assign a resource with a more derived
    /// data type to a variable expecting a resource with a less derived data type.
    /// For read-only operations, this is typically populated during resource construction and remains immutable.
    /// </remarks>
    TData Data { get; }

    /// <summary>
    /// Adds an embedded resource to this resource, allowing hierarchical or related resources to be included
    /// directly within the response rather than requiring additional requests.
    /// </summary>
    /// <typeparam name="T">The type of data contained within the embedded resource.</typeparam>
    /// <param name="key">The unique identifier or relation name for the embedded resource (e.g., "author", "comments", "relatedProducts").</param>
    /// <param name="resource">The embedded resource to include, which implements <see cref="IEmbeddedResource{T}"/>.</param>
    /// <remarks>
    /// Embedded resources are useful for:
    /// <list type="bullet">
    /// <item><description>Reducing the number of round trips required by the client</description></item>
    /// <item><description>Including related data that is frequently accessed together</description></item>
    /// <item><description>Providing a complete representation of an aggregate root with its children</description></item>
    /// <item><description>Optimizing API performance by batching related data into a single response</description></item>
    /// </list>
    /// While links tell clients where to find related resources, embedded resources directly include them.
    /// This follows the HATEOAS principle where resources can contain both links and embedded representations.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> or <paramref name="resource"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when a resource with the same <paramref name="key"/> already exists.</exception>
    void AddEmbeddedResource<T>(string key, IEmbeddedResource<T> resource);
}

/// <summary>
/// Represents a complete RESTful resource that includes both data payload and metadata, along with hypermedia links.
/// This interface extends <see cref="IResource{TData}"/> to support additional metadata information about the resource.
/// </summary>
/// <typeparam name="TData">The type of the resource's data payload. Must be covariant (out).</typeparam>
/// <typeparam name="TMeta">The type of the resource's metadata. Must be covariant (out).</typeparam>
/// <remarks>
/// Metadata provides supplementary information about the resource that is not part of the core business data.
/// This separation allows for clean distinction between the actual content and supporting information such as:
/// <list type="bullet">
/// <item><description>Pagination metadata (total count, page number, page size)</description></item>
/// <item><description>Timestamps (created at, updated at, expires at)</description></item>
/// <item><description>Versioning information (ETag, version number)</description></item>
/// <item><description>Authorization and permission metadata</description></item>
/// <item><description>Audit information (created by, modified by)</description></item>
/// <item><description>Resource statistics and aggregates</description></item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Example of a paginated collection response
/// public class ProductListResource : IResource&lt;IEnumerable&lt;ProductDto&gt;, PaginationMeta&gt;
/// {
///     public IEnumerable&lt;ProductDto&gt; Data { get; }
///     public PaginationMeta Meta { get; }  // Contains TotalCount, PageNumber, PageSize
///     public ISet&lt;ILink&gt; Links { get; }
/// }
/// </code>
/// </example>
public interface IResource<out TData, out TMeta> : IResource<TData>
{
    /// <summary>
    /// Gets the metadata associated with this resource.
    /// Metadata provides supplementary information about the resource that is not part of the core data payload.
    /// </summary>
    /// <value>
    /// An instance of <typeparamref name="TMeta"/> containing the resource's metadata.
    /// The metadata structure is typically designed to be optional and can vary by resource type or context.
    /// </value>
    /// <remarks>
    /// Metadata is covariant (out), allowing you to assign a resource with more derived metadata
    /// to a variable expecting a resource with less derived metadata.
    /// 
    /// Common metadata scenarios include:
    /// <list type="bullet">
    /// <item><description><b>Collections</b> - Total count, page information, filtering/sorting metadata</description></item>
    /// <item><description><b>Individual Resources</b> - ETag for caching, last modified timestamps, version numbers</description></item>
    /// <item><description><b>Audit Trails</b> - Creation and modification user information</description></item>
    /// <item><description><b>Rate Limiting</b> - Remaining requests, reset times, quota information</description></item>
    /// <item><description><b>Hypermedia Profiles</b> - Available actions, supported media types, schema information</description></item>
    /// </list>
    /// 
    /// For resources that don't require metadata, consider using <see cref="IResource{TData}"/> instead.
    /// </remarks>
    TMeta Meta { get; }
}
