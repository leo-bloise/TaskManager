using TaskManager.Models.Filters;

namespace TaskManager.Models.Repositories;

public interface ITaskRepository
{
    /**
    <summary>
    Creates a new task and return it with the new ID. The entity returned is tracked by Entity Framework.
    </summary>
    */
    public Entities.Task Create(Entities.Task task);
    /**
    <summary>
    Returns the task with its category (the category is loaded without the children tasks)
    </summary>
    */
    public Entities.Task? FindById(long id, long userId);
    public TaskFilterPage FilterAndPaginate(TaskFilterPage taskFilterPage, long userId);
    /**
    <summary>
    Updates an existing task
    </summary>
    */
    public Entities.Task Update(Action<Entities.Task> delegateAction, Entities.Task task);
    /**
    <summary>
    Deletes a task
    </summary>
    */
    public void Delete(Entities.Task task);
    public void DetachCategoryFromTask(long categoryId, long userId);
}