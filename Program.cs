using Microsoft.AspNetCore.Mvc;
using TaskManager.Application;
using TaskManager.Infra;
using TaskManager.Infra.Authentication;

namespace TaskManager;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.ConfigureDatabase();
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ModelStateInvalidFilter>();
        });
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        builder.ConfigureJwt();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        WebApplication app = builder.Build();
        app.MapControllers();
        app.ConfigureExceptionHandler();
        app.UseAuthorization();
        app.Run();
    }
}