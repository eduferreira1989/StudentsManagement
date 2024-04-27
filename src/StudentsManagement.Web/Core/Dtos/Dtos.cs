namespace StudentsManagement.Web.Core.Dtos;

public record AnswerResponseDto(int Id, int QuestionId, int StudentExamId, string AnswerText, bool AnswerCorrect, float Grade);

public record AnswerDetailedResponseDto(int Id, int QuestionId, string QuestionText, float QuestionValue, string AnswerText, bool AnswerCorrect, float Grade);

public record StudentExamResponseDto(int Id, string ExamName, float ExamValue, float Grade, IEnumerable<AnswerDetailedResponseDto> Answers);

public record StudentResponseDto(int Id, string Name, DateOnly BirthDate);

public record StudentDetailedResponseDto(int Id, string Name, DateOnly BirthDate, IEnumerable<StudentExamResponseDto> Exams);

public record AddAnswerDto(int Id, int ExamId, int QuestionId, int StudentId, string AnswerText);