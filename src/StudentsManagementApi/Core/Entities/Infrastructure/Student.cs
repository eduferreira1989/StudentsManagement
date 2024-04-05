using System.ComponentModel.DataAnnotations;

namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class Student : Entity
{
    public Student()
    {
        StudentExams = new List<StudentExam>();
    }

    public string Name { get; set; }

    public ICollection<StudentExam> StudentExams { get; set; }
}
