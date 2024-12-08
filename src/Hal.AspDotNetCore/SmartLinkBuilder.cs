using Hal.Core;

namespace Hal.AspDotNetCore;
public class SmartLinkBuilder : ISmartLinkBuilder
{
    private readonly IHalLinkGenerator _halLinkGenerator;
    private string? _routeName;
    private string? _rel;
    private object? _routeValues;
    private HttpVerbs _method = HttpVerbs.Get;
    private string? _actionName;
    private string? _controllerName;

    public SmartLinkBuilder(IHalLinkGenerator halLinkGenerator)
    {
        _halLinkGenerator = halLinkGenerator ?? throw new ArgumentNullException(nameof(halLinkGenerator));
    }

    public SmartLinkBuilder SetRouteName(string routeName)
    {
        _routeName = routeName;
        return this;
    }

    public SmartLinkBuilder SetActionName(string actionName)
    {
        _actionName = actionName;
        return this;
    }

    public SmartLinkBuilder SetControllerName(string controllerName)
    {
        _controllerName = controllerName;
        return this;
    }

    public SmartLinkBuilder SetRel(string rel)
    {
        _rel = rel;
        return this;
    }

    public SmartLinkBuilder SetMethod(HttpVerbs method)
    {
        _method = method;
        return this;
    }

    public SmartLinkBuilder SetRouteValues(object routeValues)
    {
        _routeValues = routeValues;
        return this;
    }

    public ILink Build()
    {
        if (string.IsNullOrEmpty(_routeName) &&
            (string.IsNullOrEmpty(_actionName) || string.IsNullOrEmpty(_controllerName)))
        {
            throw new InvalidOperationException("Either RouteName or Action/Controller must be set.");
        }

        string? href = null;

        if (!string.IsNullOrEmpty(_routeName))
        {
            href = _halLinkGenerator.GenerateUri(_routeName, _routeValues ?? new { });
        }
        else if (!string.IsNullOrEmpty(_actionName) && !string.IsNullOrEmpty(_controllerName))
        {
            href = _halLinkGenerator.GenerateUriForAction(_actionName, _controllerName, _routeValues ?? new { });
        }

        if (string.IsNullOrEmpty(href))
        {
            throw new InvalidOperationException("Failed to generate a valid URI.");
        }

        return new Link
        {
            Href = href,
            Method = _method,
            Rel = _rel ?? "self"
        };
    }
}


