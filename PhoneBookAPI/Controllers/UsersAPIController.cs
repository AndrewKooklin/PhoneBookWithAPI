using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Domain;
using PhoneBookAPI.Domain.Entities;

namespace PhoneBookAPI.Controllers
{
    [ApiController]
    public class UsersAPIController : Controller
    {
        private DataManager _dataManager;

        public UsersAPIController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet("/api/[controller]/GetUsers")]
        public List<IdentityUser> GetUsers()
        {
            return _dataManager.Accounts.GetUsers();
        }

        [HttpGet("/api/[controller]/GetUserWithRoles/{id?}")]
        public UserWithRolesModel GetUserWithRoles(string id)
        {
            return _dataManager.Accounts.GetUserWithRoles(id);
        }

        [HttpPost("/api/[controller]/AddRoleToUser/{RoleUserModel?}")]
        public bool AddRoleToUser(RoleUserModel model)
        {
            return _dataManager.Accounts.AddRoleToUser(model);
        }
    }
}
