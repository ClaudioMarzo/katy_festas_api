namespace KatyFestas.API.Responses;

/// <summary>
/// Response padronizado de sucesso
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public string? CorrelationId { get; set; }
    public T? Data { get; set; }

    public ApiResponse() { }

    public ApiResponse(T data, string message, string? correlationId = null)
    {
        Data = data;
        Message = message;
        CorrelationId = correlationId;
    }
}

/// <summary>
/// Response para listagens paginadas
/// </summary>
public class PagedApiResponse<T> : ApiResponse<IEnumerable<T>>
{
    public PaginationMetadata Pagination { get; set; } = new();

    public PagedApiResponse(IEnumerable<T> data, int page, int pageSize, int totalItems, int totalPages, string message, string? correlationId = null)
    {
        Data = data;
        Message = message;
        CorrelationId = correlationId;
        Pagination = new PaginationMetadata
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            HasPreviousPage = page > 1,
            HasNextPage = page < totalPages
        };
    }
}

public class PaginationMetadata
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}
