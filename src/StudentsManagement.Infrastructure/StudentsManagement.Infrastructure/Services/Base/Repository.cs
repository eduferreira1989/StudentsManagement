using Microsoft.EntityFrameworkCore;
using StudentsManagement.Infrastructure.DataContext;
using StudentsManagement.Infrastructure.Interfaces.Services.Base;
using StudentsManagement.Infrastructure.Models.Data.Base;
using System.Linq.Expressions;

namespace StudentsManagement.Infrastructure.Services.Base;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly StudentsApiDbContext _dbContext;

    public Repository(StudentsApiDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<T> AddAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsyncNoTracking()
    {
        return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<ICollection<T>> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbContext.Set<T>().Where(filter).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsyncNoTracking(Expression<Func<T, bool>> filter)
    {
        return await _dbContext.Set<T>().Where(filter).AsNoTracking().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<T?> GetByIdAsyncNoTracking(int id)
    {
        return await _dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}