using StudentsManagementApi.Core.Entities.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace StudentsManagementApi.Core.Interfaces.Infrastructure;

public interface IRepository<T> where T : IEntity
{
    Task<ICollection<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsyncNoTracking();
    Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> filter);
    Task<IReadOnlyList<T>> GetAsyncNoTracking(Expression<Func<T, bool>> filter);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsyncNoTracking(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
