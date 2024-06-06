using StackExchange.Redis;

namespace DemoRedis.Services
{
    public interface IRedisService
    {
        IDatabase GetDatabase();

        void SetValue<T>(string key, T value);
        T GetValue<T>(string key);

        void RemoveCache(string key);
    }
}
