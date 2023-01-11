using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order[]> GetOrdersByUserName(string userName);
}