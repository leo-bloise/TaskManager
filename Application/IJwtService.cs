using TaskManager.Models.Entities;

namespace TaskManager.Application;

public interface IJwtService
{
    public string GenerateToken(User user);    
}