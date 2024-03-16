using Microsoft.AspNetCore.Identity;
using PhoneBookAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain.Repositories.Abstract
{
    public interface IAccountRepositoryAPI
    {
        Task<bool> CheckUserToDB(LoginModel model);

        void LogoutUser();

        Task<bool> CreateUser(RegisterModel model);

        Task<IdentityUser> GetUser(LoginModel model);

        Task<List<string>> GetRoles(IdentityUser user);

        UserWithRolesModel GetUserWithRoles(LoginModel model);

        UserManager<IdentityUser> GetUserManager();

        SignInManager<IdentityUser> GetSignInManager();

        RoleManager<IdentityRole> GetRoleManager();
    }
}
