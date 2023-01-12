using LogicLayer.General;
using LogicLayer.KidValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Santa_WishList.Models;
using Santa_WishList.Models.Enums;
using Santa_WishList.Models.Viewmodels;
using SantasWishlist.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Santa_WishList.Controllers
{
	[Authorize(Roles = "Child")]
	public class KidController : Controller
	{
		private readonly IGiftRepository giftRepository;
		private readonly AccountController _accountController;
		private WishlistValidation wishlistValidation;

        public KidController(IGiftRepository injectedGiftRepository, AccountController injectedAccountController)
		{
			giftRepository = injectedGiftRepository;
            _accountController = injectedAccountController;
			wishlistValidation = new WishlistValidation(injectedGiftRepository);
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
			string[] otherGifts = null;

            if (!ModelState.IsValid)
			{
				List<string> errors = ModelState.Values.SelectMany(ms => ms.Errors).Select(err => err.ErrorMessage).ToList();
				ViewBag.Errors = errors;
				return View("Wishlist", model);
			}

			if (model.Other != null)
			{
				otherGifts = General.SplitString(model.Other);
                foreach (string otherGift in otherGifts)
                {
                    model.ChosenGifts.Add(otherGift);
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

			//Checking the validation rules
			wishlistValidation.ResetErrors(wishlistValidation.GetErrors());
			if (!wishlistValidation.Rule9(model.Name))
			{
				wishlistValidation.CertainGiftAmount(model.ChosenGifts, model.Niceness, ((System.Security.Claims.ClaimsIdentity)User.Identity)
					.HasClaim("BeenNice", "IsNice"), model.Name, model.NicenessExample, otherGifts);
				wishlistValidation.GiftCombinations(model.ChosenGifts);
				wishlistValidation.DivertFromAgeRating(model.ChosenGifts, model.Age);
				wishlistValidation.GiftAvailibilityInList(otherGifts);
			}
			
			if (wishlistValidation.GetErrorCount() > 0)
			{
                ViewBag.Errors = wishlistValidation.GetErrors();
                return View("Wishlist", model);
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
