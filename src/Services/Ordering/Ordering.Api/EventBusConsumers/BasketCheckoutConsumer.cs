using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Contracts;
using Ordering.Application.Features.Orders.Commands;

namespace Ordering.Api.EventBusConsumers;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketCheckoutConsumer> _logger;
    private readonly ManualCurrentUserContext _manualCurrentUserContext;

    public BasketCheckoutConsumer(
        IMediator mediator,
        IMapper mapper,
        ILogger<BasketCheckoutConsumer> logger,
        ManualCurrentUserContext manualCurrentUserContext)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
        _manualCurrentUserContext = manualCurrentUserContext;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        _manualCurrentUserContext.CurrentUserName = context.Message.UserName;

        var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
        try
        {
            await _mediator.Send(command);
        }
        catch (Exception err)
        {
            _logger.LogError(err, "Error occured when consume BasketCheckoutEvent.");
        }
        _manualCurrentUserContext.CurrentUserName = null;
    }
}