using Bogus;
using IDistributedCacheImp.Model;

namespace IDistributedCacheImp.DatabaseManager
{
    public class DatabaseService
    { 
        public List<Currencies> GetDbAllCurrency()
        {
            List<Currencies> listCurrencies = new List<Currencies>();
            listCurrencies.Add(new Currencies
            {
                CurrencyID = 1,
                CurrencyName = "TL"
            });
            listCurrencies.Add(new Currencies
            {
                CurrencyID = 2,
                CurrencyName = "EUR"
            });
            listCurrencies.Add(new Currencies
            {
                CurrencyID = 3,
                CurrencyName = "USD"
            });
            return listCurrencies;
        } 
        public List<Products> GetDbAllProduct()
        {
            var faker = new Faker();
            List<Products> products = new List<Products>();

            for (int i = 0; i < 30; i++)
            {
                products.Add(new Products
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductName = faker.Commerce.Product()
                });
            }
            return products;
        }
    }
}
