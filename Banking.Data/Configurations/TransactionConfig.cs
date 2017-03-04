using System.Data.Entity.ModelConfiguration;
using Banking.Model;

namespace Banking.Data.Configurations
{
    public class TransactionConfig : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfig()
        {
            HasOptional(p => p.OuterAccount)
                .WithMany()
                .HasForeignKey(p => p.OuterAccountId)
                .WillCascadeOnDelete(false);
        }
    }
}
