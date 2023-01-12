using AutoMapper;
using Basket.Api.Entities;
using Basket.Api.Models;
using EventBus.Messages.Events;

namespace Basket.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BasketCheckout, BasketCheckoutEvent>();

        CreateMap<ShoppingCart, BasketCheckoutEvent>()
            .ForMember(d => d.OrderItems, cfg => cfg.MapFrom(s => s.Items));

        CreateMap<ShopingCartItem, BasketCheckoutItem>()
            .ForMember(d => d.ProductName, cfg => cfg.MapFrom(s => s.Name));
    }
}