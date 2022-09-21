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
            //�e�itli validasyonlardan ge�ip login oldu�unu d���n�yoruz ve login bilgisini redis e at�yoruz.
            _redisService.Add("USER", userLogin,    new DistributedCacheEntryOptions { AbsoluteExpiration=DateTime.Now.AddSeconds(30)});
            //Uygulamaya giri� yap�ld��� anda de�i�mesi m�mk�n olmayan bir veri grubunu redis e at�yoruz. 
            _redisService.Add("CURRENCIES", _databaseService.GetDbAllCurrency(),new DistributedCacheEntryOptions { SlidingExpiration=TimeSpan.FromMinutes(60) });
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        { 
            // Yap�lmas� planlanan herhangi bir i�lemde �r�n bilgilerine ula��lmas� gerekti�inde
            if (_redisService.Any("USER"))
            { 
                //Db e gidip verileri ald���n� d���nelim.
                return Ok(_databaseService.GetDbAllProduct());
            }
            return BadRequest("�ye bulunamad�");

        }
        [HttpGet("GetAllCurrency")]
        public IActionResult GetAllCurrency()
        {
            // Yap�lmas� planlanan herhangi bir i�lemde �r�n bilgilerine ula��lmas� gerekti�inde
            if (_redisService.Any("USER"))
            {
                return Ok(_redisService.Get<List<Currencies>>("CURRENCIES"));
            }
            return BadRequest("�ye bulunamad�");

        }

    }
}