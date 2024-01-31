namespace Customers.Entities;

public class Customer : DomainEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}