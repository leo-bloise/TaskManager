using TaskManager.Controllers.DTOs;

namespace TaskManager.Application.Exceptions;

public abstract class ApplicationException : Exception
{
    public int StatusCode { get; set; }
    public ApplicationException(string message) : base(message)
    {

    }
    public ApiResponse ToApiResponse()
    {
        return new ApiResponse(Message);
    }
}