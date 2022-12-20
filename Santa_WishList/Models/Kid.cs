using SantasWishlist.Domain;
using System.ComponentModel.DataAnnotations;
using static Santa_WishList.Controllers.KidController;

namespace Santa_WishList.Models
{
	public class Kid : IValidatableObject
	{
		[Required]
		public string Name { get; set; }

		[Required(ErrorMessage = "Leeftijd invullen is verplicht!")]
		[Range(0, int.MaxValue, ErrorMessage = "Leeftijd moet een positief getal zijn!")]
		public int? Age { get; set; }

		[Required]
		public Niceness Niceness { get; set; }

		public string? NicenessExample { get; set; }

		public List<Gift>? PossibleGifts { get; set; }

		public List<Gift>? ChosenGifts { get; set; }

		public string? Other { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Niceness != Niceness.Naughty && NicenessExample == null)
			{
				{
					yield return new ValidationResult(
						errorMessage: "Je moet hier een voorbeeld geven waarom je braaf bent geweest!",
						memberNames: new[] { "NicenessExample" }
						);
				}
			}
		}
	}
}
