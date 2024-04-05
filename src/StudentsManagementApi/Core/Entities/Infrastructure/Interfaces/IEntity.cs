using System.ComponentModel.DataAnnotations;

namespace StudentsManagementApi.Core.Entities.Infrastructure.Interfaces;

public interface IEntity
{
    [Key]
    int Id { get; set; }
}
