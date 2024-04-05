using StudentsManagementApi.Core.Entities.Application.Data;
using StudentsManagementApi.Core.Entities.Application.Response;

namespace StudentsManagementApi.Core.Interfaces.Application;

public interface IStudentService
{
    Task<DomainResponse<StudentModel>> GetStudentById(int id);

    Task<DomainResponse<IEnumerable<StudentModel>>> GetStudentsByExam(int examId);
}
