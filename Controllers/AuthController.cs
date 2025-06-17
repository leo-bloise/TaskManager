using Microsoft.AspNetCore.Mvc;
using TaskManager.Application;
using TaskManager.Controllers.DTO;
using TaskManager.Controllers.DTOs.Input;

namespace TaskManager.Controllers;

public class AuthController : Controller
{
    private IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        string token = _authService.Login(loginRequest);
        return Ok(new ApiResponseData<string>("User authenticated successfully", token));
    }
}