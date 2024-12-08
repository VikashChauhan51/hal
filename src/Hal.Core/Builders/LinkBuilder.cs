namespace Hal.Core.Builders;
public class LinkBuilder : ILinkBuilder
{
    private string? _href;
    private string? _rel;
    private HttpVerbs _method;

    public ILinkBuilder SetHref(string href)
    {
        _href = href;
        return this;
    }

    public ILinkBuilder SetRel(string rel)
    {
        _rel = rel;
        return this;
    }

    public ILinkBuilder SetMethod(HttpVerbs method)
    {
        _method = method;
        return this;
    }

    public ILink Build()
    {
        ArgumentNullException.ThrowIfNull(_href);
        ArgumentNullException.ThrowIfNull(_rel);
        return new Link
        {
            Href = _href,
            Rel = _rel,
            Method = _method
        };
    }
}

