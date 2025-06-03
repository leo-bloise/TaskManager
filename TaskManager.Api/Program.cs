using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data.Repositories;
using TaskManager.Api.Infra;
using TaskManager.Data;
using TaskManager.Data.Repositories;
using TaskManager.Domain;

namespace TaskManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.ConfigureApiLogging();
            builder.ConfigureApiExceptionHandler();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IPasswordHasher<User>, BCryptPasswordHasher>();
            builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddControllers();
            builder.Services.AddDbContext<TaskManagerContext>(options =>
            {
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("Default")
                );
            });
            WebApplication app = builder.Build();
            app.MapControllers();
            app.Run();
        }
    }
}