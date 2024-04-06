using StudentsManagement.Infrastructure.Models.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.Infrastructure.Models.Data;

public class StudentExam : BaseEntity
{
    public StudentExam()
    {
        Answers = new List<Answer>();
    }

    [ForeignKey("Student")]
    public int StudentId { get; set; }

    [ForeignKey("Exam")]
    public int ExamId { get; set; }

    public ICollection<Answer> Answers { get; set; }
}