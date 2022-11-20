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
        public void Connect()
        {
            var config = $"{_redisHost}:{_redisPort}";
            _ConnectionMultiplexer = ConnectionMultiplexer.Connect(config);
        }
        public IDatabase GetDb(int dbIndex) =>  _ConnectionMultiplexer.GetDatabase(dbIndex);
    }
}
