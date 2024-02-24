using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookAPI.Domain;

namespace PhoneBookAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteRecordController : Controller
    {
        private readonly DataManager _dataManager;

        public DeleteRecordController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("/api/[controller]/DeleteRecord/{id}")]
        public void DeleteRecord(int id)
        {
            _dataManager.PhoneBookRecords.DeletePhoneBookRecord(id);
        }
    }
}
