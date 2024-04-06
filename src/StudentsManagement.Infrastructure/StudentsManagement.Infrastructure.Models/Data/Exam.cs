using StudentsManagement.Infrastructure.Models.Data.Base;

namespace StudentsManagement.Infrastructure.Models.Data;

public class Exam : BaseEntity
{
    public Exam()
    {
        Questions = new List<Question>();
        StudentExams = new List<StudentExam>();
    }

    public string Name { get; set; }

    public float Value { get; set; }

    public ICollection<StudentExam> StudentExams { get; set; }

    public ICollection<Question> Questions { get; set; }
}