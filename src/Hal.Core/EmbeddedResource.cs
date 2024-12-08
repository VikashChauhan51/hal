using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Hal.Core;
public class EmbeddedResource<T> : IEmbeddedResource<T>
{
    [JsonProperty("_embedded")]
    [JsonPropertyName("_embedded")]
    public T Embedded { get; init; }
    public EmbeddedResource(T embedded)
    {
        Embedded = embedded;
    }
}
