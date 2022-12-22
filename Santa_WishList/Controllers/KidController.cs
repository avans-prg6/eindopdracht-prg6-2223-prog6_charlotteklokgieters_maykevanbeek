using LogicLayer.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;
using Santa_WishList.Models.Viewmodels;
using SantasWishlist.Domain;
using System.ComponentModel.DataAnnotations;

namespace Santa_WishList.Controllers
{
	[Authorize(Roles = "Child")]
	public class KidController : Controller
	{
		private readonly GiftRepository giftRepository;
		private readonly AccountController _accountController;

		public KidController(GiftRepository injectedGiftRepository, AccountController accountController)
		{
			giftRepository = injectedGiftRepository;
            _accountController = accountController;
        }

		[Route("{controller}")]
		public IActionResult Index()
		{
			Kid model = new Kid();
			model.Name = this.User.Identity.Name;

			return View("Index", model);
		}

		public IActionResult PersonalInfo(Kid model)
		{
			if (!ModelState.IsValid)
			{
				List<string> errors = ModelState.Values.SelectMany(ms => ms.Errors).Select(err => err.ErrorMessage).ToList();

				ViewBag.Errors = errors;
				return Index();
			}

			model.PossibleGifts = giftRepository.GetPossibleGifts();
			return View("Wishlist", model);
		}


		public IActionResult ChoosingGifts(Kid model, List<GiftViewModel> chosenGifts)
		{
			model.PossibleGifts = giftRepository.GetPossibleGifts();
			model.ChosenGifts = new List<string>();

			if (!ModelState.IsValid)
			{
				List<string> errors = ModelState.Values.SelectMany(ms => ms.Errors).Select(err => err.ErrorMessage).ToList();

				ViewBag.Errors = errors;
				return View("Wishlist", model);
			}

			if (model.Other != null)
			{
				string[] otherGifts = General.SplitString(model.Other);
				
				foreach (string otherGift in otherGifts)
				{
					foreach (Gift gift in model.PossibleGifts)
					{
						if (gift.Name.ToLower() == otherGift.ToLower())
						{
							ValidationResult result = new ValidationResult(
								errorMessage: "Een cadeautje dat je hebt gekozen staat al tussen de cadeautjes hierboven waar je uit kan kiezen!",
								memberNames: new[] { "Other" }
								);

							List<string> errors = ModelState.Values.SelectMany(ms => ms.Errors).Select(err => err.ErrorMessage).ToList();
							errors.Add(result.ErrorMessage);

							ViewBag.Errors = errors;
							return View("Wishlist", model);
						}
						else
						{
							model.ChosenGifts.Add(otherGift);
							break;
						}
					}
				}
			}
			
			foreach (Gift gift in model.PossibleGifts)
			{
				foreach (GiftViewModel gvm in chosenGifts)
				{
					if (gvm.IsChecked && gvm.Gift == gift.Name)
					{
						model.ChosenGifts.Add(gift.Name); 
					}
				}
			}

			return View("Confirmation", model);
		}

		public async Task<IActionResult> Confirm(Kid model)
		{
			model.PossibleGifts = giftRepository.GetPossibleGifts();

			WishList list = new WishList();
			list.Name = model.Name;
			list.Wanted = new List<Gift>();

			foreach (Gift gift in model.PossibleGifts)
			{
				foreach (string chosenGift in model.ChosenGifts)
				{
					if (chosenGift == gift.Name)
					{
						list.Wanted.Add(gift);
					} 
					else
					{
						Gift other = new Gift();
						other.Name = chosenGift;
						list.Wanted.Add(other);
					}
				}
			}

			giftRepository.SendWishList(list);

			await _accountController.AddWishListClaim(this.User.Identity.Name);
			return await _accountController.Logout();
        }

		public IActionResult BackToWishlist(Kid model)
		{
			model.PossibleGifts = giftRepository.GetPossibleGifts();
			return View("Wishlist", model);
		}		
	}
}
