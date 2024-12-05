using Newtonsoft.Json;

namespace Hal.Core;
public class EmbeddedResource<T> : IEmbeddedResource<T>
{
    [JsonProperty("_embedded")]
    public T Embedded { get; init; }
    public EmbeddedResource(T embedded)
    {
        Embedded = embedded;
    }
}
