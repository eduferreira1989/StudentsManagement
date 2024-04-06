namespace StudentsManagementApi.Core.Entities.DTOs;

public record StudentDto(int Id, string Name, DateOnly BirthDate);

public record AnswerDto(int Id, int QuestionId, int StudentExamId, string AnswerText, bool AnswerCorrect);

public record AddAnswerDto(int Id, int QuestionId, int StudentId, string AnswerText);
