using System.ComponentModel.DataAnnotations;

namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class StudentExam : Entity
{
    public StudentExam()
    {
        Answers = new List<Answer>();
    }

    public Student Student { get; set; }

    public Exam Exam { get; set; }

    public ICollection<Answer> Answers { get; set; }
}
