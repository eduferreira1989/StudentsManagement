namespace StudentsManagementApi.Core.Entities.DTOs;

public record StudentDto(int Id, string Name);

public record AddAnswerDto(int Id, int QuestionId, int StudentId, string AnswerText);
