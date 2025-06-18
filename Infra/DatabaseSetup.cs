using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;
using TaskManager.Models.Repositories;

namespace TaskManager.Infra;

public static class DatabaseSetup {
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TaskManagerDbContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("Default")
            );
        });
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }
}