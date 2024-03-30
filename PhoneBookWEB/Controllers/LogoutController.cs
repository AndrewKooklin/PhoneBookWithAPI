using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhoneBookWEB.Domain;
using PhoneBookWEB.Domain.Entities;

namespace PhoneBookWEB.Controllers
{
    public class LogoutController : Controller
    {
        private readonly DataManager _dataManager;

        public LogoutController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult LogoutUser()
        {
            _dataManager.Accounts.LogoutUser();
            UserRoles.EMail = "";
            UserRoles.Roles = new List<string> { "Anonymus" };
            return RedirectToAction("Index", "Home");
        }
    }
}
