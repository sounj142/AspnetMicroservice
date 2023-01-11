using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

internal class RepositoryBase<T> : IRepository<T> where T : EntityBase
{
    protected readonly OrderDbContext _dbContext;
    protected readonly DbSet<T> _orderDbSet;

    public RepositoryBase(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
        _orderDbSet = _dbContext.Set<T>();
    }

    public Task<T[]> GetAll()
    {
        return _orderDbSet.ToArrayAsync();
    }

    public Task<T?> GetById(int id)
    {
        return _orderDbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> Add(T entity)
    {
        entity.Id = 0;
        _orderDbSet.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _orderDbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}