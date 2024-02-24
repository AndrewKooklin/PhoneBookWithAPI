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
using PhoneBookAPI.Views.Register;

namespace PhoneBookAPI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        RegisterModel _registerModel = new RegisterModel();
        public string ReturnUrl { get; set; }

        public IActionResult CreateUser()
        {
            _registerModel.Input = new RegisterModel.InputModel
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
        public async Task<IActionResult> CreateUser(RegisterModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Input.UserName, Email = model.Input.Email };
                var result = await _userManager.CreateAsync(user, model.Input.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Input.Role);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("LogInIndex", "Login");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
            return RedirectToAction("LogInIndex", "Login");
        }
    }
}
