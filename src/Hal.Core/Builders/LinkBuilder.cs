namespace Hal.Core.Builders;
public class LinkBuilder
{
    private string? _href;
    private string? _rel;
    private HttpVerbs _method;

    public LinkBuilder SetHref(string href)
    {
        _href = href;
        return this;
    }

    public LinkBuilder SetRel(string rel)
    {
        _rel = rel;
        return this;
    }

    public LinkBuilder SetMethod(HttpVerbs method)
    {
        _method = method;
        return this;
    }

    public Link Build()
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

