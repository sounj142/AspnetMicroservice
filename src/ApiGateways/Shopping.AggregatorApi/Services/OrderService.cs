using Shopping.AggregatorApi.Helpers;
using Shopping.AggregatorApi.Models;

namespace Shopping.AggregatorApi.Services;

public class OrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<OrderModel[]> GetOrderModel(string userName)
    {
        var response = await _httpClient.GetAsync($"/api/Order?userName={userName}");
        return await response.ReadContentAs<OrderModel[]>();
    }
}