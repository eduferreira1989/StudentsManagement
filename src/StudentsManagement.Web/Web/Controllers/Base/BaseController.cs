using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Application.Models.Response;
using System.Net;

namespace StudentsManagement.Web.Web.Controllers.Base;

public class BaseController : ControllerBase
{
    private readonly ILogger<BaseController> _logger;

    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected IActionResult ValidateResponse(IEnumerable<DomainError> responseErrors)
    {
        if (responseErrors.Any())
        {
            _logger.LogInformation("Request couldn't be processed, because of the following issues: {errorMessages}", string.Join("\n\t", responseErrors.Select(e => e.Message)));

            return responseErrors.Any(x => x.HttpCode.Equals(HttpStatusCode.NotFound))
                ? NotFound(responseErrors)
                : BadRequest(responseErrors);
        }

        return null;
    }

    protected IActionResult ReturnExceptionResponse(Exception ex)
    {
        _logger.LogError("Error: {exceptionMessage}", ex.Message);

        return new ContentResult
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Content = "An error occurred while processing the request."
        };
    }
}
