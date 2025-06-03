namespace TaskManager.Api.Application.Errors
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message) {}
    }
}