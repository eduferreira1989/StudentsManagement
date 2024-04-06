using StudentsManagement.Application.Models.Data;
using StudentsManagement.Application.Models.Response;

namespace StudentsManagement.Application.Interfaces.Services;

public interface IStudentExamService
{
    Task<DomainResponse<StudentExamModel>> FillStudentExamDetails(StudentExamModel studentExam);
}