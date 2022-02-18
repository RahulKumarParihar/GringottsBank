using BankLibrary.Abstracts;
using BankLibrary.Data;
using BankLibrary.Mappers;
using BankLibrary.Services;
using GringottsBank.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GringottsBank.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureInjection(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<CustomerManager>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var bankConnectionString = configuration.GetConnectionString("Bank");
            services.AddDbContext<BankDbContext>(options =>
                   options.UseSqlServer(bankConnectionString),
                   ServiceLifetime.Transient);
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CustomerMapper));
        }
    }
}
