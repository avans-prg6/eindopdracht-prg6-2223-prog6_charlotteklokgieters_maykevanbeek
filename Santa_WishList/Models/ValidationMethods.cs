using Santa_WishList.Controllers;
using static Santa_WishList.Controllers.KidController;
using System.ComponentModel.DataAnnotations;

namespace Santa_WishList.Models
{
	public class ValidationMethods
	{
		private static Niceness Niceness { set; get; }

		public static ValidationResult SetNiceness(Niceness niceness, ValidationContext context)
		{
			Niceness = niceness;
			return ValidationResult.Success;
		}

		public static ValidationResult GiveNicenessExample(string input, ValidationContext context)
		{
			if (input == null && Niceness != Niceness.Naughty)
			{
				return new ValidationResult(
					string.Format("Je moet hier een voorbeeld geven waarom je braaf bent geweest!", context.MemberName),
					new List<string>() { context.MemberName });
			}
			return ValidationResult.Success;
		}
	}
}
