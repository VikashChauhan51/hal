using Newtonsoft.Json;


namespace Hal.Core;
public class HalJsonSerializerSettings : JsonSerializerSettings
{
    public string? Meta { get; set; }
}
