using TaskManager.Application.Exceptions;
using TaskManager.Controllers.DTOs;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;
using TaskManager.Models.Filters;
using TaskManager.Models.Repositories;

namespace TaskManager.Application;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserService _userService;
    private readonly ICacheManager _cacheManager;
    public TaskService(ITaskRepository taskRepository, ICategoryRepository categoryRepository, IUserService userService, ICacheManager cacheManager)
    {
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
        _userService = userService;
        _cacheManager = cacheManager;
    }
    private Models.Entities.Task CreateWithCategory(CreateTaskRequest createTaskRequest, long userId)
    {
        var user = _userService.GetById(userId);
        if (user == null) throw new UnauthorizedException("Unauthorized");
        var categoryId = createTaskRequest.CategoryId.Value;
        Category? category = _categoryRepository.FindById(categoryId, userId);
        if (category == null) throw new CategoryDoesNotExistsException(categoryId);
        Models.Entities.Task task = new()
        {
            CategoryId = category.Id,
            Description = createTaskRequest.Description,
            Name = createTaskRequest.Name,
            UserId = user.Id            
        };
        task = _taskRepository.Create(task);
        task.Category = category;
        task.User = user;
        _cacheManager.RemoveAllRelated($"task_page_{userId}");
        return task;
    }
    public Models.Entities.Task Create(CreateTaskRequest createTaskRequest, long userId)
    {        
        if (createTaskRequest.CategoryId.HasValue)
        {
            return CreateWithCategory(createTaskRequest, userId);
        }
        var user = _userService.GetById(userId);
        if (user == null) throw new UnauthorizedException("Unauthorized");
        Models.Entities.Task task = new()
        {
            Category = null,
            Description = createTaskRequest.Description,
            Name = createTaskRequest.Name,
            UserId = user.Id
        };
        _cacheManager.RemoveAllRelated($"task_page_{userId}");
        return _taskRepository.Create(task);
    }
    public Models.Entities.Task? Get(long id, long userId)
    {
        return _taskRepository.FindById(id, userId);
    }
    public Page<Models.Entities.Task> GetPage(int page, int size, TaskFilter? taskFilter, long userId)
    {
        var key = $"task_page_{userId}_{page}_{size}_${taskFilter?.ToCacheKey() ?? "null"}";
        var cachedTaskFilterPage = _cacheManager.Get<TaskFilterPage>(key);
        if(cachedTaskFilterPage != null)
        {
            return cachedTaskFilterPage.Page;
        }
        TaskPage taskPage = new TaskPage(new List<Models.Entities.Task>(), page, size, 0, 0);
        var newTaskPageFiltered = _taskRepository.FilterAndPaginate(TaskFilterPage.Adapt(taskFilter, taskPage), userId);
        _cacheManager.Set(key, newTaskPageFiltered);
        return newTaskPageFiltered.Page;
    }
    private void UpdateCateogry(UpdateTaskRequest updateTaskRequest, Models.Entities.Task task, long userId)
    {
        if (!updateTaskRequest.CategoryId.HasValue)
        {
            task.Category = null;
            task.CategoryId = null;
            return;
        }
        var categoryId = updateTaskRequest.CategoryId;
        var newCategory = _categoryRepository.FindById(categoryId.Value, userId);
        if (newCategory == null)
        {
            throw new CategoryNotFound(categoryId.Value);
        }
        task.Category = newCategory;
        task.CategoryId = newCategory.Id;   
    }
    private void UpdateCateogry(PatchTaskRequest patchTaskRequest, Models.Entities.Task task, long userId)
    {
        if (!patchTaskRequest.CategoryId.HasValue) return;
        UpdateCateogry(new UpdateTaskRequest(patchTaskRequest.Name ?? "", patchTaskRequest.Description ?? "", patchTaskRequest.CategoryId), task, userId);
    }
    public Models.Entities.Task? Update(UpdateTaskRequest updateTaskRequest, long id, long userId)
    {
        var task = _taskRepository.FindById(id, userId);
        if (task == null) return task;
        return _taskRepository.Update((task) =>
        {
            task.Name = updateTaskRequest.Name;
            task.Description = updateTaskRequest.Description;
            UpdateCateogry(updateTaskRequest, task, userId);
            task.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            _cacheManager.RemoveAllRelated($"task_page_{userId}");
        }, task);
    }
    public Models.Entities.Task? Patch(PatchTaskRequest patchTaskRequest, long id, long userId)
    {
        var task = _taskRepository.FindById(id, userId);
        if (task == null) return task;
        return _taskRepository.Update((task) =>
        {
            task.Name = patchTaskRequest.Name ?? task.Name;
            task.Description = patchTaskRequest.Description ?? task.Description;
            UpdateCateogry(patchTaskRequest, task, userId);
            task.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            _cacheManager.RemoveAllRelated($"task_page_{userId}");
        }, task);
    }
    public void Delete(long id, long userId)
    {
        var task = _taskRepository.FindById(id, userId);
        if (task == null) throw new TaskNotFound(id);
        _taskRepository.Delete(task);
        _cacheManager.RemoveAllRelated($"task_page_{userId}");
    }
}