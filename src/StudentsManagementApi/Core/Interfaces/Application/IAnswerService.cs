using StudentsManagementApi.Core.Entities.Application.Data;
using StudentsManagementApi.Core.Entities.Application.Response;

namespace StudentsManagementApi.Core.Interfaces.Application;

public interface IAnswerService
{
    Task<DomainResponse<AnswerModel>> GetAnswerById(int id);

    Task<DomainResponse<AnswerModel>> AddOrUpdateAnswer(AddAnswerModel answer);
}
