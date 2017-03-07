using System.ComponentModel.DataAnnotations;

namespace Banking.Web.Models
{
    public class IoTransactionVm
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}