using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Dtos;

namespace Ordering.Application.Features.Orders.Queries;

public class GetOrdersListQuery : IRequest<OrderDto[]>
{
    public string UserName { get; set; } = string.Empty;
}

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, OrderDto[]>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto[]> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByUserName(request.UserName);

        return _mapper.Map<OrderDto[]>(orders);
    }
}