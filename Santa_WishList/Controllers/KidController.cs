using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;
using System.Reflection;

namespace Santa_WishList.Controllers
{
	public class KidController : Controller
	{
		public IActionResult Index()
		{
			KidViewModel model = new KidViewModel();

			return View("Index", model);
		}

		public IActionResult PersonalInfo(int age, Niceness niceness, string example)
		{
			KidViewModel model = new KidViewModel();
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
