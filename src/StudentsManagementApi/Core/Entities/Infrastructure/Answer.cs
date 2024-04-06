namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class Answer : Entity
{
    public int StudentExamId { get; set; }

    public int QuestionId { get; set; }

    public string AnswerText { get; set; }

    public bool AnswerCorrect { get; set; }
}
