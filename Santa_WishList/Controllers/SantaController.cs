using LogicLayer.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;
using Santa_WishList.Models.Viewmodels;
using SantasWishlist_Data;
using System.Data;

namespace Santa_WishList.Controllers
{
    [Authorize(Roles = "Santa")]
    public class SantaController : Controller
    {
        private readonly SantaDbContext _context;
        private readonly AccountController controller;

        public SantaController(SantaDbContext context, AccountController controller)
        {
            _context = context;
            this.controller = controller;
        }

        public IActionResult Index()
        {
            SantaViewModel model = new SantaViewModel();
            return View("Index", model);
        }

        public async Task<IActionResult> MakeAccounts(SantaViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                string[] kids = General.SplitString(viewmodel.KidsNames);

                bool error = false;
                List<string> dubbles = new List<string>();

                foreach (string kid in kids)
                {
                    if (_context.Users.Where(x => x.UserName == kid).FirstOrDefault() != null)
                    {
                        error = true;
                        dubbles.Add(kid);
                    }
                }

                if (!error)
                {
                    Console.WriteLine(viewmodel.BeenNice);
                    foreach (string kid in kids)
                    {
                        AccountInput model = new AccountInput();
                        model.Name = kid;
                        model.Password = viewmodel.Password.ToLower();
                        model.IsNice = viewmodel.BeenNice;
                        await controller.Register(model);
                    }

                    SantaViewModel vm = new SantaViewModel();
                    vm.KidsNames = viewmodel.KidsNames;
                    vm.Password = viewmodel.Password;

                    return View("Overview", vm);
                }
                else
                {
                    List<string> errors = new List<string>();

                    string message = "De volgende namen komen al voor in de database: ";
                    foreach (string name in dubbles)
                    {
                        message += name + " ";
                    }
                    errors.Add(message);
                    ViewBag.Errors = errors;

                    return View("Index", viewmodel);
                }
            }
            else
            {
                return View("Index", viewmodel);
            }
        }

        //public string[] SplitNames(string names)
        //{
        //    string[] kids;

        //    if (names.Contains(", "))
        //    {
        //        kids = names.Split(", ");
        //    }
        //    else if (names.Contains(","))
        //    {
        //        kids = names.Split(",");
        //    }
        //    else
        //    {
        //        kids = new string[1];
        //        kids[0] = names;
        //    }

        //    return kids;
        //}
    }
}
