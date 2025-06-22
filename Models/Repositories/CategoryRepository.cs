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
    public Page<Category> PageCategory(Page<Category> category, long userId)
    {
        var totalItems = _taskManagerDbContext.Categories.Count();
        int totalPage;
        if(totalItems % category.PageSize == 0)
        {
            totalPage = totalItems / category.PageSize;
        } else
        {
            totalPage = (totalItems / category.PageSize) + 1;
        }
        var skip = (category.PageNumber - 1) * category.PageSize;
        var data = _taskManagerDbContext.Categories
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Id)
            .Skip(skip)
            .Take(category.PageSize)
            .Select(c => new Category()
            {
                Id = c.Id,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                Name = c.Name,
                User = c.User,
                UserId = c.UserId
            })
            .ToList();
        return new CategoryPage(data, category.PageNumber, category.PageSize, totalItems, totalPage);
    }
    public Category Update(Category category)
    {
        _taskManagerDbContext.Categories.Update(category);
        _taskManagerDbContext.SaveChanges();
        return category;
    }
}