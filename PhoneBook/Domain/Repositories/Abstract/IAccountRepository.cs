using Microsoft.AspNetCore.Identity;
using PhoneBook.Views.Login;
using PhoneBook.Views.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Domain.Repositories.Abstract
{
    public interface IAccountRepository
    {
        Task<bool> CheckUserToDB(LoginModel model);

        void LogoutUser();

        Task<bool> CreateUser(RegisterModel model);

        Task<IdentityUser> GetUser(LoginModel model);

        Task<List<string>> GetRoles(IdentityUser user);

        //Task<UserWithRolesModel> GetUserWithRoles(LoginModel model);

        Task<UserManager<IdentityUser>> GetUserManager();

        Task<SignInManager<IdentityUser>> GetSignInManager();

        Task<RoleManager<IdentityRole>> GetRoleManager();


    }
}
