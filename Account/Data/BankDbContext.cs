using BankLibrary.Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace BankLibrary.Data
{
    public class BankDbContext: DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> contextOptions) : base(contextOptions)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAccount>()
            .HasKey(ca => new { ca.AccountId, ca.CustomerId });

            //// configures one-to-many relationship
            modelBuilder.Entity<Account>()
                .HasOne(acc => acc.Customers)
                .WithOne(cus => cus.Account);

            modelBuilder.Entity<Customer>()
                .HasMany(acc => acc.Accounts)
                .WithOne(cus => cus.Customer);

        }
        public DbSet<CustomerAccount> CustomerAccountMappings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
