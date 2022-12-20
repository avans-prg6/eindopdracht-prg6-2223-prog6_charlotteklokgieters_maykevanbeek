﻿using Santa_WishList.Controllers;
using static Santa_WishList.Controllers.KidController;
using System.ComponentModel.DataAnnotations;

namespace Santa_WishList.Models
{
	public class ValidationMethods
	{
		public static ValidationResult CheckDubbleNames(string kidsnames, ValidationContext context)
		{
			string[] kids = kidsnames.Split(", ");
			bool error = false;
			List<string> dubbles = new List<string>();
			List<string> accounts = new List<string>();

			foreach (string kid in kids)
			{
				if (accounts.Contains(kid))
				{
					error = true;
					dubbles.Add(kid);
				}
			}

			if (error)
			{
				string message = "De volgende namen komen al voor: ";
				foreach (string name in dubbles)
				{
					message += name + " ";
				}

				return new ValidationResult(
					string.Format(message, context.MemberName),
					new List<string>() { context.MemberName });
			}

			return ValidationResult.Success;
		}
	}
}