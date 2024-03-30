using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBookWEB.Domain;
using PhoneBookWEB.Domain.Entities;

namespace PhoneBookWEB.Controllers
{
    public class LoginController : Controller
    {
        private DataManager _dataManager;

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
                UserRoles.Roles = await _dataManager.Accounts.GetUserRoles(model);
                if (UserRoles.Roles == null)
                {
                    UserRoles.EMail = model.Email;
                    UserRoles.Roles = new List<string> { "Anonymus" };
                    return RedirectToAction("Index", "Home");
                }
                else if(UserRoles.Roles != null)
                {
                    UserRoles.EMail = model.Email;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect("LogInError");
                }
            }
            return View(model);
        }
    }
}
