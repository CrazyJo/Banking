using System;

namespace Banking.Model
{
    public class Transaction : Entity
    {
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
        public DateTime OperationDate { get; set; }

        public int PrincipalAccountId { get; set; }
        public virtual BankAccount PrincipalAccount { get; set; }

        public int? OuterAccountId { get; set; }
        public virtual BankAccount OuterAccount { get; set; }

        public override string ToString()
        {
            return $"{Type} Pac: {PrincipalAccountId} Oac: {OuterAccountId}";
        }
    }
}
