using StudentsManagementApi.Core.Entities.Infrastructure.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentsManagementApi.Core.Entities.Infrastructure;

public class Entity : IEntity
{
    [Key]
    public int Id { get; set; }
}
