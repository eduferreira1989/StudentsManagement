﻿using Microsoft.EntityFrameworkCore;
using StudentsManagementApi.Core.Entities.Infrastructure;

namespace StudentsManagementApi.Infrastructure.Data;

public class StudentsApiDbContext : DbContext
{
    public StudentsApiDbContext(DbContextOptions options) : base(options)
    {
        // Adding some fake data to test the endpoints
        SetUpTestingData();
    }

    public DbSet<Student> Students { get; set; }

    public DbSet<Exam> Exams { get; set; }

    public DbSet<StudentExam> StudentExams { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<ExpectedAnswer> ExpectedAnswers { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public void SetUpTestingData()
    {
        #region ExpectedAnswer

        var expectedAnswer1 = new ExpectedAnswer
        {
            Id = 1,
            Answer = "It is a plataform for software development multiplatform and multipurpose."
        };

        var expectedAnswer2 = new ExpectedAnswer
        {
            Id = 2,
            Answer = "It is a programming language developed, it is the most used language in .Net ecosystem."
        };

        var expectedAnswer3 = new ExpectedAnswer
        {
            Id = 3,
            Answer = "The current version is .Net 8, tagged as Long Term Support."
        };

        var expectedAnswer4 = new ExpectedAnswer
        {
            Id = 4,
            Answer = "It is a language with fast learning curve and there are several libraries and platfroms developed for this purpose using Python."
        };

        var expectedAnswer5 = new ExpectedAnswer
        {
            Id = 5,
            Answer = "It is a casing style generally used when writing Python codes, the names are in lower case splitted by '_', like: 'snake_case'."
        };

        var expectedAnswer6 = new ExpectedAnswer
        {
            Id = 6,
            Answer = "Bonjour"
        };

        var expectedAnswer7 = new ExpectedAnswer
        {
            Id = 7,
            Answer = "Bonsoir"
        };

        var expectedAnswer8 = new ExpectedAnswer
        {
            Id = 8,
            Answer = "Salut"
        };

        #endregion

        #region Question

        var question1 = new Question
        {
            Id = 1,
            QuestionText = "What is .Net?",
            ExpectedAnswers = [expectedAnswer1],
            Value = 3.0f
        };

        var question2 = new Question
        {
            Id = 2,
            QuestionText = "What is C#?",
            ExpectedAnswers = [expectedAnswer2],
            Value = 4.0f
        };

        var question3 = new Question
        {
            Id = 3,
            QuestionText = "Which is the current version of .Net in April/2024?",
            ExpectedAnswers = [expectedAnswer3],
            Value = 3.0f
        };

        var question4 = new Question
        {
            Id = 4,
            QuestionText = "Why Python is so popular for data analysis?",
            ExpectedAnswers = [expectedAnswer4],
            Value = 5.0f
        };

        var question5 = new Question
        {
            Id = 5,
            QuestionText = "What is Snake Case?",
            ExpectedAnswers = [expectedAnswer5],
            Value = 5.0f
        };

        var question6 = new Question
        {
            Id = 6,
            QuestionText = "Write a valid greeting:",
            ExpectedAnswers = [expectedAnswer6, expectedAnswer7, expectedAnswer8],
            Value = 10.0f
        };

        expectedAnswer1.Question = question1;
        expectedAnswer2.Question = question2;
        expectedAnswer3.Question = question3;
        expectedAnswer4.Question = question4;
        expectedAnswer5.Question = question5;
        expectedAnswer6.Question = question6;
        expectedAnswer7.Question = question6;
        expectedAnswer8.Question = question6;

        #endregion

        #region Exam

        var exam1 = new Exam
        {
            Id = 1,
            Name = "C# Exam",
            Value = 10.0f,
            Questions = [question1, question2, question3]
        };

        var exam2 = new Exam
        {
            Id = 2,
            Name = "Python Exam",
            Value = 10.0f,
            Questions = [question4, question5]
        };

        var exam3 = new Exam
        {
            Id = 3,
            Name = "Greeting exam",
            Value = 10.0f,
            Questions = [question6]
        };

        question1.Exam = exam1;
        question2.Exam = exam1;
        question3.Exam = exam1;
        question4.Exam = exam2;
        question5.Exam = exam2;
        question6.Exam = exam3;

        #endregion

        #region Student

        var student1 = new Student
        {
            Id = 1,
            Name = "Eduardo Ferreira"
        };

        var student2 = new Student
        {
            Id = 2,
            Name = "Lewis Hamilton"
        };

        var student3 = new Student
        {
            Id = 3,
            Name = "Max Verstappen"
        };

        var student4 = new Student
        {
            Id = 4,
            Name = "Simone Simons"
        };

        var student5 = new Student
        {
            Id = 5,
            Name = "Floor Jansen"
        };

        #endregion


        #region StudentExam

        var studentExam1 = new StudentExam
        {
            Id = 1,
            Student = student1,
            Exam = exam1
        };

        var studentExam2 = new StudentExam
        {
            Id = 2,
            Student = student1,
            Exam = exam2
        };

        var studentExam3 = new StudentExam
        {
            Id = 3,
            Student = student2,
            Exam = exam2
        };

        var studentExam4 = new StudentExam
        {
            Id = 4,
            Student = student3,
            Exam = exam1
        };

        var studentExam5 = new StudentExam
        {
            Id = 5,
            Student = student5,
            Exam = exam1
        };

        var studentExam6 = new StudentExam
        {
            Id = 6,
            Student = student5,
            Exam = exam3
        };

        var studentExam7 = new StudentExam
        {
            Id = 7,
            Student = student4,
            Exam = exam3
        };

        student1.StudentExams = [studentExam1, studentExam2];
        student2.StudentExams = [studentExam3];
        student3.StudentExams = [studentExam4];
        student4.StudentExams = [studentExam7];
        student5.StudentExams = [studentExam5, studentExam6];

        exam1.StudentExams = [studentExam1, studentExam4, studentExam5];
        exam2.StudentExams = [studentExam2, studentExam3];
        exam3.StudentExams = [studentExam6, studentExam7];

        #endregion

        ExpectedAnswers.AddRange([expectedAnswer1, expectedAnswer2, expectedAnswer3, expectedAnswer4, expectedAnswer5, expectedAnswer6, expectedAnswer7, expectedAnswer8]);
        Questions.AddRange([question1, question2, question3, question4, question5, question6]);
        Exams.AddRange([exam1, exam2, exam3]);
        Students.AddRange([student1, student2, student3, student4, student5]);
        StudentExams.AddRange([studentExam1, studentExam2, studentExam3, studentExam4, studentExam5, studentExam6, studentExam7]);
        base.SaveChangesAsync();
    }
}
