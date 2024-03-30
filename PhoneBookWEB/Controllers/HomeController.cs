using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhoneBookWEB.Domain;
using PhoneBookWEB.Domain.Entities;

namespace PhoneBookWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_dataManager == null)
            {
                return RedirectToAction("NoData", "NotFound");
            }
            else
            {
                List<PhoneBookRecord> records = null;
                records = _dataManager.PhoneBookRecords.GetPhoneBookRecords().GetAwaiter().GetResult();
                return View(records);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
