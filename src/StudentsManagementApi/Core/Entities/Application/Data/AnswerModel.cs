namespace StudentsManagementApi.Core.Entities.Application.Data;

public class AnswerModel : DomainModel
{
    public StudentExamModel StudentExam { get; set; }

    public int StudentExamId { get; set; }

    public QuestionModel Question { get; set; }

    public int QuestionId { get; set; }

    public string AnswerText { get; set; }

    public bool AnswerCorrect { get; set; }

    public float Grade { get; set; }
}
