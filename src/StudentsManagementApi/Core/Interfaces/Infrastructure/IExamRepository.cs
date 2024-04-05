using StudentsManagementApi.Core.Entities.Infrastructure;

namespace StudentsManagementApi.Core.Interfaces.Infrastructure;

public interface IExamRepository : IRepository<Exam>
{
    Task<Exam> GetByIdWithDetailsAsyncNoTracking(int id);
}
