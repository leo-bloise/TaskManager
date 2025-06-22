using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;

namespace TaskManager.Application;

public interface ICategoryService
{
    public Category Create(CreateCategoryRequest createCategoryRequest, long userId);
    public Category? FindById(long id, long userId);
    public System.Threading.Tasks.Task Delete(long id, long userId);
}