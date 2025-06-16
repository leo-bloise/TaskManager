using Microsoft.AspNetCore.Mvc;
using TaskManager.Application;
using TaskManager.Controllers.DTO;
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
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok();
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateUserRequest createUserRequest)
    {
        User user = _userService.Create(createUserRequest);
        return Created($"/users/{user.Id}", new ApiResponseData<UserCreatedResponse>("user created successfully", UserCreatedResponse.Adapt(user)));
    }
}