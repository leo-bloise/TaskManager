using TaskManager.Application;

namespace TaskManager.Infra
{
    public static class CacheManagerConfiguration
    {
        public static void ConfigureCacheManager(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<ICacheManager, InMemoryCacheManager>();
        }
    }
}
