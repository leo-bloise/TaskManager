namespace TaskManager.Application.Exceptions;

public class EmailAlreadyTakenException : Exception
{
    public EmailAlreadyTakenException(string email) : base($"Email {email} was already taken") {}
}