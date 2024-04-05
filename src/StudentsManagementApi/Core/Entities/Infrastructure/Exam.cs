using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class Exam : Entity
{
    public Exam()
    {
        Questions = new List<Question>();
        StudentExams = new List<StudentExam>();
    }

    public  string Name { get; set; }

    public float Value { get; set; }

    public ICollection<StudentExam> StudentExams { get; set; }

    public ICollection<Question> Questions { get; set; }
}