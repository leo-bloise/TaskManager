using Microsoft.AspNetCore.Authorization;
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
    public IActionResult PageTasks([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var taskPage = _taskService.GetPage(page, size);
        return Ok(new ApiResponseData<Page<TaskCreatedResponse>>("Task page", TaskCreatedPage.Adapt(taskPage)));
    }
    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        var task = _taskService.Get(id);
        if (task == null)
        {
            return NotFound(new ApiResponse($"Task {id} Not found"));
        }
        return Ok(new ApiResponseData<TaskCreatedResponse>($"task {id} found", TaskCreatedResponse.Adapt(task)));
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskRequest createTaskRequest)
    {
        var task = _taskService.Create(createTaskRequest);
        return Created($"/tasks/{task.Id}", new ApiResponseData<TaskCreatedResponse>("task created successfully", TaskCreatedResponse.Adapt(task)));
    }
    [HttpPut("{id}")]
    public IActionResult UpdateTask([FromBody] UpdateTaskRequest updateTaskRequest, long id)
    {
        var task = _taskService.Update(updateTaskRequest, id);
        if (task == null) return NotFound(new ApiResponse($"task id {id} not found"));
        return Ok(new ApiResponseData<TaskCreatedResponse>("task updated", TaskCreatedResponse.Adapt(task)));
    }
    [HttpPatch("{id}")]
    public IActionResult PatchTask([FromBody] PatchTaskRequest patchTaskRquest, long id)
    {
        var task = _taskService.Patch(patchTaskRquest, id);
        if (task == null) return NotFound(new ApiResponse($"task id {id} not found"));
        return Ok(new ApiResponseData<TaskCreatedResponse>("task updated", TaskCreatedResponse.Adapt(task)));
    }
}