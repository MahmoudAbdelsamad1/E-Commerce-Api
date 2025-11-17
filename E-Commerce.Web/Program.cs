
using E_Commerce.Web.Extintions;
using ECommerce.Domain.Contracts;
using ECommerce.Percistance.Data.Contexts;
using ECommerce.Percistance.Data.DataSeed;
using ECommerce.Percistance.Repositories;
using ECommerce.Service.Abstraction.IProductServices;
using ECommerce.Service.ProductServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));


            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductServices, ProductServices>();

            #endregion


            var app = builder.Build();



            #region Migarate Database - Data seeding 

            await app.Migrate();

            await app.SeedData();
         
            #endregion

            // Configure the HTTP request pipeline.
            #region Configure the HTTP request pipeline. // MidelWare 
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

           await app.RunAsync();
        } 
    }
}
//  D . O . P 

