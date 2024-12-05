namespace Hal.AspDotNetCore;

public interface IHalLinkGenerator
{
    public string Host { get; }
    string? GenerateLink(string routeName, object values);
    string? GenerateLinkForAction(string actionName, string controllerName, object values);
}
