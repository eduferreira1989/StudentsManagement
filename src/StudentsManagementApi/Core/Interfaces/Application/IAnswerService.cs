using StudentsManagementApi.Core.Entities.Application.Data;
using StudentsManagementApi.Core.Entities.Application.Response;

namespace StudentsManagementApi.Core.Interfaces.Application;

public interface IAnswerService
{
    Task<DomainResponse<bool>> AddOrUpdateAnswer(AddAnswerModel answer);
}
