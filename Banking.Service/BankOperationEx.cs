using System;
using Banking.Core;
using Banking.Model;

namespace Banking.Service
{
    public static class BankOperationEx
    {
        public static bool CheckAccount(this BankAccount acc, int amount, out Error error)
        {
            return CheckNull(acc, out error) && CheckAvailableBalance(acc, amount, out error);
        }

        public static bool CheckAvailableBalance(this BankAccount acc, int amount, out Error error)
        {
            return Check(() => acc.Balance < amount, "Insufficient funds on account", out error);
        }

        public static bool CheckNull(this BankAccount acc, out Error error)
        {
            return Check(() => acc == null, "Account with such id was not found.", out error);
        }

        public static bool Check(Func<bool> predicate, string message, out Error error)
        {
            var isInvalid = predicate();
            error = isInvalid ? new Error(message) : null;
            return !isInvalid;
        }

        public static OperationDetails Withdraw(this BankAccount acc, int amount)
        {
            return TransactionOperation(acc, amount, TransactionType.Withdraw);
        }

        public static OperationDetails Deposit(this BankAccount acc, int amount)
        {
            return TransactionOperation(acc, amount, TransactionType.Deposit);
        }

        public static OperationDetails TransactionOperation(BankAccount acc, int amount, TransactionType transactionType)
        {
            return Operation.Wrap(res =>
            {
                Error error;
                if (!CheckAccount(acc, amount, out error))
                {
                    res.SetError(error);
                }
                BalanceOperation(acc, amount, transactionType);
            });
        }

        public static void BalanceOperation(BankAccount acc, int amount, TransactionType transactionType)
        {
            checked
            {
                switch (transactionType)
                {
                    case TransactionType.Deposit:
                        acc.Balance = acc.Balance + amount;
                        break;
                    case TransactionType.Withdraw:
                        acc.Balance = acc.Balance - amount;
                        break;
                }
            }
        }

        public static void SetTransaction(this BankAccount sourceAcc, BankAccount targetAcc, int amount)
        {
            sourceAcc.Transactions.Add(new Transaction
            {
                Type = TransactionType.TransferOut,
                Amount = amount,
                PrincipalAccount = sourceAcc,
                OuterAccount = targetAcc
            });
            targetAcc.Transactions.Add(new Transaction
            {
                Type = TransactionType.TransferIn,
                Amount = amount,
                PrincipalAccount = targetAcc,
                OuterAccount = sourceAcc
            });
        }

        public static void SetTransaction(this BankAccount acc, TransactionType transactionType, int amount)
        {
            acc.Transactions.Add(new Transaction
            {
                Type = transactionType,
                Amount = amount,
                OperationDate = DateTime.Now,
                PrincipalAccountId = acc.Id
            });
        }
    }
}
