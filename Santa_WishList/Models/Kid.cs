using SantasWishlist.Domain;
using System.ComponentModel.DataAnnotations;
using static Santa_WishList.Controllers.KidController;

namespace Santa_WishList.Models
{
	public class Kid
	{
		[Required]
		public string Name { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int Age { get; set; }

		[Required]
		[CustomValidation(typeof(ValidationMethods), "SetNiceness")]
		public Niceness Niceness { get; set; }

		[CustomValidation(typeof(ValidationMethods), "GiveNicenessExample")] 
		public string? NicenessExample { get; set; }

		public List<Gift> PossibleGifts { get; set; }

		public List<Gift>? ChosenGifts { get; set; }

		public string? Other { get; set; }
	}
}
