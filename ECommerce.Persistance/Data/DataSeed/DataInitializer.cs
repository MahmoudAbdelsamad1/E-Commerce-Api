using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Percistance.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ECommerce.Percistance.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;



        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;

            var path = AppContext.BaseDirectory;

            Console.WriteLine(path + " ===========================");
        }

        async Task IDataInitializer.InitializerAsync()
   
        {
            try
            {
                var HasBrands = await  _dbContext.ProductBrands.AnyAsync();
                var HasTypes = await _dbContext.ProductTypes.AnyAsync(); ;
                var HasProducts = await _dbContext.Products.AnyAsync();




                if (HasBrands && HasTypes && HasProducts) return;

                if (!HasBrands)
                {
                  await SeedDatFromJsonAsync<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }
                if (!HasTypes)
                {
                    await SeedDatFromJsonAsync<ProductType, int>("types.json", _dbContext.ProductTypes);
                 await     _dbContext.SaveChangesAsync(); // must has brands and types before create products 
                }
                if (!HasProducts)
                {
                    await SeedDatFromJsonAsync<Product, int>("products.json", _dbContext.Products);
                  await  _dbContext.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Data seed failed  " + ex);
            }
        }



        private async Task SeedDatFromJsonAsync<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var filePath = @"..\E-CommerceSolution\ECommerce.Persistance\Data\JSONData\" + fileName;

            if (!File.Exists(filePath)) throw new FileNotFoundException($" file {fileName} is not exist ");

            try
            {

                using var datStream = File.OpenRead(filePath);

                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                var data = await JsonSerializer.DeserializeAsync<List<T>>(datStream, options) ?? new List<T>();
                if (data is not null)
                {
                 await   dbset.AddRangeAsync(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read jason file : {ex} ");
            }


        }
    }
}
