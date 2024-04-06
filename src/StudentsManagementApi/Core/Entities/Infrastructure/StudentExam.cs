namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class StudentExam : Entity
{
    public StudentExam()
    {
        Answers = new List<Answer>();
    }

    public int StudentId { get; set; }

    public int ExamId { get; set; }

    public ICollection<Answer> Answers { get; set; }
}
