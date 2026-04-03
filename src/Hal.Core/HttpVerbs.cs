namespace Hal.Core;

/// <summary>
/// Represents the standard HTTP methods (verbs) used in HTTP requests.
/// Each verb indicates the desired action to be performed on the identified resource.
/// </summary>
/// <remarks>
/// HTTP verbs (or methods) are the foundation of RESTful communication and define 
/// the semantics of a request. This enum provides a type-safe way to work with 
/// HTTP methods in .NET applications.
/// </remarks>
public enum HttpVerbs : byte
{
    /// <summary>
    /// The HTTP GET method requests a representation of the specified resource.
    /// GET requests should only retrieve data and should have no other effect on the server.
    /// </summary>
    /// <remarks>
    /// GET is considered a safe and idempotent method. It can be cached, bookmarked,
    /// and remains in browser history. GET requests have length limitations and 
    /// should never be used for sensitive data or state-changing operations.
    /// </remarks>
    Get = 1,

    /// <summary>
    /// The HTTP POST method submits data to be processed to a specified resource.
    /// POST is often used to create new resources or submit form data.
    /// </summary>
    /// <remarks>
    /// POST is neither safe nor idempotent. Multiple identical POST requests may 
    /// create multiple resources or have different effects. POST requests are not 
    /// cached by default and do not remain in browser history.
    /// </remarks>
    Post = 2,

    /// <summary>
    /// The HTTP PUT method replaces all current representations of the target resource
    /// with the request payload. It is typically used to update existing resources.
    /// </summary>
    /// <remarks>
    /// PUT is idempotent but not considered safe. Multiple identical PUT requests 
    /// should have the same effect as a single request. PUT typically requires the 
    /// client to specify the complete resource representation.
    /// </remarks>
    Put = 3,

    /// <summary>
    /// The HTTP DELETE method removes the specified resource from the server.
    /// After successful deletion, the resource should no longer be accessible.
    /// </summary>
    /// <remarks>
    /// DELETE is idempotent but not safe. Multiple identical DELETE requests should 
    /// have the same effect as a single request. The response may not include the 
    /// deleted resource, and subsequent GET requests should return 404 Not Found.
    /// </remarks>
    Delete = 4,

    /// <summary>
    /// The HTTP PATCH method applies partial modifications to a resource.
    /// Unlike PUT which replaces the entire resource, PATCH only updates specified fields.
    /// </summary>
    /// <remarks>
    /// PATCH is neither safe nor guaranteed to be idempotent. It is useful for 
    /// reducing bandwidth when only specific fields need updating. The request body 
    /// contains a set of instructions describing how the resource should be modified.
    /// </remarks>
    Patch = 5,

    /// <summary>
    /// The HTTP HEAD method requests the headers that would be returned if the specified 
    /// resource was requested with an HTTP GET method, without the response body.
    /// </summary>
    /// <remarks>
    /// HEAD is safe and idempotent. It is useful for checking resource existence,
    /// testing hypertext links, or checking for recent modifications without 
    /// downloading the entire resource payload.
    /// </remarks>
    Head = 6,

    /// <summary>
    /// The HTTP OPTIONS method describes the communication options available for 
    /// the target resource. It allows clients to determine the capabilities of a server.
    /// </summary>
    /// <remarks>
    /// OPTIONS is a safe and idempotent method. It is commonly used in CORS 
    /// (Cross-Origin Resource Sharing) preflight requests to determine which HTTP 
    /// methods and headers are supported by the server for cross-origin requests.
    /// </remarks>
    Options = 7,

    /// <summary>
    /// The HTTP TRACE method performs a message loop-back test along the path to 
    /// the target resource, providing a useful debugging mechanism.
    /// </summary>
    /// <remarks>
    /// TRACE is a safe and idempotent method. The final recipient of the request 
    /// should reflect the message received back to the client as the response body.
    /// Due to security concerns (potential for cross-site tracing attacks), TRACE 
    /// is often disabled in production environments.
    /// </remarks>
    Trace = 8,

    /// <summary>
    /// The HTTP CONNECT method establishes a tunnel to the server identified by 
    /// the target resource, typically used for SSL/TLS-encrypted communications.
    /// </summary>
    /// <remarks>
    /// CONNECT is neither safe nor idempotent. It is primarily used with proxy 
    /// servers to create a network tunnel for HTTPS connections, allowing the client 
    /// to establish an end-to-end encrypted connection through an intermediary proxy.
    /// </remarks>
    Connect = 9
}
