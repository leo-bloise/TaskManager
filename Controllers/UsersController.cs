using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application;
using TaskManager.Controllers.DTO;
using TaskManager.Controllers.DTOs;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Controllers.DTOs.Output;
using TaskManager.Models.Entities;

namespace TaskManager.Controllers;

public class UsersController : Controller
{
    private IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("me")]
    [Authorize]
    public IActionResult Get()
    {
        int? userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        User? user = _userService.GetById(userId.Value);
        if (user == null)
        {
            return NotFound(new ApiResponse("User not found"));
        }
        return Ok(new ApiResponseData<UserCreatedResponse>("User data", UserCreatedResponse.Adapt(user)));
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateUserRequest createUserRequest)
    {
        User user = _userService.Create(createUserRequest);
        return Created($"/users/me", new ApiResponseData<UserCreatedResponse>("user created successfully", UserCreatedResponse.Adapt(user)));
    }
}