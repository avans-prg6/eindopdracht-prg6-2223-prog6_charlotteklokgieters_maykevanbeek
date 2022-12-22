using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Santa_WishList.Models;
using SantasWishlist_Data.Models;
using System.Data;
using System.Security.Claims;

namespace Santa_WishList.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
           UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Santa")]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "Santa")]
        [HttpPost]
        public async Task<IActionResult> Register(AccountInput Input)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = Input.Name.ToLower();
                user.NormalizedUserName = Input.Name.ToUpper();
                user.SecurityStamp = Guid.NewGuid().ToString();

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var role = _roleManager.FindByNameAsync("Child").Result;
                    if(role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    if (Input.IsNice)
                    {
                        await _userManager.AddClaimAsync(user, new Claim("BeenNice", "IsNice"));
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountInput Input)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Name, Input.Password.ToLower(), false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect("/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/Account/Login");
        }

        public async Task<IActionResult> AddWishListClaim(string name)
        {
            IdentityUser user = await _userManager.FindByNameAsync(name);

            await _userManager.AddClaimAsync(user, new Claim("WishListDone", "WishListFilledIn"));
            return LocalRedirect("/Account/Login");
        }
    }
}
