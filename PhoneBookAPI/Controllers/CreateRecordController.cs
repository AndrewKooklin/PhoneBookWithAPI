using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Domain;
using PhoneBookAPI.Domain.Entities;

namespace PhoneBookAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateRecordController : ControllerBase
    {
        private readonly DataManager _dataManager;

        public CreateRecordController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPost("/api/[controller]/CreateRecord/{PhoneBookRecord?}")]
        public bool CreateRecord([FromBody] PhoneBookRecord record)
        {
            return _dataManager.PhoneBookRecords.SavePhoneBookRecord(record).GetAwaiter().GetResult();
        }
    }
}
