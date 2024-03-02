using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBookAPI.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly DataManager _dataManager;

        public LoginController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPost("/api/[controller]/CreateRecord/{PhoneBookRecord?}")]
        public void CreateRecord([FromBody] PhoneBookRecord record)
        {
            _dataManager.PhoneBookRecords.SavePhoneBookRecord(record);
        }
    }
}
