using Microsoft.EntityFrameworkCore;
using StudentsManagementApi.Core.Entities.Infrastructure;
using StudentsManagementApi.Core.Interfaces.Infrastructure;
using StudentsManagementApi.Infrastructure.Data;
using StudentsManagementApi.Infrastructure.Repositories.Base;

namespace StudentsManagementApi.Infrastructure.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(StudentsApiDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<Student> GetByIdWithDetailsAsyncNoTracking(int id)
    {
        return await _dbContext.Set<Student>()
            .Include(s => s.StudentExams)
            .Include(s => s.StudentExams)
            .ThenInclude(se => se.Answers)
            .AsNoTracking()
            .SingleOrDefaultAsync(entity => entity.Id == id);
    }
}
