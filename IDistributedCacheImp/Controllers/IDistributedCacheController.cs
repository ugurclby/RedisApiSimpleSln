using Bogus;
using IDistributedCacheImp.DatabaseManager;
using IDistributedCacheImp.Model;
using IDistributedCacheImp.RedisManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheImp.Controllers
{
    public class IDistributedCacheController : BaseApiController
    {
        public IDistributedCacheController(ILogger<IDistributedCacheController> logger, IRedisService redisService,DatabaseService databaseService ) : base(logger, redisService , databaseService)
        {
             
        }

        [HttpPost]
        public void Login(UserLogin userLogin)
        {
            //Çeþitli validasyonlardan geçip login olduðunu düþünüyoruz ve login bilgisini redis e atýyoruz.
            _redisService.Add("USER", userLogin,    new DistributedCacheEntryOptions { AbsoluteExpiration=DateTime.Now.AddSeconds(30)});
            //Uygulamaya giriþ yapýldýðý anda deðiþmesi mümkün olmayan bir veri grubunu redis e atýyoruz. 
            _redisService.Add("CURRENCIES", _databaseService.GetDbAllCurrency(),new DistributedCacheEntryOptions { SlidingExpiration=TimeSpan.FromMinutes(60) });
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        { 
            // Yapýlmasý planlanan herhangi bir iþlemde ürün bilgilerine ulaþýlmasý gerektiðinde
            if (_redisService.Any("USER"))
            { 
                //Db e gidip verileri aldýðýný düþünelim.
                return Ok(_databaseService.GetDbAllProduct());
            }
            return BadRequest("Üye bulunamadý");

        }
        [HttpGet("GetAllCurrency")]
        public IActionResult GetAllCurrency()
        {
            // Yapýlmasý planlanan herhangi bir iþlemde ürün bilgilerine ulaþýlmasý gerektiðinde
            if (_redisService.Any("USER"))
            {
                return Ok(_redisService.Get<List<Currencies>>("CURRENCIES"));
            }
            return BadRequest("Üye bulunamadý");

        }

    }
}