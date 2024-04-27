using NUnit.Framework;
using Moq;
using StudentsManagement.Infrastructure.Interfaces.Services;
using StudentsManagement.Infrastructure.Models.Data;
using StudentsManagement.Application.Interfaces.Services;
using StudentsManagement.Application.Services;
using StudentsManagement.Application.Models.Data;
using System.Linq.Expressions;
using System.Net;

namespace StudentsManagement.Application.Tests.Services;

public class AnswerServiceTests
{
    private Mock<IExamRepository> _examRepositoryMock;
    private Mock<IAnswerRepository> _answerRepositoryMock;
    private IExamService _examService;
    private IAnswerService _answerService;

    [SetUp]
    public void Setup()
    {
        _examRepositoryMock = new Mock<IExamRepository>();
        _answerRepositoryMock = new Mock<IAnswerRepository>();
        _examService = new ExamService(_examRepositoryMock.Object);
        _answerService = new AnswerService(_answerRepositoryMock.Object, _examService);
    }

    #region GetAnswerById

    [Test]
    public async Task GetAnswerById_WhenFindTheAnswer_ThenReturnIt()
    {
        // Setup
        var answer = new Answer
        {
            Id = 1,
            StudentExamId = 1,
            QuestionId = 1,
            AnswerText = "2 and 4",
            AnswerCorrect = true,
            Grade = 10
        };

        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(1)).ReturnsAsync(answer);

        // Act

