using System.Net;

namespace StudentsManagement.Application.Models.Response;

public class DomainError
{
    public DomainError()
    {

    }

    public DomainError(string message, HttpStatusCode httpCode)
    {
        Message = message;
        HttpCode = httpCode;
    }

    public string Message { get; set; }

    public HttpStatusCode HttpCode { get; set; }
}