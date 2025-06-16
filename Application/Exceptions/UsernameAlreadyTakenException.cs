namespace TaskManager.Application.Exceptions;

public class UsernameAlreadyTakenException : Exception
{
    public UsernameAlreadyTakenException(string username) : base($"Username {username} was already taken")
    {
        
    }
}