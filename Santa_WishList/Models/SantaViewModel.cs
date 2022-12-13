using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Santa_WishList.Models
{
    public class SantaViewModel
    {
        public string KidsNames { get; set; }
		[Required(ErrorMessage = "Je moet een wachtwoord invullen!")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alleen letters AUB, maak het de kindjes niet te moeilijk!")]
        //TODO niet ww kunnen zien
        public string Password { get; set; }
    }
}
