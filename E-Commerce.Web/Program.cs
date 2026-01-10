
using AutoMapper;
using E_Commerce.Web.CustomMiddlewares;
using E_Commerce.Web.Extintions;
using ECommerce.Domain.Contracts;
using ECommerce.Percistance.Data.Contexts;
using ECommerce.Percistance.Data.DataSeed;
using ECommerce.Percistance.IdentityData;
using ECommerce.Percistance.Repositories;
using ECommerce.Presintation.Controller;
using ECommerce.Service;
using ECommerce.Service.Abstraction;
using ECommerce.Service.Abstraction.ICacheService;
using ECommerce.Service.Abstraction.IProductServices;
using ECommerce.Service.BasketServices;
using ECommerce.Service.ICacheServices;
using ECommerce.Service.MappingProfiles;
using ECommerce.Service.ProductServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
    
            builder.Services.AddControllers()
       .AddApplicationPart(typeof(ProductsController).Assembly)
        .AddControllersAsServices();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));


            });
            builder.Services.AddScoped<IDataInitializer, DataInitializer>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddAutoMapper(typeof(ServicesAssemplyProvide).Assembly);

            builder.Services.AddSingleton<IConnectionMultiplexer> (sp => {
                var options = ConfigurationOptions.Parse(
                    builder.Configuration.GetConnectionString("RedisConnection"));

                options.AbortOnConnectFail = false;

                return ConnectionMultiplexer.Connect(options);
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IBasketServices, BasketServices>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            builder.Services.AddScoped<ICacheService, CacheServices>();
            builder.Services.AddDbContext<EcommerceIdentityDbContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));


            });



            #endregion
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine("Loaded Assembly: " + asm.FullName);
            }


            var app = builder.Build();

            app.MapControllers();


            #region Migarate Database - Data seeding 

            await app.Migrate();
            await app.MigrateIdentityDataBase();
            await app.SeedData();

            #endregion

            // Configure the HTTP request pipeline.
            #region Configure the HTTP request pipeline. // MidelWare 
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(); // Enabling Static Files // wwwroot files 

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

           await app.RunAsync();
        } 
    }
}
//  D . O . P 

