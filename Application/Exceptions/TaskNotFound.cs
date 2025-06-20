namespace TaskManager.Application.Exceptions;

public class TaskNotFound : ApplicationException
{
    public TaskNotFound(long id) : base($"task {id} was not found")
    {
        StatusCode = StatusCodes.Status404NotFound;
    }
}