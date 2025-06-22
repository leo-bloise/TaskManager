using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application;
using TaskManager.Controllers.DTO;
using TaskManager.Controllers.DTOs;
using TaskManager.Controllers.DTOs.Input;
using TaskManager.Controllers.DTOs.Output;
using TaskManager.Models.Entities;

namespace TaskManager.Controllers;

[Authorize]
public class TasksController : Controller
{
    private ITaskService _taskService;
    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    [HttpGet]
    public IActionResult PageTasks([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] TaskFilter? taskFilter = null)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        if (page < 1)
        {
            return new UnprocessableEntityObjectResult(new ApiResponse("0 or negative pages are not allowed"));
        }
        var taskPage = _taskService.GetPage(page, size, taskFilter, userId.Value);
        return Ok(new ApiResponseData<Page<TaskCreatedResponse>>("Task page", TaskCreatedPage.Adapt(taskPage)));
    }
    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        var task = _taskService.Get(id, userId.Value);
        if (task == null)
        {
            return NotFound(new ApiResponse($"Task {id} Not found"));
        }
        return Ok(new ApiResponseData<TaskCreatedResponse>($"task {id} found", TaskCreatedResponse.Adapt(task)));
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskRequest createTaskRequest)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        var task = _taskService.Create(createTaskRequest, userId.Value);
        return Created($"/tasks/{task.Id}", new ApiResponseData<TaskCreatedResponse>("task created successfully", TaskCreatedResponse.Adapt(task)));
    }
    [HttpPut("{id}")]
    public IActionResult UpdateTask([FromBody] UpdateTaskRequest updateTaskRequest, long id)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        var task = _taskService.Update(updateTaskRequest, id, userId.Value);
        if (task == null) return NotFound(new ApiResponse($"task id {id} not found"));
        return Ok(new ApiResponseData<TaskCreatedResponse>("task updated", TaskCreatedResponse.Adapt(task)));
    }
    [HttpPatch("{id}")]
    public IActionResult PatchTask([FromBody] PatchTaskRequest patchTaskRquest, long id)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        var task = _taskService.Patch(patchTaskRquest, id, userId.Value);
        if (task == null) return NotFound(new ApiResponse($"task id {id} not found"));
        return Ok(new ApiResponseData<TaskCreatedResponse>("task updated", TaskCreatedResponse.Adapt(task)));
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(long id)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        _taskService.Delete(id, userId.Value);
        return Ok(new ApiResponse($"task id {id} was deleted"));
    }
}