using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.Controllers.DTOs.Input
{
    public record CreateUserDTO(
        [Required]
        [StringLength(20)]
        string username,
        [Required]
        string password,
        [Required]
        [EmailAddress]
        string email
    ) {}
}