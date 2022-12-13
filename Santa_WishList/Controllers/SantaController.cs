using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;
using System.Data;

namespace Santa_WishList.Controllers
{
    public class SantaController : Controller
    {
        //[Route("{controller}/{year}/{week}/{department}")]
        //[Authorize(Roles = "Santa")]
        public IActionResult Index()
        {
            SantaViewModel model = new SantaViewModel();
            //model.KidsNames = "";
            //model.Password = "";

            return View("Index", model);
        }

        public IActionResult MakeAccounts(string names, bool beenNice, string password)
        {
            string[] kids = names.Split(", ");

            //Dictionary? met name en account?
            List<string> accounts = new List<string>();

            foreach (string kid in kids)
            {
                if (!accounts.Contains(kid))
                {
                    accounts.Add(kid);
                } 
                else
                {
                    //TODO error, no double names 
                }
            }

            SantaViewModel model = new SantaViewModel();
            model.KidsNames = names;
            model.Password = password;

            return View("Overview", model);
        }
    }
}
