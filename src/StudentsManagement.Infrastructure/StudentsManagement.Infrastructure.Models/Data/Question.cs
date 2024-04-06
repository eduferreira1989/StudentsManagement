using StudentsManagement.Infrastructure.Models.Data.Base;

namespace StudentsManagement.Infrastructure.Models.Data;

public class Question : BaseEntity
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