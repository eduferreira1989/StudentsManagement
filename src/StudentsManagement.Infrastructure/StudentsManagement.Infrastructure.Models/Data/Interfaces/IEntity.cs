using System.ComponentModel.DataAnnotations;

namespace StudentsManagement.Infrastructure.Models.Data.Interfaces;

public interface IEntity
{
    [Key]
    int Id { get; set; }
}