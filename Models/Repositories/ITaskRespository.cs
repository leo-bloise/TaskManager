using TaskManager.Models.Entities;

namespace TaskManager.Models.Repositories;

public interface ITaskRepository
{
    public Entities.Task Create(Entities.Task task);
    public Entities.Task? FindById(long id);
    public Page<Entities.Task> GetPage(Page<Entities.Task> page);
}