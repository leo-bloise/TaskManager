using TaskManager.Models.Entities;

namespace TaskManager.Controllers.DTOs.Output;

public record UserCreatedResponse(
    string Username,
    string Email,
    string CreatedAt,
    string UpdatedAt
)
{
    public static UserCreatedResponse Adapt(User user)
    {
        return new UserCreatedResponse(
            user.Username,
            user.Email,
            user.CreatedAt.ToString(),
            user.UpdatedAt.ToString()
        );
    }
}