        var result = await _answerService.GetAnswerById(1);

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result.Errors);
        Assert.That(result.Result.Id, Is.EqualTo(answer.Id));
        Assert.That(result.Result.QuestionId, Is.EqualTo(answer.QuestionId));
        Assert.That(result.Result.StudentExamId, Is.EqualTo(answer.StudentExamId));
        Assert.That(result.Result.AnswerText, Is.EqualTo(answer.AnswerText));
        Assert.That(result.Result.AnswerCorrect, Is.EqualTo(answer.AnswerCorrect));
        Assert.That(result.Result.Grade, Is.EqualTo(answer.Grade));
    }

    [Test]
    public async Task GetAnswerById_WhenNotFindTheAnswer_ThenReturnBusinessErrorMessage()
    {
        // Setup
        var answer = new Answer
        {
            Id = 1,
            StudentExamId = 1,
            QuestionId = 1,
            AnswerText = "2 and 4",
            AnswerCorrect = true,
            Grade = 10
        };

        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(1)).ReturnsAsync(answer);

        // Act

        var result = await _answerService.GetAnswerById(2);

        // Assert
        Assert.NotNull(result);
        Assert.IsNull(result.Result);
        Assert.IsNotEmpty(result.Errors);
        Assert.That(result.Errors.First().HttpCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.That(result.Errors.First().Message, Is.EqualTo("Answer not found"));
    }

    #endregion

    #region AddOrUpdateAnswer

    [Test]
    public async Task AddOrUpdateAnswer_WhenValidAndCorrectNewAnswer_ThenSaveIt()
    {
        // Setup
        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Answer>())).ReturnsAsync(new Answer());
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>());
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync((Answer)null);
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(fakeExam);

        // Act
        var answer = new AddAnswerModel
        {
            Id = 1,
            StudentId = 1,
            ExamId = 1,
            QuestionId = 1,
            AnswerText = "2 and 4"
        };

        var result = await _answerService.AddOrUpdateAnswer(answer);

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result.Errors);
        Assert.IsTrue(result.Result.AnswerCorrect);
        Assert.That(result.Result.Grade, Is.EqualTo(fakeExam.Questions.Single(q => q.Id == answer.QuestionId).Value));
    }

    [Test]
    public async Task AddOrUpdateAnswer_WhenValidAndWrongNewAnswer_ThenSaveIt()
    {
        // Setup
        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Answer>())).ReturnsAsync(new Answer());
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>());
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync((Answer)null);
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(fakeExam);

        // Act
        var answer = new AddAnswerModel
        {
            Id = 1,
            StudentId = 1,
            ExamId = 1,
            QuestionId = 1,
            AnswerText = "3 and 4"
        };

        var result = await _answerService.AddOrUpdateAnswer(answer);

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result.Errors);
        Assert.IsFalse(result.Result.AnswerCorrect);
        Assert.That(result.Result.Grade, Is.EqualTo(0));
    }

    [Test]
    public async Task AddOrUpdateAnswer_WhenValidAndCorrectExistingAnswer_ThenUpdateIt()
    {
        // Setup
        var oldAnswer = new Answer
        {
            Id = 1,
            StudentExamId = 1,
            QuestionId = 1,
            AnswerText = "1 and 4",
            AnswerCorrect = false,
            Grade = 0
        };

        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Answer>())).Returns(Task.CompletedTask);
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>() { oldAnswer });
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync((Answer)null);
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(fakeExam);

        // Act
        var answer = new AddAnswerModel
        {
            Id = 1,
            StudentId = 1,
            ExamId = 1,
            QuestionId = 1,
            AnswerText = "2 and 4"
        };

        var result = await _answerService.AddOrUpdateAnswer(answer);

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result.Errors);
        Assert.IsTrue(result.Result.AnswerCorrect);
        Assert.That(result.Result.Grade, Is.EqualTo(fakeExam.Questions.Single(q => q.Id == answer.QuestionId).Value));
    }

    [Test]
    public async Task AddOrUpdateAnswer_WhenValidAndWrongExistingAnswer_ThenUpdateIt()
    {
        // Setup
        var oldAnswer = new Answer
        {
            Id = 1,
            StudentExamId = 1,
            QuestionId = 1,
            AnswerText = "2 and 4",
            AnswerCorrect = true,
            Grade = 10
        };

        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Answer>())).Returns(Task.CompletedTask);
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>() { oldAnswer });
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync((Answer)null);
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(fakeExam);

        // Act
        var answer = new AddAnswerModel
        {
            Id = 1,
            StudentId = 1,
            ExamId = 1,
            QuestionId = 1,
            AnswerText = "3 and 4"
        };

        var result = await _answerService.AddOrUpdateAnswer(answer);

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result.Errors);
        Assert.IsFalse(result.Result.AnswerCorrect);
        Assert.That(result.Result.Grade, Is.EqualTo(0));
    }

    [Test]
    public async Task AddOrUpdateAnswer_WhenValidAndCorrectNewAnswerWithInvalidId_ThenReturnBusinessErrorMessage()
    {
        // Setup
        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Answer>())).ReturnsAsync(new Answer());
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>());
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(new Answer());
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(fakeExam);

        // Act
        var answer = new AddAnswerModel
        {
            Id = 1,
            StudentId = 1,
            ExamId = 1,
            QuestionId = 1,
            AnswerText = "2 and 4"
        };

        var result = await _answerService.AddOrUpdateAnswer(answer);

        // Assert
        Assert.NotNull(result);
        Assert.IsNull(result.Result);
        Assert.IsNotEmpty(result.Errors);
        Assert.That(result.Errors.First().HttpCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(result.Errors.First().Message, Is.EqualTo("Invalid AnswerId, already in use by another answer"));
    }

    [Test]
    public async Task AddOrUpdateAnswer_WhenNullNewAnswer_ThenReturnBusinessErrorMessage()
    {
        // Setup
        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Answer>())).ReturnsAsync(new Answer());
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>());
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(new Answer());
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(fakeExam);

        // Act
        var result = await _answerService.AddOrUpdateAnswer((AddAnswerModel)null);

        // Assert
        Assert.NotNull(result);
        Assert.IsNull(result.Result);
        Assert.IsNotEmpty(result.Errors);
        Assert.That(result.Errors.First().HttpCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(result.Errors.First().Message, Is.EqualTo("Input must not be null"));
    }

    [Test]
    public async Task AddOrUpdateAnswer_WhenNewAnswerWithEmptyText_ThenReturnBusinessErrorMessage()
    {
        // Setup
        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Answer>())).ReturnsAsync(new Answer());
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>());
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(new Answer());
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(fakeExam);

        // Act
        var answer = new AddAnswerModel
        {
            Id = 1,
            StudentId = 1,
            ExamId = 1,
            QuestionId = 1
        };

        var result = await _answerService.AddOrUpdateAnswer(answer);

        // Assert
        Assert.NotNull(result);
        Assert.IsNull(result.Result);
        Assert.IsNotEmpty(result.Errors);
        Assert.That(result.Errors.First().HttpCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(result.Errors.First().Message, Is.EqualTo("Answer must have a value"));
    }

    [Test]
    public async Task AddOrUpdateAnswer_WhenNewAnswerWithInvalidExam_ThenReturnBusinessErrorMessage()
    {
        // Setup
        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Answer>())).ReturnsAsync(new Answer());
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>());
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(new Answer());
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync((Exam)null);

        // Act
        var answer = new AddAnswerModel
        {
            Id = 1,
            StudentId = 1,
            ExamId = 2,
            QuestionId = 1,
            AnswerText = "2 and 4"
        };

        var result = await _answerService.AddOrUpdateAnswer(answer);

        // Assert
        Assert.NotNull(result);
        Assert.IsNull(result.Result);
        Assert.IsNotEmpty(result.Errors);
        Assert.That(result.Errors.First().HttpCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(result.Errors.First().Message, Is.EqualTo("Invalid Exam"));
    }

    [Test]
    public async Task AddOrUpdateAnswer_WhenNewAnswerWithInvalidStudent_ThenReturnBusinessErrorMessage()
    {
        // Setup
        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Answer>())).ReturnsAsync(new Answer());
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>());
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(new Answer());
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(fakeExam);

        // Act
        var answer = new AddAnswerModel
        {
            Id = 1,
            StudentId = 2,
            ExamId = 1,
            QuestionId = 1,
            AnswerText = "2 and 4"
        };

        var result = await _answerService.AddOrUpdateAnswer(answer);

        // Assert
        Assert.NotNull(result);
        Assert.IsNull(result.Result);
        Assert.IsNotEmpty(result.Errors);
        Assert.That(result.Errors.First().HttpCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(result.Errors.First().Message, Is.EqualTo("Invalid Student"));
    }

    [Test]
    public async Task AddOrUpdateAnswer_WhenNewAnswerWithInvalidQuestion_ThenReturnBusinessErrorMessage()
    {
        // Setup
        var fakeExam = GetExamFake();
        _answerRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Answer>())).ReturnsAsync(new Answer());
        _answerRepositoryMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Expression<Func<Answer, bool>> expr) => new List<Answer>());
        _answerRepositoryMock.Setup(m => m.GetByIdAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(new Answer());
        _examRepositoryMock.Setup(m => m.GetByIdWithDetailsAsyncNoTracking(It.IsAny<int>())).ReturnsAsync(fakeExam);

        // Act
        var answer = new AddAnswerModel
        {
            Id = 1,
            StudentId = 1,
            ExamId = 1,
            QuestionId = 2,
            AnswerText = "2 and 4"
        };

        var result = await _answerService.AddOrUpdateAnswer(answer);

        // Assert
        Assert.NotNull(result);
        Assert.IsNull(result.Result);
        Assert.IsNotEmpty(result.Errors);
        Assert.That(result.Errors.First().HttpCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(result.Errors.First().Message, Is.EqualTo("Invalid Question"));
    }

    #endregion


    #region Test Helpers

    private Exam GetExamFake()
    {
        return new Exam
        {
            Id = 1,
            Name = "Test",
            Value = 10.0f,
            StudentExams = new List<StudentExam>
            {
                new StudentExam
                {
                    Id = 1,
                    ExamId = 1,
                    StudentId = 1,
                    Answers = new List<Answer>()
                }
            },
            Questions = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    ExamId = 1,
                    QuestionText = "Which are the even numbers between 1 and 5?",
                    Value = 10f,
                    ExpectedAnswers = new List<ExpectedAnswer>
                    {
                        new ExpectedAnswer
                        {
                            Id= 1,
                            QuestionId = 1,
                            Answer = "2 and 4"
                        }
                    }
                }
            }
        };
    }

    #endregion
}