using Mapster;
using StudentsManagement.Application.Interfaces.Services;
using StudentsManagement.Application.Models.Data;
using StudentsManagement.Application.Models.Response;
using StudentsManagement.Infrastructure.Interfaces.Services;

namespace StudentsManagement.Application.Services;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;

    public ExamService(IExamRepository examRepository)
    {
        _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
    }

    public async Task<DomainResponse<ExamModel>> GetExamById(int id)
    {
        var exam = await _examRepository.GetByIdAsyncNoTracking(id);
        if (exam == null)
            return new DomainResponse<ExamModel> { Errors = [new DomainError { Message = "Exam not found", HttpCode = System.Net.HttpStatusCode.NotFound }] };

        return new DomainResponse<ExamModel> { Result = exam.Adapt<ExamModel>() };
    }

    public async Task<DomainResponse<ExamModel>> GetExamByIdWithDetails(int id)
    {
        var exam = await _examRepository.GetByIdWithDetailsAsyncNoTracking(id);
        if (exam == null)
            return new DomainResponse<ExamModel> { Errors = [new DomainError { Message = "Exam not found", HttpCode = System.Net.HttpStatusCode.NotFound }] };

        return new DomainResponse<ExamModel> { Result = exam.Adapt<ExamModel>() };
    }
}