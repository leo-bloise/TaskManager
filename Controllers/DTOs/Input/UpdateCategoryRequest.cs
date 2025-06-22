using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.DTOs.Input
{
    public record UpdateCategoryRequest(
        [Required]
        [MaxLength(255)]
        string Name
    ) { }
}
