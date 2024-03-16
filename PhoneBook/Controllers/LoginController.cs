using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Domain;
using PhoneBook.Domain.Entities;
using PhoneBook.Views.Login;

namespace PhoneBook.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly DataManager _dataManager;

        public LoginController(/*SignInManager<IdentityUser> signInManager,*/
                                DataManager dataManager)
        {
            _dataManager = dataManager;
            _signInManager = _dataManager.Accounts.GetSignInManager();
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
                var result = await _signInManager.PasswordSignInAsync(model.Email, 
                    model.Password, model.RememberMe, lockoutOnFailure: false);
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
