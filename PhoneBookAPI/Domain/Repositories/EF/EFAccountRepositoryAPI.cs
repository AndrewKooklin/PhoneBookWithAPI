﻿using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EFAccountRepositoryAPI(SignInManager<IdentityUser> signInManager,
                                      UserManager<IdentityUser> userManager,
                                      RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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
            if (!result.Succeeded)
            {
                if (!String.IsNullOrEmpty(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);

                return false; 
            }
            else
            {
                return true;
            }
        }

        public async Task<IdentityUser> GetUser(LoginModel model)
        {
            return await _userManager.FindByEmailAsync(model.EMail);
        }

        public async Task<List<string>> GetRoles(IdentityUser user)
        {
            List<string> roles = new List<string>(); 
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (string role in userRoles)
            {
                roles.Add(role);
            }
            return roles;
        }

        public UserWithRolesModel GetUserWithRoles(LoginModel model)
        {
            if(model.EMail == null)
            {
                return null;
            }
            else
            {
                UserWithRolesModel userRoles = new UserWithRolesModel();
                userRoles.User = GetUser(model).GetAwaiter().GetResult();
                userRoles.Roles = GetRoles(userRoles.User).GetAwaiter().GetResult();
                return userRoles;

            }
        }

        public UserManager<IdentityUser> GetUserManager()
        {
            return _userManager;
        }

        public SignInManager<IdentityUser> GetSignInManager()
        {
            return _signInManager;
        }

        public RoleManager<IdentityRole> GetRoleManager()
        {
            return _roleManager;
        }
    }
}
