using Basket.Api.Entities;
using Basket.Api.Repositories;
using Basket.Api.Services;
using Discount.Grpc;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly DiscountGrpcService _discountService;

    public ShoppingCartController(
        IShoppingCartRepository shoppingCartRepository,
        DiscountGrpcService discountService
        )
    {
        _shoppingCartRepository = shoppingCartRepository;
        _discountService = discountService;
    }

    [HttpGet("{userName}")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetShoppingCart(string userName)
    {
        return Ok(await _shoppingCartRepository.GetShoppingCart(userName));
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateShoppingCart(ShoppingCart shoppingCart)
    {
        foreach (var item in shoppingCart.Items)
        {
            var coupon = await _discountService.GetDiscount(item.Name);
            item.DiscountAmount = coupon?.Amount ?? 0;
        }
        await _shoppingCartRepository.UpdateShoppingCart(shoppingCart);
        return Ok();
    }

    [HttpDelete("{userName}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteShoppingCart(string userName)
    {
        await _shoppingCartRepository.DeleteShoppingCart(userName);
        return Ok();
    }
}