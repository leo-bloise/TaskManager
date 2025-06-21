using TaskManager.Application.Exceptions;
using TaskManager.Controllers.DTOs;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;
using TaskManager.Models.Filters;
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
    public Page<Models.Entities.Task> GetPage(int page, int size, TaskFilter? taskFilter)
    {
        TaskPage taskPage = new TaskPage(new List<Models.Entities.Task>(), page, size, 0, 0);
        var newTaskPageFiltered = _taskRepository.FilterAndPaginate(TaskFilterPage.Adapt(taskFilter, taskPage));
        return newTaskPageFiltered.Page;
    }
    private void UpdateCateogry(UpdateTaskRequest updateTaskRequest, Models.Entities.Task task)
    {
        if (!updateTaskRequest.CategoryId.HasValue)
        {
            task.Category = null;
            return;
        }
        var categoryId = updateTaskRequest.CategoryId;
        var newCategory = _categoryRepository.FindById(categoryId.Value);
        if (newCategory == null)
        {
            throw new CategoryNotFound(categoryId.Value);
        }
        task.Category = newCategory;
        task.CategoryId = newCategory.Id;   
    }
    private void UpdateCateogry(PatchTaskRequest patchTaskRequest, Models.Entities.Task task)
    {
        if (!patchTaskRequest.CategoryId.HasValue) return;
        UpdateCateogry(new UpdateTaskRequest(patchTaskRequest.Name ?? "", patchTaskRequest.Description ?? "", patchTaskRequest.CategoryId), task);
    }
    public Models.Entities.Task? Update(UpdateTaskRequest updateTaskRequest, long id)
    {
        var task = _taskRepository.FindById(id);
        if (task == null) return task;
        return _taskRepository.Update((task) =>
        {
            task.Name = updateTaskRequest.Name;
            task.Description = updateTaskRequest.Description;
            UpdateCateogry(updateTaskRequest, task);
            task.UpdatedAt = DateTime.UtcNow;
        }, task);
    }
    public Models.Entities.Task? Patch(PatchTaskRequest patchTaskRequest, long id)
    {
        var task = _taskRepository.FindById(id);
        if (task == null) return task;
        return _taskRepository.Update((task) =>
        {
            task.Name = patchTaskRequest.Name ?? task.Name;
            task.Description = patchTaskRequest.Description ?? task.Description;
            UpdateCateogry(patchTaskRequest, task);
            task.UpdatedAt = DateTime.UtcNow;   
        }, task);
    }
    public void Delete(long id)
    {
        var task = _taskRepository.FindById(id);
        if (task == null) throw new TaskNotFound(id);
        _taskRepository.Delete(task);
    }
}