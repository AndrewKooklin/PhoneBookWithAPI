using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
