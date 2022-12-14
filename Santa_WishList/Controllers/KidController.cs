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
			KidViewModel model = new KidViewModel();
			model.Name = this.User.Identity.Name;
			//model.Name = "Charlotte";

			return View("Index", model);
		}

		public IActionResult PersonalInfo(string name, int age, Niceness niceness, string? example) //TODO required!!
		{
			if (!ModelState.IsValid)
			{
                List<string> errors = ModelState.Values.SelectMany(ms => ms.Errors).Select(err => err.ErrorMessage).ToList();

				ViewBag.Errors = errors;
				return Index();
			}
			else
			{

			}

			KidViewModel model = new KidViewModel();
			model.Name = name;
			model.PossibleGifts = giftRepository.GetPossibleGifts();
			model.Age = age;
			model.Niceness = niceness;

			if (niceness != Niceness.Naughty)
			{
				model.NicenessExample = example;
			}

			return View("Wishlist", model);
		}

		public IActionResult ChoosingGifts(KidViewModel previous, List<Gift> chosenGifts, string? other)
		{
			KidViewModel model = previous;
			model.ChosenGifts = new List<Gift>();

			string[] otherGifts = other.Split(", "); //TODO
			foreach (string otherGift in otherGifts)
			{
				foreach (Gift gift in previous.PossibleGifts)
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

			foreach (Gift gift in chosenGifts)
			{
				model.ChosenGifts.Add(gift);
			}

			return View("Confirmation", model);
		}

		public IActionResult Confirm()
		{
			//TODO send list
			//TODO log out, can't log in again
			return View();
		}

		public IActionResult BackToWishlist(KidViewModel previous) { return View("Wishlist", previous); }

		//TODO enum verplaatsen
		public enum Niceness
		{
			Very_Nice,
			A_Little,
			Naughty
		}
	}
}
