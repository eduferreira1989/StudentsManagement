namespace StudentsManagement.Application.Models.Response;

public class DomainResponse<T>
{
    public DomainResponse()
    {
        Errors = new List<Error>();
    }

    public DomainResponse(T result, IEnumerable<Error> errors)
    {
        Result = result;
        Errors = errors;
    }

    public T Result { get; set; }

    public IEnumerable<Error> Errors { get; set; }
}