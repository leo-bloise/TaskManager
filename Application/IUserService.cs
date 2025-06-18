using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;

namespace TaskManager.Application;

public interface IUserService
{
    public User Create(CreateUserRequest createUserRequest);
    public User? GetById(long id);
}