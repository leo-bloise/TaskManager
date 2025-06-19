namespace TaskManager.Application.Exceptions;

public class CategoryNotFound : ApplicationException
{
    public CategoryNotFound(long categoryId) : base($"category of id {categoryId} was not found")
    {
        StatusCode = 404;
    }
}