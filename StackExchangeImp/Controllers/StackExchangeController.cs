using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchangeImp.Model;
using StackExchangeImp.RedisManager;

namespace StackExchangeImp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StackExchangeController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public StackExchangeController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet]
        public ActionResult ListBasket()
        {
            
            return Ok();
        }

        [HttpPost]
        public ActionResult AddBasket()
        {

            _cacheService.AddString<string>("Urun", "test", null);
            _cacheService.AddString<string>("Yeni", "test2", null);
            return Ok();
        }
        //[HttpPost]
        //public ActionResult AddBasket(Products products)
        //{

        //    _cacheService.AddString<string>("Urun", "test", null);
        //    return Ok();
        //}
        
        [HttpDelete]
        public ActionResult DeleteBasket()
        {
            _cacheService.AddString<string>("Urun", "test3", null);
            return Ok();
        } 
        
        //[HttpDelete]
        //public ActionResult DeleteBasket(int productId)
        //{
        //    return Ok();
        //}
    }
}
