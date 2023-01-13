using AspnetRunBasics.Entities;
using AspnetRunBasics.Helpers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            var jsonData = new StringContent(
                JsonSerializer.Serialize(order),
                Encoding.UTF8,
                Application.Json);

            await _client.PostAsync("/Basket/Checkout", jsonData);
        }

        public async Task<Order[]> GetOrdersByUserName(string userName)
        {
            var response = await _client.GetAsync($"/Order?userName={userName}");
            return await response.ReadContentAs<Order[]>();
        }
    }
}