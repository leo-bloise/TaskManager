using System.ComponentModel.DataAnnotations;

namespace TaskManager.Controllers.DTOs.Input;

public record CreateCategoryRequest(
    [Required]
    [MaxLength(255)]
    string Name
) {
    
}