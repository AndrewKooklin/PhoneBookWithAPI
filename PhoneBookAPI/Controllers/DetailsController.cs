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
    public class DetailsController : Controller
    {
        private readonly DataManager _dataManager;

        public DetailsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("/api/[controller]/GetRecordById/{id}")]
        public PhoneBookRecord GetRecordById(int id)
        {
            return _dataManager.PhoneBookRecords.GetPhoneBookRecordById(id);
        }
    }
}
