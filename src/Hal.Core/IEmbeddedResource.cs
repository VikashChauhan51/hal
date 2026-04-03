namespace Hal.Core;

/// <summary>
/// Defines a contract for accessing an embedded resource of a specified type.
/// </summary>
/// <remarks>Implementations of this interface provide access to a resource that is embedded within another object
/// or context. The resource is typically read-only and may represent data such as files, configuration, or other assets
/// packaged with an application.</remarks>
/// <typeparam name="T">The type of the embedded resource exposed by the interface.</typeparam>
public interface IEmbeddedResource<out T>
{
    /// <summary>
    /// Gets the embedded resource or value of type T.
    /// </summary>
    T Embedded { get; }
}
