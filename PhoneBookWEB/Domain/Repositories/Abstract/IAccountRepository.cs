using Microsoft.AspNetCore.Identity;
using PhoneBookWEB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookWEB.Domain.Repositories.Abstract
{
    public interface IAccountRepository
    {
        Task<bool> CheckUserToDB(LoginModel model);

        void LogoutUser();

        Task<bool> CreateUser(RegisterModel model);

        List<string> GetRoleNames();

        IEnumerable<IdentityRole> GetRoles();

        Task<List<string>> GetUserRoles(LoginModel model);

        Task<bool> CreateRole(IdentityRole role);

        Task<bool> DeleteRole(string id);

        List<IdentityUser> GetUsers();

        UserWithRolesModel GetUserWithRoles(string id);

        Task<bool> AddRoleToUser(RoleUserModel model);

        Task<bool> DeleteRoleUser(RoleUserModel model);

        Task<bool> DeleteRolesUser(string id);

        Task<bool> DeleteUser(string id);

    }
}
