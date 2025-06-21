
using Microsoft.EntityFrameworkCore;
using TaskManager.Infra;
using TaskManager.Models.Entities;
using TaskManager.Models.Filters;

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
        return _taskManagerDbContext.Tasks
            .Include(task => task.Category)
            .Select((task) => new Entities.Task()
            {
                Id = task.Id,
                Category = task.Category != null ? new Category()
                {
                    Id = task.Category.Id,
                    CreatedAt = task.Category.CreatedAt,
                    Name = task.Category.Name,
                    UpdatedAt = task.Category.UpdatedAt
                } : null,
                Name = task.Name,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            })
            .FirstOrDefault(task => task.Id == id);
    }
    private Page<Entities.Task> GetPageFromQuery(IQueryable<Entities.Task> query, Page<Entities.Task> page)
    {
        var skip = (page.PageNumber - 1) * page.PageSize;
        var count = query.Count();
        int totalPages = 0;
        if (count % page.PageSize != 0)
        {
            totalPages = (count / page.PageSize) + 1;
        }
        else
        {
            totalPages = count / page.PageSize;
        }
        List<Entities.Task> data = query
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
    public void Delete(Entities.Task task)
    {
        _taskManagerDbContext.Tasks.Remove(task);
        _taskManagerDbContext.SaveChanges();
    }
    public TaskFilterPage FilterAndPaginate(TaskFilterPage taskFilterPage)
    {
        var query = _taskManagerDbContext.Tasks.AsNoTracking().AsQueryable();
        if (taskFilterPage.Name != null)
        {
            query = query.Where(t => t.Name.ToLower().Contains(taskFilterPage.Name.ToLower()));
        }
        if (taskFilterPage.Description != null)
        {
            query = query.Where(t => t.Description.ToLower().Contains(taskFilterPage.Description.ToLower()));
        }
        if (taskFilterPage.CategoryId.HasValue)
        {
            query = query.Where(t => t.Category != null && t.Category.Id == taskFilterPage.CategoryId);
        }
        var page = GetPageFromQuery(query, taskFilterPage.Page);
        return new TaskFilterPage(
            taskFilterPage.Name,
            taskFilterPage.Description,
            taskFilterPage.CategoryId,
            page
        );
    }
    public void DetachCategoryFromTask(long categoryId)
    {
        _taskManagerDbContext.Database.ExecuteSql(
            $"UPDATE tasks SET category_id = null WHERE category_id = {categoryId}"
        );
        _taskManagerDbContext.SaveChanges();
    }
}
