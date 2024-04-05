using System.ComponentModel.DataAnnotations;

namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class Answer : Entity
{
    public StudentExam StudentExam { get; set; }

    public Question Question { get; set; }

    public string AnswerText { get; set; }

    public bool AnswerCorrect { get; set; }
}
