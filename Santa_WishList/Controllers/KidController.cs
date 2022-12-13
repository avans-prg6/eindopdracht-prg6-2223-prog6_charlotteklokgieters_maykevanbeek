using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;
using SantasWishlist.Domain;
using System.Reflection;

namespace Santa_WishList.Controllers
{
	public class KidController : Controller
	{
		readonly IGiftRepository giftRepository;
		public KidController(IGiftRepository injectedGiftRepository) 
		{ 
			giftRepository = injectedGiftRepository;
		}

		public IActionResult Index()
		{



			//TODO name
			KidViewModel model = new KidViewModel();
			model.PossibleGifts = giftRepository.GetPossibleGifts();

			return View("Index", model);
		}

		public IActionResult PersonalInfo(int age, Niceness niceness, string example)
		{
			//TODO name
			KidViewModel model = new KidViewModel();
			model.PossibleGifts = giftRepository.GetPossibleGifts();
			model.Age = age;
			model.Niceness = niceness;

			if (niceness != Niceness.Naughty)
			{
				model.NicenessExample = example;
			}
			
			return View("Wishlist", model);
		}

		//TODO enum verplaatsen
		public enum Niceness
		{
			Very_Nice,
			A_Little,
			Naughty
		}
	}
}
