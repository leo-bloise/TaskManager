using TaskManager.Infra;

namespace TaskManager;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.ConfigureDatabase();
        builder.Services.AddControllers();
        WebApplication app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}