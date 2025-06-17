using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.DTOs.Input;

public record LoginRequest(
    [Required]
    string Username,
    [Required]
    string Password
)
{
    
}