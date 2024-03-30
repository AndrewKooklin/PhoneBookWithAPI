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
    public class UsersController : Controller
    {
        private readonly DataManager _dataManager;
        IEnumerable<string> roleNames;

        public UsersController(DataManager dataManager)
        {
            _dataManager = dataManager;
            roleNames = _dataManager.Accounts.GetRoleNames().AsEnumerable();
        }

        [HttpGet]
        public IActionResult GetUsersList()
        {
            var users = _dataManager.Accounts.GetUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult UserDetails(string id)
        {
            var user = _dataManager.Accounts.GetUserWithRoles(id);
            user.RolesList = roleNames.Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            });
            return View(user);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRoleToUser(UserWithRolesModel model)
        {
            RoleUserModel roleUserModel = new RoleUserModel();
            var user = _dataManager.Accounts.GetUserWithRoles(model.User.Id);
            user.RolesList = roleNames.Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            });
            if (model.Role == null)
            {
                user.RolesList = roleNames.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                });
                ModelState.AddModelError("Add", "Выберите роль");
                return View("UserDetails", user);
            }
            if (user.Roles.Contains(model.Role))
            {
                user.RolesList = roleNames.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                });
                ModelState.AddModelError("Add", "The User  already has a role.");
                return View("UserDetails", user);
            }
            else
            {
                ModelState.AddModelError("Add", String.Empty);
                roleUserModel.UserId = model.User.Id;
                roleUserModel.Role = model.Role;

                bool result = _dataManager.Accounts.AddRoleToUser(roleUserModel).GetAwaiter().GetResult();
                if (result)
                {
                    user = _dataManager.Accounts.GetUserWithRoles(model.User.Id);
                    user.RolesList = roleNames.Select(i => new SelectListItem
                    {
                        Text = i,
                        Value = i
                    });
                    ModelState.AddModelError("Add", "Role added.");
                    return View("UserDetails", user);
                }
                else
                {
                    ModelState.AddModelError("Add", "Role is not added.");
                    return View("UserDetails", user);
                }
            }
        }

        [HttpPost]
        public IActionResult DeleteRoleUser(UserWithRolesModel model)
        {
            RoleUserModel roleUserModel = new RoleUserModel();
            var user = _dataManager.Accounts.GetUserWithRoles(model.User.Id);
            user.RolesList = roleNames.Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            });
            if (!user.Roles.Any() || user.Roles.Count < 1)
            {
                user.RolesList = roleNames.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                });
                ModelState.AddModelError("Delete", "The User has no roles.");
                return View("UserDetails", user);
            }
            if (model.Role == null)
            {
                user.RolesList = roleNames.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                });
                ModelState.AddModelError("Delete", "Выберите роль");
                return View("UserDetails", user);
            }
            if (!user.Roles.Contains(model.Role))
            {
                user.RolesList = roleNames.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                });
                ModelState.AddModelError("Delete", "The User has no role.");
                return View("UserDetails", user);
            }
            else
            {
                ModelState.AddModelError("Delete", String.Empty);
                roleUserModel.UserId = model.User.Id;
                roleUserModel.Role = model.Role;

                bool result = _dataManager.Accounts.DeleteRoleUser(roleUserModel).GetAwaiter().GetResult();
                if (result)
                {
                    user = _dataManager.Accounts.GetUserWithRoles(model.User.Id);
                    user.RolesList = roleNames.Select(i => new SelectListItem
                    {
                        Text = i,
                        Value = i
                    });
                    ModelState.AddModelError("Delete", "Role removed.");
                    return View("UserDetails", user);
                }
                else
                {
                    user.RolesList = roleNames.Select(i => new SelectListItem
                    {
                        Text = i,
                        Value = i
                    });
                    ModelState.AddModelError("Delete", "Role is not removed.");
                    return View("UserDetails", user);
                }
            }
        }

        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            bool resultRoles = _dataManager.Accounts.DeleteRolesUser(id).GetAwaiter().GetResult();
            if (resultRoles)
            {
                bool resultUser = _dataManager.Accounts.DeleteUser(id).GetAwaiter().GetResult();
                if (resultUser)
                {
                    var users = _dataManager.Accounts.GetUsers();
                    ModelState.AddModelError(String.Empty, "User deleted.");
                    return View("GetUsersList", users);
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "User is not deleted.");
                    var users = _dataManager.Accounts.GetUsers();
                    return View("GetUsersList", users);
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Roles for user is not removed.");
                var users = _dataManager.Accounts.GetUsers();
                return View("GetUsersList", users);
            }
        }
    }
}
