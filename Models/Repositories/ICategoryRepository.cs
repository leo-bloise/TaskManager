using TaskManager.Models.Entities;

namespace TaskManager.Models.Repositories;

public interface ICategoryRepository
{
    public Category Create(Category category);
    public Category? FindById(long id);
    public bool ExistsByName(string name);
    public void Delete(Category category);
}