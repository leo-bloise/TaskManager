using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Controllers.DTOs.Input
{
    public record CreateUserDTO(
        [Required]
        [StringLength(20)]
        string Username,
        [Required]
        string Password,
        [Required]
        [EmailAddress]
        string Email
    ) {}
}