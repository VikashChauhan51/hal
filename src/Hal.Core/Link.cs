namespace Hal.Core;

/// <summary>
/// Represents a hypermedia link with a target URI, relationship type, and HTTP method.
/// </summary>
/// <remarks>Use this class to describe links in RESTful APIs, such as those included in resource representations
/// for HATEOAS (Hypermedia as the Engine of Application State) scenarios. Each instance specifies the link's target,
/// its relation to the current resource, and the HTTP method to use when following the link.</remarks>
public class Link : ILink
{
    /// <inheritdoc/>
    public required string Href { get; init; }

    /// <inheritdoc/>
    public required string Rel { get; init; }

    /// <inheritdoc/>
    public HttpVerbs Method { get; init; }
}
