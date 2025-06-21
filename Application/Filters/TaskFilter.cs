namespace TaskManager.Application;

public record TaskFilter(
    string? Name,
    string? Description,
    int? CategoryId
)
{
    
}