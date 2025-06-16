using Microsoft.AspNetCore.Diagnostics;
using TaskManager.Controllers.DTOs;

namespace TaskManager.Infra;

public static class ExceptionHandling
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exceptionPath = context.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionPath == null) return;
                var error = exceptionPath.Error;
                if (error == null) return;
                if (error.GetType().IsAssignableTo(typeof(Application.Exceptions.ApplicationException)))
                {
                    var appError = error as Application.Exceptions.ApplicationException;
                    context.Response.StatusCode = appError!.StatusCode == 0 ? StatusCodes.Status422UnprocessableEntity : appError.StatusCode;
                    await context.Response.WriteAsJsonAsync(appError!.ToApiResponse());
                    return;
                }
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ApiResponse("Internal Server Error"));
                return;
            });
        });
    }
}