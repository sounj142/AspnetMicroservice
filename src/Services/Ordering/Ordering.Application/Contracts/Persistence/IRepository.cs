using Ordering.Domain.Common;

namespace Ordering.Application.Contracts.Persistence;

public interface IRepository<T> where T : EntityBase
{
    Task<T[]> GetAll();

    Task<T?> GetById(int id);

    Task<T> Add(T entity);

    Task Update(T entity);

    Task Delete(T entity);
}