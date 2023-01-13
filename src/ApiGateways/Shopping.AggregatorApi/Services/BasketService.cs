using Shopping.AggregatorApi.Helpers;
using Shopping.AggregatorApi.Models;

namespace Shopping.AggregatorApi.Services;

public class BasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ShopingCartModel> GetShopingCartModel(string userName)
    {
        var response = await _httpClient.GetAsync($"/api/ShoppingCart/{userName}");
        return await response.ReadContentAs<ShopingCartModel>();
    }
}