using StudentsManagement.Application.Models.Data;
using StudentsManagement.Application.Models.Response;

namespace StudentsManagement.Application.Interfaces.Services;

public interface IExamService
{
    Task<DomainResponse<ExamModel>> GetExamById(int id);
}