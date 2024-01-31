using Customers.Commands;
using Customers.Queries;
using Customers.Requests;
using Customers.Responses;
using Customers.Results;
using Customers.Shared;

namespace Customers.Mapping;

public static class CustomerMappers
{
    public static SearchCustomersQuery ToSearchCustomersQuery(this SearchCustomersRequest request)
        => new(request.FirstName, request.LastName, request.SortBy, request.SortOrder == Constants.Ascending ? EnSortOrder.Ascending : EnSortOrder.Descending, request.PageSize, request.PageNumber);

    public static PagedResponse<T> ToPagedResponse<T>(this PaginatedList<T> list)
        => new(list, list.MetaData.CurrentPage, list.MetaData.PageSize, list.MetaData.TotalCount, list.MetaData.TotalPages, list.MetaData.HasPrevious, list.MetaData.HasNext);
    
    public static CreateCustomerCommand ToCustomerCommand(this CreateCustomerRequest request)
        => new(request.Id, request.FirstName, request.LastName);
}