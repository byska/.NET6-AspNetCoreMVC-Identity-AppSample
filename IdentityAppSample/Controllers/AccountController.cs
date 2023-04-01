using IdentityAppSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAppSample.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
       
        public AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager)
        {
            signInManager = _signInManager;
            userManager=_userManager;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl= returnUrl;
            return View(login);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                AppUser user= await userManager.FindByEmailAsync(login.Email);
                if(user != null) 
                {
                    await signInManager.SignOutAsync();
                    //Microsoft.AspNetCore.Identity.SignInResult result =await signInManager.PasswordSignInAsync(user,login.Password,false,false);
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, login.Password, login.Remember, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("LoginFailer", "Login Failed. Invalid Email or Password");
            }
            return View(login);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
