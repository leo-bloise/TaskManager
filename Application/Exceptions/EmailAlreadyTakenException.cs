namespace TaskManager.Application.Exceptions;

public class EmailAlreadyTakenException : ApplicationException
{
    public EmailAlreadyTakenException(string email) : base($"Email {email} was already taken") {}
}