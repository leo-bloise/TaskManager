using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.DTOs.Input;

public record PatchTaskRequest(
    [MaxLength(255)]
    string? Name,
    [MinLength(3)]
    string? Description,
    long? CategoryId
)
{
    
}