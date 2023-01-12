using LogicLayer.General;
using LogicLayer.Santa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;
using Santa_WishList.Models.Viewmodels;
using System.Diagnostics;

namespace Santa_WishList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Child") && User.HasClaim("WishListDone", "WishListFilledIn"))
            {
                ViewBag.errors = General.AddError("Dit account heeft al een verlanglijstje ingevuld");
                return View("../Account/Login");
            }
            else if(User.IsInRole("Child"))
            {
                return RedirectToAction("Index", "Kid");
            }
            else if(User.IsInRole("Santa"))
            {
                return RedirectToAction("Index", "Santa");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}