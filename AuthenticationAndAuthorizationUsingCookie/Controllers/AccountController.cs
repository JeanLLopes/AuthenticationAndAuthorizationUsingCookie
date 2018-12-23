using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationAndAuthorizationUsingCookie.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            //Check the user name and password  
            //Here can be implemented checking logic from the database  

            if (userName == "Admin" && password == "password")
            {

                //Create the identity for the user  
                var identity = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Country, "Brasil"),
                    new Claim(ClaimTypes.Email, "teste@gmail.com"),
                    new Claim(ClaimTypes.Gender, "Male"),
                    new Claim(ClaimTypes.GivenName, "Teste"),
                    new Claim(ClaimTypes.IsPersistent,"true"),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Surname, "Testando"),
                    new Claim(ClaimTypes.Version, "1.0"),
                    new Claim(ClaimTypes.Actor, "Client")
                }, 
                CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
