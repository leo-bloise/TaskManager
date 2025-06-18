namespace TaskManager.Controllers.DTOs.Output;

public record TaskCreatedResponse(
    long Id,
    string Name,
    string Description,
    CategoryCreatedResponse? Category
)
{
    public static TaskCreatedResponse Adapt(TaskManager.Models.Entities.Task task)
    {
        CategoryCreatedResponse? categoryCreatedResponse = null;
        if (task.Category != null)
        {
            categoryCreatedResponse = CategoryCreatedResponse.Adapt(task.Category);
        }
        return new TaskCreatedResponse(
            task.Id,
            task.Name,
            task.Description,
            categoryCreatedResponse
        );
    }
}