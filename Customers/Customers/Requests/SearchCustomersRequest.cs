using Customers.Shared;

namespace Customers.Requests;

public record SearchCustomersRequest(string? FirstName, string? LastName, string? SortBy, string? SortOrder,
    int PageSize, int PageNumber) : SearchRequest(SortBy, SortOrder, PageSize, PageNumber); 