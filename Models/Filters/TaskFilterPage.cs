using TaskManager.Application;
using TaskManager.Models.Entities;

namespace TaskManager.Models.Filters;

public record TaskFilterPage(
    string? Name,
    string? Description,
    int? CategoryId,
    Page<Entities.Task> Page
)
{
    public static TaskFilterPage Adapt(TaskFilter? taskFilter, TaskPage page)
    {
        if (taskFilter == null) return new TaskFilterPage(
            null,
            null,
            null,
            page
        );
        return new TaskFilterPage(
            taskFilter.Name,
            taskFilter.Description,
            taskFilter.CategoryId,
            page
        );
    }
}