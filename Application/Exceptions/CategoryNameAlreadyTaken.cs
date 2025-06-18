namespace TaskManager.Application.Exceptions;

public class CategoryNameAlreadyTaken : ApplicationException
{
    public CategoryNameAlreadyTaken(string name) : base($"Category name {name} already taken") { }
}