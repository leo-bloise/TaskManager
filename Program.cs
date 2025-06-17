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
        builder.Services.AddControllers();
        builder.ConfigureJwt();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        WebApplication app = builder.Build();
        app.MapControllers();
        app.ConfigureExceptionHandler();
        app.UseAuthorization();
        app.Run();
    }
}