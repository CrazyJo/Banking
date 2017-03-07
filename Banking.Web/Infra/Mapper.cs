using System.Collections.Generic;
using System.Linq;
using Banking.Model;
using Banking.Web.Models;

namespace Banking.Web.Infra
{
    public static class Mapper
    {
        public static IEnumerable<BankAccountViewModel> Map(IEnumerable<BankAccount> domain)
        {
            return domain.Select(Map).ToList();
        }

        public static BankAccountViewModel Map(BankAccount domain)
        {
            return new BankAccountViewModel
            {
                Id = domain.Id,
                Balance = domain.Balance,
                Transactions = Map(domain.Transactions)
            };
        }

        public static IEnumerable<TransactionViewModel> Map(IEnumerable<Transaction> domain)
        {
            return domain.Select(Map).ToList();
        }

        public static TransactionViewModel Map(Transaction domain)
        {
            return new TransactionViewModel
            {
                Type = domain.Type,
                Amount = domain.Amount,
                OperationDate = domain.OperationDate,
                OuterAccountId = domain.OuterAccountId,
                PrincipalAccountId = domain.PrincipalAccountId
            };
        }
    }
}