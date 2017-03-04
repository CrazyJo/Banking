using System.Collections.Generic;

namespace Banking.Model
{
    public class BankAccount : Entity
    {
        public int Balance { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}
