using Mapster;
using Microsoft.AspNetCore.Mvc;
using StudentsManagementApi.Core.Entities.Application.Data;
using StudentsManagementApi.Core.Entities.DTOs;
using StudentsManagementApi.Core.Interfaces.Application;
using System.Net;

namespace StudentsManagementApi.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AnswerController : ControllerBase
{
    private readonly ILogger<AnswerController> _logger;
    private readonly IAnswerService _answerService;

    public AnswerController(ILogger<AnswerController> logger, IAnswerService answerService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _answerService = answerService ?? throw new ArgumentNullException(nameof(answerService));
    }

    [HttpPost("/addAnswer/{examId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAnswer(int examId, AddAnswerDto anwser)
    {
        _logger.LogInformation($"Adding answer for a student on exam: {examId}");
        try
        {
            var answerModel = anwser.Adapt<AddAnswerModel>();
            answerModel.ExamId = examId;

            var response = await _answerService.AddOrUpdateAnswer(answerModel);

            // Return business error
            if (response.Errors.Any())
                return response.Errors.Any(x => x.HttpCode.Equals(HttpStatusCode.NotFound))
                    ? NotFound(response.Errors)
                    : BadRequest(response.Errors);

            return Created();
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

