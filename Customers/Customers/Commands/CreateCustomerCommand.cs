using Customers.Entities;
using Customers.Persistence;
using MediatR;

namespace Customers.Commands;

public record CreateCustomerCommand(Guid Id, string FirstName, string LastName) : IRequest;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand>
{
    private readonly IAppDbContext _context;

    public CreateCustomerHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        await _context.Customers.AddAsync(customer, cancellationToken);
        await _context.SaveAsync(cancellationToken);
    }
}