using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Domain;
using PhoneBook.Views.Login;

namespace PhoneBook.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
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
                var result = await _signInManager.PasswordSignInAsync(model.Input.Email, 
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
            return  View(model);
        }
    }
}
