namespace StudentsManagementApi.Core.Entities.Application.Data;

public class StudentExamModel : DomainModel
{
    public StudentExamModel()
    {
        Answers = new List<AnswerModel>();
    }

    public StudentModel Student { get; set; }

    public int StudentId { get; set; }

    public ExamModel Exam { get; set; }

    public int ExamId { get; set; }

    public ICollection<AnswerModel> Answers { get; set; }
}
