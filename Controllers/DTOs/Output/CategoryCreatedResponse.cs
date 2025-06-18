using TaskManager.Models.Entities;

namespace TaskManager.Controllers.DTOs.Output;

public record CategoryCreatedResponse(
    long Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt
)
{
    public static CategoryCreatedResponse Adapt(Category category)
    {
        return new CategoryCreatedResponse(
            category.Id,
            category.Name,
            category.CreatedAt,
            category.UpdatedAt
        );
    }
}