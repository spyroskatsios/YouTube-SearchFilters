using Customers.Shared;

namespace Customers.Filters;

public record SearchFilters(string? SortBy, EnSortOrder SortOrder, int PageSize, int PageNumber);