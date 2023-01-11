using MediatR;

namespace Ordering.Application.Features.Orders.Commands;

public class DeleteOrderCommand : IRequest
{
    public int Id { get; set; }
}