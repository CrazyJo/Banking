using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Banking.Data.Contexts;
using Banking.Model;

namespace Banking.Data.Initializers
{
    public class BankingInitializer : DropCreateDatabaseAlways<BankingContext>
    {
        protected override void Seed(BankingContext context)
        {
            SeedUsers(context);
            SeedBankAccounts(context);

            base.Seed(context);
        }

        private void SeedUsers(BankingContext context)
        {
            var u1 = new User
            {
                Id = 1,
                Login = "user1",
                Password = "user1",
            };
            var u2 = new User
            {
                Id = 2,
                Login = "user2",
                Password = "user2",
            };
            var u3 = new User
            {
                Id = 3,
                Login = "user3",
                Password = "user3",
            };

            context.Users.AddOrUpdate(u1, u2, u3);
        }

        private void SeedBankAccounts(BankingContext context)
        {
            var bac1 = new BankAccount
            {
                Id = 1,
                UserId = 1,
                Balance = 1000,
                Transactions =
                {
                    new Transaction
                    {
                        Type = TransactionType.Deposit,
                        Amount = 500,
                        PrincipalAccountId = 1,
                        OperationDate = DateTime.Now.Subtract(new TimeSpan(8, 0, 0,0))
                    },
                    new Transaction
                    {
                        Type = TransactionType.Deposit,
                        Amount = 500,
                        PrincipalAccountId = 1,
                        OperationDate = DateTime.Now.Subtract(new TimeSpan(7, 0, 0,0))
                    }
                }
            };

            var tranOutFrom2To3 = new Transaction
            {
                Type = TransactionType.TransferOut,
                OperationDate = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0)),
                Amount = 400,
                PrincipalAccountId = 2,
                OuterAccountId = 3
            };
            var tranInFrom2To3 = new Transaction
            {
                Type = TransactionType.TransferIn,
                OperationDate = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0)),
                Amount = 400,
                PrincipalAccountId = 3,
                OuterAccountId = 2
            };

            var bac3 = new BankAccount
            {
                Id = 3,
                UserId = 3,
                Balance = 400,
                Transactions =
                {
                    tranInFrom2To3,
                    new Transaction
                    {
                        Type = TransactionType.Withdraw,
                        Amount = 250,
                        PrincipalAccountId = 3,
                        OperationDate = DateTime.Now.Subtract(new TimeSpan(6, 0, 0,0))
                    }
                }
            };
            var bac2 = new BankAccount
            {
                Id = 2,
                UserId = 2,
                Balance = 500,
                Transactions =
                {
                    tranOutFrom2To3,
                    new Transaction
                    {
                        Type = TransactionType.Deposit,
                        Amount = 250,
                        PrincipalAccountId = 2,
                        OperationDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0,0))
                    },
                    new Transaction
                    {
                        Type = TransactionType.Deposit,
                        Amount = 250,
                        PrincipalAccountId = 2,
                        OperationDate = DateTime.Now.Subtract(new TimeSpan(9, 0, 0,0))
                    }
                },
            };


            context.BankAccounts.AddRange(new List<BankAccount> { bac1, bac2, bac3});
        }
    }
}
