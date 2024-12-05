using Hal.Core.Converters;
using Newtonsoft.Json;

namespace Hal.Core;

public class Link
{
    public required string Href { get; init; }
    public required string Rel { get; init; }
    public HttpVerbs Method { get; init; }

    public override string ToString()
    {
        var jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters = new List<JsonConverter>()
            {
                new LinkConverter()
            }
        };

        return  JsonConvert.SerializeObject(this, jsonSerializerSettings);
    }
}
