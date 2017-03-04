using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }
        protected bool Disposed { get; set; }

        public UnitOfWork(IDbFactory factory)
        {
            Context = factory.GetContext();
        }

        public async Task CommitAsync()
        {
            await CommitAsync(CancellationToken.None);
        }

        public async Task CommitAsync(CancellationToken token)
        {
            await Context.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                }
            }
            Disposed = true;
        }
    }
}
