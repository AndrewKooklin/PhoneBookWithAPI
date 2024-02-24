using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Views.Login;

namespace PhoneBookAPI.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager,
                          UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult LogInIndex()
        {

            return View();
        }

        public IActionResult LogInError()
        {

            return View("LogInError");
        }

        [HttpPost]
        public async Task<IActionResult> LogInIndex(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Input.UserName, 
                    model.Input.Password, model.Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect("LogInError");
                }
            }
            return  RedirectToAction ("LogInIndex", "Login");
        }
    }
}
