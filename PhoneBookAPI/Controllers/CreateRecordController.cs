using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Domain;
using PhoneBookAPI.Domain.Entities;

namespace PhoneBookAPI.Controllers
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

        [Route("/api/[controller]/CreateRecord/{record:PhoneBookRecord}")]
        public void CreateRecord([FromBody] PhoneBookRecord record)
        {
            _dataManager.PhoneBookRecords.SavePhoneBookRecord(record);
        }
    }
}
