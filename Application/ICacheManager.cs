namespace TaskManager.Application
{
    public interface ICacheManager
    {
        public T? Get<T>(string key);
        public void Set<T>(string key, T value);
        public void RemoveAllRelated(string key);
    }
}
