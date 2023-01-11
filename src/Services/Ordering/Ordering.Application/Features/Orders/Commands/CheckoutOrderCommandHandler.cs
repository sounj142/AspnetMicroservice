using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(
        IOrderRepository orderRepository,
        IMapper mapper,
        IEmailService emailService,
        ILogger<CheckoutOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToCreate = _mapper.Map<Order>(request);

        var createdOrder = await _orderRepository.Add(orderToCreate);
        _logger.LogInformation($"Created order {createdOrder.Id}.");

        await SendEmail(createdOrder);

        return createdOrder.Id;
    }

    private async Task SendEmail(Order order)
    {
        if (string.IsNullOrEmpty(order.EmailAddress))
            return;
        try
        {
            await _emailService.SendEmail(new Email
            {
                To = order.EmailAddress,
                Body = $"Your order was created. Order id {order.Id}",
                Subject = "Your order was created."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error when sending created email for order {order.Id}");
        }
    }
}