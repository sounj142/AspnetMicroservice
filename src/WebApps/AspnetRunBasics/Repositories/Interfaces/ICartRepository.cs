using AspnetRunBasics.Entities;
using System.Threading.Tasks;

namespace AspnetRunBasics.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserName(string userName);

        Task AddItem(string userName, string productId, int quantity = 1, string color = "Black");

        Task RemoveItem(string userName, string productId);

        Task ClearCart(string userName);
    }
}