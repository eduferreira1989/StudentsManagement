using Mapster;
using Microsoft.AspNetCore.Mvc;
using StudentsManagementApi.Core.Entities.DTOs;
using StudentsManagementApi.Core.Interfaces.Application;
using System.Net;

namespace StudentsManagementApi.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IStudentService _studentService;

    public StudentController(ILogger<StudentController> logger, IStudentService studentService)
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
        _logger.LogInformation($"Getting student by Id: {id}");
        try
        {
            var response = await _studentService.GetStudentById(id);

            // Return business error
            if (response.Errors.Any())
                return response.Errors.Any(x => x.HttpCode.Equals(HttpStatusCode.NotFound))
                    ? NotFound(response.Errors)
                    : BadRequest(response.Errors);

            return Ok(response.Result.Adapt<StudentDto>());
        }
        catch (Exception ex)
        {
            _logger.LogError($"error: {ex.Message}");
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "An error occurred while processing the request."
            };
        }
    }

    [HttpGet("/byExam/{examId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStudentsByExam(int examId)
    {
        _logger.LogInformation($"Getting students by exam, for examId: {examId}");
        try
        {
            var response = await _studentService.GetStudentsByExam(examId);

            // Return business error
            if (response.Errors.Any())
                return response.Errors.Any(x => x.HttpCode.Equals(HttpStatusCode.NotFound))
                    ? NotFound(response.Errors)
                    : BadRequest(response.Errors);

            return Ok(response.Result.Adapt<List<StudentDto>>());
        }
        catch (Exception ex)
        {
            _logger.LogError($"error: {ex.Message}");
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = "An error occurred while processing the request."
            };
        }
    }
}

