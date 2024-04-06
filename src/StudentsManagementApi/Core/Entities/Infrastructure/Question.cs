namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class Question : Entity
{
    public Question()
    {
        ExpectedAnswers = new List<ExpectedAnswer>();
    }

    public int ExamId { get; set; }

    public string QuestionText { get; set; }

    public float Value { get; set; }

    public ICollection<ExpectedAnswer> ExpectedAnswers { get; set; }
}
