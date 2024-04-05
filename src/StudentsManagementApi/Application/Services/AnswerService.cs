using Mapster;
using StudentsManagementApi.Core.Entities.Application.Data;
using StudentsManagementApi.Core.Entities.Application.Response;
using StudentsManagementApi.Core.Entities.Infrastructure;
using StudentsManagementApi.Core.Interfaces.Application;
using StudentsManagementApi.Core.Interfaces.Infrastructure;

namespace StudentsManagementApi.Application.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IExamService _examService;

    public AnswerService(IAnswerRepository answerRepository, IExamService examService)
    {
        _answerRepository = answerRepository ?? throw new ArgumentNullException(nameof(answerRepository));
        _examService = examService ?? throw new ArgumentNullException(nameof(examService));
    }

    public async Task<DomainResponse<bool>> AddOrUpdateAnswer(AddAnswerModel answer)
    {
        // Validate inputs
        var validationResults = ValidateInput(answer);
        if (validationResults != null)
            return new DomainResponse<bool> { Errors = [validationResults]};

        // Get exam from ExamService
        var examResponse = await _examService.GetExamById(answer.ExamId);
        if (examResponse.Errors.Any())
            return new DomainResponse<bool> { Result = false, Errors = examResponse.Errors };

        var exam = examResponse.Result;

        // Find the StudentExam
        var studentExam = exam.StudentExam.SingleOrDefault(studentExam => studentExam.Student.Id == answer.StudentId);
        if (studentExam == null)
            return new DomainResponse<bool> { Result = false, Errors = [new Error { Message = "Invalid Student", HttpCode = System.Net.HttpStatusCode.BadRequest }] };

        // Find Question
        var question = exam.Questions.SingleOrDefault(question => question.Id == answer.QuestionId);
        if (question == null)
            return new DomainResponse<bool> { Result = false, Errors = [new Error { Message = "Invalid Question", HttpCode = System.Net.HttpStatusCode.BadRequest }] };

        // Check if answer is correct
        var expectedAnswers = question.ExpectedAnswers.ToList();
        var answerCorrect = expectedAnswers.Any(answerCorrect => answerCorrect.Answer.Equals(answer.AnswerText, StringComparison.InvariantCultureIgnoreCase));

        // Save or update the answer
        var studentAnswers = await _answerRepository.GetAsync(studentAnswer => studentAnswer.StudentExam.Student.Id == answer.StudentId 
                                    && studentAnswer.StudentExam.Exam.Id == answer.ExamId && studentAnswer.Question.Id == answer.QuestionId);

        var studentAnswer = studentAnswers.SingleOrDefault();

        if (studentAnswer != null)
        {
            studentAnswer.AnswerText = answer.AnswerText;
            studentAnswer.AnswerCorrect = answerCorrect;

            // Apply update
            await _answerRepository.UpdateAsync(studentAnswer);
        }
        else
        {
            studentAnswer = new Answer
            {
                Id = answer.Id,
                StudentExam = studentExam.Adapt<StudentExam>(),
                Question = question.Adapt<Question>(),
                AnswerText = answer.AnswerText,
                AnswerCorrect = answerCorrect
            };

            // Apply save
            await _answerRepository.AddAsync(studentAnswer);
        }

        return new DomainResponse<bool> { Result = true };

    }

    private Error ValidateInput(AddAnswerModel answer)
    {
        if (answer == null)
            return new Error
            {
                Message = "Input must not be null",
                HttpCode = System.Net.HttpStatusCode.BadRequest
            };

        if (string.IsNullOrWhiteSpace(answer.AnswerText))
            return new Error
            {
                Message = "Answer must have a value",
                HttpCode = System.Net.HttpStatusCode.BadRequest
            };

        return null;
    }
}
