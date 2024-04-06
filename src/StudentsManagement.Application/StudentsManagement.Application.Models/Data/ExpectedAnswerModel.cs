using StudentsManagement.Application.Models.Data.Base;

namespace StudentsManagement.Application.Models.Data;

public class ExpectedAnswerModel : DomainBaseModel
{
    public QuestionModel Question { get; set; }

    public int QuestionId { get; set; }

    public string Answer { get; set; }
}