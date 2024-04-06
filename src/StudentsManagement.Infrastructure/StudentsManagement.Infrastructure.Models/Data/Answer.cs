using StudentsManagement.Infrastructure.Models.Data.Base;

namespace StudentsManagement.Infrastructure.Models.Data;

public class Answer : BaseEntity
{
    public int StudentExamId { get; set; }

    public int QuestionId { get; set; }

    public string AnswerText { get; set; }

    public bool AnswerCorrect { get; set; }

    public float Grade { get; set; }
}