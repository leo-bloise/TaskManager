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
        Category? category = _categoryService.FindById(id);
        if (category == null) return NotFound(new ApiResponse($"Not found category {id}"));
        return Ok(new ApiResponseData<CategoryCreatedResponse>("category found", CategoryCreatedResponse.Adapt(category)));
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        Category category = _categoryService.Create(createCategoryRequest);
        return Created($"/categories/{category.Id}", new ApiResponseData<CategoryCreatedResponse>("category created successfully", 
        CategoryCreatedResponse.Adapt(category)));
    }
}