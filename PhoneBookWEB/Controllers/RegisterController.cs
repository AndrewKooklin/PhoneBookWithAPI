using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneBookWEB.Domain;
using PhoneBookWEB.Domain.Entities;

namespace PhoneBookWEB.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DataManager _dataManager;
        IEnumerable<string> roleNames;
        RegisterModel _registerModel = new RegisterModel();

        public RegisterController(DataManager dataManager)
        {
            _dataManager = dataManager;
            roleNames = _dataManager.Accounts.GetRoleNames().AsEnumerable();
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            _registerModel = new RegisterModel
            {
                RolesList = roleNames.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
            return View(_registerModel);
        }

        [HttpGet]
        public IActionResult CreateNewUser()
        {
            _registerModel = new RegisterModel
            {
                RolesList = roleNames.Select(i => new SelectListItem
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
                var result = await _dataManager.Accounts.CreateUser(model);
                if (result)
                {
                    return RedirectToAction("LogInIndex", "Login");
                }
                else
                {
                    _registerModel = new RegisterModel
                    {
                        RolesList = roleNames.Select(i => new SelectListItem
                        {
                            Text = i,
                            Value = i
                        })
                    };
                    ModelState.AddModelError(string.Empty, "User already exist.");
                    return View(_registerModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register model.");
                _registerModel = new RegisterModel 
                { 
                    RolesList = roleNames.Select(i => new SelectListItem 
                    { 
                        Text = i,
                        Value = i 
                    }) 
                };
                return View(_registerModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _dataManager.Accounts.CreateUser(model);
                if (result)
                {
                    return RedirectToAction("GetUsersList", "Users");
                }
                else
                {
                    _registerModel = new RegisterModel
                    {
                        RolesList = roleNames.Select(i => new SelectListItem
                        {
                            Text = i,
                            Value = i
                        })
                    };
                    ModelState.AddModelError(string.Empty, "User already exist.");
                    return View(_registerModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register model.");
                _registerModel = new RegisterModel
                {
                    RolesList = roleNames.Select(i => new SelectListItem
                    {
                        Text = i,
                        Value = i
                    })
                };
                return View(_registerModel);
            }
        }
    }
}
