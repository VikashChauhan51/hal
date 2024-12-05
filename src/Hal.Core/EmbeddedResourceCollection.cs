using Newtonsoft.Json;
using System.Collections;

namespace Hal.Core;
public class EmbeddedResourceCollection<T> : IEmbeddedResourceCollection<T>, ICollection<T>
{
    private readonly ICollection<T> _embeddedResources = new List<T>();

    [JsonProperty("_embedded")]
    public IEnumerable<T> Embedded => _embeddedResources;

    public int Count => _embeddedResources.Count;

    public bool IsReadOnly => _embeddedResources.IsReadOnly;

    public EmbeddedResourceCollection(IEnumerable<T> embedded)
    {
        foreach (var embeddedResource in embedded)
        {
            _embeddedResources.Add(embeddedResource);
        }
    }

    public EmbeddedResourceCollection(ICollection<T> embedded)
    {
        _embeddedResources = embedded;
    }

    public void Add(T item)
    {
        _embeddedResources.Add(item);
    }

    public void Clear()
    {
        _embeddedResources.Clear();
    }

    public bool Contains(T item)
    {
        return _embeddedResources.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _embeddedResources.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return _embeddedResources.Remove(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _embeddedResources.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _embeddedResources.GetEnumerator();
    }
}
