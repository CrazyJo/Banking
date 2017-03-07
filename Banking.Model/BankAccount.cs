using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Banking.Model
{
    public class BankAccount : Entity
    {
        public int Balance { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}
