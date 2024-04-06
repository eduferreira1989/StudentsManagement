using StudentsManagement.Infrastructure.Interfaces.Services.Base;
using StudentsManagement.Infrastructure.Models.Data;

namespace StudentsManagement.Infrastructure.Interfaces.Services;

public interface IExamRepository : IRepository<Exam>
{
    Task<Exam> GetByIdWithDetailsAsyncNoTracking(int id);
}