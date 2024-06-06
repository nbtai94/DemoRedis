namespace DemoRedis.Models
{
    public class AppSettings
    {
        public AppSettings() { }
        public Redis Redis { get; set; }
    }

    public class Redis
    {
        public Redis() { }

        public string ConnectionString { get; set; }
        public string Password { get; set; }
    }
}
