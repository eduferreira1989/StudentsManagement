using System.ComponentModel.DataAnnotations;

namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class ExpectedAnswer : Entity
{
    public int QuestionId { get; set; }

    public string Answer { get; set; }
}
