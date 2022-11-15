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
            var products = _cacheService.GetList<Products>("BASKET");
            return Ok(products);
        }
         
        [HttpPost]
        public ActionResult AddBasket(Products products)
        {
            _cacheService.AddList<Products>("BASKET", products); 
            return Ok();
        } 

        [HttpDelete("{productId}")]
        public ActionResult DeleteBasket(int productId)
        {
            var products = _cacheService.GetList<Products>("BASKET");
            _cacheService.DeleteList<Products>("BASKET",products.Where(x => x.ProductId == productId).FirstOrDefault()); 
            return Ok();
        }
    }
}
