using StudentsManagement.Infrastructure.Interfaces.Services.Base;
using StudentsManagement.Infrastructure.Models.Data;

namespace StudentsManagement.Infrastructure.Interfaces.Services;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student> GetByIdWithDetailsAsyncNoTracking(int id);
}