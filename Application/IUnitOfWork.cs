namespace TaskManager.Application;

public interface IUnitOfWork
{
    public Task ExecuteAsync(Action action);
}