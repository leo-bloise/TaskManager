namespace TaskManager.Controllers.DTOs;

public class ApiResponse
{
    public string Timestamp { get; set; }
    public string Message { get; set; }

    public ApiResponse(string message)
    {
        Message = message;
        Timestamp = DateTime.UtcNow.ToString();
    }
}