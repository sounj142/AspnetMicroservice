using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly IDistributedCache _redisCache;

    public ShoppingCartRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<ShoppingCart> GetShoppingCart(string userName)
    {
        var jsonContent = await _redisCache.GetStringAsync(userName.ToLowerInvariant());
        return string.IsNullOrEmpty(jsonContent)
            ? new ShoppingCart(userName)
            : JsonSerializer.Deserialize<ShoppingCart>(jsonContent)!;
    }

    public async Task UpdateShoppingCart(ShoppingCart shoppingCart)
    {
        var jsonContent = JsonSerializer.Serialize(shoppingCart);
        await _redisCache.SetStringAsync(shoppingCart.UserName.ToLowerInvariant(), jsonContent);
    }

    public Task DeleteShoppingCart(string userName)
    {
        return _redisCache.RemoveAsync(userName.ToLowerInvariant());
    }
}