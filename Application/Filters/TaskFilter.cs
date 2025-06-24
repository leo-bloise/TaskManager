namespace TaskManager.Application;

public record TaskFilter(
    string? Name,
    string? Description,
    int? CategoryId
)
{
    public string ToCacheKey()
    {
        return $"{Name}_{Description}_{CategoryId}";
    }   
}