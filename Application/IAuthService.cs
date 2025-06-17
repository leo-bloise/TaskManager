using TaskManager.Controllers.DTOs.Input;

namespace TaskManager.Application;

public interface IAuthService
{
    public string Login(LoginRequest request);
}