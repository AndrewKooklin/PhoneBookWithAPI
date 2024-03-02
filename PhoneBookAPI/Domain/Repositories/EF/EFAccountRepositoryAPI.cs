using Microsoft.AspNetCore.Identity;
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

        public EFAccountRepositoryAPI(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> CheckUserToDB(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Input.UserName,
                    model.Input.Password, false, lockoutOnFailure: false);
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
