namespace Hal.Core;

/// <summary>
/// Defines the contract for a hypermedia link, including its target URI, HTTP method, and relation type.
/// </summary>
/// <remarks>Implementations of this interface are typically used to represent links in RESTful APIs, enabling
/// clients to discover available actions and related resources. The properties correspond to standard link attributes
/// in hypermedia formats such as HAL or JSON:API.</remarks>
public interface ILink
{
    /// <summary>
    /// Gets the hyperlink reference (URL) associated with this instance.
    /// </summary>
    string Href { get; init; }

    /// <summary>
    /// Gets the HTTP verb used for the request.
    /// </summary>
    HttpVerbs Method { get; init; }

    /// <summary>
    /// Gets the relation type that describes how the current resource is related to another resource.
    /// </summary>
    /// <remarks>The relation type is typically expressed as a URI or a registered relation name, following
    /// conventions such as those defined by RFC 8288 (Web Linking). This property is commonly used in hypermedia APIs
    /// to indicate the semantic relationship between resources.</remarks>
    string Rel { get; init; }
}
