namespace TaskManager.Application.Exceptions;

public class CategoryDoesNotExistsException : ApplicationException
{
    public CategoryDoesNotExistsException(long id) : base($"category id {id} does not exist")
    {
    }
}