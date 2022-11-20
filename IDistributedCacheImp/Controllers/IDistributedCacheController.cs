using Bogus;
using IDistributedCacheImp.DatabaseManager;
using IDistributedCacheImp.Model;
using IDistributedCacheImp.CacheManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheImp.Controllers
{
    public class IDistributedCacheController : ControllerBase
    {
        protected ILogger<IDistributedCacheController> _logger;
        protected ICacheService _cacheService;
        protected DatabaseService _databaseService;
        public IDistributedCacheController(ILogger<IDistributedCacheController> logger, ICacheService cacheService,DatabaseService databaseService )
        {
            _logger = logger;
            _cacheService = cacheService;
            _databaseService = databaseService;
        }


        [HttpPost("Login")]
        public void Login(UserLogin userLogin)
        {
            //�e�itli validasyonlardan ge�ip login oldu�unu d���n�yoruz ve login bilgisini redis e at�yoruz.
            //Projenin herhangi bir yerinde istersek login kontrol� yada user bilgisinin gerekli oldu�u yerlerde kullanabiliriz.
            _cacheService.Add("USER", userLogin,DateTime.Now.AddMinutes(30),null);
            //Uygulamaya giri� yap�ld��� anda de�i�mesi m�mk�n olmayan bir veri grubunu redis e at�yoruz. 
            _cacheService.Add("CURRENCIES", _databaseService.GetDbAllCurrency(),null,TimeSpan.FromMinutes(60));
            _cacheService.Add("VEHICLES", _databaseService.GetDbAllUserVehicle(), null, TimeSpan.FromMinutes(60));
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        { 
            // Yap�lmas� planlanan herhangi bir i�lemde �r�n bilgilerine ula��lmas� gerekti�inde
            if (_cacheService.Any("USER"))
            { 
                //Db e gidip verileri ald���n� d���nelim.
                return Ok(_databaseService.GetDbAllProduct());
            }
            return BadRequest("L�tfen Giri� Yap�n�z..!");

        }
        [HttpGet("GetAllCurrency")]
        public IActionResult GetAllCurrency()
        {
            // Yap�lmas� planlanan herhangi bir i�lemde �r�n bilgilerine ula��lmas� gerekti�inde
            if (_cacheService.Any("USER"))
            {
                return Ok(_cacheService.Get<List<Currencies>>("CURRENCIES"));
            }
            return BadRequest("L�tfen Giri� Yap�n�z..!");

        }
        [HttpGet("GetAllUserVehicle")]
        public IActionResult GetAllUserVehicle()
        {
            var user = _cacheService.Get<UserLogin>("USER");

            // Yap�lmas� planlanan herhangi bir i�lemde �r�n bilgilerine ula��lmas� gerekti�inde
            if (user != null)
            {
                var vehicles = _cacheService.Get<List<Vehicles>>("VEHICLES").Where(x=>x.UserName== user.UserCode).ToList(); 

                return Ok(vehicles);
            }
            return BadRequest("L�tfen Giri� Yap�n�z..!");

        }
    }
}