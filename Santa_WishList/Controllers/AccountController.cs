using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Santa_WishList.Models;

namespace Santa_WishList.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
           UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                var user = new IdentityUser { UserName = Input.Name };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect("/");
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
