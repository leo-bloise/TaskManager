
using Microsoft.EntityFrameworkCore;
using TaskManager.Infra;
using TaskManager.Models.Entities;

namespace TaskManager.Models.Repositories;

public class TaskRepository : ITaskRepository
{
    private TaskManagerDbContext _taskManagerDbContext;
    public TaskRepository(TaskManagerDbContext taskManagerDbContext)
    {
        _taskManagerDbContext = taskManagerDbContext;
    }
    public Entities.Task Create(Entities.Task task)
    {
        _taskManagerDbContext.Add(task);
        _taskManagerDbContext.SaveChanges();
        return task;
    }
    public Entities.Task Update(Action<Entities.Task> delegateAction, Entities.Task task)
    {
        delegateAction.Invoke(task);
        _taskManagerDbContext.SaveChanges();
        return task;
    }
    public Entities.Task? FindById(long id)
    {
        return _taskManagerDbContext.Tasks.Include(task => task.Category).FirstOrDefault(task => task.Id == id);
    }
    public Page<Entities.Task> GetPage(Page<Entities.Task> page)
    {
        var skip = (page.PageNumber - 1) * page.PageSize;
        var count = _taskManagerDbContext.Tasks.Count();
        int totalPages = 0;
        if (count % page.PageSize != 0)
        {
            totalPages = (count / page.PageSize) + 1;
        }
        else
        {
            totalPages = count / page.PageSize;
        }
        List<Entities.Task> data = _taskManagerDbContext.Tasks
            .Include(task => task.Category)
            .OrderBy(task => task.Id)
            .Skip(skip)
            .Take(page.PageSize)
            .Select((task) => new Entities.Task()
            {
                Id = task.Id,
                Category = task.Category != null ? new Category()
                {
                    Id = task.Category.Id,                    
                    Name = task.Category.Name,
                } : null,
                CreatedAt = task.CreatedAt,
                Description = task.Description,
                Name = task.Name,
                UpdatedAt = task.UpdatedAt
            })
            .ToList();
        return new TaskPage(
            data,
            page.PageNumber,
            data.Count,
            count,
            totalPages
        );
    }
}
