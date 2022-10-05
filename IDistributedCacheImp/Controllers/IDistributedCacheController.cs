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
            //Projenin herhangi bir yerinde istersek login kontrolü yada user bilgisinin gerekli olduðu yerlerde kullanabiliriz.
            _redisService.Add("USER", userLogin,DateTime.Now.AddMinutes(30),null);
            //Uygulamaya giriþ yapýldýðý anda deðiþmesi mümkün olmayan bir veri grubunu redis e atýyoruz. 
            _redisService.Add("CURRENCIES", _databaseService.GetDbAllCurrency(),null,TimeSpan.FromMinutes(60));
            _redisService.Add("VEHICLES", _databaseService.GetDbAllUserVehicle(), null, TimeSpan.FromMinutes(60));
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
            return BadRequest("Lütfen Giriþ Yapýnýz..!");

        }
        [HttpGet("GetAllCurrency")]
        public IActionResult GetAllCurrency()
        {
            // Yapýlmasý planlanan herhangi bir iþlemde ürün bilgilerine ulaþýlmasý gerektiðinde
            if (_redisService.Any("USER"))
            {
                return Ok(_redisService.Get<List<Currencies>>("CURRENCIES"));
            }
            return BadRequest("Lütfen Giriþ Yapýnýz..!");

        }
        [HttpGet("GetAllUserVehicle")]
        public IActionResult GetAllUserVehicle()
        {
            var user = _redisService.Get<UserLogin>("USER");

            // Yapýlmasý planlanan herhangi bir iþlemde ürün bilgilerine ulaþýlmasý gerektiðinde
            if (user != null)
            {
                var vehicles = _redisService.Get<List<Vehicles>>("VEHICLES").Where(x=>x.UserName== user.UserCode).ToList(); 

                return Ok(vehicles);
            }
            return BadRequest("Lütfen Giriþ Yapýnýz..!");

        }
    }
}