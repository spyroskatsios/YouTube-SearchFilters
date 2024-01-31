using Customers.Entities;
using Customers.Queries;
using Customers.Shared;

namespace Customers.Filters;

public static class CustomersFilters
{
    public static IQueryable<Customer> ApplyCustomerFilters(this IQueryable<Customer> customers,
        SearchCustomersQuery query)
    {
        if (!query.FirstName.IsNullOrEmpty())
            customers = customers.Where(x => x.FirstName.Contains(query.FirstName!));
        
        if (!query.LastName.IsNullOrEmpty())
            customers = customers.Where(x => x.LastName.Contains(query.LastName!));

        return customers;
    }  
}