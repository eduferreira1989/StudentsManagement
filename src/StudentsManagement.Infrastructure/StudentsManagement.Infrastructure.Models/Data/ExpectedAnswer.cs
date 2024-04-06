using StudentsManagement.Infrastructure.Models.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.Infrastructure.Models.Data;

public class ExpectedAnswer : BaseEntity
{
    [ForeignKey("Question")]
    public int QuestionId { get; set; }

    public string Answer { get; set; }
}