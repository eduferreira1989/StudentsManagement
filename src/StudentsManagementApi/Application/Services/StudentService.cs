using Mapster;
using StudentsManagementApi.Core.Entities.Application.Data;
using StudentsManagementApi.Core.Entities.Application.Response;
using StudentsManagementApi.Core.Interfaces.Application;
using StudentsManagementApi.Core.Interfaces.Infrastructure;

namespace StudentsManagementApi.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
    }

    public async Task<DomainResponse<StudentModel>> GetStudentById(int id)
    {
        var exam = await _studentRepository.GetByIdWithDetailsAsyncNoTracking(id);
        if (exam == null)
            return new DomainResponse<StudentModel> { Errors = [new Error { Message = "Student not found", HttpCode = System.Net.HttpStatusCode.NotFound }] };

        return new DomainResponse<StudentModel> { Result = exam.Adapt<StudentModel>() };
    }

    public async Task<DomainResponse<IEnumerable<StudentModel>>> GetStudentsByExam(int examId)
    {
        var students = await _studentRepository.GetAsyncNoTracking(student => student.StudentExams.Any(exam => exam.Exam.Id == examId));

        var studentsResponse = new DomainResponse<IEnumerable<StudentModel>>
        {
            Result = students.Adapt<List<StudentModel>>()
        };

        return studentsResponse;
    }
}
