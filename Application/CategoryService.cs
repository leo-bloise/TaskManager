using TaskManager.Application.Exceptions;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;
using TaskManager.Models.Repositories;

namespace TaskManager.Application;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(ICategoryRepository categoryRepository, ITaskRepository taskRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
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

    public System.Threading.Tasks.Task Delete(long id)
    {
        var category = _categoryRepository.FindById(id) ?? throw new CategoryNotFound(id);
        return _unitOfWork.ExecuteAsync(() =>
        {
            _taskRepository.DetachCategoryFromTask(id);
            _categoryRepository.Delete(category);
        });
    }

    public Category? FindById(long id)
    {
        return _categoryRepository.FindById(id);
    }
}