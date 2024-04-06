using StudentsManagement.Infrastructure.Models.Data.Base;

namespace StudentsManagement.Infrastructure.Models.Data;

public class ExpectedAnswer : BaseEntity
{
    public int QuestionId { get; set; }

    public string Answer { get; set; }
}