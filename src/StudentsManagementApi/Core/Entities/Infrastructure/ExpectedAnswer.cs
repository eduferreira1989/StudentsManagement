using System.ComponentModel.DataAnnotations;

namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class ExpectedAnswer : Entity
{
    public Question Question { get; set; }

    public string Answer { get; set; }
}
