using Customers.Mapping;
using Customers.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Controllers;

[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("customers")]
    public async Task<IActionResult> Get([FromQuery] SearchCustomersRequest request, CancellationToken cancellationToken)
    {
        var customers = await _mediator.Send(request.ToSearchCustomersQuery(), cancellationToken);
        return Ok(customers.ToPagedResponse());
    }
    
    [HttpPost("customers")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request.ToCustomerCommand(), cancellationToken);
        return Ok();
    }
}