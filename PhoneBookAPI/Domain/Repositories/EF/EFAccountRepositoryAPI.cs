using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBookAPI.Domain.Entities;
using PhoneBookAPI.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain.Repositories.EF
{
    public class EFAccountRepositoryAPI : IAccountRepositoryAPI
    {
        private readonly AppDBContextAPI _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EFAccountRepositoryAPI(AppDBContextAPI context,
                                      SignInManager<IdentityUser> signInManager,
                                      UserManager<IdentityUser> userManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> CheckUserToDB(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.EMail,
                    model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> CreateUser(RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!String.IsNullOrEmpty(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IdentityUser> GetUser(LoginModel model)
        {
            return await _userManager.FindByEmailAsync(model.EMail);
        }

        public async Task<IdentityUser> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<string>> GetUserRoles(LoginModel model)
        {
            IdentityUser user = new IdentityUser();
            user = await _userManager.FindByEmailAsync(model.EMail);
            if (user == null)
            {
                return null;
            }
            else
            {
                List<string> roles = new List<string>();
                var rolesFromDb = await _userManager.GetRolesAsync(user);
                UserRolesModel.Roles = rolesFromDb.ToList();
                return UserRolesModel.Roles;
            }
        }

        public List<string> GetRoleNames()
        {
            List<string> roleNames = new List<string>();
            var roles = _roleManager.Roles;
            foreach (var roleName in roles)
            {
                roleNames.Add(roleName.Name);
            }
            return roleNames;
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            var roles = _roleManager.Roles.AsEnumerable();
            return roles;
        }

        public async Task<bool> CreateRole(IdentityRole role)
        {
            if (!await _roleManager.RoleExistsAsync(role.Name))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role.Name));
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteRole(string id)
        {
            IdentityRole role = _roleManager.FindByIdAsync(id).GetAwaiter().GetResult();
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<IdentityUser> GetUsers()
        {
            var users = _userManager.Users.ToList();
            return users;
        }

        public UserWithRolesModel GetUserWithRoles(string id)
        {
            UserWithRolesModel userWithRoles = new UserWithRolesModel();
            var user = GetUser(id).GetAwaiter().GetResult();
            var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToList();
            userWithRoles.User = user;
            userWithRoles.Roles = roles;
            return userWithRoles;
        }

        public List<UserWithRolesModel> GetUsersWithRoles()
        {
            List<UserWithRolesModel> usersWithRoles = new List<UserWithRolesModel>();
            var users = GetUsers();
            foreach(var user in users)
            {
                UserWithRolesModel userWithRoles = new UserWithRolesModel();
                var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToList();
                //var usersList = _context.Users;
                //var roleUser = _context.UserRoles;
                userWithRoles.User = user;
                userWithRoles.Roles = roles;
                usersWithRoles.Add(userWithRoles);
            }
            
            return usersWithRoles;
        }

        public async Task<bool> AddRoleToUser(RoleUserModel model)
        {
            IdentityUser user = GetUser(model.UserId).GetAwaiter().GetResult();
            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteRoleUser(RoleUserModel model)
        {
            IdentityUser user = GetUser(model.UserId).GetAwaiter().GetResult();
            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, model.Role);
            if (!result.Succeeded)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> DeleteRolesUser(string userId)
        {
            IdentityResult result;
            var user = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();
            var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().AsEnumerable();
            if (roles.Any())
            {
                result = await _userManager.RemoveFromRolesAsync(user, roles);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = _userManager.FindByIdAsync(id).GetAwaiter().GetResult();
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
