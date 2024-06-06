using DemoRedis.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DemoRedis.Services
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        public RedisService(IOptions<AppSettings> option)
        {
            var redisSettings = option.Value.Redis;
            var configs = ConfigurationOptions.Parse(redisSettings.ConnectionString);
            configs.Password = redisSettings.Password;
            _redis = ConnectionMultiplexer.Connect(configs);
            _database = _redis.GetDatabase();
        }

        public IDatabase GetDatabase()
        {
            return _database;
        }

        public void SetValue<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            _database.StringSet(key, json);
        }

        public T GetValue<T>(string key)
        {
            var json = _database.StringGet(key).ToString();
            if (json == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void RemoveCache(string key)
        {
            _database.KeyDelete(key);
        }
    }
}
