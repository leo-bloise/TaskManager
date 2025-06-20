using TaskManager.Controllers.DTOs;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;

namespace TaskManager.Application;

public interface ITaskService
{
    public Models.Entities.Task Create(CreateTaskRequest createTaskRequest);
    public Models.Entities.Task? Get(long id);
    public Page<Models.Entities.Task> GetPage(int page, int size);
    public Models.Entities.Task? Update(UpdateTaskRequest updateTaskRequest, long id);
    public Models.Entities.Task? Patch(PatchTaskRequest patchTaskRequest, long id);
}