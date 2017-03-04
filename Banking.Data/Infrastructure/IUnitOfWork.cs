using System;
using System.Threading;
using System.Threading.Tasks;

namespace Banking.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        Task CommitAsync(CancellationToken token);
    }
}
