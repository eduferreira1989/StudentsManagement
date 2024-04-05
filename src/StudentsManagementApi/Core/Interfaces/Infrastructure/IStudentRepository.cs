using StudentsManagementApi.Core.Entities.Infrastructure;

namespace StudentsManagementApi.Core.Interfaces.Infrastructure;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student> GetByIdWithDetailsAsyncNoTracking(int id);
}
