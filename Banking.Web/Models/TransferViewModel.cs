using System.ComponentModel.DataAnnotations;

namespace Banking.Web.Models
{
    public class TransferViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SourceAccountId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TargetAccountId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
    }
}