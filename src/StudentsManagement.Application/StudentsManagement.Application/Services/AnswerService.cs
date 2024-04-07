using Mapster;
using StudentsManagement.Application.Interfaces.Services;
using StudentsManagement.Application.Models.Data;
using StudentsManagement.Application.Models.Response;
using StudentsManagement.Infrastructure.Interfaces.Services;
using StudentsManagement.Infrastructure.Models.Data;

namespace StudentsManagement.Application.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IExamService _examService;

    public AnswerService(IAnswerRepository answerRepository, IExamService examService)
    {
        _answerRepository = answerRepository ?? throw new ArgumentNullException(nameof(answerRepository));
        _examService = examService ?? throw new ArgumentNullException(nameof(examService));
    }

    public async Task<DomainResponse<AnswerModel>> GetAnswerById(int id)
    {
        var answer = await _answerRepository.GetByIdAsyncNoTracking(id);
        if (answer == null)
            return new DomainResponse<AnswerModel> { Errors = [new DomainError { Message = "Answer not found", HttpCode = System.Net.HttpStatusCode.NotFound }] };

        return new DomainResponse<AnswerModel> { Result = answer.Adapt<AnswerModel>() };
    }

    public async Task<DomainResponse<AnswerModel>> AddOrUpdateAnswer(AddAnswerModel answer)
    {
        // Validate inputs
        var validationResults = ValidateInput(answer);
        if (validationResults != null)
            return new DomainResponse<AnswerModel> { Errors = [validationResults] };

        // Get exam from ExamService
        var examResponse = await _examService.GetExamByIdWithDetails(answer.ExamId);
        if (examResponse.Errors.Any())
            return new DomainResponse<AnswerModel> { Errors = examResponse.Errors };

        var exam = examResponse.Result;

        // Find the StudentExam
        var studentExam = exam.StudentExams.SingleOrDefault(studentExam => studentExam.StudentId == answer.StudentId);
        if (studentExam == null)
            return new DomainResponse<AnswerModel> { Errors = [new DomainError { Message = "Invalid Student", HttpCode = System.Net.HttpStatusCode.BadRequest }] };

        // Find Question
        var question = exam.Questions.SingleOrDefault(question => question.Id == answer.QuestionId);
        if (question == null)
            return new DomainResponse<AnswerModel> { Errors = [new DomainError { Message = "Invalid Question", HttpCode = System.Net.HttpStatusCode.BadRequest }] };

        // Check if answer is correct
        var expectedAnswers = question.ExpectedAnswers.ToList();
        var answerCorrect = expectedAnswers.Any(answerCorrect => answerCorrect.Answer.Equals(answer.AnswerText, StringComparison.InvariantCultureIgnoreCase));

        // Save or update the answer
        var studentAnswers = await _answerRepository.GetAsync(studentAnswer => studentAnswer.StudentExamId == studentExam.Id && studentAnswer.QuestionId == answer.QuestionId);

        var studentAnswer = studentAnswers.SingleOrDefault();

        if (studentAnswer != null)
        {
            studentAnswer.AnswerText = answer.AnswerText;
            studentAnswer.AnswerCorrect = answerCorrect;
            studentAnswer.Grade = answerCorrect ? question.Value : 0f;

            // Apply update
            await _answerRepository.UpdateAsync(studentAnswer);
        }
        else
        {
            // Validate AnswerId
            if (await _answerRepository.GetByIdAsyncNoTracking(answer.Id) != null)
                return new DomainResponse<AnswerModel> { Errors = [new DomainError { Message = "Invalid AnswerId, already in use by another answer", HttpCode = System.Net.HttpStatusCode.BadRequest }] };

            studentAnswer = new Answer
            {
                Id = answer.Id,
                StudentExamId = studentExam.Id,
                QuestionId = question.Id,
                AnswerText = answer.AnswerText,
                AnswerCorrect = answerCorrect,
                Grade = answerCorrect ? question.Value : 0f
            };

            // Apply save
            await _answerRepository.AddAsync(studentAnswer);
        }

        return new DomainResponse<AnswerModel> { Result = studentAnswer.Adapt<AnswerModel>() };

    }

    private DomainError ValidateInput(AddAnswerModel answer)
    {
        if (answer == null)
            return new DomainError
            {
                Message = "Input must not be null",
                HttpCode = System.Net.HttpStatusCode.BadRequest
            };

        if (string.IsNullOrWhiteSpace(answer.AnswerText))
            return new DomainError
            {
                Message = "Answer must have a value",
                HttpCode = System.Net.HttpStatusCode.BadRequest
            };

        return null;
    }
}