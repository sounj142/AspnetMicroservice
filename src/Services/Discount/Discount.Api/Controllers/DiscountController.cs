using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountController(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    [HttpGet(Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetDiscount([FromQuery] string productName)
    {
        var coupon = await _discountRepository.GetDiscount(productName);
        return coupon == null ? NotFound() : Ok(coupon);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateDiscount(Coupon coupon)
    {
        var result = await _discountRepository.CreateDiscount(coupon);
        return CreatedAtRoute("GetDiscount", new { ProductName = coupon.ProductName }, result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateDiscount(Coupon coupon)
    {
        var result = await _discountRepository.UpdateDiscount(coupon);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteDiscount([FromQuery] string productName)
    {
        var result = await _discountRepository.DeleteDiscount(productName);
        return Ok(result);
    }
}