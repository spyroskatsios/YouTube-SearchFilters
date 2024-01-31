using Customers.Entities;
using Customers.Filters;
using Customers.Persistence;
using Customers.Results;
using Customers.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Customers.Queries;

public record SearchCustomersQuery(string? FirstName, string? LastName, string? SortBy, 
    EnSortOrder SortOrder, int PageSize, int PageNumber) : SearchFilters(SortBy, SortOrder, PageSize, PageNumber),
        IRequest<PaginatedList<Customer>>;
        

public class SearchCustomersHandler : IRequestHandler<SearchCustomersQuery, PaginatedList<Customer>>
{
    private readonly IAppDbContext _context;

    public SearchCustomersHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Customer>> Handle(SearchCustomersQuery request, CancellationToken cancellationToken)
    {
        var customersQueryable = _context.Customers.AsNoTracking().ApplyCustomerFilters(request);

        return new PaginatedList<Customer>(
            await customersQueryable.ApplyBaseFilters(request).ToListAsync(cancellationToken),
            await customersQueryable.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
    }
}
        