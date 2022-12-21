﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Santa_WishList.Models;
using Santa_WishList.Models.Viewmodels;
using SantasWishlist.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;
using static Santa_WishList.Controllers.KidController;

namespace Santa_WishList.Controllers
{
	//[Authorize(Roles = "Child")] TODO
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
			model.Name = "Charlotte"; //TODO

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
				string[] otherGifts;
				if (model.Other.Contains(", "))
				{
					 otherGifts = model.Other.Split(", ");
				}
				else if (model.Other.Contains(","))
				{
					otherGifts = model.Other.Split(",");
				}
				else
				{
					otherGifts = new string[1];
					otherGifts[0] = model.Other;
				}
				
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

		public IActionResult Confirm(Kid model)
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
				}
			}

			giftRepository.SendWishList(list);
			//TODO log out, can't log in again
			return View();
		}

		public IActionResult BackToWishlist(Kid model)
		{
			model.PossibleGifts = giftRepository.GetPossibleGifts();
			return View("Wishlist", model);
		}		
	}
}
