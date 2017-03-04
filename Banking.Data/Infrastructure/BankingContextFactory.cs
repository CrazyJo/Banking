using System.Data.Entity;
using System.Threading;
using Banking.Data.Contexts;

namespace Banking.Data.Infrastructure
{
    public class BankingContextFactory : IDbFactory
    {
        protected DbContext DbContext;

        public DbContext GetContext()
        {
            LazyInitializer.EnsureInitialized(ref DbContext, Init);
            return DbContext;
        }

        protected virtual DbContext Init()
        {
            return new BankingContext();
        }
    }
}
