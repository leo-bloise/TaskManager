using Microsoft.Extensions.Logging.Console;
namespace TaskManager.Api.Infra
{
    public static class InfraStartup
    {
        public static void ConfigureApiLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddSimpleConsole(options =>
            {
                options.TimestampFormat = "[HH:mm:ss]";
                options.SingleLine = true;
                options.ColorBehavior = LoggerColorBehavior.Disabled;
                options.IncludeScopes = false;
            });
        }
        public static void ConfigureApiExceptionHandler(this WebApplicationBuilder builder)
        {
            builder.Services.AddExceptionHandler<ApiExceptionHandler>();
        }
    }
}