using StudentsManagement.Infrastructure.Models.Data.Base;

namespace StudentsManagement.Infrastructure.Models.Data;

public class Student : BaseEntity
{
    public Student()
    {
        StudentExams = new List<StudentExam>();
    }

    public string Name { get; set; }

    public DateOnly BirthDate { get; set; }

    public ICollection<StudentExam> StudentExams { get; set; }
}