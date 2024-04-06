using StudentsManagement.Application.Models.Data.Base;

namespace StudentsManagement.Application.Models.Data;

public class QuestionModel : DomainBaseModel
{
    public QuestionModel()
    {
        ExpectedAnswers = new List<ExpectedAnswerModel>();
    }

    public ExamModel Exam { get; set; }

    public int ExamId { get; set; }

    public string QuestionText { get; set; }

    public float Value { get; set; }

    public ICollection<ExpectedAnswerModel> ExpectedAnswers { get; set; }
}