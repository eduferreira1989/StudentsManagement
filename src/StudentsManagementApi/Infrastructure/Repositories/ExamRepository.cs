using Microsoft.EntityFrameworkCore;
using StudentsManagementApi.Core.Entities.Infrastructure;
using StudentsManagementApi.Core.Interfaces.Infrastructure;
using StudentsManagementApi.Infrastructure.Data;
using StudentsManagementApi.Infrastructure.Repositories.Base;

namespace StudentsManagementApi.Infrastructure.Repositories;

public class ExamRepository : Repository<Exam>, IExamRepository
{
    public ExamRepository(StudentsApiDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Exam> GetByIdWithDetailsAsyncNoTracking(int id)
    {
        return await _dbContext.Set<Exam>()
            .Include(e => e.StudentExams)
            .ThenInclude(se => se.Student)
            .Include(e => e.Questions)
            .ThenInclude(q => q.ExpectedAnswers)
            .AsNoTracking()
            .SingleAsync(entity => entity.Id == id);
    }
}
