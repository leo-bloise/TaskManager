using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.DTOs.Input;

public record CreateUserRequest(
    [Required]
    [MinLength(3)]
    string Username,
    [Required]
    [MinLength(3)]
    string Password,
    [Required]
    [EmailAddress]
    string Email
)
{
    
}