using System.Data.Entity.ModelConfiguration;
using Banking.Model;

namespace Banking.Data.Configurations
{
    public class BankAccountConfig : EntityTypeConfiguration<BankAccount>
    {
        public BankAccountConfig()
        {
            HasRequired(e => e.User).WithRequiredDependent(e => e.BankAccount);
            HasMany(p => p.Transactions)
                .WithRequired(p => p.PrincipalAccount)
                .HasForeignKey(p => p.PrincipalAccountId)
                .WillCascadeOnDelete(false);
        }
    }
}
