using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Domain.Entities;

namespace PhoneBookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly DataManager _dataManager;

        public LoginController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPost("/api/[controller]/CheckUserToDB/{LoginModel?}")]
        public Task<bool> CheckUserToDB([FromBody] LoginModel model)
        {
            return _dataManager.Accounts.CheckUserToDB(model);
        }

        [HttpPost("/api/[controller]/GetUserFromDB/{LoginModel?}")]
        public Task<IdentityUser> GetUserFromDB([FromBody] LoginModel model)
        {
            return _dataManager.Accounts.GetUser(model);
        }

        [HttpPost("/api/[controller]/GetUserRoles/{IdentityUser?}")]
        public Task<List<string>> GetUserRoles([FromBody] IdentityUser user)
        {
            return  _dataManager.Accounts.GetRoles(user);
        }

        [HttpPost("/api/[controller]/GetUserRoles/{IdentityUser?}")]
        public UserWithRolesModel GetUserWithRoles([FromBody] LoginModel model)
        {
            return _dataManager.Accounts.UserRoles(model);
        }
    }
}
