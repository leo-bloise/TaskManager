namespace TaskManager.Models.Entities;

public class TaskPage : Page<Task>
{
    public TaskPage(List<Task> data, int page, int size, int totalItems, int totalPage) : base(data, page, size, totalItems, totalPage)
    {
    }
}