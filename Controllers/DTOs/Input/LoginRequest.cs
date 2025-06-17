using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.DTOs.Input;

public record LoginRequest(
    [Required]
    [MaxLength(255)]
    string Username,
    [Required]
    string Password
)
{
    
}