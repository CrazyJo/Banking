using System.Collections.Generic;

namespace Banking.Web.Models
{
    public class BankAccountViewModel
    {
        public int Id { get; set; }
        public int Balance { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}