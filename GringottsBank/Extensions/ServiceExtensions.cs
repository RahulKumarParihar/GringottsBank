using BankLibrary.Abstracts;
using BankLibrary.Data;
using BankLibrary.Mappers;
using BankLibrary.Services;
using GringottsBank.Managers;
using GringottsBank.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GringottsBank.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureInjection(this IServiceCollection services)
        {
            // customer
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<CustomerManager>();
            // account
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<AccountManager>();
            // transaction
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<TransactionManager>();
            // token
            services.AddScoped<JWTTokenUtil>();
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var bankConnectionString = configuration.GetConnectionString("Bank");
            services.AddDbContext<BankDbContext>(options =>
                   options.UseNpgsql(bankConnectionString),
                   ServiceLifetime.Transient);
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(CustomerMapper), 
                typeof(AccountMapper),
                typeof(TransactionMapper));
        }
    }
}
