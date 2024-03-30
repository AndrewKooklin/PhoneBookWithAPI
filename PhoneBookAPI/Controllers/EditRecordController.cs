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
    public class EditRecordController : Controller
    {
        private readonly DataManager _dataManager;

        public EditRecordController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPost("/api/[controller]/EditRecord/{PhoneBookRecord?}")]
        public void EditRecord([FromBody] PhoneBookRecord record)
        {
            _dataManager.PhoneBookRecords.EditPhoneBookRecord(record);
        }
    }
}
