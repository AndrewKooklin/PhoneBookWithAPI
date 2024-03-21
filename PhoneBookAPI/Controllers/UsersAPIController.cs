using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Domain;

namespace PhoneBookAPI.Controllers
{
    public class UsersAPIController : Controller
    {
        private DataManager _dataManager;

        public UsersAPIController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IEnumerable<IdentityUser> GetUsers()
        {
            return _dataManager.Accounts.GetUsers();
        }
    }
}
