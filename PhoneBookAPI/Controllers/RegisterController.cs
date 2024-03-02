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

        [HttpPost("/api/[controller]/AddUser/{RegisterModel?}")]
        public Task<bool> AddUser([FromBody] RegisterModel model)
        {
            return _dataManager.Accounts.CreateUser(model);
        }
    } 
}
