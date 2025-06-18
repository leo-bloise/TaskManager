using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;

namespace TaskManager.Application;

public interface ICategoryService
{
    public Category Create(CreateCategoryRequest createCategoryRequest);
    public Category? FindById(long id);
}