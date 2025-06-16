namespace TaskManager.Application.Exceptions;

public class UsernameAlreadyTakenException : ApplicationException
{
    public UsernameAlreadyTakenException(string username) : base($"Username {username} was already taken")
    {
        
    }
}