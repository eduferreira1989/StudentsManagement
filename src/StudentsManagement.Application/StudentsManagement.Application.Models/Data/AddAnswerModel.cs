﻿using System.ComponentModel.DataAnnotations;

namespace StudentsManagement.Application.Models.Data;

public class AddAnswerModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int ExamId { get; set; }

    [Required]
    public int QuestionId { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    public string AnswerText { get; set; }
}