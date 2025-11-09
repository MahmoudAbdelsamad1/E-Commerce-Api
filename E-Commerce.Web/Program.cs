
using E_Commerce.Web.Extintions;
using ECommerce.Domain.Contracts;
using ECommerce.Percistance.Data.Contexts;
using ECommerce.Percistance.Data.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
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


            #endregion


            var app = builder.Build();



            #region Migarate Database - Data seeding 
            
            app.Migrate().SeedData();
         
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

            app.Run();
        }
    }
}
//  D . O . P 

