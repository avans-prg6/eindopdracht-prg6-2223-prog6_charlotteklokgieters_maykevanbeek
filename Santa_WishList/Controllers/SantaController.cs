using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;
using SantasWishlist_Data;
using System.Data;

namespace Santa_WishList.Controllers
{   
    //[Authorize(Roles = "Santa")]
    public class SantaController : Controller
    {
        //[Route("{controller}/{year}/{week}/{department}")]
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
                string[] kids = viewmodel.KidsNames.Split(", ");
                bool error = false;
                List<string> dubbles = new List<string>();
                List<string> accounts = new List<string>();

                foreach (string kid in kids)
                {
                    if (!accounts.Contains(kid) && _context.Users.Where(x => x.UserName == kid).FirstOrDefault() == null)
                    {
                        accounts.Add(kid);
                    }
                    else
                    {
                        error = true;
                        dubbles.Add(kid);
                    }
                }

                if (!error)
                {
                    foreach (string kid in kids)
                    {
                        AccountInput model = new AccountInput();
                        model.Name = kid;
                        model.Password = viewmodel.Password;

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

                    string message = "De volgende namen komen al voor: ";
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
    }
}
