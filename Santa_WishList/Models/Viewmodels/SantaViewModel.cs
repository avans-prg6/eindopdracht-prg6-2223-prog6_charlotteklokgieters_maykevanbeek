using LogicLayer.Santa;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Santa_WishList.Models.Viewmodels
{
    public class SantaViewModel
    {
        [Required(ErrorMessage = "Je moet minimaal een naam invullen!")]

        [CustomValidation(typeof(Santa), "CheckDubbleNames")]
        public string KidsNames { get; set; }
        [Required(ErrorMessage = "Je moet een wachtwoord invullen!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Alleen letters AUB, maak het de kindjes niet te moeilijk!")]
        public string Password { get; set; }
        public bool BeenNice { get; set; }

        public SantaViewModel()
        {

        }

        public SantaViewModel(string kidsNames, string password)
        {
            KidsNames = kidsNames;
            Password = password;    
        }
    }
}
