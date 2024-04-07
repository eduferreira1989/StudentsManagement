using StudentsManagement.Application.Interfaces.Services;
using StudentsManagement.Application.Models.Data;
using StudentsManagement.Application.Models.Response;

namespace StudentsManagement.Application.Services;

public class StudentExamService : IStudentExamService
{
    private readonly IExamService _examService;

    public StudentExamService(IExamService examService)
    {
        _examService = examService ?? throw new ArgumentNullException(nameof(examService));
    }

    public async Task<DomainResponse<StudentExamModel>> FillStudentExamDetails(StudentExamModel studentExam)
    {
        var examResponse = await _examService.GetExamById(studentExam.ExamId);
        if (examResponse.Errors.Any())
        {
            if (examResponse.Errors.Any(error => error.HttpCode == System.Net.HttpStatusCode.NotFound))
                return new DomainResponse<StudentExamModel> { Errors = [new DomainError { Message = "Invalid Exam", HttpCode = System.Net.HttpStatusCode.BadRequest }] };

            return new DomainResponse<StudentExamModel> { Errors = examResponse.Errors };
        }

        // Fill Exam object
        studentExam.Exam = examResponse.Result;

        // Fill Answer details
        foreach (var answer in studentExam.Answers)
        {
            answer.Question = studentExam.Exam.Questions.SingleOrDefault(question => question.Id == answer.QuestionId);

            // Calculate Exam Grade
            studentExam.Grade += answer.Grade;
        }

        return new DomainResponse<StudentExamModel> { Result = studentExam };
    }
}