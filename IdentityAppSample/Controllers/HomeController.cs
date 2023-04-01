using IdentityAppSample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IdentityAppSample.Controllers
{
    //[Authorize] -controlerdaki her action için yetkilendirme gerektiği anlamına gelir.
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        //User Based Authentication [Authorize]//ilgili action için yetkilendirme gerektiği anlamına gelir.
        [Authorize(Roles ="Admin")]//rol için
        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            string msg = "Hello" + user.UserName;

            return View((object)msg);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}