using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers.DTOs.Input;

namespace TaskManager.Controllers;

public class UsersController : Controller
{
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok();
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateUserRequest createUserRequest)
    {
        return Created("/users/12", "User created successfully");
    }
}