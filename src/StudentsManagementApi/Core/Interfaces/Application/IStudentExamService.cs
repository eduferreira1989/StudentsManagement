using StudentsManagementApi.Core.Entities.Application.Data;
using StudentsManagementApi.Core.Entities.Application.Response;

namespace StudentsManagementApi.Core.Interfaces.Application;

public interface IStudentExamService
{
    Task<DomainResponse<StudentExamModel>> FillStudentExamDetails(StudentExamModel studentExam);
}
