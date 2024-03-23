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

        Task<List<string>> GetUserRoles(LoginModel model);

        List<string> GetRoleNames();

        IEnumerable<IdentityRole> GetRoles();

        Task<bool> CreateRole(IdentityRole role);

        Task<bool> DeleteRole(string id);

        List<IdentityUser> GetUsers();

        UserWithRolesModel GetUserWithRoles(string id);

        bool AddRoleToUser(RoleUserModel model);

        bool DeleteRoleUser(RoleUserModel model);

        bool DeleteRolesUser(string userId);

        bool DeleteUser(string id);
    }
}
