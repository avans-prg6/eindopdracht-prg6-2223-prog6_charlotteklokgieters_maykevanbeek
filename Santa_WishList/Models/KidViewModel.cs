﻿using SantasWishlist.Domain;
using System.ComponentModel.DataAnnotations;
using static Santa_WishList.Controllers.KidController;

namespace Santa_WishList.Models
{
	public class KidViewModel
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public Niceness Niceness { get; set; }
		[CustomValidation(typeof(ValidationMethods), "GiveNicenessExample")]
		public string? NicenessExample { get; set; }
		public List<Gift> PossibleGifts { get; set; }
		public List<Gift> ChosenGifts { get; set; }
	}

	//TODO verplaats class
	public class ValidationMethods
	{
		public static ValidationResult NoNegativesOrZero(int value, ValidationContext context)
		{
			if (value > 0)
			{
				return ValidationResult.Success;
			}
			else
			{
				return new ValidationResult(
					string.Format("Je kan hier geen negatieve getallen of 0 invullen!", context.MemberName),
					new List<string>() { context.MemberName });
			}
		}

		//TODO deze hoeft alleen als t kind niet naughty is
		public static ValidationResult GiveNicenessExample(string input, ValidationContext context)
		{
			if (input == null)
			{
				return new ValidationResult(
					string.Format("Je moet hier een voorbeeld geven waarom je braaf bent geweest!", context.MemberName),
					new List<string>() { context.MemberName });
				//return new ValidationResult("Je moet hier een voorbeeld geven waarom je braaf bent geweest!");
			}
            return ValidationResult.Success;
        }
	}
}
