using Microsoft.AspNetCore.Diagnostics;

namespace TaskManager.Api.Infra
{
    public class ApiExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ApiExceptionHandler> _logger;
        public ApiExceptionHandler(ILogger<ApiExceptionHandler> logger)
        {
            _logger = logger;
        }
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Exception received of type {exception.GetType().Name}");
            return ValueTask.FromResult<bool>(true);
        }
    }
}