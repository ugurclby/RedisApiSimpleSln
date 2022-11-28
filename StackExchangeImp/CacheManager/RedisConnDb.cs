using StackExchange.Redis;

namespace StackExchangeImp.RedisManager
{
    public class RedisConnDb
    {
        private readonly string _redisHost;
        private readonly string _redisPort; 

        private ConnectionMultiplexer _ConnectionMultiplexer;

        public RedisConnDb(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
        }
        public IDatabase Connect(int dbIndex)
        {
            var config = $"{_redisHost}:{_redisPort}";
            _ConnectionMultiplexer = ConnectionMultiplexer.Connect(config);
            return _ConnectionMultiplexer.GetDatabase(dbIndex); 
        } 
    }
}
