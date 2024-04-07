namespace StudentsManagement.Application.Models.Response;

public class DomainResponse<T>
{
    public DomainResponse()
    {
        Errors = new List<DomainError>();
    }

    public DomainResponse(T result, IEnumerable<DomainError> errors)
    {
        Result = result;
        Errors = errors;
    }

    public T Result { get; set; }

    public IEnumerable<DomainError> Errors { get; set; }
}