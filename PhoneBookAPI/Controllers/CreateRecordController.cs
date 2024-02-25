using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Domain;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Controllers
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

        //[Route("/api/[controller]/CreateRecord/{record:PhoneBookRecord}")]
        [HttpPost("/api/[controller]/CreateRecord/{PhoneBookRecord}")]
        public void CreateRecord([FromBody] PhoneBookRecord record)
        {
            _dataManager.PhoneBookRecords.SavePhoneBookRecord(record);
        }
    }
}
