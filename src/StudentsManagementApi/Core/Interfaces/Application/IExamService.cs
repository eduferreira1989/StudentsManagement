using StudentsManagementApi.Core.Entities.Application.Data;
using StudentsManagementApi.Core.Entities.Application.Response;

namespace StudentsManagementApi.Core.Interfaces.Application;

public interface IExamService
{
    Task<DomainResponse<ExamModel>> GetExamById(int id);
}
