using Hal.Core.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hal.Core;

public class Link : ILink
{
    public required string Href { get; init; }
    public required string Rel { get; init; }

    [JsonConverter(typeof(HttpVerbsConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public HttpVerbs Method { get; init; }

    public override string ToString()
    {
        var jsonSerializerSettings = new HalJsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters =
            [
                new LinkConverter(),
                new HttpVerbsConverter()
            ],
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

        return JsonConvert.SerializeObject(this, jsonSerializerSettings);
    }
}
