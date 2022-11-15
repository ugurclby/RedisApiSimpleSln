using Bogus;
using IDistributedCacheImp.DatabaseManager;
using IDistributedCacheImp.Model;
using IDistributedCacheImp.CacheManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheImp.Controllers
{
    public class IDistributedCacheController : BaseApiController
    {
        public IDistributedCacheController(ILogger<IDistributedCacheController> logger, ICacheService cacheService,DatabaseService databaseService ) : base(logger, cacheService, databaseService)
        {
             
        }

        [HttpPost]
        public void Login(UserLogin userLogin)
        {
            //Çeþitli validasyonlardan geçip login olduðunu düþünüyoruz ve login bilgisini redis e atýyoruz.
            //Projenin herhangi bir yerinde istersek login kontrolü yada user bilgisinin gerekli olduðu yerlerde kullanabiliriz.
            _cacheService.Add("USER", userLogin,DateTime.Now.AddMinutes(30),null);
            //Uygulamaya giriþ yapýldýðý anda deðiþmesi mümkün olmayan bir veri grubunu redis e atýyoruz. 
            _cacheService.Add("CURRENCIES", _databaseService.GetDbAllCurrency(),null,TimeSpan.FromMinutes(60));
            _cacheService.Add("VEHICLES", _databaseService.GetDbAllUserVehicle(), null, TimeSpan.FromMinutes(60));
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        { 
            // Yapýlmasý planlanan herhangi bir iþlemde ürün bilgilerine ulaþýlmasý gerektiðinde
            if (_cacheService.Any("USER"))
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
            if (_cacheService.Any("USER"))
            {
                return Ok(_cacheService.Get<List<Currencies>>("CURRENCIES"));
            }
            return BadRequest("Lütfen Giriþ Yapýnýz..!");

        }
        [HttpGet("GetAllUserVehicle")]
        public IActionResult GetAllUserVehicle()
        {
            var user = _cacheService.Get<UserLogin>("USER");

            // Yapýlmasý planlanan herhangi bir iþlemde ürün bilgilerine ulaþýlmasý gerektiðinde
            if (user != null)
            {
                var vehicles = _cacheService.Get<List<Vehicles>>("VEHICLES").Where(x=>x.UserName== user.UserCode).ToList(); 

                return Ok(vehicles);
            }
            return BadRequest("Lütfen Giriþ Yapýnýz..!");

        }
    }
}