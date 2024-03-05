using Microsoft.Extensions.DependencyInjection;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Utils.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Warehouse.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options
                => options.UseSqlite(configuration.GetConnectionString("WarehouseDB"),
                b => b.MigrationsAssembly("Warehouse.Infrastructure")));

            return services;
        }
    }
}
