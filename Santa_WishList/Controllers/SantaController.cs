using LogicLayer.General;
using LogicLayer.Santa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;
using Santa_WishList.Models.Viewmodels;
using SantasWishlist_Data;
using SantasWishlist_Data.Repositories;
using System.Data;

namespace Santa_WishList.Controllers
{
    [Authorize(Roles = "Santa")]
    public class SantaController : Controller
    {
        private readonly ISantaRepository _repository;
        private readonly AccountController controller;

        public SantaController(ISantaRepository injectedSantaRepository, AccountController injectedAccountController)
        {
            _repository = injectedSantaRepository;
            this.controller = injectedAccountController;
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

                List<string> dubbles = new List<string>();
                foreach (string kid in kids)
                {
                    if(_repository.NameExists(kid))
                    {
                        dubbles.Add(kid);
                    }
                }

                if (!dubbles.Any())
                {
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
                    ViewBag.Errors = Santa.AddErrorDubbles("De volgende namen komen al voor in de database: ", dubbles); 
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
