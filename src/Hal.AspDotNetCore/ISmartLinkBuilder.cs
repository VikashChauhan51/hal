using Hal.Core;

namespace Hal.AspDotNetCore;
public interface ISmartLinkBuilder
{
    ILink Build();
    SmartLinkBuilder SetActionName(string actionName);
    SmartLinkBuilder SetControllerName(string controllerName);
    SmartLinkBuilder SetMethod(HttpVerbs method);
    SmartLinkBuilder SetRel(string rel);
    SmartLinkBuilder SetRouteName(string routeName);
    SmartLinkBuilder SetRouteValues(object routeValues);
}