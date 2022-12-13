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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public SantaController(SantaDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View("Index", "");
        }

        public IActionResult MakeAccounts(string names, bool beenNice, string password)
        {
            string[] kids = names.Split(", ");
            bool error = false;
            List<string> dubbles = new List<string>();
            List<string> accounts = new List<string>();

            foreach (string kid in kids)
            {
               
                if (!accounts.Contains(kid) && _context.Users.Where(x => x.UserName == kid).FirstOrDefault() != null)
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
                //bool worked = true;
                //foreach(string kid in kids)
                //{
                //    if(Register(kid, password))
                //    {

                //    }
                //}

                //if()
                //SantaViewModel model = new SantaViewModel();
                //model.KidsNames = names;
                //model.Password = password;

                //return View("Overview", model);


            }
            else
            {
                List<string> errors = new List<string>();
                
                string message = "De volgende namen komen al voor:";
                foreach(string name in dubbles)
                {
                    message += name;
                }
                errors.Add(message);
                ViewBag.Errors = errors;

                return View("Index", names);
            }
        }

        public async Task<bool> Register(string name, string password)
        {
           
                var user = new IdentityUser { UserName = name };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                
                    return true;
                }
                else
                {
                     return false;
                }
        }
    }
}
