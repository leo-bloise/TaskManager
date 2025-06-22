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
public class CategoriesController : Controller
{
    private ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        Category? category = _categoryService.FindById(id, userId.Value);
        if (category == null) return NotFound(new ApiResponse($"Not found category {id}"));
        return Ok(new ApiResponseData<CategoryCreatedResponse>("category found", CategoryCreatedResponse.Adapt(category)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        await _categoryService.Delete(id, userId.Value);
        return Ok(new ApiResponse($"category {id} deleted"));
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        Category category = _categoryService.Create(createCategoryRequest, userId.Value);
        return Created($"/categories/{category.Id}", new ApiResponseData<CategoryCreatedResponse>("category created successfully", 
        CategoryCreatedResponse.Adapt(category)));
    }
    [HttpGet]
    public IActionResult Paginate([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var userId = GetIdFromUser();
        if (!userId.HasValue) return Unauthorized(new ApiResponse("Unauthorized"));
        var categoryPage = _categoryService.Page(page, size, userId.Value);
        return Ok(new ApiResponseData<Page<CategoryCreatedResponse>>($"category page {page}", CategoryCreatedPage.Adapt(categoryPage)));
    }
}