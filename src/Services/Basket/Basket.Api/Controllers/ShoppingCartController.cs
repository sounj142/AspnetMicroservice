using AutoMapper;
using Basket.Api.Entities;
using Basket.Api.Models;
using Basket.Api.Repositories;
using Basket.Api.Services;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly DiscountGrpcService _discountService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public ShoppingCartController(
        IShoppingCartRepository shoppingCartRepository,
        DiscountGrpcService discountService,
        IMapper mapper,
        IPublishEndpoint publishEndpoint
        )
    {
        _shoppingCartRepository = shoppingCartRepository;
        _discountService = discountService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet("{userName}")]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetShoppingCart(string userName)
    {
        return Ok(await _shoppingCartRepository.GetShoppingCart(userName)
            ?? new ShoppingCart(userName));
    }

    [HttpPut]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateShoppingCart(ShoppingCart shoppingCart)
    {
        foreach (var item in shoppingCart.Items)
        {
            var coupon = await _discountService.GetDiscount(item.Name);
            item.DiscountAmount = coupon?.Amount ?? 0;
        }
        await _shoppingCartRepository.UpdateShoppingCart(shoppingCart);
        return Ok(shoppingCart);
    }

    [HttpDelete("{userName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteShoppingCart(string userName)
    {
        await _shoppingCartRepository.DeleteShoppingCart(userName);
        return Ok();
    }

    [HttpPost("Checkout")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Checkout(BasketCheckout basketCheckout)
    {
        var shoppingCart = await _shoppingCartRepository.GetShoppingCart(basketCheckout.UserName);
        if (shoppingCart == null)
            return BadRequest();

        var checkoutEvent = _mapper.Map<BasketCheckoutEvent>(shoppingCart);
        _mapper.Map(basketCheckout, checkoutEvent);
        await _publishEndpoint.Publish(checkoutEvent);

        await _shoppingCartRepository.DeleteShoppingCart(basketCheckout.UserName);
        return Accepted();
    }
}