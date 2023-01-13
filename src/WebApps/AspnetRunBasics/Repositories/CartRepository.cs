using AspnetRunBasics.Entities;
using AspnetRunBasics.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AspnetRunBasics.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly HttpClient _client;
        private readonly IServiceProvider _serviceProvider;

        public CartRepository(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
        {
            _client = httpClientFactory.CreateClient();
            _serviceProvider = serviceProvider;
        }

        public async Task<Cart> GetCartByUserName(string userName)
        {
            var response = await _client.GetAsync($"/Basket/{userName}");
            return await response.ReadContentAs<Cart>();
        }

        public async Task AddItem(string userName, string productId, int quantity = 1, string color = "Black")
        {
            var cart = await GetCartByUserName(userName);

            var productRepo = _serviceProvider.GetRequiredService<IProductRepository>();
            var product = await productRepo.GetProductById(productId);
            if (product == null)
                throw new ApplicationException("Product not found.");

            var cartItem = cart.Items.FirstOrDefault(x => x.ProductId == product.Id);
            if (cartItem == null)
            {
                cartItem = new CartItem();
                cart.Items.Add(cartItem);
            }
            cartItem.Category = product.Category;
            cartItem.Color = color;
            cartItem.ImageFile = product.ImageFile;
            cartItem.Name = product.Name;
            cartItem.Price = product.Price;
            cartItem.ProductId = product.Id;
            cartItem.Summary = product.Summary;

            cartItem.Quantity += quantity;

            var jsonData = new StringContent(
                JsonSerializer.Serialize(cart),
                Encoding.UTF8,
                Application.Json);
            var response = await _client.PutAsync("/Basket", jsonData);
            var c = await response.ReadContentAs<Cart>();
        }

        public async Task RemoveItem(string userName, string productId)
        {
            var cart = await GetCartByUserName(userName);

            cart.Items = cart.Items.Where(x => x.ProductId != productId)
                .ToList();

            var jsonData = new StringContent(
                JsonSerializer.Serialize(cart),
                Encoding.UTF8,
                Application.Json);
            await _client.PutAsync("/Basket", jsonData);
        }

        public async Task ClearCart(string userName)
        {
            await _client.DeleteAsync($"/Basket/{userName}");
        }
    }
}