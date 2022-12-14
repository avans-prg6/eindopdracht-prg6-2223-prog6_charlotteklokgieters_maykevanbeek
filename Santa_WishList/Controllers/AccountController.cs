using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;
using SantasWishlist_Data.Models;

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

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(InputModel Input)
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
        public async Task<IActionResult> Login(InputModel Input)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Name, Input.Password, false, lockoutOnFailure: false);
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
    }
}
