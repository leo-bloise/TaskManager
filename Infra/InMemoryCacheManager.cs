using Microsoft.Extensions.Caching.Memory;
using TaskManager.Application;

namespace TaskManager.Infra
{
    public class InMemoryCacheManager : ICacheManager
    {
        private readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions()
        {
            SizeLimit = 1024
        });
        public T? Get<T>(string key)
        {
            return (T?) _cache.Get(key);
        }
        public void RemoveAllRelated(string key)
        {
            var keys = _cache.Keys.Select(k => k.ToString()).Where(k => k != null && k.Contains(key)).ToList();
            foreach(var k in keys)
            {
                _cache.Remove(k);
            }
        }
        public void Set<T>(string key, T value)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(1),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                Size = 1
            };
            _cache.Set(key, value, options);
        }
    }
}
