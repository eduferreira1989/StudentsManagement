using Mapster;
using StudentsManagement.Application.Interfaces.Services;
using StudentsManagement.Application.Models.Data;
using StudentsManagement.Application.Models.Response;
using StudentsManagement.Infrastructure.Interfaces.Services;

namespace StudentsManagement.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IStudentExamService _studentExamService;

    public StudentService(IStudentRepository studentRepository, IStudentExamService studentExamService)
    {
        _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        _studentExamService = studentExamService ?? throw new ArgumentNullException(nameof(studentExamService));
    }

    public async Task<DomainResponse<StudentModel>> GetStudentById(int id)
    {
        var student = await _studentRepository.GetByIdWithDetailsAsyncNoTracking(id);
        if (student == null)
            return new DomainResponse<StudentModel> { Errors = [new DomainError { Message = "Student not found", HttpCode = System.Net.HttpStatusCode.NotFound }] };

        var studentModel = student.Adapt<StudentModel>();

        // Get details from StudentExamService
        foreach (var studentExam in studentModel.StudentExams)
        {
            var studentExamResponse = await _studentExamService.FillStudentExamDetails(studentExam);
            if (studentExamResponse.Errors.Any())
            {
                return new DomainResponse<StudentModel> { Errors = studentExamResponse.Errors };
            }

            studentExam.Exam = studentExamResponse.Result.Exam;
            studentExam.Answers = studentExamResponse.Result.Answers;
        }

        return new DomainResponse<StudentModel> { Result = studentModel };
    }

    public async Task<DomainResponse<IEnumerable<StudentModel>>> GetStudentsByExam(int examId)
    {
        var students = await _studentRepository.GetAsyncNoTracking(student => student.StudentExams.Any(exam => exam.ExamId == examId));

        var studentsResponse = new DomainResponse<IEnumerable<StudentModel>>
        {
            Result = students.Adapt<List<StudentModel>>()
        };

        return studentsResponse;
    }
}