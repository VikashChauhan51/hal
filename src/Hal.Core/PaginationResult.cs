namespace Hal.Core;

/// <summary>
/// Represents a pagination result from database queries containing paginated data with metadata.
/// This class is used to encapsulate the results of paginated database operations along with pagination information.
/// </summary>
/// <remarks>
/// Use this type to return paginated results from repository methods or data access layer operations.
/// It provides essential pagination metadata that can be used to build UI controls or API responses.
/// This class is a simple DTO and does not perform any database queries itself.
/// </remarks>
/// <example>
/// <code>
/// // Usage in a repository method
/// public async Task&lt;PaginationResult&lt;Product&gt;&gt; GetProductsAsync(int page, int pageSize, CancellationToken ct = default)
/// {
///     var query = _context.Products.AsQueryable();
///     var totalCount = await query.CountAsync(ct);
///     
///     var items = await query
///         .Skip((page - 1) * pageSize)
///         .Take(pageSize)
///         .ToListAsync(ct);
///     
///     return new PaginationResult&lt;Product&gt;(items, totalCount, page, pageSize);
/// }
/// 
/// // Converting to API response
/// var paginationResult = await _productRepository.GetProductsAsync(page, pageSize);
/// var apiResponse = new ApiResponse&lt;PaginationResult&lt;ProductDto&gt;&gt;
/// {
///     Success = true,
///     Data = paginationResult
/// };
/// </code>
/// </remarks>
public record PaginationResult
{
    /// <summary>
    /// Gets the total number of items available across all pages.
    /// This count represents the complete set of items matching the query criteria before pagination is applied.
    /// </summary>
    /// <value>
    /// A non-negative integer representing the total count of items.
    /// Zero indicates that no items match the query criteria.
    /// </value>
    /// <remarks>
    /// This value is used to calculate total pages and to determine if pagination controls should be displayed.
    /// It is independent of the current page's item count.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Display total count information
    /// Console.WriteLine($"Found {paginationResult.TotalCount} products total");
    /// Console.WriteLine($"Showing page {paginationResult.PageNumber} of {paginationResult.TotalPages}");
    /// </code>
    /// </example>
    public int TotalCount { get; init; }

    /// <summary>
    /// Gets the current page number (1-based).
    /// Page numbers start at 1, representing the first page of results.
    /// </summary>
    /// <value>
    /// A positive integer representing the current page number.
    /// Valid values range from 1 to <see cref="TotalPages"/>.
    /// </value>
    /// <remarks>
    /// This value is typically provided by the client as a query parameter.
    /// Invalid page numbers (e.g., negative numbers or zero) should be normalized to 1 by the data access layer.
    /// </remarks>
    public int PageNumber { get; init; }

    /// <summary>
    /// Gets the number of items per page.
    /// This value determines how many items are returned in the current page's item collection.
    /// </summary>
    /// <value>
    /// A positive integer representing the maximum number of items to return per page.
    /// Typical values range from 10 to 100, depending on the application's requirements.
    /// </value>
    /// <remarks>
    /// The actual number of items returned in <see cref="PaginationResult{T}.Items"/> may be less than this value
    /// if the last page contains fewer items.
    /// 
    /// Consider implementing maximum page size limits to prevent performance issues:
    /// <code>
    /// pageSize = Math.Min(pageSize, MaxPageSize);
    /// </code>
    /// </remarks>
    public int PageSize { get; init; }

