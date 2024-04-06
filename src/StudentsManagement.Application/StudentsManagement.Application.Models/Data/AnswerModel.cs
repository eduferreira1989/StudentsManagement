using StudentsManagement.Application.Models.Data.Base;

namespace StudentsManagement.Application.Models.Data;

public class AnswerModel : DomainBaseModel
{
    public StudentExamModel StudentExam { get; set; }

    public int StudentExamId { get; set; }

    public QuestionModel Question { get; set; }

    public int QuestionId { get; set; }

    public string AnswerText { get; set; }

    public bool AnswerCorrect { get; set; }

    public float Grade { get; set; }
}