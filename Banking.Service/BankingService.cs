using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Banking.Core;
using Banking.Data.Infrastructure;
using Banking.Model;

namespace Banking.Service
{
    public interface IBankingService
    {
        Task<BankAccount> GetAccountInfo(int accId);
        Task<IEnumerable<BankAccount>> GetAllAccountsInfo();
        Task<OperationDetails> Withdraw(int bankAccId, int amount);
        Task<OperationDetails> Deposit(int bankAccId, int amount);
        Task<OperationDetails> Transfer(int sourceBankAccId, int targetBankAccId, int amount);
    }

    public class BankingService : IBankingService
    {
        public IUoWFactory<IBankingUoW> UoWFactory { get; set; }

        public BankingService(IUoWFactory<IBankingUoW> factory)
        {
            UoWFactory = factory;
        }

        public async Task<BankAccount> GetAccountInfo(int accId)
        {
            BankAccount res;
            using (var uow = UoWFactory.Create())
            {
                res = await uow.BankAccounts.FindAsync(accId);
            }

            return res;
        }

        public async Task<IEnumerable<BankAccount>> GetAllAccountsInfo()
        {
            IEnumerable<BankAccount> res;
            using (var unitOfWork = UoWFactory.Create())
            {
                res = await unitOfWork.BankAccounts.GetAll().ToListAsync();
            }

            return res;
        }

        public Task<OperationDetails> Withdraw(int bankAccId, int amount)
        {
            return Operation.Wrap(async res =>
            {
                using (var unitOfWork = UoWFactory.Create())
                {
                    var acc = await unitOfWork.BankAccounts.FindAsync(bankAccId);
                    var t = acc.Withdraw(amount);
                    if (!t.Successful) return t;
                    acc.SetTransaction(TransactionType.Withdraw, amount);
                    unitOfWork.BankAccounts.Update(acc);
                    await unitOfWork.CommitAsync();
                    return res;
                }
            });
        }

        public Task<OperationDetails> Deposit(int bankAccId, int amount)
        {
            return Operation.Wrap(async res =>
            {
                using (var unitOfWork = UoWFactory.Create())
                {
                    var acc = await unitOfWork.BankAccounts.FindAsync(bankAccId);
                    var t = acc.Deposit(amount);
                    if (!t.Successful) return t;
                    acc.SetTransaction(TransactionType.Deposit, amount);
                    unitOfWork.BankAccounts.Update(acc);
                    await unitOfWork.CommitAsync();
                    return res;
                }
            });
        }

        public Task<OperationDetails> Transfer(int sourceBankAccId, int targetBankAccId, int amount)
        {
            return Operation.Wrap(async res =>
            {
                using (var unitOfWork = UoWFactory.Create())
                {
                    var sourceAcc = await unitOfWork.BankAccounts.FindAsync(sourceBankAccId);
                    var targetAcc = await unitOfWork.BankAccounts.FindAsync(targetBankAccId);

                    var withdrawRes = sourceAcc.Withdraw(amount);
                    if (!withdrawRes.Successful) return withdrawRes;

                    var depositRes = targetAcc.Deposit(amount);
                    if (!depositRes.Successful) return depositRes;

                    sourceAcc.SetTransaction(targetAcc, amount);

                    unitOfWork.BankAccounts.Update(sourceAcc);
                    unitOfWork.BankAccounts.Update(targetAcc);

                    await unitOfWork.CommitAsync();
                    return res;
                }
            });
        }
    }
}