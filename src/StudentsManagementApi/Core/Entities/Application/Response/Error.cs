using System.Net;

namespace StudentsManagementApi.Core.Entities.Application.Response;

public class Error
{
    public Error()
    {

    }

    public Error(string message, HttpStatusCode httpCode)
    {
        Message = message;
        HttpCode = httpCode;
    }

    public string Message { get; set; }

    public HttpStatusCode HttpCode { get; set; }
}
