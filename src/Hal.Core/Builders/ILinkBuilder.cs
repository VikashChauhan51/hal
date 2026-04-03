namespace Hal.Core.Builders;

/// <summary>
/// Defines a builder for constructing link objects with configurable properties such as href, HTTP method, and relation
/// type.
/// </summary>
/// <remarks>Implementations of this interface typically use a fluent API, allowing method chaining to set link
/// properties before building the final link object. This interface is commonly used to create hypermedia links in
/// RESTful APIs.</remarks>
public interface ILinkBuilder
{
    /// <summary>
    /// Creates and returns a new instance of an object that implements the ILink interface.
    /// </summary>
    /// <returns>An object that implements the ILink interface, representing the constructed link.</returns>
    ILink Build();

    /// <summary>
    /// Sets the hyperlink reference (href) for the link being built.
    /// </summary>
    /// <param name="href">The URL to assign to the link's href attribute. Cannot be null or empty.</param>
    /// <returns>The current instance of the link builder with the updated href, enabling method chaining.</returns>
    ILinkBuilder SetHref(string href);

    /// <summary>
    /// Specifies the HTTP method to use when building the link.
    /// </summary>
    /// <param name="method">The HTTP method to associate with the link. Typically one of the values defined in <see cref="HttpVerbs"/>.</param>
    /// <returns>The current <see cref="ILinkBuilder"/> instance for method chaining.</returns>
    ILinkBuilder SetMethod(HttpVerbs method);

    /// <summary>
    /// Sets the relation type for the link being built.
    /// </summary>
    /// <param name="rel">The relation type to associate with the link. This value typically indicates the nature of the link's target,
    /// such as "self", "next", or a custom relation. Cannot be null or empty.</param>
    /// <returns>The current instance of the link builder with the specified relation type set. This enables method chaining.</returns>
    ILinkBuilder SetRel(string rel);
}
