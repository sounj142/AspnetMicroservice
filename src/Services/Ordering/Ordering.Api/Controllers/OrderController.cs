using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.Models;
using Ordering.Application.Dtos;
using Ordering.Application.Features.Orders.Commands;
using Ordering.Application.Features.Orders.Queries;

namespace Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersByUserName([FromQuery] string userName)
        {
            var query = new GetOrdersListQuery { UserName = userName };
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}