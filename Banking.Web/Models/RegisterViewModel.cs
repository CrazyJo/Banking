using System.ComponentModel.DataAnnotations;

namespace Banking.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
        public bool Confirmed { get; set; }
    }
}