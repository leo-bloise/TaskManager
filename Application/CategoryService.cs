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
    private readonly IUserService _userService;
    public CategoryService(ICategoryRepository categoryRepository, ITaskRepository taskRepository, IUnitOfWork unitOfWork, IUserService userService)
    {
        _categoryRepository = categoryRepository;
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _userService = userService;
    }
    public Category Create(CreateCategoryRequest createCategoryRequest, long userId)
    {
        var user = _userService.GetById(userId);
        if (user == null) throw new UnauthorizedException("Unauthorized");
        if (_categoryRepository.ExistsByName(createCategoryRequest.Name, userId))
        {
            throw new CategoryNameAlreadyTaken(createCategoryRequest.Name);
        }
        Category category = new Category()
        {
            Name = createCategoryRequest.Name,
            User = user
        };
        _categoryRepository.Create(category);
        return category;
    }

    public System.Threading.Tasks.Task Delete(long id, long userId)
    {
        var category = _categoryRepository.FindById(id, userId) ?? throw new CategoryNotFound(id);
        return _unitOfWork.ExecuteAsync(() =>
        {
            _taskRepository.DetachCategoryFromTask(id, userId);
            _categoryRepository.Delete(category);
        });
    }

    public Category? FindById(long id, long userId)
    {
        return _categoryRepository.FindById(id, userId);
    }
}