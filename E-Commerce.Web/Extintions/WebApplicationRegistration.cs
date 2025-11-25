using ECommerce.Domain.Contracts;
using ECommerce.Percistance.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Web.Extintions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> Migrate(this WebApplication app)
        {
          await using  var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetService<StoreDbContext>();
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (!pendingMigrations?.Any() ?? false)
                dbContext.Database.Migrate();

            return app;
        }

        public static async Task<WebApplication> SeedData(this WebApplication app)
          {
            var env = app.Services.GetRequiredService<IWebHostEnvironment>();

            // Build the absolute path to JSONData folder
            var path  = Path.Combine(
                env.ContentRootPath,
                 "..",
                "ECommerce.Persistance",
                "Data",
                "JSONData"
            ); await using var scope = app.Services.CreateAsyncScope();
            var DataInitializerServices = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
           await DataInitializerServices.InitializerAsync(path);
            return app;
        }

    }
}
