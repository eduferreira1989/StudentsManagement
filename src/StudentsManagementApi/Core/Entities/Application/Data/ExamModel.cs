namespace StudentsManagementApi.Core.Entities.Application.Data;

public class ExamModel : DomainModel
{
    public ExamModel()
    {
        Questions = new List<QuestionModel>();
        StudentExam = new List<StudentExamModel>();
    }

    public string Name { get; set; }

    public float Value { get; set; }

    public ICollection<StudentExamModel> StudentExam { get; set; }

    public ICollection<QuestionModel> Questions { get; set; }
}
