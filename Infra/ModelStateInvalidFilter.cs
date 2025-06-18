using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManager.Controllers.DTO;

namespace TaskManager.Infra;

public class ModelStateInvalidFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No Filtering needed;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Dictionary<string, List<string>> errorsDictionary = new Dictionary<string, List<string>>();
        var modelState = context.ModelState;
        if (modelState == null || modelState.IsValid) return;
        foreach (var kvp in modelState)
        {
            var key = kvp.Key;
            var errors = kvp.Value;
            errorsDictionary[key] = errors.Errors.Select(e => e.ErrorMessage).ToList();
        }
        context.Result = new UnprocessableEntityObjectResult(
            new ApiResponseData<Dictionary<string, List<string>>>("Invalid request", errorsDictionary)
        );
    }
}