using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Domain;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Controllers
{
    public class DetailsController : Controller
    {
        private readonly DataManager _dataManager;

        public DetailsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        //[Route("[controller]/DetailsShow")]
        public IActionResult DetailsShow(int id)
        {
            PhoneBookRecord phoneBookRecord = _dataManager.PhoneBookRecords.GetPhoneBookRecordById(id);

            if (phoneBookRecord == null)
            {
                return RedirectToAction("NoData", "NotFound");
            }
            else
            {
                return View(phoneBookRecord);
            }
        }
    }
}
