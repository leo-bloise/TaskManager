using TaskManager.Application.Exceptions;
using TaskManager.Controllers.DTOs;
using TaskManager.Models.Entities;
using TaskManager.Models.Repositories;

namespace TaskManager.Application;

public class TaskService : ITaskService
{
    private ITaskRepository _taskRepository;
    private ICategoryRepository _categoryRepository;
    public TaskService(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
    {
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
    }
    private Models.Entities.Task CreateWithCategory(CreateTaskRequest createTaskRequest)
    {
        var categoryId = createTaskRequest.CategoryId.Value;
        Category? category = _categoryRepository.FindById(categoryId);
        if (category == null) throw new CategoryDoesNotExistsException(categoryId);
        Models.Entities.Task task = new()
        {
            Category = category,
            Description = createTaskRequest.Description,
            Name = createTaskRequest.Name
        };
        return _taskRepository.Create(task);
    }
    public Models.Entities.Task Create(CreateTaskRequest createTaskRequest)
    {
        if (createTaskRequest.CategoryId.HasValue)
        {
            return CreateWithCategory(createTaskRequest);
        }
        Models.Entities.Task task = new()
        {
            Category = null,
            Description = createTaskRequest.Description,
            Name = createTaskRequest.Name,
        };
        return _taskRepository.Create(task);
    }
    public Models.Entities.Task? Get(long id)
    {
        return _taskRepository.FindById(id);
    }
    public Page<Models.Entities.Task> GetPage(int page, int size)
    {
        TaskPage taskPage = new TaskPage(new List<Models.Entities.Task>(), page, size, 0, 0);
        return _taskRepository.GetPage(taskPage);
    }
}