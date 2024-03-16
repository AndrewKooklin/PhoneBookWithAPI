using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PhoneBookAPI.Domain.Entities;

namespace PhoneBookAPI.Controllers
{
    public class RegisterController : ControllerBase
    {
        private readonly DataManager _dataManager;

        public RegisterController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPost("/api/[controller]/CreateUser/{RegisterModel?}")]
        public Task<bool> CreateUser([FromBody] RegisterModel model)
        {
            return _dataManager.Accounts.CreateUser(model);
        }

        [HttpGet("/api/[controller]/GetUserManager")]
        public UserManager<IdentityUser> GetUserManager()
        {
            return _dataManager.Accounts.GetUserManager();
        }

        [HttpGet("/api/[controller]/GetSignInManager")]
        public SignInManager<IdentityUser> GetSignInManager()
        {
            return _dataManager.Accounts.GetSignInManager();
        }

        [HttpGet("/api/[controller]/GetRoleManager")]
        public RoleManager<IdentityRole> GetRoleManager()
        {
            return _dataManager.Accounts.GetRoleManager();
        }
    } 
}
