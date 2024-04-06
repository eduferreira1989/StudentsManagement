using StudentsManagement.Infrastructure.Models.Data.Interfaces;
using System.Linq.Expressions;

namespace StudentsManagement.Infrastructure.Interfaces.Services.Base;

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