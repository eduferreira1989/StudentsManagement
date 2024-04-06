using StudentsManagement.Application.Models.Data.Base;

namespace StudentsManagement.Application.Models.Data;

public class StudentExamModel : DomainBaseModel
{
    public StudentExamModel()
    {
        Answers = new List<AnswerModel>();
    }

    public StudentModel Student { get; set; }

    public int StudentId { get; set; }

    public ExamModel Exam { get; set; }

    public int ExamId { get; set; }

    public float Grade { get; set; }

    public ICollection<AnswerModel> Answers { get; set; }
}