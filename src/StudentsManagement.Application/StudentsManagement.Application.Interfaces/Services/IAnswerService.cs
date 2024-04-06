using StudentsManagement.Application.Models.Data;
using StudentsManagement.Application.Models.Response;

namespace StudentsManagement.Application.Interfaces.Services;

public interface IAnswerService
{
    Task<DomainResponse<AnswerModel>> GetAnswerById(int id);

    Task<DomainResponse<AnswerModel>> AddOrUpdateAnswer(AddAnswerModel answer);
}