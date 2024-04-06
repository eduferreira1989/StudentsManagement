using StudentsManagement.Application.Models.Data.Base;

namespace StudentsManagement.Application.Models.Data;

public class ExamModel : DomainBaseModel
{
    public ExamModel()
    {
        Questions = new List<QuestionModel>();
        StudentExams = new List<StudentExamModel>();
    }

    public string Name { get; set; }

    public float Value { get; set; }

    public ICollection<StudentExamModel> StudentExams { get; set; }

    public ICollection<QuestionModel> Questions { get; set; }
}