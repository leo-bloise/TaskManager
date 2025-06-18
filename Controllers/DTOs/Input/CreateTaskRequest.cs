using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.DTOs;

public record CreateTaskRequest(
    [Required]
    [MaxLength(255)]
    string Name,
    [Required]
    string Description,
    long? CategoryId
)
{
    
}