using Microsoft.EntityFrameworkCore;
using StudentsManagement.Infrastructure.DataContext;
using StudentsManagement.Infrastructure.Interfaces.Services;
using StudentsManagement.Infrastructure.Models.Data;
using StudentsManagement.Infrastructure.Services.Base;

namespace StudentsManagement.Infrastructure.Services;

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