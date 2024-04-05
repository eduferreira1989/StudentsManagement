using StudentsManagementApi.Core.Entities.Infrastructure;
using StudentsManagementApi.Core.Interfaces.Infrastructure;
using StudentsManagementApi.Infrastructure.Data;
using StudentsManagementApi.Infrastructure.Repositories.Base;

namespace StudentsManagementApi.Infrastructure.Repositories;

public class AnswerRepository : Repository<Answer>, IAnswerRepository
{
    public AnswerRepository(StudentsApiDbContext dbContext) : base(dbContext)
    {
    }
}
