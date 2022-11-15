using Bogus;
using IDistributedCacheImp.DatabaseManager;
using IDistributedCacheImp.Model;
using IDistributedCacheImp.CacheManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheImp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    { 
        protected  ILogger<IDistributedCacheController> _logger;
        protected ICacheService _cacheService;
        protected DatabaseService _databaseService;
        public BaseApiController(ILogger<IDistributedCacheController> logger, ICacheService cacheService, DatabaseService databaseService)
        {
            _logger = logger;
            _cacheService = cacheService; 
            _databaseService= databaseService;
        }
        
    } 
}