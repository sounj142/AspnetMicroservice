using AspnetRunBasics.Entities;
using System.Threading.Tasks;

namespace AspnetRunBasics.Repositories
{
    public interface IOrderRepository
    {
        Task CheckOut(Order order);

        Task<Order[]> GetOrdersByUserName(string userName);
    }
}