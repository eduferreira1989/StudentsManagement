namespace StudentsManagementApi.Core.Entities.Application.Data;

public class StudentModel : DomainModel
{
    public string Name { get; set; }

    public DateOnly BirthDate { get; set; }

    public ICollection<StudentExamModel> StudentExams { get; set; }
}
