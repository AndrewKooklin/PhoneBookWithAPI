using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogoutController : Controller
    {
        private readonly DataManager _dataManager;

        public LogoutController(DataManager dataManager)
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
