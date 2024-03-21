using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Domain;

namespace PhoneBookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogoutAPIController : Controller
    {
        private readonly DataManager _dataManager;

        public LogoutAPIController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPost("/api/[controller]/LogoutUser")]
        public void LogoutUser()
        {
            _dataManager.Accounts.LogoutUser();
        }
    }
}
