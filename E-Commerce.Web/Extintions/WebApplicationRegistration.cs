using ECommerce.Domain.Contracts;
using ECommerce.Percistance.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web.Extintions
{
    public static class WebApplicationRegistration
    {
        public static WebApplication Migrate(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<StoreDbContext>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (!pendingMigrations?.Any() ?? false)
                dbContext.Database.Migrate();

            return app;
        }

        public static WebApplication SeedData(this WebApplication app)
          {
            var scope = app.Services.CreateScope();
            var DataInitializerServices = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            DataInitializerServices.Initializer();
            return app;
        }

    }
}
