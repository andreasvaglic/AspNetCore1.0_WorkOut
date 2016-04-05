using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Http.Authentication;

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
            return Redirect("/");
        }

        public IActionResult ExternalLogIn(string provider, string returnUrl = null)
        {
            var properties = new AuthenticationProperties()
            {
               
                //If don't need additional data we can go directly to page that is protected
                RedirectUri = "/Home/Secure"
            };
            //Bug is existing here so we need to define redirect url like a property
            //and than provide that property in login request.

            //One way of doing request:
            //await HttpContext.Authentication.ChallengeAsync("Google",properties);

            //This is easier way because we are using MVC method that is internally calling method above 
            return new ChallengeResult(provider, properties);
        }

        public IActionResult ExternalLogInWithCallback(string provider, string returnUrl = null)
        {
            var properties = new AuthenticationProperties()
            {
                //We set this so that we intercept data that is coming back from social login
                RedirectUri = "/Account/Register"
            };
            //Bug is existing here so we need to define redirect url like a property
            //and than provide that property in login request.

            //One way of doing request:
            //await HttpContext.Authentication.ChallengeAsync("Google",properties);

            //This is easier way because we are using MVC method that is internally calling method above 
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> Register()
        {
            //Getting claims from social login
            var extId = await HttpContext.Authentication.AuthenticateAsync("Temp");

            //Check the id of external user
            //If user Exist you can directly SignIn
            //If not exist you can force him to enter some additional data that app. need so that he 
            //can became your user
     
            //Login functionality like in Login Action above
            var claims = new List<Claim>()
                {
                    new Claim("sub","123Google"),
                    new Claim("name",extId.FindFirst(ClaimTypes.Name).Value),
                    new Claim("email",extId.FindFirst(ClaimTypes.Email).Value),
                    new Claim("role","Geek")
                };
            var id = new ClaimsIdentity(claims, "password");
            await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
            
            //We need to destroy that Temp cookie that we created because he was practically 
            //just container for data
            await HttpContext.Authentication.SignOutAsync("Temp");
            return Redirect("/home/secure");


        }
    }
}
