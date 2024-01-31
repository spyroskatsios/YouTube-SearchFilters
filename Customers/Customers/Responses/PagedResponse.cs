namespace Customers.Responses;

public record PagedResponse<T>(IEnumerable<T> Items, int CurrentPage, int PageSize, int TotalCount, 
    int TotalPages, bool HasPrevious, bool HasNext);