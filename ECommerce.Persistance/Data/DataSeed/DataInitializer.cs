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

        void IDataInitializer.Initializer()
        {
            try
            {
                var HasBrands = _dbContext.ProductBrands.Any();
                var HasTypes = _dbContext.ProductTypes.Any();
                var HasProducts = _dbContext.Products.Any();




                if (HasBrands && HasTypes && HasProducts) return;

                if (!HasBrands)
                {
                    SeedDatFromJson<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }
                if (!HasTypes)
                {
                    SeedDatFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);
                    _dbContext.SaveChanges(); // must has brands and types before create products 
                }
                if (!HasProducts)
                {
                    SeedDatFromJson<Product, int>("products.json", _dbContext.Products);
                    _dbContext.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Data seed failed  " + ex);
            }
        }



        private void SeedDatFromJson<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            // E:\BEPROJECTS\Route\E-CommerceSolution\ECommerce.Persistance\Data\JSONData\
            var filePath = @"..\E-CommerceSolution\ECommerce.Persistance\Data\JSONData\" + fileName;



            if (!File.Exists(filePath)) throw new FileNotFoundException($" file {fileName} is not exist ");

            try
            {

                using var datStream = File.OpenRead(filePath);

                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                var data = JsonSerializer.Deserialize<List<T>>(datStream, options) ?? new List<T>();
                if (data is not null)
                {
                    dbset.AddRange(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read jason file : {ex} ");
            }


        }
    }
}
