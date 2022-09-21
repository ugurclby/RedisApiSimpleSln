using Bogus;
using IDistributedCacheImp.DatabaseManager;
using IDistributedCacheImp.Model;
using IDistributedCacheImp.RedisManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheImp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    { 
        protected  ILogger<IDistributedCacheController> _logger;
        protected IRedisService _redisService;
        protected DatabaseService _databaseService;
        public BaseApiController(ILogger<IDistributedCacheController> logger, IRedisService redisService, DatabaseService databaseService)
        {
            _logger = logger;
            _redisService = redisService; 
            _databaseService= databaseService;
        }
        
    } 
}