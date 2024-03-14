using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBookAPI.Domain;
using PhoneBookAPI.Domain.Entities;

namespace PhoneBookAPI
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
        [Route("/api/[controller]/GetRecords")]
        public List<PhoneBookRecord> GetRecords()
        {
            return _dataManager.PhoneBookRecords.GetPhoneBookRecordsFromAPI();
        }
    }
}
