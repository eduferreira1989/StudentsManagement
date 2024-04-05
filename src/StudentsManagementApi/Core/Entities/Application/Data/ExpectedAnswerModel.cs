namespace StudentsManagementApi.Core.Entities.Application.Data;

public class ExpectedAnswerModel : DomainModel
{
    public QuestionModel Question { get; set; }

    public string Answer { get; set; }
}