    /// <summary>
    /// Gets the total number of pages available based on <see cref="TotalCount"/> and <see cref="PageSize"/>.
    /// This value is calculated as the ceiling of TotalCount divided by PageSize.
    /// </summary>
    /// <value>
    /// A non-negative integer representing the total number of pages.
    /// Returns 0 when TotalCount is 0, otherwise returns at least 1.
    /// </value>
    /// <remarks>
    /// This is a calculated property that is not persisted in the database.
    /// Example calculations:
    /// <list type="bullet">
    /// <item><description>TotalCount = 0 → TotalPages = 0</description></item>
    /// <item><description>TotalCount = 5, PageSize = 10 → TotalPages = 1</description></item>
    /// <item><description>TotalCount = 25, PageSize = 10 → TotalPages = 3</description></item>
    /// </list>
    /// </remarks>
    public int TotalPages => TotalCount == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// Gets a value indicating whether there is a previous page available.
    /// Returns true when the current page is not the first page.
    /// </summary>
    /// <value>
    /// <c>true</c> if <see cref="PageNumber"/> is greater than 1; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property is useful for enabling/disabling "Previous" buttons in UI controls.
    /// </remarks>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Gets a value indicating whether there is a next page available.
    /// Returns true when the current page is not the last page.
    /// </summary>
    /// <value>
    /// <c>true</c> if <see cref="PageNumber"/> is less than <see cref="TotalPages"/>; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property is useful for enabling/disabling "Next" buttons in UI controls
    /// or for determining if more data can be loaded (e.g., infinite scrolling).
    /// </remarks>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Gets the zero-based index of the first item in the current page.
    /// This value represents the starting position in the complete dataset before pagination.
    /// </summary>
    /// <value>
    /// A non-negative integer representing the offset from the beginning of the dataset.
    /// Calculated as (<see cref="PageNumber"/> - 1) * <see cref="PageSize"/>.
    /// </value>
    /// <remarks>
    /// This property is useful when implementing pagination with LINQ's Skip() method:
    /// <code>
    /// var items = query.Skip(paginationResult.Offset).Take(paginationResult.PageSize);
    /// </code>
    /// </remarks>
    public int Offset => (PageNumber - 1) * PageSize;

    /// <summary>
    /// Gets the index of the last item in the current page (1-based, global across all pages).
    /// This value represents the global position of the last item in the complete dataset.
    /// </summary>
    /// <value>
    /// A positive integer representing the global index of the last item in the current page.
    /// Calculated as the minimum of <see cref="PageNumber"/> * <see cref="PageSize"/> and <see cref="TotalCount"/>.
    /// </value>
    /// <remarks>
    /// Example: If PageNumber = 2, PageSize = 10, and TotalCount = 25, then LastItemIndex = 20.
    /// On the last page, this value equals TotalCount.
    /// </remarks>
    public int LastItemIndex => Math.Min(PageNumber * PageSize, TotalCount);

