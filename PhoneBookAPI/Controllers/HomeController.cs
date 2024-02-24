using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBookAPI.Domain;
using PhoneBookAPI.Domain.Entities;

namespace PhoneBookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        [Route("/api/[controller]")]
        [Route("/api/[controller]/GetRecords")]
        public IEnumerable<PhoneBookRecord> GetRecords()
        {
            return _dataManager.PhoneBookRecords.GetPhoneBookRecords();
        }
    }
}
