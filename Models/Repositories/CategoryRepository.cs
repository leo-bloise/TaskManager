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
    public bool ExistsByName(string name)
    {
        return _taskManagerDbContext.Categories.Any(c => c.Name == name);
    }
    public Category? FindById(long id)
    {
        return _taskManagerDbContext.Categories.FirstOrDefault(c => c.Id == id);
    }
}