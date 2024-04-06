using Microsoft.EntityFrameworkCore;
using StudentsManagement.Infrastructure.DataContext;
using StudentsManagement.Infrastructure.Interfaces.Services;
using StudentsManagement.Infrastructure.Models.Data;
using StudentsManagement.Infrastructure.Services.Base;

namespace StudentsManagement.Infrastructure.Services;

public class ExamRepository : Repository<Exam>, IExamRepository
{
    public ExamRepository(StudentsApiDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Exam> GetByIdWithDetailsAsyncNoTracking(int id)
    {
        return await _dbContext.Set<Exam>()
            .Include(e => e.StudentExams)
            .Include(e => e.Questions)
            .ThenInclude(q => q.ExpectedAnswers)
            .AsNoTracking()
            .SingleOrDefaultAsync(entity => entity.Id == id);
    }
}