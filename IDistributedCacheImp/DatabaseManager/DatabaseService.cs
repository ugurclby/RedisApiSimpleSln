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
        public List<Vehicles> GetDbAllUserVehicle()
        {
            var faker = new Faker();
            List<Vehicles> listVehicles = new List<Vehicles>();
            for (int i = 0; i < 100; i++)
            {
                listVehicles.Add(new Vehicles
                {
                    VehicleVin = faker.Vehicle.Vin(),
                    VehicleName = faker.Vehicle.Model(),
                    UserName = i%2==0?faker.Name.FullName():"ugur"
                });
            } 

            return listVehicles;
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
