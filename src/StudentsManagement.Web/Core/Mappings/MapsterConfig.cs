using Mapster;
using StudentsManagement.Application.Models.Data;
using StudentsManagement.Web.Core.Dtos;

namespace StudentsManagement.Web.Core.Mappings;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<AnswerModel, AnswerDetailedResponseDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.QuestionId, src => src.QuestionId)
            .Map(dest => dest.QuestionText, src => src.Question.QuestionText)
            .Map(dest => dest.QuestionValue, src => src.Question.Value)
            .Map(dest => dest.AnswerText, src => src.AnswerText)
            .Map(dest => dest.AnswerCorrect, src => src.AnswerCorrect)
            .Map(dest => dest.Grade, src => src.Grade);

        TypeAdapterConfig<StudentExamModel, StudentExamResponseDto>
            .NewConfig()
            .Map(dest => dest.ExamId, src => src.ExamId)
            .Map(dest => dest.ExamName, src => src.Exam.Name)
            .Map(dest => dest.ExamValue, src => src.Exam.Value)
            .Map(dest => dest.Grade, src => src.Grade)
            .Map(dest => dest.Answers, src => src.Answers.Adapt<List<AnswerDetailedResponseDto>>())
            .Map(dest => dest.Grade, src => src.Grade);

        TypeAdapterConfig<StudentModel, StudentDetailedResponseDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.BirthDate, src => src.BirthDate)
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Exams, src => src.StudentExams.Adapt<List<StudentExamResponseDto>>());
    }
}