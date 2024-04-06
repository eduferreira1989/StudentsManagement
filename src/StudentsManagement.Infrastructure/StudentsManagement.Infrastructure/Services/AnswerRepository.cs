using StudentsManagement.Infrastructure.DataContext;
using StudentsManagement.Infrastructure.Interfaces.Services;
using StudentsManagement.Infrastructure.Models.Data;
using StudentsManagement.Infrastructure.Services.Base;

namespace StudentsManagement.Infrastructure.Services;

public class AnswerRepository : Repository<Answer>, IAnswerRepository
{
    public AnswerRepository(StudentsApiDbContext dbContext) : base(dbContext)
    {
    }
}