using Shopping.AggregatorApi.Helpers;
using Shopping.AggregatorApi.Models;

namespace Shopping.AggregatorApi.Services;

public class CatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductModel[]> GetProducts(IEnumerable<string> ids)
    {
        var idsString = string.Join(",", ids);
        var response = await _httpClient.GetAsync($"/api/Products/GetByIds?ids={idsString}");
        return await response.ReadContentAs<ProductModel[]>();
    }
}