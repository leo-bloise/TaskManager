using TaskManager.Application.Exceptions;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;
using TaskManager.Models.Repositories;

namespace TaskManager.Application;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public Category Create(CreateCategoryRequest createCategoryRequest)
    {
        if (_categoryRepository.ExistsByName(createCategoryRequest.Name))
        {
            throw new CategoryNameAlreadyTaken(createCategoryRequest.Name);
        }
        Category category = new Category()
        {
            Name = createCategoryRequest.Name
        };
        _categoryRepository.Create(category);
        return category;
    }
    public Category? FindById(long id)
    {
        return _categoryRepository.FindById(id);
    }
}