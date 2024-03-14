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
        private readonly DataManager _dataManager;

        public LoginController(DataManager dataManager)
        {
            _dataManager = dataManager;
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
                bool result = await _dataManager.Accounts.CheckUserToDB(model);
                if (result)
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
