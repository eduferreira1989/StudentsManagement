using StudentsManagement.Application.Models.Data;
using StudentsManagement.Application.Models.Response;

namespace StudentsManagement.Application.Interfaces.Services;

public interface IStudentService
{
    Task<DomainResponse<StudentModel>> GetStudentById(int id);

    Task<DomainResponse<IEnumerable<StudentModel>>> GetStudentsByExam(int examId);
}