    /// <summary>
    /// Gets the index of the first item in the current page (1-based, global across all pages).
    /// This value represents the global position of the first item in the complete dataset.
    /// </summary>
    /// <value>
    /// A positive integer representing the global index of the first item in the current page.
    /// Calculated as ((<see cref="PageNumber"/> - 1) * <see cref="PageSize"/>) + 1.
    /// Returns 0 if there are no items.
    /// </value>
    /// <remarks>
    /// Example: If PageNumber = 2 and PageSize = 10, then FirstItemIndex = 11.
    /// This property is useful for displaying item ranges to users (e.g., "Showing 11-20 of 100 items").
    /// </remarks>
    public int FirstItemIndex => TotalCount == 0 ? 0 : Offset + 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationResult"/> class with specified pagination parameters.
    /// </summary>
    /// <param name="totalCount">The total number of items available across all pages.</param>
    /// <param name="pageNumber">The current page number (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="pageNumber"/> is less than 1 or <paramref name="pageSize"/> is less than 1.
    /// </exception>
    public PaginationResult(int totalCount, int pageNumber, int pageSize)
    {
        if (pageNumber < 1) throw new ArgumentException("Page number must be at least 1.", nameof(pageNumber));
        if (pageSize < 1) throw new ArgumentException("Page size must be at least 1.", nameof(pageSize));

        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

/// <summary>
/// Represents a pagination result from database queries containing typed items with pagination metadata.
/// This class extends <see cref="PaginationResult"/> to include the actual data items for the current page.
/// </summary>
/// <typeparam name="T">The type of items contained in the paginated result.</typeparam>
/// <remarks>
/// Use this type as the return value for repository methods that support pagination.
/// It provides both the paginated items and the metadata needed for client-side pagination controls.
/// This generic version allows strongly-typed access to the paginated data.
/// This is a simple DTO and does not perform any database operations.
/// </remarks>
/// <example>
/// <code>
/// // Creating a pagination result from already fetched data
/// var items = await _dbContext.Products
///     .Skip((page - 1) * pageSize)
///     .Take(pageSize)
///     .ToListAsync();
///     
/// var totalCount = await _dbContext.Products.CountAsync();
///     
/// var result = new PaginationResult&lt;Product&gt;(items, totalCount, page, pageSize);
/// 
/// // Iterating through items
/// foreach (var item in result.Items)
/// {
///     Console.WriteLine($"{item.Id}: {item.Name}");
/// }
/// 
/// // Displaying pagination info
/// Console.WriteLine($"Showing {result.FirstItemIndex}-{result.LastItemIndex} of {result.TotalCount} items");
/// 
/// // Creating navigation links
/// var navigation = new
/// {
///     Previous = result.HasPreviousPage ? $"/api/products?page={result.PageNumber - 1}" : null,
///     Next = result.HasNextPage ? $"/api/products?page={result.PageNumber + 1}" : null,
///     CurrentPage = result.PageNumber,
///     TotalPages = result.TotalPages
/// };
/// </code>
/// </remarks>
public record PaginationResult<T> : PaginationResult
{
    /// <summary>
    /// Gets the collection of items for the current page.
    /// This is the actual data returned from the database query after applying pagination.
    /// </summary>
    /// <value>
    /// An enumerable collection of type <typeparamref name="T"/> containing the items for the current page.
    /// Returns an empty collection if there are no items on the current page.
    /// </value>
    /// <remarks>
    /// The number of items in this collection will be between 0 and <see cref="PaginationResult.PageSize"/>.
    /// The last page may contain fewer items than the page size.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Check if there are items
    /// if (!paginationResult.Items.Any())
    /// {
    ///     return NotFound("No products found");
    /// }
    /// 
    /// // Transform items to DTOs
    /// var dtos = paginationResult.Items.Select(p => new ProductDto
    /// {
    ///     Id = p.Id,
    ///     Name = p.Name,
    ///     Price = p.Price
    /// });
    /// 
    /// // Count items on current page
    /// var currentPageCount = paginationResult.Items.Count();
    /// Console.WriteLine($"Current page has {currentPageCount} items");
    /// </code>
    /// </example>
    public IEnumerable<T> Items { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationResult{T}"/> class with items and pagination parameters.
    /// </summary>
    /// <param name="items">The collection of items for the current page.</param>
    /// <param name="totalCount">The total number of items available across all pages.</param>
    /// <param name="pageNumber">The current page number (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="items"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="pageNumber"/> is less than 1 or <paramref name="pageSize"/> is less than 1.
    /// </exception>
    /// <example>
    /// <code>
    /// // Creating a pagination result
    /// var products = await GetProductsFromDatabaseAsync(page, pageSize);
    /// var totalCount = await GetTotalProductCountAsync();
    /// 
    /// var result = new PaginationResult&lt;Product&gt;(
    ///     products,
    ///     totalCount,
    ///     page,
    ///     pageSize
    /// );
    /// </code>
    /// </example>
    public PaginationResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        : base(totalCount, pageNumber, pageSize)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
    }

    /// <summary>
    /// Creates an empty pagination result with no items.
    /// This factory method is useful for scenarios where no data is found or when returning default responses.
    /// </summary>
    /// <param name="pageNumber">The current page number (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A new instance of <see cref="PaginationResult{T}"/> with an empty items collection and zero total count.</returns>
    /// <example>
    /// <code>
    /// // Return empty result when no data matches the criteria
    /// if (!products.Any())
    /// {
    ///     return PaginationResult&lt;Product&gt;.Empty(page, pageSize);
    /// }
    /// </code>
    /// </example>
    public static PaginationResult<T> Empty(int pageNumber = 1, int pageSize = 10)
    {
        return new PaginationResult<T>(Array.Empty<T>(), 0, pageNumber, pageSize);
    }
}
