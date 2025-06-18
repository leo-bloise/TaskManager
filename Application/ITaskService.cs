using TaskManager.Controllers.DTOs;
using TaskManager.Models.Entities;

namespace TaskManager.Application;

public interface ITaskService
{
    public Models.Entities.Task Create(CreateTaskRequest createTaskRequest);
    public Models.Entities.Task? Get(long id);
    public Page<Models.Entities.Task> GetPage(int page, int size);
}