using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBookWEB.Domain;

namespace PhoneBookWEB.Controllers
{
    public class RolesController : Controller
    {
        private readonly DataManager _dataManager;

        public RolesController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult GetRoleList()
        {
            var roles = _dataManager.Accounts.GetRoles();
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRole(IdentityRole role)
        {
            var result = _dataManager.Accounts.CreateRole(role).GetAwaiter().GetResult();
            if (result)
            {
                return RedirectToAction("GetRoleList", "Roles" );
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid add role.");
                return View();
            }
        }

        [HttpGet]
        public IActionResult DeleteRole(string id)
        {
            var result = _dataManager.Accounts.DeleteRole(id).GetAwaiter().GetResult();
            if (result)
            {
                return RedirectToAction("GetRoleList", "Roles");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid delete role.");
                return RedirectToAction("GetRoleList", "Roles");
            }
        }
    }
}
