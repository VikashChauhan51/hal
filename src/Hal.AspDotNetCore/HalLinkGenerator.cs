using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace Hal.AspDotNetCore;

public class HalLinkGenerator : IHalLinkGenerator
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;

    protected HttpContext? HttpContext => _httpContextAccessor?.HttpContext;

    protected EndpointDataSource? EndpointDataSource => HttpContext?.RequestServices.GetRequiredService<EndpointDataSource>();

    public string Host
    {
        get
        {
            var context = HttpContext;
            var request = context?.Request;

            if (request == null)
            {
                return string.Empty;
            }
            return GetFormatedPath($"{request.Scheme}://{request.Host}");
        }
    }


    public HalLinkGenerator(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
    {
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }

    public string? GenerateLink(string routeName, object values)
    {
        return _linkGenerator.GetPathByRouteValues(HttpContext!, routeName, values);
    }

    public string? GenerateLinkForAction(string actionName, string controllerName, object values)
    {
        return _linkGenerator.GetPathByAction(HttpContext!, actionName, controllerName, values);
    }


    public bool IsRouteNameValid(string routeName)
    {
        return EndpointDataSource?
            .Endpoints.
            OfType<RouteEndpoint>()
            .Any(e => e.RoutePattern?.RawText?.Contains(routeName, StringComparison.OrdinalIgnoreCase) ?? false) ?? false;
    }
    public bool IsActionAndControllerValid(string actionName, string controllerName)
    {
        return EndpointDataSource?
            .Endpoints
            .OfType<RouteEndpoint>()
            .Any(e => e.RoutePattern.RequiredValues.TryGetValue("action", out var action) &&
            action != null &&
            action.ToString().Equals(actionName, StringComparison.OrdinalIgnoreCase) &&
            e.RoutePattern.RequiredValues.TryGetValue("controller", out var controller) &&
            controller != null &&
            controller.ToString().Equals(controllerName, StringComparison.OrdinalIgnoreCase)) ?? false;
    }

    private static string GetFormatedPath(string path)
    {
        if (path == null)
        {
            return string.Empty;
        }

        if (path.StartsWith('/'))
        {
            var startReplaceRegex = new Regex("^(/)*");
            path = startReplaceRegex.Replace(path, string.Empty);
        }

        if (path.EndsWith('/'))
        {
            var endReplaceRegex = new Regex("(/)$");
            path = endReplaceRegex.Replace(path, string.Empty);
        }

        return path.ToLower();
    }

}
