using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Dtos;
using Ordering.Application.Features.Orders.Commands;

namespace Ordering.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>();
        CreateMap<BasketCheckoutItem, OrderItemDto>();
    }
}