using Banking.Data.Repositories;
using Banking.Model;

namespace Banking.Data.Infrastructure
{
    public interface IBankingUoW : IUnitOfWork
    {
        IRepository<User, int> Users { get; }
        IRepository<BankAccount, int> BankAccounts { get; }
        IRepository<Transaction, int> Transactions { get; }
    }

    public class BankingUoW : UnitOfWork, IBankingUoW
    {
        protected IRepository<User, int> UsersRepo;
        protected IRepository<BankAccount, int> BankAccountsRepo;
        protected IRepository<Transaction, int> TransactionsRepo;

        public BankingUoW() : base(new BankingContextFactory())
        {
        }

        public IRepository<User, int> Users => UsersRepo ?? new EntityRepository<User, int>(Context);
        public IRepository<BankAccount, int> BankAccounts => BankAccountsRepo ?? new EntityRepository<BankAccount, int>(Context);
        public IRepository<Transaction, int> Transactions => TransactionsRepo ?? new EntityRepository<Transaction, int>(Context);
    }
}
