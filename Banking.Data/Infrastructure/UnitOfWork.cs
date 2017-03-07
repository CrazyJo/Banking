using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        Task<int> CommitAsync(CancellationToken token);
    }

    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }
        protected bool Disposed { get; set; }

        public UnitOfWork(IDbFactory factory)
        {
            Context = factory.GetContext();
        }

        public Task<int> CommitAsync()
        {
            return CommitAsync(CancellationToken.None);
        }

        public Task<int> CommitAsync(CancellationToken token)
        {
            return Context.SaveChangesAsync(token);
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
