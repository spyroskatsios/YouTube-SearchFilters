namespace Customers.Requests;

public record CreateCustomerRequest(Guid Id, string FirstName, string LastName);