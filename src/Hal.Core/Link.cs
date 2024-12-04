namespace Hal.Core;

public class Link
{
    public required string Href { get; init; }
    public required string Rel { get; init; }
    public HttpVerbs Method { get; init; }
}
