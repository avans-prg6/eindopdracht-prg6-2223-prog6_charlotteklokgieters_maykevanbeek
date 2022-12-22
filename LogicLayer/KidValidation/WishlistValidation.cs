using Microsoft.AspNetCore.Identity;
using Santa_WishList.Models.Enums;
using SantasWishlist.Domain;

namespace LogicLayer.KidValidation
{
	public class WishlistValidation
	{
		//Validation rule 1: max 3 gifts per category
		//						if naughty, 1 gift per category
		//						if naughty and lied about it, 1 gift total
		public void ThreeGiftsPerCategory(List<string> chosenGifts, List<Gift> possibleGifts, Niceness niceness, bool isNice)
		{
			int counter;
			int totalCounter = 0;
			foreach (GiftCategory category in Enum.GetValues(typeof(GiftCategory)))
			{
				counter = 0;
				foreach (string chosen in chosenGifts)
				{
					totalCounter++; //TODO ??
					foreach (Gift possible in possibleGifts)
					{
						if (chosen == possible.Name && possible.Category == category)
						{
							counter++;
							break;
						}

						if (!isNice && niceness != Niceness.Naughty && totalCounter > 1)
						{
							//TODO error, lied about niceness so cant have more than 1 total gift
						}
						else if (!isNice && niceness == Niceness.Naughty && counter > 1)
						{
							//TODO error, has been naugty so can only have 1 gift per category
						}
						else if (isNice && counter > 3)
						{
							//TODO error, has been nice but can only have 3 gifts per category
						}
					}
				}
			}
		}

		//Validation rule 2: only 1 gift per gift type
		public void OneGiftPerType(List<string> chosenGifts)
		{
			if ((chosenGifts.Contains("Lego") && chosenGifts.Contains("K'nex")) ||
				(chosenGifts.Contains("lego voor dummies") && chosenGifts.Contains("K'nex voor dummies")))
			{
				//TODO error, can only have 1 gift per gift type
			}
		}

		//Validation rule 3: 
	}
}
