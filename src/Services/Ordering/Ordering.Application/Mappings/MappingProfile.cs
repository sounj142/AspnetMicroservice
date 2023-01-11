using AutoMapper;
using Ordering.Application.Dtos;
using Ordering.Application.Features.Orders.Commands;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();

        CreateMap<CheckoutOrderCommand, Order>();
        CreateMap<UpdateOrderCommand, Order>();
    }
}