namespace TaskManager.Api.Controllers.Output
{
    public record UserCreatedDTO(
        int Id,
        string Username,
        string Email,
        DateTime CreatedAt,
        DateTime UpdatedAt
    ) { }
}