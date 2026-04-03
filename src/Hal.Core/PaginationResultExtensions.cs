namespace Hal.Core;

/// <summary>
/// Provides extension methods for working with pagination results and converting them to API responses.
/// </summary>
public static class PaginationResultExtensions
{
    /// <summary>
    /// Converts a pagination result to an API response with a success status.
    /// </summary>
    /// <typeparam name="T">The type of items in the pagination result.</typeparam>
    /// <param name="paginationResult">The pagination result to convert.</param>
    /// <returns>An API response containing the pagination result as data.</returns>
    /// <example>
    /// <code>
    /// var paginationResult = await _repository.GetProductsAsync(page, pageSize);
    /// return Ok(paginationResult.ToApiResponse());
    /// </code>
    /// </example>
    public static ApiResponse<PaginationResult<T>> ToApiResponse<T>(this PaginationResult<T> paginationResult)
    {
        return new ApiResponse<PaginationResult<T>>
        {
            Success = true,
            Data = paginationResult
        };
    }

    /// <summary>
    /// Converts the items in a pagination result to a new type using a selector function.
    /// </summary>
    /// <typeparam name="TSource">The source type of items.</typeparam>
    /// <typeparam name="TTarget">The target type to convert to.</typeparam>
    /// <param name="paginationResult">The pagination result to transform.</param>
    /// <param name="selector">The transformation function to apply to each item.</param>
    /// <returns>A new pagination result with transformed items.</returns>
    /// <example>
    /// <code>
    /// var productResult = await _repository.GetProductsAsync(page, pageSize);
    /// var dtoResult = productResult.Select(p => new ProductDto
    /// {
    ///     Id = p.Id,
    ///     Name = p.Name
    /// });
    /// </code>
    /// </example>
    public static PaginationResult<TTarget> Select<TSource, TTarget>(
        this PaginationResult<TSource> paginationResult,
        Func<TSource, TTarget> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);

        var transformedItems = paginationResult.Items.Select(selector);
        return new PaginationResult<TTarget>(
            transformedItems,
            paginationResult.TotalCount,
            paginationResult.PageNumber,
            paginationResult.PageSize
        );
    }
}
