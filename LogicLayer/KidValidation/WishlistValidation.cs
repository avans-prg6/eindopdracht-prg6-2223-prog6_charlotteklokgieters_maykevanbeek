
﻿using Microsoft.AspNetCore.Identity;
using Santa_WishList.Models.Enums;
using SantasWishlist.Domain;


namespace LogicLayer.KidValidation
{
	public class WishlistValidation
	{
		readonly GiftRepository giftRepository;

		public WishlistValidation(GiftRepository injectedGiftRepository) 
		{
			giftRepository = injectedGiftRepository;
		}

		//Validation rule 1: max 3 gifts per category
		//						if naughty, 1 gift per category
		//						if naughty and lied about it, 1 gift total
		//Validation rule 4: Stijn can get Dolfje Weerwolfje as extra gift, on top of the max
		//Validation rule 5: if a kid used the woord 'vrijwilligerswerk' in the reason they've been nice, they can get as many gifts as they want
		public void CertainGiftAmount(List<string> chosenGifts, Niceness niceness, bool isNice, string name, string nicenessExample)
		{			
			if ((name == "Stijn" && chosenGifts.Contains("Dolfje Weerwolfje") && !isNice && niceness != Niceness.Naughty && chosenGifts.Count() > 2) || 
				(!isNice && niceness != Niceness.Naughty && chosenGifts.Count() > 1)) //rules 1 && 4
			{
				//TODO error, lied about niceness so they cant have more than 1 total gift, or no more than 2 if youre stijn and you chose dolfje weerwolfje
			}

			

			int counter;
			if ((nicenessExample.Contains("Vrijwilligerswerk") || nicenessExample.Contains("vrijwilligerswerk")) && isNice) //rule 5
			{
				//TODO can get as many gifts as they want
			}
			else 
			{
				foreach (GiftCategory category in Enum.GetValues(typeof(GiftCategory)))
				{
					counter = 0;
					foreach (string chosen in chosenGifts)
					{
						foreach (Gift possible in giftRepository.GetPossibleGifts())
						{
							bool giftCounted = false;
							if (name == "Stijn" && chosen == possible.Name && chosen == "Dolfje Weerwolfje" && //rule 4
								possible.Category == category) 
							{
								giftCounted = true;
							}
							else if (chosen == possible.Name && possible.Category == category)
							{
								counter++;
								giftCounted = true;
							}

							
							if (!isNice && niceness == Niceness.Naughty && counter > 1) //rule 1
							{
								//TODO error, has been naughty so can only have 1 gift per category
							}
							else if (isNice && counter > 3) //rule 1
							{
								//TODO error, has been nice but can only have 3 gifts per category
							}

							if (giftCounted)
							{
								break;
							}

						}
					}
				}
			}
		}

		//Validation rule 2: only 1 gift per gift type
		//Validation rule 6: if the gift 'nachtlampje' has been chosen, then the gift 'ondergoed' must also be chosen
		public void GiftCombinations(List<string> chosenGifts)
		{
			if ((chosenGifts.Contains("Lego") && chosenGifts.Contains("K'nex")) ||
				(chosenGifts.Contains("lego voor dummies") && chosenGifts.Contains("K'nex voor dummies"))) //rule 2
			{
				//TODO error, can only have 1 gift per gift type
			}

			if (chosenGifts.Contains("Nachtlampje") && !chosenGifts.Contains("Ondergoed")) //rule 6
			{
				//TODO error, if 'nachtlampje' is chosen then 'ondergoed' also has to be chosen
			}
		}

		//Validation rule 3: can divert from the age rating once
		public void DivertFromAgeRating(List<string> chosenGifts, int age)
		{
			int giftAmount = 0;
			foreach(string gift in chosenGifts)
			{
				if (giftRepository.CheckAge(gift) > age)
				{
					giftAmount++;
				}

				if (giftAmount > 1)
				{
					//TODO error, cant divert from the age rating more than once
				}
			}
		}
	}
}
