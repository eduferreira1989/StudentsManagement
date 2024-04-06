using StudentsManagement.Infrastructure.Models.Data.Base;

namespace StudentsManagement.Infrastructure.Models.Data;

public class StudentExam : BaseEntity
{
    public StudentExam()
    {
        Answers = new List<Answer>();
    }

    public int StudentId { get; set; }

    public int ExamId { get; set; }

    public ICollection<Answer> Answers { get; set; }
}