using StudentsManagement.Application.Models.Data.Base;

namespace StudentsManagement.Application.Models.Data;

public class StudentModel : DomainBaseModel
{
    public StudentModel()
    {
        StudentExams = new List<StudentExamModel>();
    }

    public string Name { get; set; }

    public DateOnly BirthDate { get; set; }

    public ICollection<StudentExamModel> StudentExams { get; set; }
}