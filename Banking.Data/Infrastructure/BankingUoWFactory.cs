namespace Banking.Data.Infrastructure
{
    public interface IUoWFactory<out TUow> where TUow : IUnitOfWork
    {
        TUow Create();
    }

    public class BankingUoWFactory : IUoWFactory<IBankingUoW>
    {
        public IBankingUoW Create()
        {
            return new BankingUoW();
        }
    }
}
