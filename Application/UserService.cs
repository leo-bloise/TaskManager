using Microsoft.AspNetCore.Identity;
using TaskManager.Application.Exceptions;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;
using TaskManager.Models.Repositories;

namespace TaskManager.Application;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private IPasswordHasher<User> _passwordHasher;
    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public User Create(CreateUserRequest createUserRequest)
    {
        if (_userRepository.ExistsByEmail(createUserRequest.Email))
        {
            throw new EmailAlreadyTakenException(createUserRequest.Email);
        }
        if (_userRepository.ExistsByUsername(createUserRequest.Username))
        {
            throw new UsernameAlreadyTakenException(createUserRequest.Username);
        }
        User user = new User()
        {
            Email = createUserRequest.Email,
            Username = createUserRequest.Username
        };
        user.Password = _passwordHasher.HashPassword(user, createUserRequest.Password);
        return _userRepository.Create(user);
    }
    public User? GetById(long id)
    {
        return _userRepository.FindById(id);
    }
}