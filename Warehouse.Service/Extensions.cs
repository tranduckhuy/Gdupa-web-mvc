using Microsoft.Extensions.DependencyInjection;
using Warehouse.Domain.Interfaces;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.MappingProfiles;
using Warehouse.Infrastructure.Utils.Helper.Impl;
using Warehouse.Infrastructure.Utils.Helper;
using Warehouse.Infrastructure.Utils.Mail;
using WarehouseWebMVC.Services;
using Microsoft.Extensions.Configuration;

namespace Warehouse.Service
{
    public static class Extensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImportNoteService, ImportNoteSerivce>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IWarehouseService, WarehouseSerivce>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IAddressHelper, AddressHelper>();
            services.AddScoped<IEmailHelper, EmailHelper>();

            return services;
        }
    }
}
