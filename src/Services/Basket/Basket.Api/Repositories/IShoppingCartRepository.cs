using Basket.Api.Entities;

namespace Basket.Api.Repositories;

public interface IShoppingCartRepository
{
    Task<ShoppingCart?> GetShoppingCart(string userName);

    Task UpdateShoppingCart(ShoppingCart shoppingCart);

    Task DeleteShoppingCart(string userName);
}