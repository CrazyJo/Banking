using System;
using Banking.Model;

namespace Banking.Web.Models
{
    public class TransactionViewModel
    {
        public TransactionType Type { get; set; }
        public int Amount { get; set; }
        public DateTime OperationDate { get; set; }
        public int PrincipalAccountId { get; set; }
        public int? OuterAccountId { get; set; }
    }
}