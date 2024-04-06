using StudentsManagement.Infrastructure.Models.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.Infrastructure.Models.Data;

public class Question : BaseEntity
{
    public Question()
    {
        ExpectedAnswers = new List<ExpectedAnswer>();
    }

    [ForeignKey("Exam")]
    public int ExamId { get; set; }

    public string QuestionText { get; set; }

    public float Value { get; set; }

    public ICollection<ExpectedAnswer> ExpectedAnswers { get; set; }
}