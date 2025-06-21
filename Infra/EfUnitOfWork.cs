using TaskManager.Application;

namespace TaskManager.Infra;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly TaskManagerDbContext _taskManagerDbContext;
    public EfUnitOfWork(TaskManagerDbContext taskManagerDbContext)
    {
        _taskManagerDbContext = taskManagerDbContext;
    }
    public Task ExecuteAsync(Action action)
    {
        using var transaction = _taskManagerDbContext.Database.BeginTransaction();
        try
        {
            action.Invoke();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
        return Task.CompletedTask;
    }
}