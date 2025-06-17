using Microsoft.AspNetCore.Identity;
using TaskManager.Application.Exceptions;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Models.Entities;
using TaskManager.Models.Repositories;

namespace TaskManager.Application;

public class AuthService : IAuthService
{
    private IJwtService _jwtService;
    private IUserRepository _userRepository;
    private IPasswordHasher<User> _passwordHasher;
    public AuthService(IJwtService jwtService, IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    public string Login(LoginRequest request)
    {
        User? user = _userRepository.FindByUsername(request.Username);
        if (user == null)
        {
            throw new UnauthorizedException("Unauthorized");
        }
        if (_passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedException("Unauthorized");
        }
        return _jwtService.GenerateToken(user);
    }
}
