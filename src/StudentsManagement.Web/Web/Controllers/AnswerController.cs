using Mapster;
using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Application.Interfaces.Services;
using StudentsManagement.Application.Models.Data;
using StudentsManagement.Web.Core.Dtos;
using StudentsManagement.Web.Web.Controllers.Base;

namespace StudentsManagementApi.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AnswerController : BaseController
{
    private readonly ILogger<AnswerController> _logger;
    private readonly IAnswerService _answerService;

    public AnswerController(ILogger<AnswerController> logger, IAnswerService answerService) : base(logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _answerService = answerService ?? throw new ArgumentNullException(nameof(answerService));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnswerById(int id)
    {
        _logger.LogInformation("Getting answer by Id: {id}", id);
        try
        {
            var response = await _answerService.GetAnswerById(id);

            // Return business error
            var validateResponse = ValidateResponse(response.Errors);
            if (validateResponse != null)
                return validateResponse;

            return Ok(response.Result.Adapt<AnswerResponseDto>());
        }
        catch (Exception ex)
        {
            return ReturnExceptionResponse(ex);
        }
    }

    [HttpPost("addAnswer/{examId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAnswer(int examId, AddAnswerDto answer)
    {
        _logger.LogInformation("Adding answer for a student for ExamId: {examId}, \n\tPayload: {answer}", examId, answer);
        try
        {
            var answerModel = answer.Adapt<AddAnswerModel>();
            answerModel.ExamId = examId;

            var response = await _answerService.AddOrUpdateAnswer(answerModel);

            // Return business error
            var validateResponse = ValidateResponse(response.Errors);
            if (validateResponse != null)
                return validateResponse;

            return Created($"/Answer/{response.Result.Id}", response.Result.Adapt<AnswerResponseDto>());
        }
        catch (Exception ex)
        {
            return ReturnExceptionResponse(ex);
        }
    }
}