using TaskManager.Controllers.DTOs;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;

namespace TaskManager.Application;

public interface ITaskService
{
    public Models.Entities.Task Create(CreateTaskRequest createTaskRequest, long userId);
    public Models.Entities.Task? Get(long id, long userId);
    public Page<Models.Entities.Task> GetPage(int page, int size, TaskFilter? taskFilter, long userId);
    public Models.Entities.Task? Update(UpdateTaskRequest updateTaskRequest, long id, long userId);
    public Models.Entities.Task? Patch(PatchTaskRequest patchTaskRequest, long id, long userId);
    public void Delete(long id, long userId);
}