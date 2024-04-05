namespace StudentsManagementApi.Core.Entities.Application.Data;

public class QuestionModel : DomainModel
{
    public QuestionModel()
    {
        ExpectedAnswers = new List<ExpectedAnswerModel>();
    }

    public ExamModel Exam { get; set; }

    public string QuestionText { get; set; }

    public float Value { get; set; }

    public ICollection<ExpectedAnswerModel> ExpectedAnswers { get; set; }
}
