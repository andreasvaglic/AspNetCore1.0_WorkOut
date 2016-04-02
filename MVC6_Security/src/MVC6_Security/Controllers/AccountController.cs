using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC6_Security.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!string.IsNullOrWhiteSpace(userName) &&
                 userName == password)
            {
                //Which identity data about user I want to remember across post backs
                //This data is represent as user claims
                //Wherry important claim called subject (sub) that is user identifier ID
                var claims = new List<Claim>()
                {
                    new Claim("sub",userName),
                    new Claim("name","Bob"),
                    new Claim("email","bob@smith.com"),
                    new Claim("role","Geek")
                };
                //You now need to create ClaimsIdenty from those claims list
                var id = new ClaimsIdentity(claims, "password");
                //SignIn this User now
                //We can have multiple auth. providers so we need to specify what provider
                //we wont to use and set the claims for that user
                
                //Claim identity will be in the cookie and every time when you login in cookie will be regenerate
                //you will get claims and you can use than in your User object

               
                await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));

                //Redirect to returnUrl but we wont to be sure that this is local Url that is not changed
                //in the process
                return new LocalRedirectResult(returnUrl);

            }
            return View();
        }

        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
