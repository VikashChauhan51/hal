namespace Hal.Core;

/// <summary>
/// Represents the result of an API operation, including success status and error details.
/// </summary>
/// <remarks>Use this type to convey the outcome of an API call, including whether it succeeded and, if not,
/// details about the error. The error metadata can provide additional context for error handling or
/// diagnostics.</remarks>
public record ApiResponse
{
    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Gets the error message associated with the current operation.
    /// </summary>
    public string Error { get; init; } = string.Empty;

    /// <summary>
    /// Gets additional metadata associated with the error as a read-only dictionary.
    /// </summary>
    /// <remarks>The dictionary contains key-value pairs that provide supplementary information about the
    /// error. Keys are case-sensitive and should be unique within the dictionary. The contents and structure of the
    /// metadata depend on the context in which the error occurred.</remarks>
    public IReadOnlyDictionary<string, object> ErrorMeta { get; init; } = new Dictionary<string, object>(StringComparer.Ordinal);

}

/// <summary>
/// Represents an API response that includes a strongly typed data payload.
/// </summary>
/// <remarks>Use this type to encapsulate both the result data and any additional response metadata when handling
/// API calls. The generic parameter allows the response to carry data of any type, providing flexibility for various
/// API endpoints.</remarks>
/// <typeparam name="T">The type of the data returned in the response.</typeparam>
public record ApiResponse<T> : ApiResponse
{
    /// <summary>
    /// Gets the data associated with the current result.
    /// </summary>
    public T? Data { get; init; }
}
