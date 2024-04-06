using StudentsManagement.Infrastructure.Models.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentsManagement.Infrastructure.Models.Data.Base;

public class BaseEntity : IEntity
{
    [Key]
    public int Id { get; set; }
}