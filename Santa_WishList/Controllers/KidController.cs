using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Santa_WishList.Models;
using SantasWishlist.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;
using static Santa_WishList.Controllers.KidController;

namespace Santa_WishList.Controllers
{

    [Authorize(Policy = "IsNice")]
    public class KidController : Controller
	{
		readonly GiftRepository giftRepository;

		public KidController(GiftRepository injectedGiftRepository) 
		{ 
			giftRepository = injectedGiftRepository;
		}

		[Route("{controller}")]
		public IActionResult Index()
		{
			Kid model = new Kid();
			model.Name = this.User.Identity.Name;
			model.PossibleGifts = giftRepository.GetPossibleGifts();
			model.ChosenGifts = new List<Gift>();
			model.Name = "Charlotte"; //TODO

			return View("Index", model);
		}

		public IActionResult PersonalInfo(Kid model) //TODO required!!
		{
			if (!ModelState.IsValid)
			{
                List<string> errors = ModelState.Values.SelectMany(ms => ms.Errors).Select(err => err.ErrorMessage).ToList();

				ViewBag.Errors = errors;
				return Index();
			}

			return View("Wishlist", model);
		}

		public IActionResult ChoosingGifts(Kid model)
		{
			if (model.Other != null)
			{
				string[] otherGifts = model.Other.Split(", "); //TODO
				foreach (string otherGift in otherGifts)
				{
					foreach (Gift gift in model.PossibleGifts)
					{
						if (gift.Name.ToLower() == otherGift.ToLower())
						{
							//Error, gift is in list
						}
					}
				}

				foreach (string otherGift in otherGifts)
				{
					Gift newGift = new Gift();
					newGift.Name = otherGift;
					//TODO category???

					model.ChosenGifts.Add(newGift);
				}
			}			

			return View("Confirmation", model);
		}

		public IActionResult Confirm()
		{
			//TODO send list
			//TODO log out, can't log in again
			return View();
		}

		public IActionResult BackToWishlist(Kid previous) { return View("Wishlist", previous); }

		//TODO enum verplaatsen
		public enum Niceness
		{
			Very_Nice,
			A_Little,
			Naughty
		}
	}
}
