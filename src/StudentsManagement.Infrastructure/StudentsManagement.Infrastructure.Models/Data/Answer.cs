using StudentsManagement.Infrastructure.Models.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.Infrastructure.Models.Data;

public class Answer : BaseEntity
{
    [ForeignKey("StudentExam")]
    public int StudentExamId { get; set; }

    [ForeignKey("Question")]
    public int QuestionId { get; set; }

    public string AnswerText { get; set; }

    public bool AnswerCorrect { get; set; }

    public float Grade { get; set; }
}