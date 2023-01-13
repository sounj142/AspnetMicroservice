using AspnetRunBasics.Entities;
using AspnetRunBasics.Helpers;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HttpClient _client;

        public OrderRepository(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        public async Task CheckOut(Order order)
        {
            await _client.PostAsync("/Basket/Checkout", order);
        }

        public async Task<Order[]> GetOrdersByUserName(string userName)
        {
            var response = await _client.GetAsync($"/Order?userName={userName}");
            return await response.ReadContentAs<Order[]>();
        }
    }
}