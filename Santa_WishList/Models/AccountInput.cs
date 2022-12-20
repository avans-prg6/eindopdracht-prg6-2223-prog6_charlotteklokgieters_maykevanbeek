using System.ComponentModel.DataAnnotations;

namespace Santa_WishList.Models
{
    public class AccountInput
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        public bool IsNice { get; set; }
    }
}
