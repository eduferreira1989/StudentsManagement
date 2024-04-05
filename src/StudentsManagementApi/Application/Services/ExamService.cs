using Mapster;
using StudentsManagementApi.Core.Entities.Application.Data;
using StudentsManagementApi.Core.Entities.Application.Response;
using StudentsManagementApi.Core.Interfaces.Application;
using StudentsManagementApi.Core.Interfaces.Infrastructure;

namespace StudentsManagementApi.Application.Services;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;

    public ExamService(IExamRepository examRepository)
    {
        _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
    }

    public async Task<DomainResponse<ExamModel>> GetExamById(int id)
    {
        var exam = await _examRepository.GetByIdWithDetailsAsyncNoTracking(id);
        if (exam == null)
            return new DomainResponse<ExamModel> { Errors = [new Error { Message = "Exam not found", HttpCode = System.Net.HttpStatusCode.NotFound }]};

        return new DomainResponse<ExamModel> { Result = exam.Adapt<ExamModel>() };
    }
}
