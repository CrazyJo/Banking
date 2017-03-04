using System.Data.Entity;
using Banking.Data.Configurations;
using Banking.Data.Initializers;
using Banking.Model;

namespace Banking.Data.Contexts
{
    public class BankingContext : DbContext
    {
        public BankingContext() : base("BankingContext")
        {
            Database.SetInitializer(new BankingInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BankAccountConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new TransactionConfig());
        }
    }
}
