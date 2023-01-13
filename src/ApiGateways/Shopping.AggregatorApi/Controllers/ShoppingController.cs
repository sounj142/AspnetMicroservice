using Microsoft.AspNetCore.Mvc;
using Shopping.AggregatorApi.Models;
using Shopping.AggregatorApi.Services;

namespace Shopping.AggregatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly CatalogService _catalogService;
        private readonly BasketService _basketService;
        private readonly OrderService _orderService;

        public ShoppingController(
            CatalogService catalogService,
            BasketService basketService,
            OrderService orderService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _orderService = orderService;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShoppingModel(string userName)
        {
            ShopingCartModel? basket = null;
            OrderModel[]? orders = null;

            var getShopingCartModel = async () =>
            {
                basket = await _basketService.GetShopingCartModel(userName);
                if (basket.Items.Count > 0)
                {
                    var products = await _catalogService.GetProducts(basket.Items.Select(p => p.ProductId));
                    foreach (var product in products)
                    {
                        var item = basket.Items.First(i => i.ProductId == product.Id);

                        item.Description = product.Description;
                        item.Category = product.Category;
                        item.Name = product.Name;
                        item.ImageFile = product.ImageFile;
                        item.Summary = product.Summary;
                    }
                }
            };
            var getOrders = async () =>
            {
                orders = await _orderService.GetOrderModel(userName);
            };

            await Task.WhenAll(
                getShopingCartModel(),
                getOrders());

            return Ok(new ShoppingModel
            {
                UserName = userName,
                Basket = basket,
                Orders = orders
            });
        }
    }
}