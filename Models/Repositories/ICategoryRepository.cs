using TaskManager.Models.Entities;

namespace TaskManager.Models.Repositories;

public interface ICategoryRepository
{
    public Category Create(Category category);
    public Category? FindById(long id, long userId);
    public bool ExistsByName(string name, long userId);
    public void Delete(Category category);
    public Page<Category> PageCategory(Page<Category> category, long userId);
    public Category Update(Category category);
}