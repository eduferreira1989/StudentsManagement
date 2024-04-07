using Mapster;
using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Application.Interfaces.Services;
using StudentsManagement.Web.Core.Dtos;
using StudentsManagement.Web.Web.Controllers.Base;

namespace StudentsManagementApi.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class StudentController : BaseController
{
    private readonly ILogger<StudentController> _logger;
    private readonly IStudentService _studentService;

    public StudentController(ILogger<StudentController> logger, IStudentService studentService) : base(logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStudentById(int id)
    {
        _logger.LogInformation("Getting student by Id: {id}", id);
        try
        {
            var response = await _studentService.GetStudentById(id);

            // Return business error
            var validateResponse = ValidateResponse(response.Errors);
            if (validateResponse != null)
                return validateResponse;

            return Ok(response.Result.Adapt<StudentDetailedResponseDto>());
        }
        catch (Exception ex)
        {
            return ReturnExceptionResponse(ex);
        }
    }

    [HttpGet("byExam/{examId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStudentsByExam(int examId)
    {
        _logger.LogInformation("Getting students by exam, for ExamId: {examId}", examId);
        try
        {
            var response = await _studentService.GetStudentsByExam(examId);

            // Return business error
            var validateResponse = ValidateResponse(response.Errors);
            if (validateResponse != null)
                return validateResponse;

            return Ok(response.Result.Adapt<List<StudentResponseDto>>());
        }
        catch (Exception ex)
        {
            return ReturnExceptionResponse(ex);
        }
    }
}