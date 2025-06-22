using Microsoft.EntityFrameworkCore;
using TaskManager.Infra;
using TaskManager.Models.Entities;

namespace TaskManager.Models.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private TaskManagerDbContext _taskManagerDbContext;
    public CategoryRepository(TaskManagerDbContext taskManagerDbContext)
    {
        _taskManagerDbContext = taskManagerDbContext;
    }
    public Category Create(Category category)
    {
        _taskManagerDbContext.Add(category);
        _taskManagerDbContext.SaveChanges();
        return category;
    }
    public void Delete(Category category)
    {
        _taskManagerDbContext.Categories.Remove(category);
        _taskManagerDbContext.SaveChanges();
    }
    public bool ExistsByName(string name, long userId)
    {
        return _taskManagerDbContext.Categories.Any(c => c.Name == name && c.User.Id == userId);
    }
    public Category? FindById(long id, long userId)
    {
        return _taskManagerDbContext
            .Categories
            .AsNoTracking()
            .FirstOrDefault(c => c.Id == id && c.UserId == userId);
    }
}