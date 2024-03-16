using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using PhoneBook.Domain;
using PhoneBook.Views.Register;
using PhoneBook.Views.Roles;

namespace PhoneBook.Controllers
{
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataManager _dataManager;

        public RegisterController(
            //UserManager<IdentityUser> userManager,
            //SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            DataManager dataManager)
        {
            _dataManager = dataManager;
            if (Role.RoleName.Equals("Anonymus"))
            {
                _roleManager = roleManager;
            }
            else
            {
                _userManager = _dataManager.Accounts.GetUserManager().GetAwaiter().GetResult();
                _signInManager = _dataManager.Accounts.GetSignInManager().GetAwaiter().GetResult();
                _roleManager = _dataManager.Accounts.GetRoleManager().GetAwaiter().GetResult();
            }
            
        }

        RegisterModel _registerModel = new RegisterModel();
        public string ReturnUrl { get; set; }

        [HttpGet]
        public IActionResult CreateUser()
        {
            _registerModel = new RegisterModel
            {
                RolesList = _roleManager.Roles.Select(r => r.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
            return View(_registerModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _dataManager.Accounts.CreateUser(model);
                if (result)
                {
                    Role.RoleName = "";
                    return RedirectToAction("LogInIndex", "Login");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }
    }
}
