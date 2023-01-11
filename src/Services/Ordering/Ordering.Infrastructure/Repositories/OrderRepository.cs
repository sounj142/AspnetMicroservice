using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    internal class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        { }

        public Task<Order[]> GetOrdersByUserName(string userName)
        {
            return _orderDbSet
                .Include(x => x.OrderItems)
                .Where(x => x.UserName == userName)
                .ToArrayAsync();
        }

        public override Task<Order[]> GetAll()
        {
            return _orderDbSet
                .Include(x => x.OrderItems)
                .ToArrayAsync();
        }

        public override Task<Order?> GetById(int id)
        {
            return _orderDbSet
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}